using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProductCatalogSample.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data.OA;
using System.ComponentModel;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Lifecycle;

namespace ProductCatalogSample.Data.Implementation
{
    /// <summary>
    /// Open access implementation of the products data provider
    /// </summary>
    [ContentProviderDecorator(typeof(OpenAccessContentDecorator))]
    public class OpenAccessProvider : ProductsDataProvider, IOpenAccessDataProvider
    {
        #region Fields
        private static Assembly[] peristentAssemblies;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="OpenAccessProvider"/> class.
        /// </summary>
        static OpenAccessProvider()
        {
            peristentAssemblies = new Assembly[] { typeof(ProductItem).Assembly };
        }

        #endregion

        #region IOpenAccessDataProvider

        /// <summary>
        /// Gets or sets the database instance for this provider.
        /// </summary>
        /// <value>The database.</value>
        public Database Database
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether to use implicit transactions.
        /// The recommended value for this property is true.
        /// </summary>
        /// <value>
        /// <c>true</c> if [use implicit transactions]; otherwise, <c>false</c>.
        /// </value>
        public bool UseImplicitTransactions
        {
            get { return true; }
        }

        /// <summary>
        /// The list of all assemblies with persistent classes inside.
        /// Only this list of assemblies will be used, it must be complete!
        /// </summary>
        /// <returns>The persistent assemblies.</returns>
        /// <value>The persistent assemblies.</value>
        public Assembly[] GetPersistentAssemblies()
        {
            return peristentAssemblies;
        }

        #endregion

        #region ProductItem methods

        /// <summary>
        /// Create a product item with random id
        /// </summary>
        /// <returns>Newly created product item in transaction</returns>
        public override ProductItem CreateProduct()
        {
            return this.CreateProduct(Guid.NewGuid());
        }

        /// <summary>
        /// Create a product item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created product item in transaction</returns>
        public override ProductItem CreateProduct(Guid id)
        {
            var product = new ProductItem();
            product.Id = id;
            product.ApplicationName = this.ApplicationName;
            product.Owner = SecurityManager.GetCurrentUserId();
            var dateValue = DateTime.UtcNow;
            product.DateCreated = dateValue;
            product.PublicationDate = dateValue;
            ((IDataItem)product).Provider = this;

            // news permissions inherit form the security root
            var securityRoot = this.GetSecurityRoot();
            if (securityRoot != null)
            {
                this.providerDecorator.CreatePermissionInheritanceAssociation(securityRoot, product);
            }
            else
            {
                var msg = Res.Get<SecurityResources>().NoSecurityRoot;
                msg = string.Format(msg, typeof(ProductItem).AssemblyQualifiedName);
                throw new InvalidOperationException(msg);
            }

            // items with empty guid are used in the UI to get a "blank" data item
            // -> i.e. to fill a data item with default values
            // if this is the case, we leave the item out of the transaction
            if (id != Guid.Empty)
            {
                this.GetContext().Add(product);
            }

            return product;
        }

        /// <summary>
        /// Get a prodct by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Product item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>
        public override ProductItem GetProduct(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be Empty Guid");
            }

            // Always use this method. Do NOT change it to query. Catch the exception if the Id can be wrong.
            var newsItem = this.GetContext().GetItemById<ProductItem>(id.ToString());
            ((IDataItem)newsItem).Provider = this;
            return newsItem;
        }

        /// <summary>
        /// Get a query of all products in this provider
        /// </summary>
        /// <returns>Query of all products</returns>
        public override IQueryable<ProductItem> GetProducts()
        {
            var appName = this.ApplicationName;

            var query =
                SitefinityQuery
                .Get<ProductItem>(this, MethodBase.GetCurrentMethod())
                .Where(b => b.ApplicationName == appName);

            return query;
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product to delete</param>
        public override void DeleteProduct(ProductItem product)
        {
            var scope = this.GetContext();

            this.ClearContentLinks(product);


            ////remove the item from the parent list of inheritors
            //var securityRoot = this.GetSecurityRoot();
            //if (securityRoot != null)
            //{
            //    List<PermissionsInheritanceMap> parentInheritors = securityRoot.PermissionChildren.Where(c => c.ChildObjectId == product.Id).ToList();
            //    for (int inheritor = 0; inheritor < parentInheritors.Count(); inheritor++)
            //    {
            //        securityRoot.PermissionChildren.Remove(parentInheritors[inheritor]);
            //    }
            //}
            ////remove the relevant permissions
            this.providerDecorator.DeletePermissions(product);
            this.ClearLifecycle(product, this.GetProducts());
            if (scope != null)
            {
                scope.Remove(product);
            }

            this.DeleteItemComments(product.GetType(), product.Id);
        }

        #endregion

        public TransactionMode TransactionConcurrency
        {
            get
            {
                // TODO: Implement this property getter
                return TransactionMode.PESSIMISTIC_EXPLICIT;
            }
        }

        public MetadataSource GetMetaDataSource(IDatabaseMappingContext context)
        {
            // TODO: Implement this method
            return new ProductsFluentMetadataSource(context);
        }

        public OpenAccessProviderContext Context
        {
            get;
            set;
            
        }
    }
}
