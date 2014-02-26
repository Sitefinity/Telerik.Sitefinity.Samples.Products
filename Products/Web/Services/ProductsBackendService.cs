using ProductCatalogSample.Data;
using ProductCatalogSample.Model;
using ProductCatalogSample.Web.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Lifecycle;

namespace ProductCatalogSample.Web.Services
{
    /// <summary>
    /// Services for product items that is used in the Sitefinity backend
    /// </summary>
    public class ProductsBackendService : ContentServiceBase<ProductItem, ProductItemViewModel, LifecycleDecoratorWrapper<ProductItem, ProductsManager, ProductsDataProvider>>
    {
        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="parentId">Not used</param>
        /// <param name="providerName">Not used</param>
        /// <returns>Throws exception</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override IQueryable<ProductItem> GetChildContentItems(Guid parentId, string providerName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Get a product by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <param name="providerName">Provider to use</param>
        /// <returns>Product item</returns>
        public override ProductItem GetContentItem(Guid id, string providerName)
        {
            return ProductsManager.GetManager(providerName).GetProduct(id);
        }

        /// <summary>
        /// Get a query of all products in a provider
        /// </summary>
        /// <param name="providerName">Name of the provider to use</param>
        /// <returns>Query of all products in the provider</returns>
        public override IQueryable<ProductItem> GetContentItems(string providerName)
        {
            return ProductsManager.GetManager(providerName).GetProducts();
        }
       

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="id">Not used</param>
        /// <param name="providerName">Not used</param>
        /// <returns>Throws exception</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override ProductItem GetParentContentItem(Guid id, string providerName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Convert a list of model items into a list of viewmodel items
        /// </summary>
        /// <param name="contentList">List of product items</param>
        /// <param name="dataProvider">Provider to use</param>
        /// <returns>List of view model items</returns>
        public override IEnumerable<ProductItemViewModel> GetViewModelList(IEnumerable<ProductItem> contentList, ContentDataProviderBase dataProvider)
        {
            var viewModelList = new List<ProductItemViewModel>();
            foreach (var product in contentList)
                viewModelList.Add(new ProductItemViewModel(product, dataProvider));
            return viewModelList;
        }

        /// <summary>
        /// Represents a wrapper around ILifecycleManager
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public override LifecycleDecoratorWrapper<ProductItem, ProductsManager, ProductsDataProvider> GetManager(string providerName)
        {
            return new LifecycleDecoratorWrapper<ProductItem, ProductsManager, ProductsDataProvider>(providerName);
        }
      
    }
}
