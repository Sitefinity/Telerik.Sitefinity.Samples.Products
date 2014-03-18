using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.Configuration;
using System.Web.UI;

namespace ProductCatalogSample.Web.Controls
{
    public class TaxonomyDropDownField : ChoiceField
    {

        protected override Type ResourcesAssemblyInfo
        {
            get
            {
                return Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
            }
        }

        public override void Configure(IFieldDefinition definition)
        {
            base.Configure(definition);
            var tdefinition = definition as ITaxonFieldDefinition;
            var tManager = TaxonomyManager.GetManager();
            var tid = tdefinition.TaxonomyId;
            var taxonomy = tManager.GetTaxonomies<FlatTaxonomy>().Where(t => t.Id == tid).SingleOrDefault();
            if (taxonomy != null)
            {
                var colorsTaxa = taxonomy.Taxa.OrderBy(c => c.Title.ToString());
                this.RenderChoicesAs = Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.DropDown; //
                // or you can use Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.CheckBoxes for multiple choice

                this.Choices.Clear();
                foreach (var taxon in colorsTaxa)
                {
                    var choice = new ChoiceItem();
                    choice.Value = taxon.Id.ToString();
                    choice.Text = taxon.Title;
                    choice.Enabled = true;
                    this.Choices.Add(choice);
                }
            }
        }

        public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var assemblyName = typeof(TaxonomyDropDownField).Assembly.FullName;
            var scripts = new List<ScriptReference>(base.GetScriptReferences())
                              {
                                  new ScriptReference(TaxonomyDropDownField.script, assemblyName),
                              };
            return scripts;

        }

        #region Private Fields and constants

        internal const string script = "ProductCatalogSample.Web.Controls.Scripts.TaxonomyDropDownField.js";

        #endregion
    }
}
