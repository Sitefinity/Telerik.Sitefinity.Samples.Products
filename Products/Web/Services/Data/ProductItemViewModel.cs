using ProductCatalogSample.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;

namespace ProductCatalogSample.Web.Services.Data
{
    /// <summary>
    /// View model calss for a product item
    /// </summary>
    public class ProductItemViewModel : ContentViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductItemViewModel"/> class.
        /// </summary>
        public ProductItemViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductItemViewModel"/> class.
        /// </summary>
        /// <param name="contentItem">The content item.</param>
        /// <param name="provider">The provider.</param>
        public ProductItemViewModel(ProductItem contentItem, ContentDataProviderBase provider)
            : base(contentItem, provider)
        {
            this.price = contentItem.Price;
            this.quantityInStock = contentItem.QuantityInStock;
            this.whatIsInThebox = contentItem.WhatIsInTheBox;
        }

        #endregion
        
        #region Overrides

        /// <summary>
        /// Get live version of this.ContentItem using this.provider
        /// </summary>
        /// <returns>Live version of this.ContentItem</returns>
        protected override Content GetLive()
        {
            return this.provider.GetLiveBase<ProductItem>((ProductItem)this.ContentItem);
        }

        /// <summary>
        /// Get temp version of this.ContentItem using this.provider
        /// </summary>
        /// <returns>Temp version of this.ContentItem</returns>
        protected override Content GetTemp()
        {
            return this.provider.GetTempBase<ProductItem>((ProductItem)this.ContentItem);
        }

        #endregion

        #region Own properties

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        /// <summary>
        /// Quantity in stock
        /// </summary>
        public int QuantityInStock
        {
            get { return this.quantityInStock; }
            set { this.quantityInStock = value; }
        }

        /// <summary>
        /// Description of the product's contents
        /// </summary>
        public string WhatIsInTheBox
        {
            get { return this.whatIsInThebox; }
            set { this.whatIsInThebox = value; }
        }

        #endregion

        #region Fields

        private decimal price;
        private int quantityInStock;
        private string whatIsInThebox;

        #endregion
    }
}
