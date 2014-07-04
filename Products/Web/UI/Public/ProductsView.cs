using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ContentUI;
using ProductCatalogSample.Web.UI.Public.Designers;

namespace ProductCatalogSample.Web.UI.Public
{
    /// <summary>
    /// Represents ContentView control for news articles. 
    /// </summary>
    [RequireScriptManager]
    [ControlDesigner(typeof(ProductsDesigner))]
    [PropertyEditorTitle(typeof(ProductsResources), "ProductsViewTitle")]
    public class ProductsView : ContentView
    {
        /// <summary>
        /// Gets or sets the name of the module which initialization should be ensured prior to rendering this control.
        /// </summary>
        /// <value>The name of the module.</value>
        public override string ModuleName
        {
            get
            {
                if (String.IsNullOrEmpty(base.ControlDefinitionName))
                    return ProductsModule.ModuleName;
                return base.ModuleName;
            }
            set
            {
                base.ModuleName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the configuration definition for the whole control. From this definition
        /// control can find out all other configurations needed in order to construct views.
        /// </summary>
        /// <value>The name of the control definition.</value>
        public override string ControlDefinitionName
        {
            get
            {
                if (String.IsNullOrEmpty(base.ControlDefinitionName))
                    return ProductsDefinitions.FrontendDefinitionName;
                return base.ControlDefinitionName;
            }
            set
            {
                base.ControlDefinitionName = value;
            }
        }

        /// <summary>
        /// Hides the prices on the front-end from the designer
        /// 
        /// </summary>
        /// <value>The name of the hide prices property.</value>
        public bool HidePrices
        {
            get
            {
                return this.hidePrice;
            }

            set
            {
                this.hidePrice = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the master view to be loaded when
        /// control is in the ContentViewDisplayMode.Master
        /// </summary>
        /// <value></value>
        public override string MasterViewName
        {
            get
            {
                if (!String.IsNullOrEmpty(base.MasterViewName))
                    return base.MasterViewName;
                return ProductsDefinitions.FrontendListViewName;
            }
            set
            {
                base.MasterViewName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the detail view to be loaded when
        /// control is in the ContentViewDisplayMode.Detail
        /// </summary>
        /// <value></value>
        public override string DetailViewName
        {
            get
            {
                if (!String.IsNullOrEmpty(base.DetailViewName))
                    return base.DetailViewName;

                return ProductsDefinitions.FrontendDetailViewName;
            }
            set
            {
                base.DetailViewName = value;
            }
        }

        /// <summary>
        /// Gets or sets the text to be shown when the box in the designer is empty
        /// </summary>
        /// <value></value>
        public override string EmptyLinkText
        {
            get
            {
                return Res.Get<ProductsResources>().EditProductsSettings;
            }
        }

        /// <summary>
        /// Default value of hide price
        /// </summary>
        private bool hidePrice = true;
    }
}
