using System;
using Telerik.OpenAccess;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;

namespace ProductCatalogSample.Model
{
    /// <summary>
    /// Represents URL data for a product
    /// </summary>
    [Persistent]    
    public class ProductItemUrlData : UrlData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductItemUrlData" /> class.
        /// </summary>
        public ProductItemUrlData()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the parent product item
        /// </summary>
        /// <value>The product item</value>
        [FieldAlias("parent")]
        [NonSerializableProperty]
        public override IDataItem Parent
        {
            get
            {
                if (this.parent != null)
                    ((IDataItem)this.parent).Provider = ((IDataItem)this).Provider;
                return this.parent;
            }
            set
            {
                this.parent = (ProductItem)value;
            }
        }

        private ProductItem parent;
    }
}
