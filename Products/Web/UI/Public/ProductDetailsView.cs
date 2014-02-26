using ProductCatalogSample.Model;
using System;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Web.UI;

namespace ProductCatalogSample.Web.UI.Public
{
    /// <summary>
    /// Represents a view that displays detailed information about a specified content item
    /// </summary>
    [ControlTemplateInfo("ProductsResources", "ProductDetailViewFriendlyName", "ModuleTitle")]
    public class ProductDetailsView : ViewBase
    {       
        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
        protected override string LayoutTemplateName
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the layout template path.
        /// </summary>
        /// <value>The layout template path.</value>
        public override string LayoutTemplatePath
        {
            get
            {
                return ProductsModule.ProductsVirtualPath + layoutTemplateName;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }


        /// <summary>
        /// Gets the repeater for news list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView DetailsView
        {
            get
            {
                return this.Container.GetControl<RadListView>("DetailsView", true);
            }
        }

        

        #region Overridden methods

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                this.AddAttributesToRender(writer);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            }
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                base.RenderEndTag(writer);
            }
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The controls container.</param>
        /// <param name="definition">The content view definition.</param>
        protected override void InitializeControls(GenericContainer container, IContentViewDefinition definition)
        {
            var detailDefinition = definition as IContentViewDetailDefinition;
            if (detailDefinition != null)
            {
                var productsView = (ProductsView)this.Host;
                var item = productsView.DetailItem as ProductItem;
                if (item == null)
                {
                    if (this.IsDesignMode())
                    {
                        this.Controls.Clear();
                        this.Controls.Add(new LiteralControl("A product item was not selected or has been deleted. Please select another one."));
                    }
                    return;
                }
                else
                {
                        this.DetailsView.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(DetailsView_ItemDataBound);
                        this.DetailsView.DataSource = new ProductItem[] { item };
                }
            }
        }        

        #endregion

        #region Event Handlers

     

        /// <summary>
        /// Handles the ItemDataBound event of the DetailsView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadListViewItemEventArgs"/> instance containing the event data.</param>
        private void DetailsView_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem || e.Item.ItemType == RadListViewItemType.AlternatingItem)
            {

                var productsView = (ProductsView)this.Host;
                if (productsView.HidePrices)
                {

                    var priceControl = e.Item.FindControl("priceControl");
                    priceControl.Visible = false;
                }
                
            }
        }

        #endregion

        #region Private Fields & Constants

        internal const string layoutTemplateName = "ProductCatalogSample.Web.UI.Public.FrontendProductsDetailsView.ascx";

        #endregion    
    }
}
