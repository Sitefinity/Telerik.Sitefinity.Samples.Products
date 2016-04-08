using System;
using System.Collections;
using System.Linq;
using ProductCatalogSample.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Data.Linq;

namespace ProductCatalogSample.Data
{
    /// <summary>
    /// Base provider class for the products module
    /// </summary>
    public abstract class ProductsDataProvider : ContentDataProviderBase, ILanguageDataProvider, IOpenAccessDataProvider
    {
        #region Fields

        private static Type[] knownTypes;
        private string[] supportedPermissionSets = new string[] { ProductsConstants.Security.PermissionSetName };

        #endregion

        #region Overiden properties

        /// <summary>
        /// Gets a unique key for each data provider base.
        /// </summary>
        /// <value></value>
        public override string RootKey
        {
            get { return "ProductsDataProvider"; }
        }

        /// <summary>
        /// Gets the permission sets relevant to this specific secured object.
        /// To be overridden by relevant providers (which involve security roots)
        /// </summary>
        /// <value>The supported permission sets.</value>
        public override string[] SupportedPermissionSets
        {
            get
            {
                return this.supportedPermissionSets;
            }

            set
            {
                this.supportedPermissionSets = value;
            }
        }

        #endregion

        #region Product Item methods

        /// <summary>
        /// Create a product item with random id
        /// </summary>
        /// <returns>Newly created product item in transaction</returns>
        [MethodPermission(ProductsConstants.Security.PermissionSetName, ProductsConstants.Security.Create)]
        public abstract ProductItem CreateProduct();

        /// <summary>
        /// Create a product item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created product item in transaction</returns>
        [MethodPermission(ProductsConstants.Security.PermissionSetName, ProductsConstants.Security.Create)]
        public abstract ProductItem CreateProduct(Guid id);

        /// <summary>
        /// Get a prodct by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Product item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>
        [ValuePermission(ProductsConstants.Security.PermissionSetName, ProductsConstants.Security.View)]
        public abstract ProductItem GetProduct(Guid id);

        /// <summary>
        /// Get a query of all products in this provider
        /// </summary>
        /// <returns>Query of all products</returns>
        [EnumeratorPermission(ProductsConstants.Security.PermissionSetName, ProductsConstants.Security.View)]
        public abstract IQueryable<ProductItem> GetProducts();

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product to delete</param>
        [ParameterPermission("product", ProductsConstants.Security.PermissionSetName, ProductsConstants.Security.Delete)]
        public abstract void DeleteProduct(ProductItem product);

        #endregion

        #region Item Methods

        /// <summary>
        /// Creates new data item.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="id">The pageId.</param>
        /// <returns>The item or null</returns>
        public override object CreateItem(Type itemType, Guid id)
        {
            if (itemType == null)
            {
                throw new ArgumentNullException("itemType");
            }

            if (itemType == typeof(ProductItem))
            {
                return this.CreateProduct(id);
            }

            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        /// <summary>
        /// Gets the data item with the specified ID.
        /// An exception should be thrown if an item with the specified ID does not exist.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="id">The ID of the item to return.</param>
        /// <returns>The item or null</returns>
        public override object GetItem(Type itemType, Guid id)
        {
            if (itemType == typeof(Comment))
            {
                return this.GetComment(id);
            }

            if (itemType == typeof(ProductItem) || itemType == null)
            {
                return this.GetProduct(id);
            }

            return base.GetItem(itemType, id);
        }

        /// <summary>
        /// Get item by primary key without throwing exceptions if it doesn't exist
        /// </summary>
        /// <param name="itemType">Type of the item to get</param>
        /// <param name="id">Primary key</param>
        /// <returns>Item or default value</returns>
        public override object GetItemOrDefault(Type itemType, Guid id)
        {
            if (itemType == typeof(Comment))
            {
                return this.GetComments().Where(c => c.Id == id).FirstOrDefault();
            }

            if (itemType == typeof(ProductItem))
            {
                return this.GetProducts().Where(n => n.Id == id).FirstOrDefault();
            }

            return base.GetItemOrDefault(itemType, id);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="orderExpression">The order expression.</param>
        /// <param name="skip">The skip value</param>
        /// <param name="take">The take value</param>
        /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression"/></param>
        /// <returns>The result</returns>
        public override IEnumerable GetItems(Type itemType, string filterExpression, string orderExpression, int skip, int take, ref int? totalCount)
        {
            if (itemType == null)
            {
                throw new ArgumentNullException("itemType");
            }

            if (itemType == typeof(Comment))
            {
                return SetExpressions(this.GetComments(), filterExpression, orderExpression, skip, take, ref totalCount);
            }

            if (itemType == typeof(ProductItem))
            {
                return SetExpressions(this.GetProducts(), filterExpression, orderExpression, skip, take, ref totalCount);
            }

            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        /// <summary>
        /// Marks the provided persistent item for deletion.
        /// The item is deleted form the storage when the transaction is committed.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        public override void DeleteItem(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var itemType = item.GetType();
            this.providerDecorator.DeletePermissions(item);

            if (itemType == typeof(Comment))
            {
                this.Delete((Comment)item);
                return;
            }

            if (itemType == typeof(ProductItem))
            {
                this.DeleteProduct((ProductItem)item);
                return;
            }

            throw GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
        }

        #endregion

        #region ILanguageDataProvider methods

        /// <summary>
        /// Creates a language data item
        /// </summary>
        /// <returns></returns>
        public Telerik.Sitefinity.Lifecycle.LanguageData CreateLanguageData()
        {
            return this.CreateLanguageData(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a language data item
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Telerik.Sitefinity.Lifecycle.LanguageData CreateLanguageData(Guid id)
        {
            var languageData = new LanguageData(this.ApplicationName, id);
            ((IDataItem)languageData).Provider = this;

            if (id != Guid.Empty)
            {
                this.GetContext().Add(languageData);
            }

            return languageData;
        }

        /// <summary>
        /// Gets language data item by its id
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Telerik.Sitefinity.Lifecycle.LanguageData GetLanguageData(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Argument 'id' cannot be empty GUID.");

            var languageData = this.GetContext().GetItemById<LanguageData>(id.ToString());
            ((IDataItem)languageData).Provider = this;
            return languageData;
        }

        /// <summary>
        /// Gets a query of all language data items
        /// </summary>
        /// <returns></returns>
        public IQueryable<Telerik.Sitefinity.Lifecycle.LanguageData> GetLanguageData()
        {
            var appName = this.ApplicationName;
            return SitefinityQuery.Get<LanguageData>(this).Where(c => c.ApplicationName == appName);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the items by taxon.
        /// </summary>
        /// <param name="taxonId">The taxon id.</param>
        /// <param name="isSingleTaxon">A value indicating if it is a single taxon.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="orderExpression">The order expression.</param>
        /// <param name="skip">Items to skip.</param>
        /// <param name="take">Items to take.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>Collection of items</returns>
        public override IEnumerable GetItemsByTaxon(Guid taxonId, bool isSingleTaxon, string propertyName, Type itemType, string filterExpression, string orderExpression, int skip, int take, ref int? totalCount)
        {
            if (itemType == typeof(ProductItem))
            {
                this.CurrentTaxonomyProperty = propertyName;

                IQueryable<ProductItem> query = this.GetProducts();
                if (!String.IsNullOrWhiteSpace(filterExpression))
                    query = query.Where(filterExpression);
                if (!String.IsNullOrWhiteSpace(orderExpression))
                    query = query.OrderBy(orderExpression);
                if (isSingleTaxon)
                {
                    var query0 = from i in query
                                 where i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId
                                 select i;

                    query = query0;
                }
                else
                {

                    var query1 = from i in query
                                 where (i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty)).Any(t => t == taxonId)
                                 select i;
                    query = query1;
                }

                if (totalCount.HasValue)
                {
                    totalCount = query.Count();
                }

                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);
                return query;
            }

            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }


        /// <summary>
        /// Override this method in order to return the type of the Parent object of the specified content type.
        /// If the type has no parent type, return null.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>The parrent type</returns>
        public override Type GetParentTypeFor(Type contentType)
        {
            return null;
        }

        /// <summary>
        /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData"/> implementation for the specified content type.
        /// </summary>
        /// <param name="itemType">Type of the content item.</param>
        /// <returns>The url type</returns>
        public override Type GetUrlTypeFor(Type itemType)
        {
            if (itemType == typeof(ProductItem))
            {
                return typeof(ProductItemUrlData);
            }

            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        /// <summary>
        /// Get a list of types served by this manager
        /// </summary>
        /// <returns>a list of types served by this manager</returns>
        public override Type[] GetKnownTypes()
        {
            if (knownTypes == null)
            {
                knownTypes = new Type[]
                {
                    typeof(ProductItem)
                };
            }

            return knownTypes;
        }

        /// <summary>
        /// Sets default permissions
        /// </summary>
        /// <param name="root">Security root</param>
        public override void SetRootPermissions(SecurityRoot root)
        {
            if (root.Permissions != null || root.Permissions.Count > 0)
            {
                root.Permissions.Clear();
            }

            var appRoles = Config.Get<SecurityConfig>().ApplicationRoles;
            var everyoneRoleId = appRoles[SecurityConstants.AppRoles.Everyone].Id;
            var authorsRoleId = appRoles[SecurityConstants.AppRoles.Authors].Id;
            var editorsRoleId = appRoles[SecurityConstants.AppRoles.Editors].Id;

            // Products
            var permissionsforEveryoneToViewProducts = this.CreatePermission(ProductsConstants.Security.PermissionSetName, root.Id, everyoneRoleId);
            permissionsforEveryoneToViewProducts.GrantActions(false, ProductsConstants.Security.View);
            root.Permissions.Add(permissionsforEveryoneToViewProducts);

            var permissionsForOwnersToModifyAndDeleteProducts = this.CreatePermission(ProductsConstants.Security.PermissionSetName, root.Id, SecurityManager.OwnerRole.Id);
            permissionsForOwnersToModifyAndDeleteProducts.GrantActions(false, ProductsConstants.Security.Modify, ProductsConstants.Security.Delete);
            root.Permissions.Add(permissionsForOwnersToModifyAndDeleteProducts);

            var editorsPermissionsForProducts = this.CreatePermission(ProductsConstants.Security.PermissionSetName, root.Id, editorsRoleId);
            editorsPermissionsForProducts.GrantActions(
                false,
                ProductsConstants.Security.Create, 
                ProductsConstants.Security.Modify,
                ProductsConstants.Security.Delete, 
                ProductsConstants.Security.ChangeOwner);
            root.Permissions.Add(editorsPermissionsForProducts);

            var authorsPermissionsForProducts = this.CreatePermission(ProductsConstants.Security.PermissionSetName, root.Id, authorsRoleId);
            authorsPermissionsForProducts.GrantActions(false, ProductsConstants.Security.Create);
            root.Permissions.Add(authorsPermissionsForProducts);
        }

        /// <summary>
        /// Commits the provided transaction.
        /// </summary>
        [TransactionPermission(typeof(ProductItem), ProductsConstants.Security.PermissionSetName, SecurityConstants.TransactionActionType.Updated, ProductsConstants.Security.Modify)]
        public override void CommitTransaction()
        {
            base.CommitTransaction();
        }

        #endregion        

        #region IOpenAccessDataProvider

        public Telerik.Sitefinity.Data.OpenAccessProviderContext Context
        {
            get;
            set;
        }

        public Telerik.OpenAccess.Metadata.MetadataSource GetMetaDataSource(IDatabaseMappingContext context)
        {
            return new ProductsFluentMetadataSource(context);
        }

        #endregion

    }
}
