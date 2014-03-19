using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Configuration;
using ProductCatalogSample.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using System.Web.UI;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Modules.Pages;
using ProductCatalogSample.Web.UI.Public;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using ProductCatalogSample.Web.Controls;
using Telerik.Sitefinity.Model.ContentLinks;

namespace ProductCatalogSample.Web.UI
{
    /// <summary>
    /// This is a static class used to initialize the properties for all ContentView control views
    /// of supplied by default for the products module.
    /// </summary>
    public class ProductsDefinitions
    {
        /// <summary>
        /// Static constructor that makes it impossible to use the definitions
        /// without the module 
        /// </summary>
        static ProductsDefinitions()
        {
            // Ensure Products module is initialized.
            SystemManager.GetApplicationModule(ProductsModule.ModuleName);
        }

        /// <summary>
        /// Defines the products backend content view.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public static ContentViewControlElement DefineProductsBackendContentView(ConfigElement parent)
        {
            // define content view control
            var backendContentView = new ContentViewControlElement(parent)
            {
                ControlDefinitionName = BackendDefinitionName,
                ContentType = typeof(ProductItem)
            };

            // *** define views ***

            #region products backend list view

            var productsGridView = new MasterGridViewElement(backendContentView.ViewsConfig)
            {
                ViewName = ProductsDefinitions.BackendListViewName,
                ViewType = typeof(MasterGridView),
                AllowPaging = true,
                DisplayMode = FieldDisplayMode.Read,
                ItemsPerPage = 50,
                ResourceClassId = typeof(ProductsResources).Name,
                SearchFields = "Title",
                SortExpression = "Title ASC",
                Title = "ProductsViewTitle",
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Products.svc/"
            };

            #region Toolbar definition

            WidgetBarSectionElement masterViewToolbarSection = new WidgetBarSectionElement(productsGridView.ToolbarConfig.Sections)
            {
                Name = "toolbar"
            };


            var createproductsWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
            {
                Name = "CreateproductsWidget",
                ButtonType = CommandButtonType.Create,
                CommandName = DefinitionsHelper.CreateCommandName,
                Text = "CreateItem",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfMainAction",
                WidgetType = typeof(CommandWidget),
                PermissionSet = ProductsConstants.Security.PermissionSetName,
                ActionName = ProductsConstants.Security.Create
            };
            masterViewToolbarSection.Items.Add(createproductsWidget);

            var deleteproductsWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
            {
                Name = "DeleteproductsWidget",
                ButtonType = CommandButtonType.Standard,
                CommandName = DefinitionsHelper.GroupDeleteCommandName,
                Text = "Delete",
                ResourceClassId = typeof(ProductsResources).Name,
                WidgetType = typeof(CommandWidget),
                CssClass = "sfGroupBtn"
            };
            masterViewToolbarSection.Items.Add(deleteproductsWidget);


            masterViewToolbarSection.Items.Add(DefinitionsHelper.CreateSearchButtonWidget(masterViewToolbarSection.Items, typeof(ProductItem)));

            productsGridView.ToolbarConfig.Sections.Add(masterViewToolbarSection);

            #endregion

            #region Sidebar definition

            var languagesSection = new LocalizationWidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "Languages",
                Title = "Languages",
                ResourceClassId = typeof(LocalizationResources).Name,
                CssClass = "sfFirst sfSeparator sfLangSelector",
                WrapperTagId = "languagesSection"
            };

            languagesSection.Items.Add(new LanguagesDropDownListWidgetElement(languagesSection.Items)
            {
                Name = "Languages",
                Text = "Languages",
                ResourceClassId = typeof(LocalizationResources).Name,
                CssClass = "",
                WidgetType = typeof(LanguagesDropDownListWidget),
                IsSeparator = false,
                LanguageSource = LanguageSource.Frontend,
                AddAllLanguagesOption = false,
                CommandName = DefinitionsHelper.ChangeLanguageCommandName
            });

            WidgetBarSectionElement sidebarSection = new WidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "Filter",
                Title = "FilterProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
                WrapperTagId = "filterSection"
            };

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "AllProducts",
                CommandName = DefinitionsHelper.ShowAllItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "AllProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                ButtonCssClass = "sfSel",
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "MyProducts",
                CommandName = DefinitionsHelper.ShowMyItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "MyProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "DraftProducts",
                CommandName = DefinitionsHelper.ShowMasterItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "DraftProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "PublishedProducts",
                CommandName = DefinitionsHelper.ShowPublishedItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "PublishedProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "ScheduledProducts",
                CommandName = DefinitionsHelper.ShowScheduledItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ScheduledProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "PendingApprovalProducts",
                CommandName = DefinitionsHelper.PendingApprovalItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "WaitingForApproval",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new LiteralWidgetElement(sidebarSection.Items)
            {
                Name = "Separator",
                WrapperTagKey = HtmlTextWriterTag.Li,
                WidgetType = typeof(LiteralWidget),
                CssClass = "sfSeparator",
                Text = "&nbsp;",
                IsSeparator = true
            });

            var categoryFilterSection = new WidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "Category",
                Title = "ProductItemsByCategory",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfFilterBy sfSeparator",
                WrapperTagId = "categoryFilterSection",
                Visible = false
            };
            productsGridView.SidebarConfig.Sections.Add(categoryFilterSection);

            var tagFilterSection = new WidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "ByTag",
                Title = "ProductItemsByTag",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfFilterBy sfSeparator",
                WrapperTagId = "tagFilterSection",
                Visible = false
            };
            productsGridView.SidebarConfig.Sections.Add(tagFilterSection);

            var dateFilterSection = new WidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "UpdatedProducts",
                Title = "DisplayLastUpdatedProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfFilterBy sfFilterByDate sfSeparator",
                WrapperTagId = "dateFilterSection",
                Visible = false
            };
            productsGridView.SidebarConfig.Sections.Add(dateFilterSection);

            categoryFilterSection.Items.Add(new CommandWidgetElement(categoryFilterSection.Items)
            {
                Name = "CloseCategories",
                CommandName = DefinitionsHelper.ShowSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(
                    categoryFilterSection.WrapperTagId, tagFilterSection.WrapperTagId, dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CloseCategories",
                ResourceClassId = typeof(Labels).Name,
                CssClass = "sfCloseFilter",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            var categoryFilterWidget = new DynamicCommandWidgetElement(categoryFilterSection.Items)
            {
                Name = "CategoryFilter",
                CommandName = "filterByCategory",
                PageSize = 10,
                WidgetType = typeof(DynamicCommandWidget),
                IsSeparator = false,
                BindTo = BindCommandListTo.HierarchicalData,
                BaseServiceUrl = String.Format("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/{0}/", TaxonomyManager.CategoriesTaxonomyId),
                ChildItemsServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/subtaxa/",
                PredecessorServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/",
                ClientItemTemplate = @"<a href='javascript:void(0);' class='sf_binderCommand_filterByCategory'>{{ Title }}</a> <span class='sfCount'>({{ItemsCount}})</span>"
            };

            categoryFilterWidget.UrlParameters.Add("itemType", typeof(ProductItem).AssemblyQualifiedName);
            categoryFilterSection.Items.Add(categoryFilterWidget);

            tagFilterSection.Items.Add(new CommandWidgetElement(tagFilterSection.Items)
            {
                Name = "CloseTags",
                CommandName = DefinitionsHelper.ShowSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(
                    tagFilterSection.WrapperTagId, categoryFilterSection.WrapperTagId, dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CloseTags",
                ResourceClassId = typeof(Labels).Name,
                CssClass = "sfCloseFilter",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            var clientTemplateBuilder = new System.Text.StringBuilder();
            clientTemplateBuilder.Append(@"<a href=""javascript:void(0);"" class=""sf_binderCommand_filterByTag");
            clientTemplateBuilder.Append(@""">{{Title}}</a> <span class='sfCount'>({{ItemsCount}})</span>");
            var tagFilterWidget = new DynamicCommandWidgetElement(tagFilterSection.Items)
            {
                Name = "TagFilter",
                CommandName = "filterByTag",
                PageSize = 10,
                WidgetType = typeof(DynamicCommandWidget),
                IsSeparator = false,
                BindTo = BindCommandListTo.Client,
                BaseServiceUrl = String.Format("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/{0}/", TaxonomyManager.TagsTaxonomyId),
                ResourceClassId = typeof(Labels).Name,
                MoreLinkText = "ShowMoreTags",
                MoreLinkCssClass = "sfShowMore",
                LessLinkText = "ShowLessTags",
                LessLinkCssClass = "sfShowMore",
                SelectedItemCssClass = "sfSel",
                ClientItemTemplate = clientTemplateBuilder.ToString()
            };
            tagFilterWidget.UrlParameters.Add("itemType", typeof(ProductItem).AssemblyQualifiedName);

            tagFilterSection.Items.Add(tagFilterWidget);


            DefinitionsHelper.CreateTaxonomyLink(
                TaxonomyManager.CategoriesTaxonomyId,
                DefinitionsHelper.HideSectionsExceptCommandName,
                DefinitionsHelper.ConstructDisplaySectionsCommandArgument(categoryFilterSection.WrapperTagId),
                sidebarSection);

            DefinitionsHelper.CreateTaxonomyLink(
                TaxonomyManager.TagsTaxonomyId,
                DefinitionsHelper.HideSectionsExceptCommandName,
                DefinitionsHelper.ConstructDisplaySectionsCommandArgument(tagFilterSection.WrapperTagId),
                sidebarSection);

            #region Filter by date

            var closeDateFilterWidget = (new CommandWidgetElement(dateFilterSection.Items)
            {
                Name = "CloseDateFilter",
                CommandName = DefinitionsHelper.ShowSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(
                    tagFilterSection.WrapperTagId, categoryFilterSection.WrapperTagId, dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CloseDateFilter",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfCloseFilter",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });
            dateFilterSection.Items.Add(closeDateFilterWidget);

            var dateFilterWidget = new DateFilteringWidgetDefinitionElement(dateFilterSection.Items)
            {
                Name = "DateFilter",
                WidgetType = typeof(DateFilteringWidget),
                IsSeparator = false,
                PropertyNameToFilter = "LastModified"
            };

            DefinitionsHelper.GetPredefinedDateFilteringRanges(dateFilterWidget.PredefinedFilteringRanges);

            dateFilterSection.Items.Add(dateFilterWidget);

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "FilterByDate",
                CommandName = DefinitionsHelper.HideSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ByDateModified",
                ResourceClassId = typeof(ProductsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            #endregion Filter by date

            WidgetBarSectionElement manageAlso = new WidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "ManageAlso",
                Title = "ManageAlso",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfWidgetsList sfSeparator",
                WrapperTagId = "manageAlsoSection"
            };

            CommandWidgetElement productsComments = new CommandWidgetElement(manageAlso.Items)
            {
                Name = "productsComments",
                CommandName = DefinitionsHelper.CommentsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CommentsForProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfComments",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            manageAlso.Items.Add(productsComments);

            WidgetBarSectionElement settings = new WidgetBarSectionElement(productsGridView.SidebarConfig.Sections)
            {
                Name = "Settings",
                Title = "Settings",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfWidgetsList sfSettings",
                WrapperTagId = "settingsSection"
            };

            CommandWidgetElement productsPermissions = new CommandWidgetElement(settings.Items)
            {
                Name = "productsPermissions",
                CommandName = DefinitionsHelper.PermissionsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "PermissionsForProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(productsPermissions);

            CommandWidgetElement newsCustomFields = new CommandWidgetElement(settings.Items)
            {
                Name = "ProductsCustomFields",
                CommandName = DefinitionsHelper.ModuleEditor,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CustomFields",
                ResourceClassId = typeof(ProductsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(newsCustomFields);

            CommandWidgetElement productsSettings = new CommandWidgetElement(settings.Items)
            {
                Name = "productsSettings",
                CommandName = DefinitionsHelper.SettingsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "SettingsForProducts",
                ResourceClassId = typeof(ProductsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(productsSettings);



            productsGridView.SidebarConfig.Title = "ManageProducts";
            productsGridView.SidebarConfig.ResourceClassId = typeof(ProductsResources).Name;
            productsGridView.SidebarConfig.Sections.Add(languagesSection);
            productsGridView.SidebarConfig.Sections.Add(sidebarSection);
            productsGridView.SidebarConfig.Sections.Add(manageAlso);
            productsGridView.SidebarConfig.Sections.Add(settings);

            #endregion

            #region ContextBar definition

            var translationsContextBarSection = new LocalizationWidgetBarSectionElement(productsGridView.ContextBarConfig.Sections)
            {
                Name = "Languages",
                WrapperTagKey = HtmlTextWriterTag.Div,
                CssClass = "sfContextWidgetWrp",
                MinLanguagesCountTreshold = DefinitionsHelper.LanguageItemsPerRow
            };

            translationsContextBarSection.Items.Add(new CommandWidgetElement(translationsContextBarSection.Items)
            {
                Name = "ShowMoreTranslations",
                CommandName = DefinitionsHelper.ShowMoreTranslationsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ShowAllTranslations",
                ResourceClassId = typeof(LocalizationResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                CssClass = "sfShowHideLangVersions",
                WrapperTagKey = HtmlTextWriterTag.Div
            });

            translationsContextBarSection.Items.Add(new CommandWidgetElement(translationsContextBarSection.Items)
            {
                Name = "HideMoreTranslations",
                CommandName = DefinitionsHelper.HideMoreTranslationsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ShowBasicTranslationsOnly",
                ResourceClassId = typeof(LocalizationResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                CssClass = "sfDisplayNone sfShowHideLangVersions",
                WrapperTagKey = HtmlTextWriterTag.Div
            });

            productsGridView.ContextBarConfig.Sections.Add(translationsContextBarSection);

            #endregion

            #region Grid View Mode

            var gridMode = new GridViewModeElement(productsGridView.ViewModesConfig)
            {
                Name = "Grid"
            };
            productsGridView.ViewModesConfig.Add(gridMode);

            DataColumnElement titleColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Title",
                HeaderText = Res.Get<Labels>().Title,
                HeaderCssClass = "sfTitleCol",
                ItemCssClass = "sfTitleCol",
                ClientTemplate = @"<a sys:href='javascript:void(0);' sys:class=""{{ 'sf_binderCommand_edit sfItemTitle sf' + UIStatus.toLowerCase()}}"">
                    <strong>{{Title}}</strong>
                    <span class='sfStatusLocation'>{{Status}}</span></a>"
            };
            gridMode.ColumnsConfig.Add(titleColumn);


            DataColumnElement qtyColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "QuantityInStock",
                HeaderText = "QuantityInStock",
                ResourceClassId = typeof(ProductsResources).Name,
                ClientTemplate = "<span>{{QuantityInStock}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular"
            };
            gridMode.ColumnsConfig.Add(qtyColumn);

            DataColumnElement priceColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Price",
                HeaderText = "Price",
                ResourceClassId = typeof(ProductsResources).Name,
                ClientTemplate = "<span>{{Price}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular"
            };
            gridMode.ColumnsConfig.Add(priceColumn);


            var translationsColumn = new DynamicColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Translations",
                HeaderText = "Translations",
                ResourceClassId = typeof(LocalizationResources).Name,
                DynamicMarkupGenerator = typeof(LanguagesColumnMarkupGenerator),
                ItemCssClass = "sfLanguagesCol",
                HeaderCssClass = "sfLanguagesCol"
            };
            translationsColumn.GeneratorSettingsElement = new LanguagesColumnMarkupGeneratorElement(translationsColumn)
            {
                LanguageSource = LanguageSource.Frontend,
                ItemsInGroupCount = DefinitionsHelper.LanguageItemsPerRow,
                ContainerTag = "div",
                GroupTag = "div",
                ItemTag = "div",
                ContainerClass = string.Empty,
                GroupClass = string.Empty,
                ItemClass = string.Empty
            };
            gridMode.ColumnsConfig.Add(translationsColumn);

            ActionMenuColumnElement actionsColumn = new ActionMenuColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Actions",
                HeaderText = Res.Get<Labels>().Actions,
                HeaderCssClass = "sfMoreActions",
                ItemCssClass = "sfMoreActions"
            };
            FillActionMenuItems(actionsColumn.MenuItems, actionsColumn, typeof(ProductsResources).Name);
            gridMode.ColumnsConfig.Add(actionsColumn);

            DataColumnElement authorColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Author",
                HeaderText = Res.Get<Labels>().Author,
                ClientTemplate = "<span>{{Author}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular"
            };
            gridMode.ColumnsConfig.Add(authorColumn);

            DataColumnElement dateColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Date",
                HeaderText = Res.Get<Labels>().Date,
                ClientTemplate = "<span>{{ (DateCreated) ? DateCreated.sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss'): '-' }}</span>",
                HeaderCssClass = "sfDate",
                ItemCssClass = "sfDate"
            };
            gridMode.ColumnsConfig.Add(dateColumn);

            #endregion

            #region DecisionScreens definition

            DecisionScreenElement dsElement = new DecisionScreenElement(productsGridView.DecisionScreensConfig)
            {
                Name = "NoItemsExistScreen",
                DecisionType = DecisionType.NoItemsExist,
                MessageType = MessageType.Neutral,
                Displayed = false,
                Title = "WhatDoYouWantToDoNow",
                MessageText = "NoProductItems",
                ResourceClassId = typeof(ProductsResources).Name
            };

            CommandWidgetElement actionCreateNew = new CommandWidgetElement(dsElement.Actions)
            {
                Name = "Create",
                ButtonType = CommandButtonType.Create,
                CommandName = DefinitionsHelper.CreateCommandName,
                Text = "CreateItem",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfCreateItem",
                PermissionSet = ProductsConstants.Security.PermissionSetName,
                ActionName = ProductsConstants.Security.Create
            };
            dsElement.Actions.Add(actionCreateNew);

            productsGridView.DecisionScreensConfig.Add(dsElement);

            #endregion

            #region Dialogs definition

            var parameters = string.Concat(
                "?ControlDefinitionName=",
                ProductsDefinitions.BackendDefinitionName,
                "&ViewName=",
                ProductsDefinitions.BackendInsertViewName);
            DialogElement createDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.CreateCommandName,
                "ContentViewInsertDialog",
                parameters);
            productsGridView.DialogsConfig.Add(createDialogElement);

            parameters = string.Concat(
                "?ControlDefinitionName=",
                ProductsDefinitions.BackendDefinitionName,
                "&ViewName=",
                ProductsDefinitions.BackendEditViewName);
            DialogElement editDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.EditCommandName,
                "ContentViewEditDialog",
                parameters);
            productsGridView.DialogsConfig.Add(editDialogElement);

            parameters = string.Concat(
                "?ControlDefinitionName=",
                ProductsDefinitions.BackendDefinitionName,
                "&ViewName=",
                ProductsDefinitions.BackendPreviewViewName,
                "&backLabelText=", Res.Get<ProductsResources>().BackToItems, "&SuppressBackToButtonLabelModify=true");
            DialogElement previewDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.PreviewCommandName,
                "ContentViewEditDialog",
                parameters);
            productsGridView.DialogsConfig.Add(previewDialogElement);

            string permissionsParams = string.Concat(
                "?moduleName=", ProductsModule.ModuleName,
                "&typeName=", typeof(ProductItem).AssemblyQualifiedName,
                "&backLabelText=", Res.Get<ProductsResources>().BackToItems,
                "&title=", Res.Get<ProductsResources>().PermissionsForProducts);
            DialogElement permissionsDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.PermissionsCommandName,
                "ModulePermissionsDialog",
                permissionsParams);
            productsGridView.DialogsConfig.Add(permissionsDialogElement);

            string versioningParams = string.Concat(
                "?ControlDefinitionName=",
                ProductsDefinitions.BackendDefinitionName,
                "&moduleName=", ProductsModule.ModuleName,
                "&typeName=", typeof(ProductItem).AssemblyQualifiedName,
                "&title=", Res.Get<ProductsResources>().PermissionsForProducts,
                "&backLabelText=", Res.Get<ProductsResources>().BackToItems,
                "&" + ProductsDefinitions.ComparisonViewHistoryScreenQueryParameter + "=" + ProductsDefinitions.BackendVersionComapreViewName);

            DialogElement versioningDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.HistoryCommandName,
                "VersionHistoryDialog",
                versioningParams);
            productsGridView.DialogsConfig.Add(versioningDialogElement);

            string versioningGridParams = string.Concat(
               "?ControlDefinitionName=",
               ProductsDefinitions.BackendDefinitionName,
               "&moduleName=", ProductsModule.ModuleName,
               "&typeName=", typeof(ProductItem).AssemblyQualifiedName,
               "&title=", Res.Get<ProductsResources>().PermissionsForProducts,
               "&backLabelText=", Res.Get<ProductsResources>().BackToItems,
               "&" + ProductsDefinitions.ComparisonViewHistoryScreenQueryParameter + "=" + ProductsDefinitions.BackendVersionComapreViewName);

            DialogElement versioningGridDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                ProductsDefinitions.HistoryGridCommandName,
                "VersionHistoryDialog",
                versioningGridParams);
            productsGridView.DialogsConfig.Add(versioningGridDialogElement);

            parameters = string.Concat(
               "?ControlDefinitionName=",
               ProductsDefinitions.BackendDefinitionName,
               "&ViewName=",
               ProductsDefinitions.BackendVersionPreviewViewName, "&backLabelText=", Res.Get<Labels>().BackToRevisionHistory, "&SuppressBackToButtonLabelModify=true");
            DialogElement previewVersionDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.VersionPreviewCommandName,
                "ContentViewEditDialog",
                parameters);
            productsGridView.DialogsConfig.Add(previewVersionDialogElement);


            parameters = string.Concat("?TypeName=ProductCatalogSample.Model.ProductItem&Title=", "Product Fields", "&BackLabelText=", Res.Get<ProductsResources>().BackToItems, "&ItemsName=", "Products");
            DialogElement moduleEditorDialogElement = DefinitionsHelper.CreateDialogElement(
                productsGridView.DialogsConfig,
                DefinitionsHelper.ModuleEditor,
                "ModuleEditorDialog",
                parameters);
            productsGridView.DialogsConfig.Add(moduleEditorDialogElement);
            #endregion

            #region Links definition

            productsGridView.LinksConfig.Add(new LinkElement(productsGridView.LinksConfig)
            {
                Name = "viewComments",
                CommandName = DefinitionsHelper.CommentsCommandName,
                NavigateUrl = RouteHelper.CreateNodeReference(ProductsModule.CommentsPageId)
            });

            productsGridView.LinksConfig.Add(new LinkElement(productsGridView.LinksConfig)
            {
                Name = "viewSettings",
                CommandName = DefinitionsHelper.SettingsCommandName,
                NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.AdvancedSettingsNodeId) + "/products"
            });

            DefinitionsHelper.CreateNotImplementedLink(productsGridView);

            #endregion

            backendContentView.ViewsConfig.Add(productsGridView);

            #endregion

            #region products backend details view

            var productsEditDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "EditItem",
                ViewName = ProductsDefinitions.BackendEditViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Write,
                ShowTopToolbar = true,
                ResourceClassId = typeof(ProductsResources).Name,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Products.svc/",
                IsToRenderTranslationView = true
            };

            backendContentView.ViewsConfig.Add(productsEditDetailView);

            #region Versioning Comparison Screen

            var versionComparisonView = new ComparisonViewElement(backendContentView.ViewsConfig)
            {
                Title = "VersionComparison",
                ViewName = ProductsDefinitions.BackendVersionComapreViewName,
                ViewType = typeof(VersionComparisonView),
                DisplayMode = FieldDisplayMode.Read,
                ResourceClassId = typeof(ProductsResources).Name,
                UseWorkflow = false
            };

            backendContentView.ViewsConfig.Add(versionComparisonView);

            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "Title", Title = "lTitle", ResourceClassId = typeof(ProductsResources).Name });
            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "Content", Title = "Content", ResourceClassId = typeof(ProductsResources).Name });
            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "WhatIsInTheBox", Title = "WhatIsInTheBox", ResourceClassId = typeof(ProductsResources).Name });
            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "Price", Title = "Price", ResourceClassId = typeof(ProductsResources).Name });
            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "QuantityInStock", Title = "QuantityInStock", ResourceClassId = typeof(ProductsResources).Name });

            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "Category", Title = "Category", ResourceClassId = typeof(ProductsResources).Name });
            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "Tags", Title = "Tags", ResourceClassId = typeof(ProductsResources).Name });

            versionComparisonView.Fields.Add(new ComparisonFieldElement(versionComparisonView.Fields) { FieldName = "UrlName", Title = "UrlName", ResourceClassId = typeof(ProductsResources).Name });

            #endregion

            var productsInsertDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "CreateNewItem",
                ViewName = ProductsDefinitions.BackendInsertViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Write,
                ShowTopToolbar = true,
                ResourceClassId = typeof(ProductsResources).Name,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Products.svc/",
                IsToRenderTranslationView = false
            };

            backendContentView.ViewsConfig.Add(productsInsertDetailView);


            var previewExternalScripts = DefinitionsHelper.GetExtenalClientScripts(
                "Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity",
                "OnDetailViewLoaded");

            var productsPreviewDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                ViewName = ProductsDefinitions.BackendPreviewViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Read,
                ShowTopToolbar = true,
                ResourceClassId = typeof(ProductsResources).Name,
                ShowNavigation = false,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Products.svc/",
                UseWorkflow = false,
                ExternalClientScripts = previewExternalScripts
            };

            backendContentView.ViewsConfig.Add(productsPreviewDetailView);

            #region products backend forms definition

            #region Insert Form

            ProductsDefinitions.CreateBackendSections(productsInsertDetailView, FieldDisplayMode.Write);
            ProductsDefinitions.CreateBackendFormToolbar(productsInsertDetailView, typeof(ProductsResources).Name, true);

            #endregion

            #region Edit Form

            ProductsDefinitions.CreateBackendSections(productsEditDetailView, FieldDisplayMode.Write);
            ProductsDefinitions.CreateBackendFormToolbar(productsEditDetailView, typeof(ProductsResources).Name, false);

            #endregion

            #region Preview History Form

            var previewLocalization = new Dictionary<string, string>()
            {
                { "ItemVersionOfClientTemplate", Res.Get<VersionResources>().ItemVersionOfClientTemplate },
                { "PreviouslyPublished", Res.Get<VersionResources>().PreviouslyPublishedBrackets },
                { "CannotDeleteLastPublishedVersion", Res.Get<VersionResources>().CannotDeleteLastPublishedVersion }
            };
            //var previewExternalScripts = DefinitionsHelper.GetExtenalClientScripts(
            //    "Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity",
            //    "OnDetailViewLoaded");

            var productsHistoryPreviewDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "EditItem",
                ViewName = ProductsDefinitions.BackendVersionPreviewViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Read,
                ShowTopToolbar = false,
                ResourceClassId = typeof(ProductsResources).Name,
                ExternalClientScripts = previewExternalScripts,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Products.svc/",
                ShowNavigation = true,
                Localization = previewLocalization,
                UseWorkflow = false
            };

            CreateHistoryPreviewToolbar(productsHistoryPreviewDetailView);

            backendContentView.ViewsConfig.Add(productsHistoryPreviewDetailView);

            ProductsDefinitions.CreateBackendSections(productsHistoryPreviewDetailView, FieldDisplayMode.Read);

            #endregion

            #region Preview Form
            ProductsDefinitions.CreateBackendSections(productsPreviewDetailView, FieldDisplayMode.Read);
            //TODO: add the preview screen toolbar widgets -->Edit,etc...

            #endregion

            #endregion

            #endregion

            return backendContentView;

        }



        /// <summary>
        /// Creates the action menu widget element.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="wrapperTagKey">The wrapper tag key.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="text">The text.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        /// <returns></returns>
        public static CommandWidgetElement CreateActionMenuCommand(
            ConfigElement parent,
            string name,
            HtmlTextWriterTag wrapperTagKey,
            string commandName,
            string text,
            string resourceClassId)
        {
            return new CommandWidgetElement(parent)
            {
                Name = name,
                WrapperTagKey = wrapperTagKey,
                CommandName = commandName,
                Text = text,
                ResourceClassId = resourceClassId,
                WidgetType = typeof(CommandWidget)
            };
        }

        /// <summary>
        /// Creates the action menu command.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="wrapperTagKey">The wrapper tag key.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="text">The text.</param>
        /// <param name="resourceClassId">The resource class id.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        public static CommandWidgetElement CreateActionMenuCommand(
            ConfigElement parent,
            string name,
            HtmlTextWriterTag wrapperTagKey,
            string commandName,
            string text,
            string resourceClassId,
            string cssClass)
        {
            var commandWidgetElement = DefinitionsHelper.CreateActionMenuCommand(parent, name, wrapperTagKey, commandName, text, resourceClassId);
            commandWidgetElement.CssClass = cssClass;
            return commandWidgetElement;
        }

        /// <summary>
        /// Creates the actions menu separator.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="WrapperTagKey">The wrapper tag key.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="text">The text.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        /// <returns></returns>
        public static WidgetElement CreateActionMenuSeparator(
            ConfigElement parent,
            string name,
            HtmlTextWriterTag WrapperTagKey,
            string cssClass,
            string text,
            string resourceClassId)
        {
            return new LiteralWidgetElement(parent)
            {
                Name = name,
                WrapperTagKey = WrapperTagKey,
                CssClass = cssClass,
                Text = text,
                ResourceClassId = resourceClassId,
                WidgetType = typeof(LiteralWidget),
                IsSeparator = true
            };
        }




        /// <summary>
        /// Fills the action menu items.
        /// </summary>
        /// <param name="menuItems">The menu items.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        public static void FillActionMenuItems(ConfigElementList<WidgetElement> menuItems, ConfigElement parent, string resourceClassId)
        {
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "View", HtmlTextWriterTag.Li, PreviewCommandName, "View", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Delete", HtmlTextWriterTag.Li, DeleteCommandName, "Delete", resourceClassId, "sfDeleteItm"));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Publish", HtmlTextWriterTag.Li, PublishCommandName, "Publish", resourceClassId, "sfPublishItm"));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Unpublish", HtmlTextWriterTag.Li, UnpublishCommandName, "Unpublish", resourceClassId, "sfUnpublishItm"));
            menuItems.Add(
                CreateActionMenuSeparator(menuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Content", HtmlTextWriterTag.Li, EditCommandName, "Content", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Permissions", HtmlTextWriterTag.Li, PermissionsCommandName, "Permissions", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "History", HtmlTextWriterTag.Li, HistoryGridCommandName, "HistoryMenuItemTitle", "VersionResources"));
        }


        /// <summary>
        /// Creates the toolbar in the backend details form.
        /// </summary>
        /// <param name="detailView">The detail view.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        /// <param name="isCreateMode">if set to <c>true</c> the form is in Create mode.</param>
        private static void CreateBackendFormToolbar(DetailFormViewElement detailView, string resourceClassId, bool isCreateMode)
        {
            ProductsDefinitions.CreateBackendFormToolbar(detailView, resourceClassId, isCreateMode, "ThisItem", true);
        }

        private static void CreateBackendFormToolbar(DetailFormViewElement detailView, string resourceClassId, bool isCreateMode, string itemName, bool showPreview, string backToItems = "BackToItems")
        {
            var toolbarSectionElement = new WidgetBarSectionElement(detailView.Toolbar.Sections)
            {
                Name = "BackendForm",
                WrapperTagKey = HtmlTextWriterTag.Div,
                CssClass = "sfWorkflowMenuWrp"
            };

            // Create 
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "SaveChangesWidgetElement",
                ButtonType = CommandButtonType.Save,
                CommandName = DefinitionsHelper.SaveCommandName,
                Text = (isCreateMode) ? String.Concat("Create", itemName) : "SaveChanges",
                ResourceClassId = resourceClassId,
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget)
            });

            // Preview
            if (showPreview == true)
            {
                toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
                {
                    Name = "PreviewWidgetElement",
                    ButtonType = CommandButtonType.Standard,
                    CommandName = DefinitionsHelper.PreviewCommandName,
                    Text = "Preview",
                    ResourceClassId = typeof(Labels).Name,
                    WrapperTagKey = HtmlTextWriterTag.Span,
                    WidgetType = typeof(CommandWidget)
                });
            }
            if (!isCreateMode)
            {
                var actionsMenuWidget = new ActionMenuWidgetElement(toolbarSectionElement.Items)
                {
                    Name = "moreActions",
                    Text = Res.Get<Labels>().MoreActionsLink,
                    ResourceClassId = resourceClassId,
                    WrapperTagKey = HtmlTextWriterTag.Div,
                    WidgetType = typeof(ActionMenuWidget),
                    CssClass = "sfInlineBlock sfAlignMiddle"
                };
                actionsMenuWidget.MenuItems.Add(new CommandWidgetElement(actionsMenuWidget.MenuItems)
                {
                    Name = DeleteCommandName,
                    Text = "DeleteThisItem",
                    CommandName = DefinitionsHelper.DeleteCommandName,
                    ResourceClassId = resourceClassId,
                    WidgetType = typeof(CommandWidget),
                    CssClass = "sfDeleteItm"
                });

                actionsMenuWidget.MenuItems.Add(new CommandWidgetElement(actionsMenuWidget.MenuItems)
                {
                    Name = PermissionsCommandName,
                    ButtonType = CommandButtonType.SimpleLinkButton,
                    Text = "SetPermissions",
                    CommandName = DefinitionsHelper.PermissionsCommandName,
                    ResourceClassId = resourceClassId,
                    WidgetType = typeof(CommandWidget)
                });
                toolbarSectionElement.Items.Add(actionsMenuWidget);
            }

            // Cancel
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "CancelWidgetElement",
                ButtonType = CommandButtonType.Cancel,
                CommandName = DefinitionsHelper.CancelCommandName,
                Text = backToItems,
                ResourceClassId = resourceClassId,
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget)
            });


            detailView.Toolbar.Sections.Add(toolbarSectionElement);
        }

        /// <summary>
        /// Creates the toolbar in the backend details form for preview mode.
        /// </summary>
        /// <param name="detailView">The detail view.</param>
        private static void CreateHistoryPreviewToolbar(DetailFormViewElement detailView)
        {
            var toolbarSectionElement = new WidgetBarSectionElement(detailView.Toolbar.Sections)
            {
                Name = "History",
                WrapperTagKey = HtmlTextWriterTag.Div,
                CssClass = "sfDetachedBtnArea"
            };


            // Copy as New
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "CopyAsNewestWidgetElement",
                ButtonType = CommandButtonType.Standard,
                CommandName = DefinitionsHelper.RestoreVersionAsNewCommandName,
                Text = "CopyAsNewest",
                ResourceClassId = typeof(Labels).Name,
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget)
            });

            // Delete
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "DeleteVersionWidgetElement",
                ButtonType = CommandButtonType.Standard,
                CommandName = DefinitionsHelper.DeleteCommandName,
                Text = "Delete",
                ResourceClassId = typeof(ProductsResources).Name,
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget)
            });

            //Cancel
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "CancelWidgetElement",
                ButtonType = CommandButtonType.Cancel,
                CommandName = DefinitionsHelper.CancelCommandName,
                Text = "BackToRevisionHistory",
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget),
                ResourceClassId = typeof(Labels).Name
            });

            detailView.Toolbar.Sections.Add(toolbarSectionElement);
        }



        private static void CreateBackendSections(DetailFormViewElement detailView, FieldDisplayMode displayMode)
        {
            #region Toolbar section

            if (detailView.ViewName == ProductsDefinitions.BackendEditViewName)
            {
                var toolbarSection = new ContentViewSectionElement(detailView.Sections)
                {
                    Name = DefinitionsHelper.ToolbarSectionName
                };

                var languageListFieldElement = new LanguageListFieldElement(toolbarSection.Fields)
                {
                    ID = "languageListField",
                    FieldType = typeof(LanguageListField),
                    ResourceClassId = typeof(LocalizationResources).Name,
                    Title = "OtherTranslationsColon",
                    DisplayMode = displayMode,
                    FieldName = "languageListField",
                    DataFieldName = "AvailableLanguages"
                };
                toolbarSection.Fields.Add(languageListFieldElement);

                detailView.Sections.Add(toolbarSection);
            }

            #endregion

            #region Main section

            var mainSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "MainSection",
                CssClass = "sfFirstForm"
            };

            var titleField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "titleFieldControl",
                DataFieldName = (displayMode == FieldDisplayMode.Write) ? "Title.PersistedValue" : "Title",
                DisplayMode = displayMode,
                Title = "lTitle",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(ProductsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
            };
            titleField.ValidatorConfig = new ValidatorDefinitionElement(titleField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "TitleCannotBeEmpty",
                ResourceClassId = typeof(ProductsResources).Name
            };
            mainSection.Fields.Add(titleField);

            if (detailView.ViewName == ProductsDefinitions.BackendEditViewName || detailView.ViewName == ProductsDefinitions.BackendInsertViewName)
            {
                var languageChoiceField = new ChoiceFieldElement(mainSection.Fields)
                {
                    ID = "languageChoiceField",
                    FieldType = typeof(LanguageChoiceField),
                    ResourceClassId = typeof(ProductsResources).Name,
                    Title = "Language",
                    DisplayMode = displayMode,
                    FieldName = "languageField",
                    RenderChoiceAs = RenderChoicesAs.DropDown,
                    MutuallyExclusive = true,
                    DataFieldName = "AvailableLanguages"
                };
                mainSection.Fields.Add(languageChoiceField);
            }

            var dropDownTaxonomyField = new TaxonFieldDefinitionElement(mainSection.Fields)
            {
                ID = "testTaxonomyField",
                Title = "Colors",
                DataFieldName = "Colors",
                TaxonomyId = ProductsModule.ColorsTaxonomyId,
                FieldType = typeof(TaxonomyDropDownField),
                DisplayMode = displayMode,
            };
            mainSection.Fields.Add(dropDownTaxonomyField);

            var contentField = new HtmlFieldElement(mainSection.Fields)
            {
                ID = "contentFieldControl",
                DataFieldName = (displayMode == FieldDisplayMode.Write) ? "Content.PersistedValue" : "Content",
                DisplayMode = displayMode,
                CssClass = "sfFormSeparator sfContentField",
                ResourceClassId = typeof(ProductsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
                EditorContentFilters = Telerik.Web.UI.EditorFilters.DefaultFilters,
                EditorStripFormattingOptions = (Telerik.Web.UI.EditorStripFormattingOptions?)(Telerik.Web.UI.EditorStripFormattingOptions.MSWord | Telerik.Web.UI.EditorStripFormattingOptions.Css | Telerik.Web.UI.EditorStripFormattingOptions.Font | Telerik.Web.UI.EditorStripFormattingOptions.Span | Telerik.Web.UI.EditorStripFormattingOptions.ConvertWordLists)
            };
            mainSection.Fields.Add(contentField);



            //Product Image
            var productImageField = new ImageFieldElement(mainSection.Fields)
            {
                ID = "avatarField",
                DataFieldName = "ProductImage",
                DisplayMode = displayMode,
                UploadMode = ImageFieldUploadMode.Dialog,
                Title = "ProductImageFieldTitle",
                WrapperTag = HtmlTextWriterTag.Li,
                CssClass = "sfUserAvatar",
                ResourceClassId = typeof(ProductsResources).Name,
                DataFieldType = typeof(ContentLink),
                DefaultSrc = "~/SFRes/images/ProductCatalogSample/Images.NoProductImage.png",// put your default image location example: 
                SizeInPx = 100
            };
            mainSection.Fields.Add(productImageField);




            var summaryField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "whatsInTheBoxFieldControl",
                DataFieldName = (displayMode == FieldDisplayMode.Write) ? "WhatIsInTheBox.PersistedValue" : "WhatIsInTheBox",
                DisplayMode = displayMode,
                Title = "WhatIsInTheBox",
                CssClass = "sfFormSeparator",
                WrapperTag = HtmlTextWriterTag.Li,
                ResourceClassId = typeof(ProductsResources).Name,
                Rows = 5
            };
            summaryField.ExpandableDefinitionConfig = new ExpandableControlElement(summaryField)
            {
                Expanded = false,
                ExpandText = "ClickToAddSummary",
                ResourceClassId = typeof(ProductsResources).Name
            };
            summaryField.ValidatorConfig.Required = false;
            mainSection.Fields.Add(summaryField);


            var quantityField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "quantityFieldControl",
                DataFieldName = "QuantityInStock",
                DisplayMode = displayMode,
                Title = "QuantityInStock",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(ProductsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            quantityField.ValidatorConfig = new ValidatorDefinitionElement(quantityField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "TitleCannotBeEmpty",
                ResourceClassId = typeof(ProductsResources).Name,
                RegularExpression = "^[0-9]+[.]*[0-9]*$",
                RegularExpressionViolationMessage = "TheQuantityMustBeAPositiveNumber"
            };
            mainSection.Fields.Add(quantityField);

            var priceField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "priceFieldControl",
                DataFieldName = "Price",
                DisplayMode = displayMode,
                Title = "Price",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(ProductsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
            };
            priceField.ValidatorConfig = new ValidatorDefinitionElement(priceField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "TitleCannotBeEmpty",
                ResourceClassId = typeof(ProductsResources).Name,
                RegularExpression = "^[0-9]+[.]*[0-9]*$",
                RegularExpressionViolationMessage = "ThePriceMustBeAPositiveNumber"
            };
            mainSection.Fields.Add(priceField);

            detailView.Sections.Add(mainSection);

            #endregion

            #region Categories and Tags
            var taxonSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "TaxonSection",
                Title ="CategoriesAndTags",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfExpandableForm",
                ExpandableDefinitionConfig =
                {
                    Expanded = false
                }
            };

            var categories = DefinitionTemplates.CategoriesFieldWriteMode(taxonSection.Fields);
            categories.DisplayMode = displayMode;
            taxonSection.Fields.Add(categories);

            var tags = DefinitionTemplates.TagsFieldWriteMode(taxonSection.Fields);
            tags.DisplayMode = displayMode;
            tags.CssClass = "sfFormSeparator";
            tags.ExpandableDefinition.Expanded = true;
            tags.Description = "TagsFieldInstructions";
            taxonSection.Fields.Add(tags);

            detailView.Sections.Add(taxonSection);
            #endregion

            #region More options section

            var moreOptionsSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "MoreOptionsSection",
                Title ="MoreOptionsURL",
                ResourceClassId = typeof(ProductsResources).Name,
                CssClass = "sfExpandableForm",
                ExpandableDefinitionConfig =
                {
                    Expanded = false
                }
            };

            if (displayMode == FieldDisplayMode.Write)
            {
                var urlName = new MirrorTextFieldElement(moreOptionsSection.Fields)
                {
                    Title = "UrlName",
                    ResourceClassId = typeof(ProductsResources).Name,
                    ID = "urlName",
                    MirroredControlId = titleField.ID,
                    DataFieldName = (displayMode == FieldDisplayMode.Write) ? "UrlName.PersistedValue" : "UrlName",
                    DisplayMode = displayMode,
                    RegularExpressionFilter = DefinitionsHelper.UrlRegularExpressionFilter,
                    WrapperTag = HtmlTextWriterTag.Li,
                    ReplaceWith = "-"
                };
                var validationDef = new ValidatorDefinitionElement(urlName)
                {
                    Required = true,
                    MessageCssClass = "sfError",
                    RequiredViolationMessage = Res.Get<ProductsResources>().UrlNameCannotBeEmpty,
                    RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForValidator,
                    RegularExpressionViolationMessage = Res.Get<PageResources>().UrlNameInvalidSymbols
                };
                urlName.ValidatorConfig = validationDef;

                moreOptionsSection.Fields.Add(urlName);
            }

            var allowCommentsFieldElement = new ChoiceFieldElement(moreOptionsSection.Fields)
            {
                ID = "allowCommentsField",
                Title = (displayMode == FieldDisplayMode.Read) ? "AllowComments" : string.Empty,
                DataFieldName = "AllowComments",
                DisplayMode = displayMode,
                RenderChoiceAs = RenderChoicesAs.SingleCheckBox,
                CssClass = "sfCheckBox sfFormSeparator",
                WrapperTag = HtmlTextWriterTag.Li,
                ResourceClassId = typeof(ProductsResources).Name
            };
            allowCommentsFieldElement.ChoicesConfig.Add(
                new ChoiceElement(allowCommentsFieldElement.ChoicesConfig)
                {
                    Text = "AllowComments",
                    ResourceClassId = typeof(ProductsResources).Name
                });
            moreOptionsSection.Fields.Add(allowCommentsFieldElement);

            detailView.Sections.Add(moreOptionsSection);

            #endregion

            #region Sidebar

            if (displayMode == FieldDisplayMode.Write)
            {
                var sidebar = new ContentViewSectionElement(detailView.Sections)
                {
                    Name = DefinitionsHelper.SidebarSectionName,
                    CssClass = "sfItemReadOnlyInfo"
                };

                sidebar.Fields.Add(new ContentWorkflowStatusInfoFieldElement(sidebar.Fields)
                {
                    DisplayMode = displayMode,
                    FieldName = "NewsWorkflowStatusInfoField",
                    ResourceClassId = typeof(ProductsResources).Name,
                    WrapperTag = HtmlTextWriterTag.Li,
                    FieldType = typeof(ContentWorkflowStatusInfoField)
                });

                detailView.Sections.Add(sidebar);
            }

            #endregion

        }

        #region Constants

        #region Backend

        /// <summary>
        /// Name of the backend definition (all backend interface)
        /// </summary>
        public const string BackendDefinitionName = "ProductsBackend";
        /// <summary>
        /// Name of the the list view in the backend definition
        /// </summary>
        public const string BackendListViewName = "ProductsBackendList";
        /// <summary>
        /// Name of the backend view that edits product items
        /// </summary>
        public const string BackendEditViewName = "ProductsBackendEdit";
        /// <summary>
        /// Name of the backend view that creates (inserts) backend products
        /// </summary>
        public const string BackendInsertViewName = "ProductsBackendInsert";
        /// <summary>
        /// Name of the backend view that previews an item before it is saved
        /// </summary>
        public const string BackendPreviewViewName = "ProductsBackendPreview";
        /// <summary>
        /// Name of the backend view that previes history (version) items
        /// </summary>
        public const string BackendVersionPreviewViewName = "ProductsBackendVersionPreview";
        /// <summary>
        /// Name of the backend view that compares two history items
        /// </summary>
        public const string BackendVersionComapreViewName = "ProdcutsBackendVersionComparisonView";

        /// <summary>
        /// Name of the backend view that shows all comments
        /// </summary>
        public static string BackendCommentsDefinitionName = "ProductsCommentsBackend";

        /// <summary>
        /// Query string parameter for revision history dialog
        /// </summary>
        private const string ComparisonViewHistoryScreenQueryParameter = "VersionComparisonView";

        #endregion


        #region Frontend Definitions

        /// <summary>
        /// Defines the ContentView control for News on the frontend
        /// </summary>
        /// <param name="parent">The parent configuration element.</param>
        /// <returns>A configured instance of <see cref="ContentViewControlElement"/>.</returns>
        internal static ContentViewControlElement DefineProductsFrontendContentView(ConfigElement parent)
        {
            // define content view control
            var controlDefinition = new ContentViewControlElement(parent)
            {
                ControlDefinitionName = ProductsDefinitions.FrontendDefinitionName,
                ContentType = typeof(ProductItem)
            };

            // *** define views ***

            #region News backend list view

            var newsListView = new ContentViewMasterElement(controlDefinition.ViewsConfig)
            {
                ViewName = ProductsDefinitions.FrontendListViewName,
                ViewType = typeof(ProductCatalogSample.Web.UI.Public.MasterListView),
                AllowPaging = true,
                DisplayMode = FieldDisplayMode.Read,
                ItemsPerPage = 20,
                ResourceClassId = typeof(ProductsResources).Name,
                FilterExpression = DefinitionsHelper.PublishedOrScheduledFilterExpression,
                SortExpression = "PublicationDate DESC"
            };

            controlDefinition.ViewsConfig.Add(newsListView);

            #endregion

            #region News backend details view

            var newsDetailsView = new ContentViewDetailElement(controlDefinition.ViewsConfig)
            {
                ViewName = ProductsDefinitions.FrontendDetailViewName,
                ViewType = typeof(ProductDetailsView),
                ShowSections = false,
                DisplayMode = FieldDisplayMode.Read,
                ResourceClassId = typeof(ProductsResources).Name
            };

            controlDefinition.ViewsConfig.Add(newsDetailsView);

            #endregion

            return controlDefinition;
        }



        /// <summary>
        /// Frontend definitoin name
        /// </summary>
        public const string FrontendDefinitionName = "ProductsFrontend";
        /// <summary>
        /// Detail view definition
        /// </summary>
        public const string FrontendDetailViewName = "ProductsFrontendDetails";
        /// <summary>
        /// Master view definition
        /// </summary>
        public const string FrontendListViewName = "ProductsFrontendList";

        /// <summary>
        /// Name of the view that displays only titles
        /// </summary>
        public const string FrontendTitlesOnlyListViewName = "Products sample list - Titles only";
        /// <summary>
        /// Name of the view that displays titles and days
        /// </summary>
        public const string FrontendTitlesDatesListViewName = "Products sample list - Titles and dates";
        /// <summary>
        /// Name of the view that displays title,s dates and full content
        /// </summary>
        public const string FrontendTitlesDatesContentsListViewName = "Products sample list  - Titles, dates and full content";
        /// <summary>
        /// Name of the view that displays titles, dates and summary
        /// </summary>
        public const string FrontendTitlesDatesSummariesListViewName = "Products sample list - Titles, dates and summaries";
        /// <summary>
        /// Name of the view that displays the whole product item
        /// </summary>
        public const string FrontendFullProductItemDetailViewName = "Products sample - Full product";
        /// <summary>
        /// Definition name for the frontend commments list.
        /// </summary>
        public static readonly string FrontendCommentsDefinitionName = "ProductsCommentsFrontend";
        #endregion

        #region Commands
        /// <summary>
        /// Common name for the command used to create a new item.
        /// </summary>
        public const string CreateCommandName = "create";

        /// <summary>
        /// Common name for the command used to save an item.
        /// </summary>
        public const string SaveCommandName = "save";

        /// <summary>
        /// Common name for a command used to cancel an operation.
        /// </summary>
        public const string CancelCommandName = "cancel";

        /// <summary>
        /// Common name used for a command that saves an item and resets the entry form.
        /// </summary>
        public const string SaveAndContinueCommandName = "saveAndContinue";

        /// <summary>
        /// Common name used for a command that publishes an item.
        /// </summary>
        public const string PublishCommandName = "publish";

        /// <summary>
        /// Common name used for a command that unpublishes an item.
        /// </summary>
        public const string UnpublishCommandName = "unpublish";

        /// <summary>
        /// Common name used for a command that performs a batch publish
        /// </summary>
        public const string GroupPublishCommandName = "groupPublish";

        /// <summary>
        /// Common name used for a command that edits an item.
        /// </summary>
        public const string EditCommandName = "edit";
        /// <summary>
        /// Common name used for a command that performs a batch unpublish
        /// </summary>
        public const string GroupUnpublishCommandName = "groupUnpublish";
        
        /// <summary>
        /// Common name used for a command that delets an item.
        /// </summary>
        public const string DeleteCommandName = "delete";

        /// <summary>
        /// Common name used for a command that displays the history of an item
        /// </summary>
        public const string HistoryCommandName = "history";

        /// <summary>
        /// Common name used for a command that opens an item in a preview mode.
        /// </summary>
        public const string PreviewCommandName = "preview";

        /// <summary>
        /// Common name used for a command that shows the permissions for a given secured
        /// object.
        /// </summary>
        public const string PermissionsCommandName = "permissions";

        /// <summary>
        /// Common name used for a command that displays the history of an item.
        /// </summary>
        public const string HistoryGridCommandName = "historygrid";

        #endregion

        #endregion

    }
}
