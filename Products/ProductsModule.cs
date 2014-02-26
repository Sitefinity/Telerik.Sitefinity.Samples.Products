using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ProductCatalogSample.Configuration;
using ProductCatalogSample.Data;
using ProductCatalogSample.Model;
using ProductCatalogSample.Web.Services;
using ProductCatalogSample.Web.UI;
using ProductCatalogSample.Web.UI.Public;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using SecModel = Telerik.Sitefinity.Security.Model;

namespace ProductCatalogSample
{
    /// <summary>
    /// Procuts module - API entry point
    /// </summary>
    public class ProductsModule : ContentModuleBase
    {
        #region Properties
        /// <summary>
        /// Gets the CLR types of all data managers provided by this module.
        /// </summary>
        /// <value>An array of <see cref="Type"/> objects.</value>
        public override Type[] Managers
        {
            get { return managerTypes; }
        }

        /// <summary>
        /// Gets the identity of the home (landing) page for the Products module.
        /// </summary>
        /// <value>The landing page id.</value>
        public override Guid LandingPageId
        {
            get
            {
                return ProductsModule.HomePageId;
            }
        }

        #endregion

        #region Module Initialization

        /// <summary>
        /// Initializes the service with specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This method is obsolete")]
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This method is obsolete")]
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            App.WorkWith()
                .Module(settings.Name)
                .Initialize()
                    .Configuration<ProductsConfig>()
                    .Localization<ProductsResources>()
                    .WebService<ProductsBackendService>("Sitefinity/Services/Content/Products.svc")
                    .TemplatableControl<MasterListView, ProductItem>()
                    .TemplatableControl<ProductDetailsView, ProductItem>();
        }

        #endregion

        #region Module Installation

        /// <summary>
        /// Installs this module in Sitefinity system for the first time.
        /// </summary>
        /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
        public override void Install(SiteInitializer initializer)
        {
            base.Install(initializer);

            string resourceAssembly = this.GetType().Assembly.FullName;
            string customTemplatesArea = "Product catalog SDK sample";
            string friendlyWidgetName = "Products catalog sample widget";
            
            initializer.RegisterControlTemplate(MasterListView.titlesOnlyLayoutTemplateName, typeof(MasterListView).FullName, ProductsDefinitions.FrontendTitlesOnlyListViewName, null, customTemplatesArea, Presentation.AspNetTemplate, friendlyControlName:friendlyWidgetName );
            initializer.RegisterControlTemplate(MasterListView.titlesDatesLayoutTemplateName, typeof(MasterListView).FullName, ProductsDefinitions.FrontendTitlesDatesListViewName, null, customTemplatesArea, Presentation.AspNetTemplate, friendlyControlName: friendlyWidgetName);
            initializer.RegisterControlTemplate(MasterListView.titlesDatesSummariesLayoutTemplateName, typeof(MasterListView).FullName, ProductsDefinitions.FrontendTitlesDatesSummariesListViewName, null, customTemplatesArea, Presentation.AspNetTemplate, friendlyControlName: friendlyWidgetName);
            initializer.RegisterControlTemplate(MasterListView.titlesDatesContentsLayoutTemplateName, typeof(MasterListView).FullName, ProductsDefinitions.FrontendTitlesDatesContentsListViewName, null, customTemplatesArea, Presentation.AspNetTemplate, friendlyControlName: friendlyWidgetName);
            initializer.RegisterControlTemplate(ProductDetailsView.layoutTemplateName, typeof(ProductDetailsView).FullName, ProductsDefinitions.FrontendFullProductItemDetailViewName, null, customTemplatesArea, Presentation.AspNetTemplate, resourceAssembly, friendlyWidgetName);

            this.InstallCustomVirtualPaths(initializer);
            this.InstallCustomTaxonomies(initializer);
        }

        private void InstallCustomVirtualPaths(SiteInitializer initializer)
        {
            var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
            ConfigManager.Executed += new EventHandler<ExecutedEventArgs>(ConfigManager_Executed);
            var productsModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = ProductsModule.ProductsVirtualPath + "*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = "ProductCatalogSample"
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey(ProductsModule.ProductsVirtualPath + "*"))
                virtualPathConfig.VirtualPaths.Add(productsModuleVirtualPathConfig);
        }

        private void ConfigManager_Executed(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args)
        {
            if (args.CommandName == "SaveSection")
            {
                var section = args.CommandArguments as VirtualPathSettingsConfig;
                if (section != null)
                {
                    // Reset the VirtualPathManager whenever we save the VirtualPathConfig section.
                    // This is needed so that our prefixes for the widget templates in the module assembly are taken into account.
                    VirtualPathManager.Reset();
                }
            }
        }

        /// <summary>
        /// Installs the pages.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallPages(SiteInitializer initializer)
        {
            initializer.Installer
                .CreateModuleGroupPage(ProductsPageGroupId, "Products")
                    .PlaceUnder(CommonNode.TypesOfContent)
                    .LocalizeUsing<ProductsResources>()
                    .SetTitleLocalized("PageGroupNodeTitle")
                    .SetUrlNameLocalized("PageGroupNodeTitle")
                    .SetDescriptionLocalized("PageGroupNodeDescription")
                    .AddChildPage(ProductsModule.HomePageId, "Products")
                        .LocalizeUsing<ProductsResources>()
                        .SetTitleLocalized("ProductsLandingPageTitle")
                        .SetHtmlTitleLocalized("ProductsLandingPageHtmlTitle")
                        .SetUrlNameLocalized("ProductsLandingPageUrlName")
                        .SetDescriptionLocalized("ProductsLandingPageDescription")
                        .AddContentView(c =>
                        {
                            c.ControlDefinitionName = ProductsDefinitions.BackendDefinitionName;
                            c.ModuleName = ProductsModule.ModuleName;
                        })
                        .Done()
                    .AddChildPage(ProductsModule.CommentsPageId, "Comments")
                        .LocalizeUsing<ProductsResources>()
                        .SetTitleLocalized("ProductCommentsPageTitle")
                        .SetHtmlTitleLocalized("ProductCommentsPageHtmlTitle")
                        .SetUrlNameLocalized("ProductCommentsPageUrlName")
                        .SetDescriptionLocalized("ProductCommentsPageDescription")
                        .AddContentView(c =>
                        {
                            c.ControlDefinitionName = ProductsDefinitions.BackendCommentsDefinitionName;
                            c.ModuleName = ProductsModule.ModuleName;
                        })
                        .Done();
        }

        /// <summary>
        /// Installs the taxonomies.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected void InstallCustomTaxonomies(SiteInitializer initializer)
        {
            //installs the default Tags and Category taxonomies
            this.InstallTaxonomy(initializer, typeof(ProductItem));


            var metaMan = initializer.Context.MetadataManager;
            var taxMan = initializer.TaxonomyManager;

            var flatTaxonomy = this.CreateTaxonomy<FlatTaxonomy>(initializer, "Colors", ColorsTaxonomyId, "Color");

            var taxon1 = initializer.TaxonomyManager.CreateTaxon<FlatTaxon>();
            taxon1.Title = "Red";
            taxon1.Name = "Red";
            var taxon2 = initializer.TaxonomyManager.CreateTaxon<FlatTaxon>();
            taxon2.Title = "Blue";
            taxon2.Name = "Blue";
            flatTaxonomy.Taxa.Add(taxon1);
            flatTaxonomy.Taxa.Add(taxon2);

            var type = metaMan.GetMetaType(typeof(ProductItem));
            if (type == null)
            {
                type = metaMan.CreateMetaType(typeof(ProductItem));
            }

            if (!type.Fields.ToList().Any(fld => fld.FieldName == "Colors"))
            {
                var field = metaMan.CreateMetafield("Colors");
                field.TaxonomyProvider = taxMan.Provider.Name;
                field.TaxonomyId = ColorsTaxonomyId;
                field.IsSingleTaxon = false;
                type.Fields.Add(field);
            }

            if (!type.Fields.ToList().Any(fld => fld.FieldName == "ProductImage"))
            {
                type.Fields.Add(ContentLinksExtensions.CreateContentLinkField("ProductImage", "OpenAccessDataProvider", metaMan, RelationshipType.OneToOne));
            }
            

        }

        /// <summary>
        /// Gets the module config.
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<ProductsConfig>();
        }

        /// <summary>
        /// Installs module's toolbox configuration.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallConfiguration(SiteInitializer initializer)
        {
            // Module widget is installed on Bootstrapper_Initialized
            initializer.Installer
                .PageToolbox()
                    .ContentSection()
                        .LoadOrAddWidget<ProductsView>("ProductsView")
                            .SetTitle("WidgetTitle")
                            .SetDescription("WidgetDescription")
                            .LocalizeUsing<ProductsResources>()
                            .Done()
                        .Done()
                    .Done()
                .AddWorkflowType<ProductItem>()
                    .SetTitle("ProductsTitle")
                    .LocalizeUsing<ProductsResources>()
                    .Done();
        }

        /// <summary>
        /// Upgrades this module from the specified version.
        /// </summary>
        /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
        /// <param name="upgradeFrom">The version this module us upgrading from.</param>
        public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
        {
        }

        private TTaxonomy CreateTaxonomy<TTaxonomy>(SiteInitializer initializer, string taxonomyName, Guid taxonomyId, string taxonName) where TTaxonomy : class, ITaxonomy
        {

            var taxMan = initializer.TaxonomyManager;
            var taxonomy = taxMan.GetTaxonomies<TTaxonomy>().FirstOrDefault(t => t.Name == taxonomyName);
            if (taxonomy == null)
            {
                taxonomy = taxMan.CreateTaxonomy<TTaxonomy>(taxonomyId);
                taxonomy.Name = taxonomyName;
                taxonomy.Title = taxonomyName;
                taxonomy.TaxonName = taxonName;
                ((SecModel.ISecuredObject)taxonomy).CanInheritPermissions = true;
                ((SecModel.ISecuredObject)taxonomy).InheritsPermissions = true;
                ((SecModel.ISecuredObject)taxonomy).SupportedPermissionSets = new string[] { SecurityConstants.Sets.Taxonomies.SetName };
            }
            return taxonomy;
        }

        #endregion

        #region Constants

        /// <summary>
        /// Url that will be registered for the products workflow
        /// </summary>
        public static readonly string WorkflowRelativeUrl = "~/Workflows/Products.xamlx";

        /// <summary>
        /// Name of the news module. (e.g. used in ProductsManager)
        /// </summary>
        public const string ModuleName = "ProductsCatalog";

        /// <summary>
        /// Identity for the page group used by all pages in the products module
        /// </summary>
        public static readonly Guid ProductsPageGroupId = new Guid("F0EB1F97-6E95-455C-B349-86F4A25C75EB");

        /// <summary>
        /// Identity of the home (landing) page for the products module
        /// </summary>
        public static readonly Guid HomePageId = new Guid("21848245-6CB3-41F0-8A87-487B459914EB");

        /// <summary>
        /// Identity for the comments page for the products module
        /// </summary>
        public static readonly Guid CommentsPageId = new Guid("4e0f96fb-62c9-4423-8484-c05aa690971a");

        /// <summary>
        /// Localization resources' class Id for products
        /// </summary>
        public static readonly string ResourceClassId = typeof(ProductsResources).Name;

        /// <summary>
        /// Defines the configuration key that the ProductsView control will use to load its sub-views
        /// </summary>
        public const string ProductsViewConfigKey = "ProductsView";

        /// <summary>
        /// Defines the configuration key that the PublicProductsView control will use to load its sub-views
        /// </summary>
        public const string PublicProductsViewConfigKey = "PublicProductsView";

        #endregion

        #region Private Constants

        private static readonly Type[] managerTypes = new Type[] { typeof(ProductsManager) };

        private static readonly string WorkflowEmbeddedPath = "ProductCatalogSample.ProductsWorkflow.xamlx";
        public static readonly Guid ColorsTaxonomyId = new Guid("47FC0316-0F9B-4D6D-A606-6CDC1977E10D");
        public static readonly string ProductsVirtualPath = "~/SFProducts/";

        #endregion

        protected override void InstallTaxonomies(SiteInitializer initializer)
        {
            return;
        }
    }
}



