using ProductCatalogSample.Model;
using System.Collections.Generic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace ProductCatalogSample.Web.UI.Public.Designers
{
    /// <summary>
    /// Product view designer control
    /// </summary>
    public class ProductsDesigner : ContentViewDesignerBase
    {
        /// <summary>
        /// Gets the name of the javascript type that the designer will use.
        /// The designers can reuse for exampel the base class implementation and just customize some labels
        /// </summary>
        /// <value>The name of the script descriptor type.</value>
        protected override string ScriptDescriptorTypeName
        {
            get
            {
                return typeof(ContentViewDesignerBase).FullName;
            }
        }

        /// <summary>
        /// Gets a type from the resource assembly.
        /// Resource assembly is an assembly that contains embedded resources such as templates, images, CSS files and etc.
        /// By default this is Telerik.Sitefinity.Resources.dll.
        /// </summary>
        /// <value>The resources assembly info.</value>
        protected override System.Type ResourcesAssemblyInfo
        {
            get
            {
                return Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
            }
        }

        /// <summary>
        /// Adds the designer views.
        /// </summary>
        /// <param name="views">The views.</param>
        protected override void AddViews(Dictionary<string, ControlDesignerView> views)
        {

            var resources = Res.Get<ProductsResources>();
            var contentSelectorsSettings = new ContentSelectorsDesignerView();
            contentSelectorsSettings.LayoutTemplatePath = ProductsModule.ProductsVirtualPath +
                "ProductCatalogSample.Web.UI.Public.CustomContentSelectorsDesignerView.ascx";
            contentSelectorsSettings.ContentSelectorWebServiceUrl = "~/Sitefinity/Services/Content/Products.svc";
            contentSelectorsSettings.ContentTitleText = resources.DesignerContentTitleText;
            contentSelectorsSettings.ChooseAllText = resources.DesignerChooseAllText;
            contentSelectorsSettings.ChooseSingleText = resources.DesignerChooseSingleText;
            contentSelectorsSettings.ChooseSimpleFilterText = resources.DesignerChooseSimpleFilterText;
            contentSelectorsSettings.ChooseAdvancedFilterText = resources.DesignerChooseAdvancedFilterText;
            contentSelectorsSettings.NoContentToSelectText = resources.DesignerNoContentToSelectText;
            contentSelectorsSettings.ContentSelector.TitleText = resources.DesignerContentSelectorTitleText;
            contentSelectorsSettings.ContentSelector.ItemType = typeof(ProductItem).FullName;

            //var listSettings = new ListSettingsDesignerView();
            //listSettings.SortItemsText = resources.DesignerListSettingsSortItemstext;
            //listSettings.DesignedMasterViewType = typeof(MasterListView).FullName;

            var singleItemSettings = new SingleItemSettingsDesignerView();
            singleItemSettings.DesignedDetailViewType = typeof(ProductDetailsView).FullName;

            var customSettings = new CustomSettingsDesignerView();

            views.Add(contentSelectorsSettings.ViewName, contentSelectorsSettings);
            views.Add(singleItemSettings.ViewName, singleItemSettings);
            views.Add(customSettings.ViewName, customSettings);
        }
    }
}
