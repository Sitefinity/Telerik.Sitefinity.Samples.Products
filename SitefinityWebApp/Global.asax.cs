using System;
using ProductCatalogSample;
using ProductCatalogSample.Web.UI.Public;
using Telerik.Sitefinity.Samples.Common;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        private const string SamplesThemeName = "SamplesTheme";
        private const string SamplesThemePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Themes/Samples";

        private const string SamplesTemplateId = "015b4db0-1d4f-4938-afec-5da59749e0e8";
        private const string SamplesTemplateName = "SamplesMasterPage";
        private const string SamplesTemplatePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Master/Samples.master";

        private const string ProductsPageId = "2FF33642-8F5C-49DA-8FA6-BCA5307DC846";
        private const string ProductsPageName = "ProductsSample";

        protected void Application_Start(object sender, EventArgs e)
        {
            Telerik.Sitefinity.Abstractions.Bootstrapper.Initializing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(Bootstrapper_Initializing);
            SystemManager.ApplicationStart += SystemManager_ApplicationStart;
        }

        protected void SystemManager_ApplicationStart(object sender, EventArgs e)
        {
            SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(CreateSampleWorker);
            SystemManager.RunWithElevatedPrivilege(worker);
        }

        protected void Bootstrapper_Initializing(object sender, Telerik.Sitefinity.Data.ExecutingEventArgs args)
        {
            if (args.CommandName == "RegisterRoutes")
            {
                SampleUtilities.RegisterModule<ProductsModule>("ProductsCatalog", "This is a module that provides product listings built on top of a native content-based module in Sitefinity. Content-based modules themselves are based on Generic Content and reuse much of the functionality available there. This is the most comprehensive example of a content-based module that features all built-in functionality of a native Sitefinity module.");
            }
        }

        private void CreateSampleWorker(object[] args)
        {                        
            SampleUtilities.RegisterToolboxWidget("Products Widget", typeof(ProductsView), "Samples");
            SampleUtilities.RegisterTheme(SamplesThemeName, SamplesThemePath);
            SampleUtilities.RegisterTemplate(new Guid(SamplesTemplateId), SamplesTemplateName, SamplesTemplateName, SamplesTemplatePath, SamplesThemeName);

            var result = SampleUtilities.CreatePage(new Guid(ProductsPageId), ProductsPageName, true);

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(ProductsPageId), new Guid(SamplesTemplateId));

                ProductsView productsView = new ProductsView();
                SampleUtilities.AddControlToPage(new Guid(ProductsPageId), productsView, "Content", "Products Widget");
            }            

            //create admin
            SampleUtilities.CreateUsersAndRoles();
            //SampleUtilities.FrontEndAuthenticate();
        }       

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}