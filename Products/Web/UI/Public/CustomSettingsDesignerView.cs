using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace ProductCatalogSample.Web.UI.Public
{
    /// <summary>
    /// represents the list view designer
    /// TODO: register script resources
    /// </summary>
    public class CustomSettingsDesignerView : ContentViewDesignerView
    {
        #region Properties


        /// <summary>
        /// The type name of the view used to display the designed control in master mode.
        /// </summary>
        public string DesignedMasterViewType
        {
            get;
            set;
        }

        /// <summary>
        /// The bool property that holds the hide/show price option
        /// </summary>
        public bool HidePrice
        {
            get;
            set;
        }

        #region Labels


        #endregion

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value>The name of the view.</value>
        public override string ViewName
        {
            get
            {
                return "customViewSettings";
            }
        }

        /// <summary>
        /// Gets the view title.
        /// </summary>
        /// <value>The view title.</value>
        public override string ViewTitle
        {
            get
            {
                return "Custom Settings";
            }
        }


        #endregion

        #region Control References

        /// <summary>
        /// Gets the choice field representing the hide/show price
        /// </summary>
        protected virtual ChoiceField HidePriceControl
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("hidePrice", true);
            }
        }

        #endregion

        #region Overrides of SimpleView

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            if (this.HidePrice == true)
            {
                HidePriceControl.Choices[0].Selected = true;
            }
            else
            {
                HidePriceControl.Choices[0].Selected = false;
            }
        }

        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
        protected override string LayoutTemplateName
        {
            get
            {
                if (DesignerTemplateName != null) return DesignerTemplateName;
                return "ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.ascx";
            }
        }


        #endregion

        #region Overrides of SimpleScriptView

        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of <see cref="T:System.Web.UI.ScriptDescriptor"/> objects.
        /// </returns>
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor desc = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);

            desc.AddProperty("hidePriceControlId", this.HidePriceControl.ClientID);
            desc.AddProperty("hidePriceControlDataFieldName", this.HidePriceControl.DataFieldName);


            return new[] { desc };
        }

        /// <summary>
        /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference"/> objects that define script resources that the control requires.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of <see cref="T:System.Web.UI.ScriptReference"/> objects.
        /// </returns>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var res = PageManager.GetScriptReferences(ScriptRef.JQuery);
            var assemblyName = this.GetType().Assembly.GetName().ToString();
            var telerikAssemblyName = typeof(Telerik.Sitefinity.Web.UI.Fields.TextField).Assembly.GetName().FullName;

            res.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", telerikAssemblyName));
            res.Add(new ScriptReference("ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.js", assemblyName));
            return res;
        }

        #endregion

        #region Private Fields

        private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";

        #endregion
    }
}