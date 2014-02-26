using System.Collections.Specialized;
using ProductCatalogSample.Data;
using ProductCatalogSample.Data.Implementation;
using ProductCatalogSample.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security;
using ProductCatalogSample.Model;

namespace ProductCatalogSample.Configuration
{
    /// <summary>
    /// Global configuration file
    /// </summary>
    [ObjectInfo(typeof(ProductsResources), Title = "ProductsConfigCaption", Description = "ProductsConfigDescription")]
    public class ProductsConfig : ContentModuleConfigBase
    {
        /// <summary>
        /// Initializes the default providers.
        /// </summary>
        /// <param name="providers">Config element to fill with defautt providers</param>
        protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
        {
            providers.Add(new DataProviderSettings(providers)
            {
                Name = "OpenAccessDataProvider",
                Description = "A provider that stores news data in database using OpenAccess ORM.",
                ProviderType = typeof(OpenAccessProvider),
                Parameters = new NameValueCollection() { { "applicationName", "/Products" } }
            });
        }

        protected override void OnPropertiesInitialized()
        {
            base.OnPropertiesInitialized();
            this.InitializeDefaultPermissions();
        }

        protected virtual void InitializeDefaultPermissions()
        {
            #region Create Custom Set
            
            var productsPermissionSet = new Permission(this.Permissions)
                {
                    Name = ProductsConstants.Security.PermissionSetName,
                    Title = "ProductsPermissions",
                    Description = "ProductsPermissionsDescription",
                    ResourceClassId = typeof(ProductsResources).Name
                };
            this.Permissions.Add(productsPermissionSet);

            productsPermissionSet.Actions.Add(new SecurityAction(productsPermissionSet.Actions)
            {
                Name = ProductsConstants.Security.View,
                Type = SecurityActionTypes.View,
                Title = "ViewProducts",
                Description = "ViewProductsDescription",
                ResourceClassId = typeof(ProductsResources).Name
            });
            productsPermissionSet.Actions.Add(new SecurityAction(productsPermissionSet.Actions)
            {
                Name = ProductsConstants.Security.Create,
                Type = SecurityActionTypes.Create,
                Title = "CreateProducts",
                Description = "CreateProductsDescription",
                ResourceClassId = typeof(ProductsResources).Name
            });
            productsPermissionSet.Actions.Add(new SecurityAction(productsPermissionSet.Actions)
            {
                Name = ProductsConstants.Security.Modify,
                Type = SecurityActionTypes.Modify,
                Title = "ModifyProducts",
                Description = "ModifyProductsDescription",
                ResourceClassId = typeof(ProductsResources).Name
            });
            productsPermissionSet.Actions.Add(new SecurityAction(productsPermissionSet.Actions)
            {
                Name = ProductsConstants.Security.Delete,
                Type = SecurityActionTypes.Delete,
                Title = "DeleteProducts",
                Description = "DeleteProductsDescription",
                ResourceClassId = typeof(ProductsResources).Name
            });
            productsPermissionSet.Actions.Add(new SecurityAction(productsPermissionSet.Actions)
            {
                Name = ProductsConstants.Security.ChangeOwner,
                Type = SecurityActionTypes.ChangeOwner,
                Title = "ChangeProductsOwner",
                Description = "ChangeProductsOwnerDescription",
                ResourceClassId = typeof(ProductsResources).Name
            });
            productsPermissionSet.Actions.Add(new SecurityAction(productsPermissionSet.Actions)
            {
                Name = ProductsConstants.Security.ChangePermissions,
                Type = SecurityActionTypes.ChangePermissions,
                Title = "ChangeProductsPermissions",
                Description = "ChangeProductsPermissionsDescription",
                ResourceClassId = typeof(ProductsResources).Name
            });

            #endregion

            #region Custom Permission Display Settings

            #region General Permission Set

            var customPermissionsDisplaySettings = this.CustomPermissionsDisplaySettings;
            var generalCustomSet = new CustomPermissionsDisplaySettingsConfig(customPermissionsDisplaySettings)
            {
                SetName = SecurityConstants.Sets.General.SetName
            };
            customPermissionsDisplaySettings.Add(generalCustomSet);


            var productsCustomActions = new SecuredObjectCustomPermissionSet(generalCustomSet.SecuredObjectCustomPermissionSets)
            {
                TypeName = typeof(ProductItem).FullName
            };
            generalCustomSet.SecuredObjectCustomPermissionSets.Add(productsCustomActions);

            var productCreateAction = new CustomSecurityAction(productsCustomActions.CustomSecurityActions)
            {
                Name = SecurityConstants.Sets.General.Create,
                ShowActionInList = false,
                Title = string.Empty,
                ResourceClassId = string.Empty
            };
            productsCustomActions.CustomSecurityActions.Add(productCreateAction);

            var productModifyAction = new CustomSecurityAction(productsCustomActions.CustomSecurityActions)
            {
                Name = SecurityConstants.Sets.General.Modify,
                ShowActionInList = true,
                Title = "ModifyThisItem",
                ResourceClassId = typeof(SecurityResources).Name
            };
            productsCustomActions.CustomSecurityActions.Add(productModifyAction);

            var productViewAction = new CustomSecurityAction(productsCustomActions.CustomSecurityActions)
            {
                Name = SecurityConstants.Sets.General.View,
                ShowActionInList = true,
                Title = "ViewThisItem",
                ResourceClassId = typeof(SecurityResources).Name
            };
            productsCustomActions.CustomSecurityActions.Add(productViewAction);

            var productDeleteAction = new CustomSecurityAction(productsCustomActions.CustomSecurityActions)
            {
                Name = SecurityConstants.Sets.General.Delete,
                ShowActionInList = true,
                Title = "DeleteThisItem",
                ResourceClassId = typeof(SecurityResources).Name
            };
            productsCustomActions.CustomSecurityActions.Add(productDeleteAction);

            var productChangeOwnerAction = new CustomSecurityAction(productsCustomActions.CustomSecurityActions)
            {
                Name = SecurityConstants.Sets.General.ChangeOwner,
                ShowActionInList = true,
                Title = "ChangeOwnerOfThisItem",
                ResourceClassId = typeof(SecurityResources).Name
            };
            productsCustomActions.CustomSecurityActions.Add(productChangeOwnerAction);

            var productChangePermissionsAction = new CustomSecurityAction(productsCustomActions.CustomSecurityActions)
            {
                Name = SecurityConstants.Sets.General.ChangePermissions,
                ShowActionInList = true,
                Title = "ChangePermissionsOfThisItem",
                ResourceClassId = typeof(SecurityResources).Name
            };
            productsCustomActions.CustomSecurityActions.Add(productChangePermissionsAction);

            #endregion

            #region Products Permission Set

            var productsCustomSet = new CustomPermissionsDisplaySettingsConfig(CustomPermissionsDisplaySettings)
                {
                    SetName = ProductsConstants.Security.PermissionSetName
                };
            CustomPermissionsDisplaySettings.Add(productsCustomSet);

            var productsCustomSetActions = new SecuredObjectCustomPermissionSet(productsCustomSet.SecuredObjectCustomPermissionSets) { TypeName = typeof(ProductItem).FullName };
            productsCustomSet.SecuredObjectCustomPermissionSets.Add(productsCustomSetActions);

            var productSetCreateAction = new CustomSecurityAction(productsCustomSetActions.CustomSecurityActions)
            {
                Name = ProductsConstants.Security.Create,
                ShowActionInList = false,
                Title = string.Empty,
                ResourceClassId = string.Empty
            };
            productsCustomSetActions.CustomSecurityActions.Add(productSetCreateAction);

            var productSetModifyAction = new CustomSecurityAction(productsCustomSetActions.CustomSecurityActions)
            {
                Name = ProductsConstants.Security.Modify,
                ShowActionInList = true,
                Title = "ModifyThisProduct",
                ResourceClassId = typeof(ProductsResources).Name
            };
            productsCustomSetActions.CustomSecurityActions.Add(productSetModifyAction);

            var productSetViewAction = new CustomSecurityAction(productsCustomSetActions.CustomSecurityActions)
            {
                Name = ProductsConstants.Security.View,
                ShowActionInList = true,
                Title = "ViewThisProduct",
                ResourceClassId = typeof(ProductsResources).Name
            };
            productsCustomSetActions.CustomSecurityActions.Add(productSetViewAction);

            var productSetDeleteAction = new CustomSecurityAction(productsCustomSetActions.CustomSecurityActions)
            {
                Name = ProductsConstants.Security.Delete,
                ShowActionInList = true,
                Title = "DeleteThisProduct",
                ResourceClassId = typeof(ProductsResources).Name
            };
            productsCustomSetActions.CustomSecurityActions.Add(productSetDeleteAction);

            var productSetChangeOwnerAction = new CustomSecurityAction(productsCustomSetActions.CustomSecurityActions)
            {
                Name = ProductsConstants.Security.ChangeOwner,
                ShowActionInList = true,
                Title = "ChangeOwnerOfThisProduct",
                ResourceClassId = typeof(ProductsResources).Name
            };
            productsCustomSetActions.CustomSecurityActions.Add(productSetChangeOwnerAction);

            var productSetChangePermissionsAction = new CustomSecurityAction(productsCustomSetActions.CustomSecurityActions)
            {
                Name = ProductsConstants.Security.ChangePermissions,
                ShowActionInList = true,
                Title = "ChangePermissionsOfThisProduct",
                ResourceClassId = typeof(ProductsResources).Name
            };
            productsCustomSetActions.CustomSecurityActions.Add(productSetChangePermissionsAction);
            
            #endregion
            
            #endregion
        }

        /// <summary>
        /// Initializes the default views.
        /// </summary>
        /// <param name="contentViewControls">Content view controls config element to fill with default views</param>
        protected override void InitializeDefaultViews(ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
        {
            contentViewControls.Add(ProductsDefinitions.DefineProductsBackendContentView(contentViewControls));

            contentViewControls.Add(
                    CommentsDefinitions.DefineCommentsBackendContentView(
                    contentViewControls,
                    ProductsDefinitions.BackendCommentsDefinitionName,
                    this.DefaultProvider,
                    typeof(ProductsManager),
                    typeof(ProductsResources).Name));

            contentViewControls.Add(ProductsDefinitions.DefineProductsFrontendContentView(contentViewControls));

            contentViewControls.Add(
                    CommentsDefinitions.DefineCommentsFrontendView(
                    contentViewControls,
                    ProductsDefinitions.FrontendCommentsDefinitionName,
                    this.DefaultProvider,
                    typeof(ProductsManager)));
        }
    }
}
