using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace ProductCatalogSample
{
    /// <summary>
    /// Resource class for the procuts module
    /// </summary>
    [ObjectInfo(typeof(ProductsResources), Title = "ProductsResourcesTitle", Description = "ProductsResourcesDescription")]
    public class ProductsResources : Resource
    {
        #region Constructions
        
        /// <summary>
        /// Initializes new instance of <see cref="ProductsResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ProductsResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="ProductsResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public ProductsResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Class Description
        /// <summary>
        /// CustomPipeInboundName
        /// </summary>
        [ResourceEntry("CustomPipeInboundName",
            Value = "CustomPipe-Inbound",
            Description = "Custom Pipe Inbound Name",
            LastModified = "2011/02/28")]
        public string CustomPipeInboundName
        {
            get { return this["CustomPipeInboundName"]; }
        }

        /// <summary>
        /// CustomPipeOutboundName
        /// </summary>
        [ResourceEntry("CustomPipeOutboundName",
            Value = "CustomPipe-Outbound",
            Description = "Custom Pipe Outbound Name",
            LastModified = "2011/03/01")]
        public string CustomPipeOutboundName
        {
            get { return this["CustomPipeOutboundName"]; }
        }

        /// <summary>
        /// The title of this class
        /// </summary>
        [ResourceEntry("ProductsResourcesTitle",
            Value = "Products Catalog",
            Description = "The title of this class.",
            LastModified = "2010/12/01")]
        public string ProductsResourcesTitle
        {
            get { return this["ProductsResourcesTitle"];  }
        }

        /// <summary>
        /// The description of this class
        /// </summary>
        [ResourceEntry("ProductsResourcesDescription",
            Value = "Contains localizable resources for Products Catalog module.",
            Description = "The description of this class.",
            LastModified = "2010/12/01")]
        public string ProductsResourcesDescription
        {
            get { return this["ProductsResourcesDescription"];  }
        }

        #endregion

        #region ProductsConfig

        /// <summary>
        /// Products
        /// </summary>
        [ResourceEntry("ProductsConfigCaption",
            Value = "Products",
            Description = "Global configuration file for the Products module",
            LastModified = "2010/12/17")]
        public string ProductsConfigCaption
        {
            get { return this["ProductsConfigCaption"]; }
        }

        /// <summary>
        /// Global configuration file for the Products module
        /// </summary>
        [ResourceEntry("ProductsConfigDescription",
            Value = "Global configuration file for the Products module",
            Description = "Description of the ProductsConfig configuration class",
            LastModified = "2010/12/17")]
        public string ProductsConfigDescription
        {
            get { return this["ProductsConfigDescription"]; }
        }


        #endregion

        #region Resources

        /// <summary>
        /// Title of the DesignerContentTitleText
        /// </summary>
        [ResourceEntry("DesignerContentTitleText",
            Value = "Which products to display?",
            Description = "DesignerContentTitleText",
            LastModified = "2010/12/07")]
        public string DesignerContentTitleText
        {
            get
            {
                return this["DesignerContentTitleText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseAllText
        /// </summary>
        [ResourceEntry("DesignerChooseAllText",
            Value = "Choose all",
            Description = "DesignerChooseAllText",
            LastModified = "2010/12/07")]
        public string DesignerChooseAllText
        {
            get
            {
                return this["DesignerChooseAllText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseSingleText
        /// </summary>
        [ResourceEntry("DesignerChooseSingleText",
            Value = "Choose single",
            Description = "DesignerChooseSingleText",
            LastModified = "2010/12/07")]
        public string DesignerChooseSingleText
        {
            get
            {
                return this["DesignerChooseSingleText"];
            }
        }

        /// <summary>
        /// Title of the Products
        /// </summary>
        [ResourceEntry("Products",
            Value = "Products",
            Description = "Products",
            LastModified = "2011/05/26")]
        public string Products
        {
            get
            {
                return this["Products"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseSimpleFilterText
        /// </summary>
        [ResourceEntry("DesignerChooseSimpleFilterText",
            Value = "Choose simple filter",
            Description = "DesignerChooseSimpleFilterText",
            LastModified = "2010/12/07")]
        public string DesignerChooseSimpleFilterText
        {
            get
            {
                return this["DesignerChooseSimpleFilterText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseAdvancedFilterText
        /// </summary>
        [ResourceEntry("DesignerChooseAdvancedFilterText",
            Value = "Choose advanced filter",
            Description = "DesignerChooseAdvancedFilterText",
            LastModified = "2010/12/07")]
        public string DesignerChooseAdvancedFilterText
        {
            get
            {
                return this["DesignerChooseAdvancedFilterText"];
            }
        }

        /// <summary>
        /// Title of the DesignerNoContentToSelectText
        /// </summary>
        [ResourceEntry("DesignerNoContentToSelectText",
            Value = "No content to select",
            Description = "DesignerNoContentToSelectText",
            LastModified = "2010/12/07")]
        public string DesignerNoContentToSelectText
        {
            get
            {
                return this["DesignerNoContentToSelectText"];
            }
        }

        /// <summary>
        /// Title of the DesignerContentSelectorTitleText
        /// </summary>
        [ResourceEntry("DesignerContentSelectorTitleText",
            Value = "Content selector title",
            Description = "DesignerContentSelectorTitleText",
            LastModified = "2010/12/07")]
        public string DesignerContentSelectorTitleText
        {
            get
            {
                return this["DesignerContentSelectorTitleText"];
            }
        }

        /// <summary>
        /// Title of the DesignerListSettingsSortItemstext
        /// </summary>
        [ResourceEntry("DesignerListSettingsSortItemstext",
            Value = "Sort products",
            Description = "DesignerListSettingsSortItemstext",
            LastModified = "2010/12/07")]
        public string DesignerListSettingsSortItemstext
        {
            get
            {
                return this["DesignerListSettingsSortItemstext"];
            }
        }

        /// <summary>
        /// Title of the ManageProducts
        /// </summary>
        [ResourceEntry("ManageProducts",
            Value = "Manage Products",
            Description = "ManageProducts",
            LastModified = "2010/12/07")]
        public string ManageProducts
        {
            get
            {
                return this["ManageProducts"];
            }
        }


        #region Security-related resources

        /// <summary>
        /// Title of the Products permissions set
        /// </summary>
        [ResourceEntry("ProductsPermissions",
            Value = "Products",
            Description = "Title of the Products permissions set",
            LastModified = "2010/12/03")]
        public string ProductsPermissions
        {
            get
            {
                return this["ProductsPermissions"];
            }
        }

        /// <summary>
        /// Description of the Products permissions set
        /// </summary>
        [ResourceEntry("ProductsPermissionsDescription",
            Value = "Represents the Products set of security actions permissions.",
            Description = "Description of the Products permissions set",
            LastModified = "2010/12/03")]
        public string ProductsPermissionsDescription
        {
            get
            {
                return this["ProductsPermissionsDescription"];
            }
        }

        /// <summary>
        /// The title of ViewProducts security action.
        /// </summary>
        [ResourceEntry("ViewProducts",
            Value = "View products",
            Description = "The title of ViewProducts security action.",
            LastModified = "2010/12/03")]
        public string ViewProducts
        {
            get
            {
                return this["ViewProducts"];
            }
        }

        /// <summary>
        /// The description of ViewProducts security action.
        /// </summary>
        [ResourceEntry("ViewProductsDescription",
            Value = "Allows or denies viewing of products",
            Description = "The description of ViewProducts security action.",
            LastModified = "2010/12/03")]
        public string ViewProductsDescription
        {
            get
            {
                return this["ViewProductsDescription"];
            }
        }

        /// <summary>
        /// The title of CreateProducts security action.
        /// </summary>
        [ResourceEntry("CreateProducts",
            Value = "Create a product",
            Description = "The title of CreateProducts security action.",
            LastModified = "2010/12/03")]
        public string CreateProducts
        {
            get
            {
                return this["CreateProducts"];
            }
        }

        /// <summary>
        /// The description of CreateProducts security action.
        /// </summary>
        [ResourceEntry("CreateProductsDescription",
            Value = "Allows or denies creation of products",
            Description = "The description of CreateProducts security action.",
            LastModified = "2010/12/03")]
        public string CreateProductsDescription
        {
            get
            {
                return this["CreateProductsDescription"];
            }
        }

        /// <summary>
        /// The title of CreateProducts security action.
        /// </summary>
        [ResourceEntry("ModifyProducts",
            Value = "Modify a product",
            Description = "The title of ModifyProducts security action.",
            LastModified = "2010/12/03")]
        public string ModifyProducts
        {
            get
            {
                return this["ModifyProducts"];
            }
        }

        /// <summary>
        /// The description of ModifyProducts security action.
        /// </summary>
        [ResourceEntry("ModifyProductsDescription",
            Value = "Allows or denies modifying products",
            Description = "The description of ModifyProducts security action.",
            LastModified = "2010/12/03")]
        public string ModifyProductsDescription
        {
            get
            {
                return this["ModifyProductsDescription"];
            }
        }

        /// <summary>
        /// The title of DeleteProducts security action.
        /// </summary>
        [ResourceEntry("DeleteProducts",
            Value = "Delete a product",
            Description = "The title of DeleteProducts security action.",
            LastModified = "2010/12/03")]
        public string DeleteProducts
        {
            get
            {
                return this["DeleteProducts"];
            }
        }

        /// <summary>
        /// The description of DeleteProducts security action.
        /// </summary>
        [ResourceEntry("DeleteProductsDescription",
            Value = "Allows or denies deleting products",
            Description = "The description of DeleteProducts security action.",
            LastModified = "2010/12/03")]
        public string DeleteProductsDescription
        {
            get
            {
                return this["DeleteProductsDescription"];
            }
        }

        /// <summary>
        /// The title of ChangeProductsOwner security action.
        /// </summary>
        [ResourceEntry("ChangeProductsOwner",
            Value = "Change product owner",
            Description = "The title of ChangeProductsOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeProductsOwner
        {
            get
            {
                return this["ChangeProductsOwner"];
            }
        }

        /// <summary>
        /// The description of ChangeProductsOwner security action.
        /// </summary>
        [ResourceEntry("ChangeProductsOwnerDescription",
            Value = "Allows or denies changing the owner of a product.",
            Description = "The description of ChangeProductsOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeProductsOwnerDescription
        {
            get
            {
                return this["ChangeProductsOwnerDescription"];
            }
        }

        /// <summary>
        /// The title of ChangeProductsPermissions security action.
        /// </summary>
        [ResourceEntry("ChangeProductsPermissions",
            Value = "Change product permissions",
            Description = "The title of ChangeProductsPermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangeProductsPermissions
        {
            get
            {
                return this["ChangeProductsPermissions"];
            }
        }

        /// <summary>
        /// The description of ChangeProductsPermissions security action.
        /// </summary>
        [ResourceEntry("ChangeProductsPermissionsDescription",
            Value = "Allows or denies changing the permissions of a product.",
            Description = "The description of ChangeProductsPermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangeProductsPermissionsDescription
        {
            get
            {
                return this["ChangeProductsPermissionsDescription"];
            }
        }

        /// <summary>
        /// The title of ModifyThisProduct security action: Modify action, for a specific product item.
        /// </summary>
        [ResourceEntry("ModifyThisProduct",
            Value = "Modify this product",
            Description = "The title of ModifyThisProduct security action.",
            LastModified = "2010/12/03")]
        public string ModifyThisProduct
        {
            get
            {
                return this["ModifyThisProduct"];
            }
        }

        /// <summary>
        /// The title of ViewThisProduct security action: View action, for a specific product item.
        /// </summary>
        [ResourceEntry("ViewThisProduct",
            Value = "View this product",
            Description = "The title of ViewThisProduct security action.",
            LastModified = "2010/12/03")]
        public string ViewThisProduct
        {
            get
            {
                return this["ViewThisProduct"];
            }
        }

        /// <summary>
        /// The title of DeleteThisProduct security action: Delete action, for a specific product item.
        /// </summary>
        [ResourceEntry("DeleteThisProduct",
            Value = "Delete this product",
            Description = "The title of DeleteThisProduct security action.",
            LastModified = "2010/12/03")]
        public string DeleteThisProduct
        {
            get
            {
                return this["DeleteThisProduct"];
            }
        }

        /// <summary>
        /// The title of ChangeOwnerOfThisProduct security action: ChangeOwner action, for a specific product item.
        /// </summary>
        [ResourceEntry("ChangeOwnerOfThisProduct",
            Value = "Change this product's owner",
            Description = "The title of ChangeOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeOwnerOfThisProduct
        {
            get
            {
                return this["ChangeOwnerOfThisProduct"];
            }
        }

        /// <summary>
        /// The title of ChangePermissionsOfThisProduct security action: ChangePermissions action, for a specific product item.
        /// </summary>
        [ResourceEntry("ChangePermissionsOfThisProduct",
            Value = "Change this product's permissions",
            Description = "The title of ChangePermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangePermissionsOfThisProduct
        {
            get
            {
                return this["ChangePermissionsOfThisProduct"];
            }
        }

        #endregion

        #region Default Pages

        #region Menu page group

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("PageGroupNodeTitle",
            Value="Products",
            Description="phrase: Products",
            LastModified="2010/12/03")]
        public string PageGroupNodeTitle
        {
            get { return this["PageGroupNodeTitle"]; }
        }

        /// <summary>
        /// phrase: This is the page group that contains all pages for the products module.
        /// </summary>
        [ResourceEntry("PageGroupNodeDescription",
            Value = "This is the page group that contains all pages for the products module.",
            Description = "phrase: This is the page group that contains all pages for the products module.",
            LastModified = "2010/12/03")]
        public string PageGroupNodeDescription
        {
            get { return this["PageGroupNodeDescription"]; }
        }

        #endregion

        #region Landing page

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("ProductsLandingPageTitle",
            Value = "Products",
            Description = "phrase: Products",
            LastModified = "2010/12/03")]
        public string ProductsLandingPageTitle
        {
            get { return this["ProductsLandingPageTitle"]; }
        }

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("ProductsLandingPageHtmlTitle",
            Value = "Products",
            Description = "phrase: Products",
            LastModified = "2010/12/03")]
        public string ProductsLandingPageHtmlTitle
        {
            get { return this["ProductsLandingPageHtmlTitle"]; }
        }

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("ProductsLandingPageUrlName",
            Value = "Products",
            Description = "phrase: Products",
            LastModified = "2010/12/03")]
        public string ProductsLandingPageUrlName
        {
            get { return this["ProductsLandingPageUrlName"]; }
        }

        /// <summary>
        /// phrase: Landing page for the Products module
        /// </summary>
        [ResourceEntry("ProductsLandingPageDescription",
            Value = "Landing page for the Products module",
            Description = "phrase: Landing page for the Products module",
            LastModified = "2010/12/03")]
        public string ProductsLandingPageDescription
        {
            get { return this["ProductsLandingPageDescription"]; }
        }

        #endregion

        #region Comments
        /// <summary>
        /// Phrase: AllowComments 
        /// </summary>
        [ResourceEntry("AllowComments",
            Value = "Allow comments",
            Description = "Phrase: Allow comments",
            LastModified = "2010/09/20")]
        public string AllowComments
        {
            get
            {
                return this["AllowComments"];
            }
        }
        /// <summary>
        /// phrase: Comments
        /// </summary>
        [ResourceEntry("ProductCommentsPageTitle",
            Value = "Comments",
            Description = "phrase: Comments",
            LastModified = "2010/12/03")]
        public string ProductCommentsPageTitle
        {
            get { return this["ProductCommentsPageTitle"]; }
        }

        /// <summary>
        /// phrase: Comments
        /// </summary>
        [ResourceEntry("ProductCommentsPageUrlName",
            Value = "Comments",
            Description = "phrase: Comments",
            LastModified = "2010/12/03")]
        public string ProductCommentsPageUrlName
        {
            get { return this["ProductCommentsPageUrlName"]; }
        }

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("ProductCommentsPageHtmlTitle",
            Value = "Products",
            Description = "phrase: Products",
            LastModified = "2010/12/03")]
        public string ProductCommentsPageHtmlTitle
        {
            get { return this["ProductCommentsPageHtmlTitle"]; }
        }

        /// <summary>
        /// phrase: Page that displays comments for products
        /// </summary>
        [ResourceEntry("ProductCommentsPageDescription",
            Value = "Page that displays comments for products",
            Description = "phrase: Page that displays comments for products",
            LastModified = "2010/12/03")]
        public string ProductCommentsPageDescription
        {
            get { return this["ProductCommentsPageDescription"]; }
        }

        #endregion

        #endregion

        #region ContentView registration
        
        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("ProductsViewTitle",
            Value = "Products",
            Description = "phrase: Products",
            LastModified = "2010/12/03")]
        public string ProductsViewTitle
        {
            get { return this["ProductsViewTitle"]; }
        }
     
        /// <summary>
        /// phrase: Widget that displays product items
        /// </summary>
        [ResourceEntry("ProductsViewDescription",
            Value = "Widget that displays product items",
            Description = "phrase: Widget that displays product items",
            LastModified = "2010/12/03")]
        public string ProductsViewDescription
        {
            get { return this["ProductsViewDescription"]; }
        }

        #endregion

        /// <summary>
        /// The title of the edit item dialog
        /// </summary>
        [ResourceEntry("EditItem",
            Value = "Edit a product",
            Description = "The title of the edit item dialog",
            LastModified = "2009/12/20")]
        public string EditItem
        {
            get { return this["EditItem"]; }
        }

        /// <summary>
        /// Gets the quantity in stock.
        /// </summary>
        /// <value>The quantity in stock.</value>
        [ResourceEntry("QuantityInStock",
        Value = "Quantity in stock",
        Description = "QuantityInStock",
        LastModified = "2010/12/03")]
        public string QuantityInStock
        {
            get
            {
                return this["QuantityInStock"];
            }
        }

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <value>The price.</value>
        [ResourceEntry("Price",
        Value = "Price",
        Description = "Price",
        LastModified = "2010/12/03")]
        public string Price
        {
            get
            {
                return this["Price"];
            }
        }

        /// <summary>
        /// Gets the price must be A positive number.
        /// </summary>
        /// <value>The price must be A positive number.</value>
        [ResourceEntry("ThePriceMustBeAPositiveNumber",
        Value = "The price must be a positive number",
        Description = "The price must be a positive number",
        LastModified = "2010/12/09")]
        public string ThePriceMustBeAPositiveNumber
        {
            get
            {
                return this["ThePriceMustBeAPositiveNumber"];
            }
        }

        /// <summary>
        /// Gets the quantity must be A positive number.
        /// </summary>
        /// <value>The quantity must be A positive number.</value>
        [ResourceEntry("TheQuantityMustBeAPositiveNumber",
        Value = "The quantity must be a positive number",
        Description = "The quantity must be a positive number",
        LastModified = "2010/12/09")]
        public string TheQuantityMustBeAPositiveNumber
        {
            get
            {
                return this["TheQuantityMustBeAPositiveNumber"];
            }
        }

        /// <summary>
        /// word: View
        /// </summary>
        /// <value>The view.</value>
        [ResourceEntry("View",
            Value = "View",
            Description = "word: View",
            LastModified = "2010/01/28")]
        public string View
        {
            get
            {
                return this["View"];
            }
        }


        /// <summary>
        /// word: Delete
        /// </summary>
        /// <value>The delete.</value>
        [ResourceEntry("Delete",
            Value = "Delete",
            Description = "word: Delete",
            LastModified = "2010/01/25")]
        public string Delete
        {
            get { return this["Delete"]; }
        }

        /// <summary>
        /// word: <strong>Edit...</strong>
        /// </summary>
        [ResourceEntry("Edit",
            Value = "<strong>Edit...</strong>",
            Description = "word",
            LastModified = "2010/01/29")]
        public string Edit
        {
            get
            {
                return this["Edit"];
            }
        }

        /// <summary>
        /// word: Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2010/01/28")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// word: Permissions
        /// </summary>
        [ResourceEntry("Permissions",
            Value = "Permissions",
            Description = "word: Permissions",
            LastModified = "2010/01/29")]
        public string Permissions
        {
            get { return this["Permissions"]; }
        }

        /// <summary>
        /// Messsage: Create products item
        /// </summary>
        /// <value>Label of the dialog that creates a products item.</value>
        [ResourceEntry(
            "CreateItem",
            Value = "Create a products item",
            Description = "Label of the dialog that creates a products item.",
            LastModified = "2010/6/27")
        ]
        public string CreateItem
        {
            get { return this["CreateItem"]; }
        }


        /// <summary>
        /// phrase: No products items have been created yet.
        /// </summary>
        [ResourceEntry("NoProductItems",
            Value = "No product items have been created yet",
            Description = "phrase: No product items have been created yet",
            LastModified = "2010/07/26")]
        public string NoProductItems
        {
            get
            {
                return this["NoProductItems"];
            }
        }


        /// <summary>
        /// phrase: What do you want to do now?
        /// </summary>
        [ResourceEntry("WhatDoYouWantToDoNow",
            Value = "What do you want to do now?",
            Description = "phrase: What do you want to do now?",
            LastModified = "2009/01/28")]
        public string WhatDoYouWantToDoNow
        {
            get
            {
                return this["WhatDoYouWantToDoNow"];
            }
        }


        /// <summary>
        /// Phrase: Products items by category
        /// </summary>
        [ResourceEntry("ProductItemsByCategory",
            Value = "Product items by category",
            Description = "Phrase: Product items by category",
            LastModified = "2010/07/23")]
        public string ProductItemsByCategory
        {
            get
            {
                return this["ProductItemsByCategory"];
            }
        }


        /// <summary>
        /// Phrase: Products items by tag
        /// </summary>
        [ResourceEntry("ProductItemsByTag",
            Value = "Product items by tag",
            Description = "Phrase: Product items by tag",
            LastModified = "2010/07/23")]
        public string ProductItemsByTag
        {
            get
            {
                return this["ProductItemsByTag"];
            }
        }

        /// <summary>
        /// The text of last updated products sidebar button
        /// </summary>
        [ResourceEntry("DisplayLastUpdatedProducts",
            Value = "Display product items last updated in...",
            Description = "The text of last updated products sidebar button",
            LastModified = "2010/08/20")]
        public string DisplayLastUpdatedProducts
        {
            get
            {
                return this["DisplayLastUpdatedProducts"];
            }
        }


        /// <summary>
        /// phrase: Filter products
        /// </summary>
        [ResourceEntry("FilterProducts",
            Value = "Filter products",
            Description = "phrase: Filter products",
            LastModified = "2010/01/25")]
        public string FilterProducts
        {
            get { return this["FilterProducts"]; }
        }

        /// <summary>
        /// phrase: Manage also.
        /// </summary>
        [ResourceEntry("ManageAlso",
            Value = "Manage also",
            Description = "phrase: Manage also",
            LastModified = "2010/01/25")]
        public string ManageAlso
        {
            get { return this["ManageAlso"]; }
        }


        /// <summary>
        /// Phrase: Edit News settings 
        /// </summary>
        [ResourceEntry("EditProductsSettings",
            Value = "Set which products to display",
            Description = "Phrase: Edit News settings",
            LastModified = "2010/11/13")]
        public string EditProductsSettings
        {
            get
            {
                return this["EditProductsSettings"];
            }
        }

        /// <summary>
        /// phrase: Products Settings.
        /// </summary>
        [ResourceEntry("Settings",
            Value = "Settings for products",
            Description = "phrase: Products Settings",
            LastModified = "2010/01/25")]
        public string Settings
        {
            get { return this["Settings"]; }
        }

        /// <summary>
        /// Phrase: Close date
        /// </summary>
        [ResourceEntry("CloseDateFilter",
                       Value = "Close dates",
                       Description = "The link for closing the date filter widget in the sidebar.",
                       LastModified = "2010/08/20")]
        public string CloseDateFilter
        {
            get
            {
                return this["CloseDateFilter"];
            }
        }

        /// <summary>
        /// Phrase: All products
        /// </summary>
        [ResourceEntry("AllProducts",
            Value = "All products",
            Description = "Phrase: All products",
            LastModified = "2010/07/27")]
        public string AllProducts
        {
            get
            {
                return this["AllProducts"];
            }
        }


        /// <summary>
        /// The text of my products sidebar button
        /// </summary>
        [ResourceEntry("MyProducts",
            Value = "My products",
            Description = "The text of my products sidebar button",
            LastModified = "2010/08/19")]
        public string MyProducts
        {
            get
            {
                return this["MyProducts"];
            }
        }


        /// <summary>
        /// Gets the published products.
        /// </summary>
        /// <value>The published products.</value>
        [ResourceEntry("PublishedProducts",
            Value = "Published",
            Description = "The text of published products sidebar button.",
            LastModified = "2010/08/19")]
        public string PublishedProducts
        {
            get
            {
                return this["PublishedProducts"];
            }
        }

        /// <summary>
        /// word: Drafts
        /// </summary>
        [ResourceEntry("DraftProducts",
            Value = "Drafts",
            Description = "The text of draft products sidebar button.",
            LastModified = "2010/08/19")]
        public string DraftProducts
        {
            get
            {
                return this["DraftProducts"];
            }
        }



        /// <summary>
        /// word: Scheduled
        /// </summary>
        [ResourceEntry("ScheduledProducts",
            Value = "Scheduled",
            Description = "The text of scheduled products sidebar button.",
            LastModified = "2010/08/20")]
        public string ScheduledProducts
        {
            get
            {
                return this["ScheduledProducts"];
            }
        }

        /// <summary>
        /// phrase: Waiting for approval
        /// </summary>
        [ResourceEntry("WaitingForApproval",
            Value = "Waiting for approval",
            Description = "The text of the 'Waiting for approval' button in the products sidebar.",
            LastModified = "2010/11/08")]
        public string WaitingForApproval
        {
            get
            {
                return this["WaitingForApproval"];
            }
        }


        /// <summary>
        /// phrase: Drafts
        /// </summary>
        [ResourceEntry("ByDateModified",
            Value = "by Date modified...",
            Description = "The text of the date filter sidebar button.",
            LastModified = "2010/08/20")]
        public string ByDateModified
        {
            get
            {
                return this["ByDateModified"];
            }
        }



        /// <summary>
        /// phrase: Comments for products.
        /// </summary>
        [ResourceEntry("CommentsForProducts",
            Value = "Comments for products",
            Description = "phrase: Comments for products",
            LastModified = "2010/01/25")]
        public string CommentsForProducts
        {
            get { return this["CommentsForProducts"]; }
        }


        /// <summary>
        /// phrase: Permissions for products
        /// </summary>
        [ResourceEntry("PermissionsForProducts",
            Value = "Permissions",
            Description = "phrase: Permissions for products",
            LastModified = "2010/01/29")]
        public string PermissionsForProducts
        {
            get { return this["PermissionsForProducts"]; }
        }


        /// <summary>
        /// phrase: Settings for products
        /// </summary>
        [ResourceEntry("SettingsForProducts",
            Value = "Settings",
            Description = "phrase: Settings for products",
            LastModified = "2010/01/29")]
        public string SettingsForProducts
        {
            get { return this["SettingsForProducts"]; }
        }

        /// <summary>
        /// The title of the create new item dialog
        /// </summary>
        [ResourceEntry("CreateNewItem",
            Value = "Create a products item",
            Description = "The title of the create new item dialog",
            LastModified = "2010/07/26")]
        public string CreateNewItem
        {
            get { return this["CreateNewItem"]; }
        }

        /// <summary>
        /// Back to products
        /// </summary>
        [ResourceEntry("BackToItems",
                       Value = "Back to Products",
                       Description = "The text of the back to products link",
                       LastModified = "2010/10/13")]
        public string BackToItems
        {
            get
            {
                return this["BackToItems"];
            }
        }

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("ModuleTitle",
            Value = "Products",
            Description = "phrase: Products",
            LastModified = "2010/12/07")]
        public string ModuleTitle
        {
            get { return this["ModuleTitle"]; }
        }

        /// <summary>
        /// phrase: Products
        /// </summary>
        [ResourceEntry("WhatIsInTheBox",
            Value = "What's in the box",
            Description = "phrase: Products",
            LastModified = "2010/12/07")]
        public string WhatIsInTheBox
        {
            get { return this["WhatIsInTheBox"]; }
        }


        /// <summary>
        /// phrase: Title
        /// </summary>
        [ResourceEntry("lTitle",
            Value = "Title",
            Description = "phrase: Title",
            LastModified = "2010/12/09")]
        public string lTitle
        {
            get { return this["lTitle"]; }
        }


        /// <summary>
        /// phrase: Title cannot be empty
        /// </summary>
        [ResourceEntry("TitleCannotBeEmpty",
            Value = "Title cannot be empty",
            Description = "phrase: Title cannot be empty",
            LastModified = "2010/12/09")]
        public string TitleCannotBeEmpty
        {
            get { return this["TitleCannotBeEmpty"]; }
        }


        /// <summary>
        /// phrase: Language
        /// </summary>
        [ResourceEntry("Language",
            Value = "Language",
            Description = "phrase: Language",
            LastModified = "2010/12/09")]
        public string Language
        {
            get { return this["Language"]; }
        }


        /// <summary>
        /// phrase: Summary
        /// </summary>
        [ResourceEntry("lSummary",
            Value = "Summary",
            Description = "phrase: Summary",
            LastModified = "2010/12/09")]
        public string lSummary
        {
            get { return this["lSummary"]; }
        }


        /// <summary>
        /// phrase: Categories and Tags
        /// </summary>
        [ResourceEntry("CategoriesAndTags",
            Value = "Categories and Tags",
            Description = "phrase: Categories and Tags",
            LastModified = "2010/12/09")]
        public string CategoriesAndTags
        {
            get { return this["CategoriesAndTags"]; }
        }


        /// <summary>
        /// phrase: Click to add summary
        /// </summary>
        [ResourceEntry("ClickToAddSummary",
            Value = "Click to add summary",
            Description = "phrase: Click to add summary",
            LastModified = "2010/12/09")]
        public string ClickToAddSummary
        {
            get { return this["ClickToAddSummary"]; }
        }


        /// <summary>
        /// phrase: Additional info <em class='sfNote'>(Author, Source)</em>
        /// </summary>
        [ResourceEntry("AuthorSourceThumbnail",
            Value = "Additional info <em class='sfNote'>(Author, Source)</em>",
            Description = "phrase: Additional info <em class='sfNote'>(Author, Source)</em>",
            LastModified = "2010/12/09")]
        public string AuthorSourceThumbnail
        {
            get { return this["AuthorSourceThumbnail"]; }
        }

        /// <summary>
        /// phrase: Author
        /// </summary>
        [ResourceEntry("Author",
            Value = "Author",
            Description = "phrase: Author",
            LastModified = "2010/12/09")]
        public string Author
        {
            get { return this["Author"]; }
        }

        /// <summary>
        /// phrase: Source name
        /// </summary>
        [ResourceEntry("SourceName",
            Value = "Source name",
            Description = "phrase: Source name",
            LastModified = "2010/12/09")]
        public string SourceName
        {
            get { return this["SourceName"]; }
        }

        /// <summary>
        /// phrase: More options <em class='sfNote'>(URL, Comments)</em>
        /// </summary>
        [ResourceEntry("MoreOptionsURL",
            Value = "More options <em class='sfNote'>(URL, Comments)</em>",
            Description = "phrase: More options <em class='sfNote'>(URL, Comments)</em>",
            LastModified = "2010/12/09")]
        public string MoreOptionsURL
        {
            get { return this["MoreOptionsURL"]; }
        }

        /// <summary>
        /// phrase: URL
        /// </summary>
        [ResourceEntry("UrlName",
            Value = "URL",
            Description = "phrase: URL",
            LastModified = "2010/12/09")]
        public string UrlName
        {
            get { return this["UrlName"]; }
        }

        /// <summary>
        /// phrase: URL cannot be empty
        /// </summary>
        [ResourceEntry("UrlNameCannotBeEmpty",
            Value = "URL cannot be empty",
            Description = "phrase: URL cannot be empty",
            LastModified = "2010/12/09")]
        public string UrlNameCannotBeEmpty
        {
            get { return this["UrlNameCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Single product item
        /// </summary>
        [ResourceEntry("ProductDetailViewFriendlyName",
            Value = "Single product item",
            Description = "End-user-friendly name for ProductDetailsView",
            LastModified = "2010/12/20")]
        public string ProductDetailViewFriendlyName
        {
            get { return this["ProductDetailViewFriendlyName"]; }
        }
        
        //

        /// <summary>
        /// phrase: List of product items
        /// </summary>
        [ResourceEntry("MasterListViewFriendlyName",
            Value = "List of product items",
            Description = "End-user-friendly name for MasterListView",
            LastModified = "2010/12/20")]
        public string MasterListViewFriendlyName
        {
            get { return this["MasterListViewFriendlyName"]; }
        }

        /// <summary>
        /// word: Tags
        /// </summary>
        [ResourceEntry("Tags",
            Value = "Tags",
            Description = "word: Tags",
            LastModified = "2011/02/09")]
        public string Tags
        {
            get
            {
                return this["Tags"];
            }
        }

        /// <summary>
        /// Word: Category
        /// </summary>
        [ResourceEntry("Category",
            Value = "Category",
            Description = "word",
            LastModified = "2011/02/09")]
        public string Category
        {
            get
            {
                return this["Category"];
            }
        }

        /// <summary>
        /// Version comparison screeen Title
        /// </summary>
        [ResourceEntry("VersionComparison",
            Value = "version comparison",
            Description = "word: News",
            LastModified = "2011/02/09")]
        public string VersionComparison
        {
            get { return this["VersionComparison"]; }
        }


        /// <summary>
        /// phrase: Custom Fields for news.
        /// </summary>
        [ResourceEntry("CustomFields",
            Value = "Custom Fields for products",
            Description = "phrase: Custom Fields for products",
            LastModified = "2010/01/26")]
        public string CustomFields
        {
            get { return this["CustomFields"]; }
        }

        /// <summary>
        /// phrase: Product image
        /// </summary>
        [ResourceEntry("ProductImageFieldTitle",
            Value = "Product image",
            Description = "phrase:  Product image",
            LastModified = "2011/15/06")]
        public string ProductImageFieldTitle
        {
            get { return this["ProductImageFieldTitle"]; }
        }

        /// <summary>
        /// phrase: Publish phrase
        /// </summary>
        [ResourceEntry("Publish",
            Value = "Publish",
            Description = "phrase",
            LastModified = "2012/30/01")]
        public string Publish
        {
            get { return this["Publish"]; }
        }

        /// <summary>
        /// phrase: Publish phrase
        /// </summary>
        [ResourceEntry("Unpublish",
            Value = "Unpublish",
            Description = "phrase",
            LastModified = "2012/30/01")]
        public string Unpublish
        {
            get { return this["Unpublish"]; }
        }

        /// <summary>
        /// phrase: Select products
        /// </summary>
        [ResourceEntry("SelectProduct",
        Value = "Select product",
        Description = "phrase: Select product",
        LastModified = "02/02/2012")]
        public string SelectProduct
        {
            get
            {
                return this["SelectProduct"];
            }
        }



        /// <summary>
        /// phrase: Choose Products
        /// </summary>
        [ResourceEntry("ChooseProduct",
        Value = "Choose product",
        Description = "phrase: Choose product",
        LastModified = "02/02/2012")]
        public string ChooseProduct
        {
            get
            {
                return this["ChooseProduct"];
            }
        }

        /// <summary>
        /// phrase: Which Products to display?
        /// </summary>
        [ResourceEntry("WhichProductsToDisplay",
        Value = "Which products to display?",
        Description = "phrase: Which Products to display?",
        LastModified = "02/02/2012")]
        public string WhichProductsToDisplay
        {
            get
            {
                return this["WhichProductsToDisplay"];
            }
        }

        /// <summary>
        /// phrase: All published Products
        /// </summary>
        [ResourceEntry("AllPublishedProducts",
        Value = "All published products",
        Description = "phrase: All published Products",
        LastModified = "02/02/2012")]
        public string AllPublishedProducts
        {
            get
            {
                return this["AllPublishedProducts"];
            }
        }

        /// <summary>
        /// phrase: One particular Products item only...
        /// </summary>
        [ResourceEntry("OneParticularProductsOnly",
        Value = "One particular products item only...",
        Description = "phrase: One particular Products item only...",
        LastModified = "02/02/2012")]
        public string OneParticularProductsOnly
        {
            get
            {
                return this["OneParticularProductsOnly"];
            }
        }

        /// <summary>
        /// phrase: Selection of Products:
        /// </summary>
        [ResourceEntry("SelectionOfProducts",
        Value = "Selection of products:",
        Description = "phrase: Selection of Products:",
        LastModified = "02/02/2012")]
        public string SelectionOfProducts
        {
            get
            {
                return this["SelectionOfProducts"];
            }
        }

        /// <summary>
        /// phrase: Display Products published in...
        /// </summary>
        [ResourceEntry("DisplayProductsPublishedIn",
        Value = "Display products published in...",
        Description = "phrase: Display Products published in...",
        LastModified = "02/02/2012")]
        public string DisplayProductsPublishedIn
        {

            get
            {
                return this["DisplayProductsPublishedIn"];
            }
        }

        [ResourceEntry("WidgetTitle",
        Value = "Products Widget",
        Description = "phrase: The title of the widget appearing when dropped on the page",
        LastModified = "07/20/2012")]
        public string WidgetTitle
        {

            get
            {
                return this["WidgetTitle"];
            }
        }

        [ResourceEntry("WidgetDescription",
        Value = "Show a list of products or a single product",
        Description = "phrase: The description of the widget appearing when dropped on the page",
        LastModified = "07/20/2012")]
        public string WidgetDescription
        {

            get
            {
                return this["WidgetDescription"];
            }
        }

        #endregion
    }
}
