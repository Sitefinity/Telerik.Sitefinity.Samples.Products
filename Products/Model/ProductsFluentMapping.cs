using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity;

namespace ProductCatalogSample.Model
{
    public class ProductsFluentMapping : OpenAccessFluentMappingBase
	{

        public ProductsFluentMapping(IDatabaseMappingContext context)
            : base(context)
        { }


        public override IList<MappingConfiguration> GetMapping() 
        {
			var mappings = new List<MappingConfiguration>();
			MapItem(mappings);
			MapUrlData(mappings);
			return mappings;
		}

        private void MapItem(IList<MappingConfiguration> mappings)
		{
			var itemMapping = new MappingConfiguration<ProductItem>();
            itemMapping.HasProperty(p => p.Id).IsIdentity();
			itemMapping.MapType(p => new { }).ToTable("custom_products");
            itemMapping.HasProperty(p => p.Price);
            itemMapping.HasProperty(p => p.QuantityInStock);
			itemMapping.HasAssociation<Telerik.Sitefinity.Security.Model.Permission>(p => p.Permissions);
			itemMapping.HasProperty(p => p.InheritsPermissions);
			itemMapping.HasProperty(p => p.CanInheritPermissions);
            itemMapping.HasAssociation(p => p.Urls).WithOppositeMember("parent", "Parent").ToColumn("content_id").IsDependent().IsManaged();

			//map language data & published translations
			CommonFluentMapping.MapILifecycleDataItemFields<ProductItem>(itemMapping, this.Context);
			mappings.Add(itemMapping);
		}

        private void MapUrlData(IList<MappingConfiguration> mappings)
		{
			var urlDataMapping = new MappingConfiguration<ProductItemUrlData>(); 
			urlDataMapping.MapType(p => new { }).Inheritance(InheritanceStrategy.Flat).ToTable("sf_url_data");
			mappings.Add(urlDataMapping);
		}
	}
}
