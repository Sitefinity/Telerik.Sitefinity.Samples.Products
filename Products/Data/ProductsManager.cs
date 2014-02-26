using ProductCatalogSample.Configuration;
using ProductCatalogSample.Model;
using System;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;

namespace ProductCatalogSample.Data
{
    /// <summary>
    /// Products data manager
    /// </summary>
    public class ProductsManager : ContentManagerBase<ProductsDataProvider>, IContentLifecycleManager<ProductItem>, ILifecycleManager
    {
        #region Constructors - required

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsManager"/> class.
        /// </summary>
        public ProductsManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsManager"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public ProductsManager(string providerName)
            : base(providerName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsManager"/> class.
        /// </summary>
        /// <param name="providerName">
        /// The name of the provider. If empty string or null the default provider is set
        /// </param>
        /// <param name="transactionName">
        /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
        /// </param>
        public ProductsManager(string providerName, string transactionName)
            : base(providerName, transactionName)
        {
        }

        #endregion

        #region overriden properties

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public override string ModuleName
        {
            get { return ProductsModule.ModuleName; }
        }

        /// <summary>
        /// Gets the providers settings.
        /// </summary>
        /// <value>The providers settings.</value>
        protected override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
        {
            get { return Config.Get<ProductsConfig>().Providers; }
        }

        /// <summary>
        /// Gets the default provider delegate.
        /// </summary>
        /// <value>The default provider delegate.</value>
        protected override GetDefaultProvider DefaultProviderDelegate
        {
            get { return () => Config.Get<ProductsConfig>().DefaultProvider; }
        }

        #endregion

        #region Static methods - required

        /// <summary>
        /// Get an instance of the products manager using the default provider
        /// </summary>
        /// <returns>Instance of products manager</returns>
        public static ProductsManager GetManager()
        {
            return ManagerBase<ProductsDataProvider>.GetManager<ProductsManager>();
        }

        /// <summary>
        /// Get an instance of the products manager by explicitly specifying the required provider to use
        /// </summary>
        /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
        /// <param name="transactionName">Name of the transaction.</param>
        /// <returns>Instance of the products manager</returns>
        public static ProductsManager GetManager(string providerName, string transactionName)
        {
            return ManagerBase<ProductsDataProvider>.GetManager<ProductsManager>(providerName, transactionName);
        }

        /// <summary>
        /// Get an instance of the products manager by explicitly specifying the required provider to use
        /// </summary>
        /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
        /// <returns>Instance of the products manager</returns>
        public static ProductsManager GetManager(string providerName)
        {
            return ManagerBase<ProductsDataProvider>.GetManager<ProductsManager>(providerName);
        }

        #endregion

        #region Product Item methods

        /// <summary>
        /// Create a product item with random id
        /// </summary>
        /// <returns>Newly created product item in transaction</returns>       
        public ProductItem CreateProduct()
        {
            return this.Provider.CreateProduct();
        }

        /// <summary>
        /// Create a product item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created product item in transaction</returns>        
        public ProductItem CreateProduct(Guid id)
        {
            return this.Provider.CreateProduct(id);
        }
        
        /// <summary>
        /// Get a prodct by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Product item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>        
        public ProductItem GetProduct(Guid id)
        {
            return this.Provider.GetProduct(id);
        }

        /// <summary>
        /// Get a query of all products in this provider
        /// </summary>
        /// <returns>Query of all products</returns>        
        public IQueryable<ProductItem> GetProducts()
        {
            return this.Provider.GetProducts();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product to delete</param>       
        public void DeleteProduct(ProductItem product)
        {

            this.Provider.DeleteProduct(product);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Get items by type
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <returns>Querable of all items</returns>
        public override IQueryable<TItem> GetItems<TItem>()
        {
            if (typeof(ProductItem).IsAssignableFrom(typeof(TItem)))
            {
                return this.GetProducts() as IQueryable<TItem>;
            }

            if (typeof(TItem) == typeof(UrlData) || typeof(TItem) == typeof(ProductItemUrlData))
            {
                return this.GetUrls<ProductItemUrlData>() as IQueryable<TItem>;
            }

            if (typeof(TItem) == typeof(Comment))
            {
                return this.GetComments() as IQueryable<TItem>;
            }

            throw new NotSupportedException();
        }

        #endregion

        #region ILifecycleManager Members

        /// <summary>
        /// Gets the lifecycle decorator
        /// </summary>
        /// <value>The lifecycle.</value>
        public ILifecycleDecorator Lifecycle
        {
            get
            {
                return LifecycleFactory.CreateLifecycle<ProductItem>(this, this.Copy);
            }
        }

        /// <summary>
        /// Creates a language data instance
        /// </summary>
        /// <returns></returns>
        public LanguageData CreateLanguageData()
        {
            return this.Provider.CreateLanguageData();
        }

        /// <summary>
        /// Creates a language data instance
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public LanguageData CreateLanguageData(Guid id)
        {
            return this.Provider.CreateLanguageData(id);
        }

        /// <summary>
        /// Gets language data instance by its Id
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public LanguageData GetLanguageData(Guid id)
        {
            return this.Provider.GetLanguageData(id);
        }
        #endregion

        #region Content lifecycle

        #region ProductItem

        /// <summary>
        /// Checks in the content in the temp state. Content becomes master after the check in.
        /// </summary>
        /// <param name="item">Content in temp state that is to be checked in.</param>
        /// <returns>An item in master state.</returns>
        public virtual ProductItem CheckIn(ProductItem item)
        {
            return (ProductItem)this.Lifecycle.CheckIn(item);
        }

        /// <summary>
        /// Checks out the content in master state. Content becomes temp after the check out.
        /// </summary>
        /// <param name="item">Content in master state that is to be checked out.</param>
        /// <returns>A content that was checked out in temp state.</returns>
        public virtual ProductItem CheckOut(ProductItem item)
        {
            return (ProductItem)this.Lifecycle.CheckOut(item);
        }
        
        /// <summary>
        /// Copy one product item to another for the uses of content lifecycle management
        /// </summary>
        /// <param name="source">Product item to copy from</param>
        /// <param name="destination">Product item to copy to</param>
        public void Copy(ProductItem source, ProductItem destination)
        {
            destination.Urls.ClearDestinationUrls(source.Urls, this.Delete);
            source.Urls.CopyTo(destination.Urls, destination);

            ContentLinksExtensions.CopyContentLink("ProductImage", source, destination);

            destination.Price = source.Price;
            destination.QuantityInStock = source.QuantityInStock;
        }

        /// <summary>
        /// Edits the content in live state. Content becomes master after the edit.
        /// </summary>
        /// <param name="item">Content in live state that is to be edited.</param>
        /// <returns>A content that was edited in master state.</returns>
        public ProductItem Edit(ProductItem item)
        {
            return (ProductItem)this.Lifecycle.Edit(item);
        }

        /// <summary>
        /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
        /// </summary>
        /// <param name="item">Item to get the user ID it is locked by</param>      
        /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
        public Guid GetCheckedOutBy(ProductItem item)
        {
            return this.Lifecycle.GetCheckedOutBy(item);
        }

        /// <summary>
        /// Gets the public (live) version of <paramref name="cnt"/>, if it exists
        /// </summary>
        /// <param name="cnt">Type of the content item</param>        
        /// <returns>Public (live) version of <paramref name="cnt"/>, if it exists</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public ProductItem GetLive(ProductItem cnt)
        {
            return (ProductItem)(this.Lifecycle).GetLive(cnt);
        }

        /// <summary>
        /// Accepts a content item and returns an item in master state
        /// </summary>        
        /// <param name="cnt">Content item whose master to get</param>        
        /// <returns>
        /// If <paramref name="cnt"/> is master itself, returns cnt.
        /// Otherwize, looks up the master associated with <paramref name="cnt"/> and returns it.
        /// When there is no master, an exception will be thrown.
        /// </returns>
        /// <exception cref="InvalidOperationException">When no master can be found for <paramref name="cnt"/>.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public ProductItem GetMaster(ProductItem cnt)
        {
            return (ProductItem)(this.Lifecycle).GetMaster(cnt);
        }

        /// <summary>
        /// Get a temp for <paramref name="cnt"/>, if it exists.
        /// </summary>        
        /// <param name="cnt">Content item to get a temp for</param>        
        /// <returns>Temp version of <paramref name="cnt"/>, if it exists.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public ProductItem GetTemp(ProductItem cnt)
        {
            return (ProductItem)(this.Lifecycle).GetTemp(cnt);
        }

        /// <summary>
        /// Returns true or false, depending on whether the <paramref name="item"/> is checked out or not
        /// </summary>
        /// <param name="item">Item to test</param>        
        /// <returns>True if the item is checked out, false otherwize.</returns>
        public bool IsCheckedOut(ProductItem item)
        {
            return this.Lifecycle.IsCheckedOut(item);
        }

        /// <summary>
        /// Checks if <paramref name="item"/> is checked out by user with a specified id
        /// </summary>
        /// <param name="item">Item to test</param>
        /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item"/></param>        
        /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
        public bool IsCheckedOutBy(ProductItem item, Guid userId)
        {
            return this.Lifecycle.IsCheckedOutBy(item, userId);
        }

        /// <summary>
        /// Publishes the content in master state. Content becomes live after the publish.
        /// </summary>
        /// <param name="item">Content in master state that is to be published.</param>
        public ProductItem Publish(ProductItem item)
        {
            return (ProductItem)this.Lifecycle.Publish(item);
        }

        /// <summary>
        /// Schedule a content item - to be published from one date to another
        /// </summary>
        /// <param name="item">Content item in master state</param>
        /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
        /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>   
        public ProductItem Schedule(ProductItem item, DateTime publicationDate, DateTime? expirationDate)
        {
            return this.Provider.Schedule(item, publicationDate, expirationDate, this.Copy, this.GetProducts());
        }

        /// <summary>
        /// Unpublish a content item in live state.
        /// </summary>
        /// <param name="item">Live item to unpublish.</param>
        /// <returns>Master (draft) state.</returns>
        public ProductItem Unpublish(ProductItem item)
        {
            return (ProductItem)this.Lifecycle.Unpublish(item);
        }

        #endregion

        #region Generic

        /// <summary>
        /// Checks in the content in the temp state. Content becomes master after the check in.
        /// </summary>
        /// <param name="item">Content in temp state that is to be checked in.</param>
        /// <returns>An item in master state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content CheckIn(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.CheckIn(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Checks out the content in master state. Content becomes temp after the check out.
        /// </summary>
        /// <param name="item">Content in master state that is to be checked out.</param>
        /// <returns>A content that was checked out in temp state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content CheckOut(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.CheckOut(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Copy one item to another for the uses of content lifecycle management
        /// </summary>
        /// <param name="source">Item to copy from</param>
        /// <param name="destination">Item to copy to</param>
        public void Copy(Telerik.Sitefinity.GenericContent.Model.Content source, Telerik.Sitefinity.GenericContent.Model.Content destination)
        {

            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("destination");
            var productSource = source as ProductItem;
            var productDestination = destination as ProductItem;
            if (productSource == null || productDestination == null) throw new ArgumentException("Source and destination must be of the same type");
            this.Copy(productSource, productDestination);
        }

        /// <summary>
        /// Edits the content in live state. Content becomes master after the edit.
        /// </summary>
        /// <param name="item">Content in live state that is to be edited.</param>
        /// <returns>A content that was edited in master state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content Edit(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.Edit(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
        /// </summary>
        /// <param name="item">Item to get the user ID it is locked by</param>        
        /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
        public Guid GetCheckedOutBy(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.GetCheckedOutBy(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the public (live) version of <paramref name="cnt"/>, if it exists
        /// </summary>
        /// <param name="cnt">Type of the content item</param>        
        /// <returns>Public (live) version of <paramref name="cnt"/>, if it exists</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public Telerik.Sitefinity.GenericContent.Model.Content GetLive(Telerik.Sitefinity.GenericContent.Model.Content cnt)
        {
            var product = cnt as ProductItem;
            if (product != null)
                return this.GetLive(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Accepts a content item and returns an item in master state
        /// </summary>        
        /// <param name="cnt">Content item whose master to get</param>        
        /// <returns>
        /// If <paramref name="cnt"/> is master itself, returns cnt.
        /// Otherwize, looks up the master associated with <paramref name="cnt"/> and returns it.
        /// When there is no master, an exception will be thrown.
        /// </returns>
        /// <exception cref="InvalidOperationException">When no master can be found for <paramref name="cnt"/>.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public Telerik.Sitefinity.GenericContent.Model.Content GetMaster(Telerik.Sitefinity.GenericContent.Model.Content cnt)
        {
            var product = cnt as ProductItem;
            if (product != null)
                return this.GetMaster(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Get a temp for <paramref name="cnt"/>, if it exists.
        /// </summary>        
        /// <param name="cnt">Content item to get a temp for</param>        
        /// <returns>Temp version of <paramref name="cnt"/>, if it exists.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public Telerik.Sitefinity.GenericContent.Model.Content GetTemp(Telerik.Sitefinity.GenericContent.Model.Content cnt)
        {
            var product = cnt as ProductItem;
            if (product != null)
                return this.GetTemp(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns true or false, depending on whether the <paramref name="item"/> is checked out or not
        /// </summary>
        /// <param name="item">Item to test</param>        
        /// <returns>True if the item is checked out, false otherwize.</returns>
        public bool IsCheckedOut(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.IsCheckedOut(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Checks if <paramref name="item"/> is checked out by user with a specified id
        /// </summary>
        /// <param name="item">Item to test</param>
        /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item"/></param>        
        /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
        public bool IsCheckedOutBy(Telerik.Sitefinity.GenericContent.Model.Content item, Guid userId)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.IsCheckedOutBy(product, userId);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Publishes the content in master state. Content becomes live after the publish.
        /// </summary>
        /// <param name="item">Content in master state that is to be published.</param>
        public Telerik.Sitefinity.GenericContent.Model.Content Publish(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.Publish(product);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Schedule a content item - to be published from one date to another
        /// </summary>
        /// <param name="item">Content item in master state</param>
        /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
        /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>   
        public Telerik.Sitefinity.GenericContent.Model.Content Schedule(Telerik.Sitefinity.GenericContent.Model.Content item, DateTime publicationDate, DateTime? expirationDate)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.Schedule(product, publicationDate, expirationDate);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Unpublish a content item in live state.
        /// </summary>
        /// <param name="item">Live item to unpublish.</param>
        /// <returns>Master (draft) state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content Unpublish(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var product = item as ProductItem;
            if (product != null)
                return this.Unpublish(product);
            throw new NotSupportedException();
        }

        #endregion

        #endregion      
    }
}
