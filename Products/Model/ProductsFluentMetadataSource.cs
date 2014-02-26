using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Data.OA;

namespace ProductCatalogSample.Model
{
    public class ProductsFluentMetadataSource : ContentBaseMetadataSource
    {
        #region Construction

        public ProductsFluentMetadataSource()
            : base(null)
        { }


        public ProductsFluentMetadataSource(IDatabaseMappingContext context)
            : base(context)
        {

        }

        protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
        {
            var sitefinityMappings = base.BuildCustomMappings();
            sitefinityMappings.Add(new ProductsFluentMapping(this.Context));
            return sitefinityMappings;

        }

        #endregion

        #region Overriden Members

        #endregion
    }
}
