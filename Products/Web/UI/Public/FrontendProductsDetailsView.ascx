<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.Fields" Assembly="Telerik.Sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend" TagPrefix="comments" %>


<telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemContainer" AllowPaging="False" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <layouttemplate>
        <div class="sfproductsDetails">
            </div>
            <asp:PlaceHolder ID="ItemContainer" runat="server" />
        </div>
    </layouttemplate>
    <itemtemplate>
        <h1 class="sfproductsTitle">
            <asp:Literal ID="Literal1" Text='<%# Eval("Title") %>' runat="server" />           
            <comments:CommentsCountControl runat="server"
                ThreadKey='<%# ControlUtilities.GetLocalizedKey((Guid)Eval("Id")) %>'
                NavigateUrl="#commentsWidget"
                AllowComments='<%# Eval("AllowComments") %>'
                DisplayMode="ShortText"
                ThreadType='<%# Container.DataItem.GetType().FullName %>'/>

        </h1>
        <div class="sfproductsAuthorAndDate">
            <asp:Literal ID="Literal2" Text="<%$ Resources:Labels, By %>" runat="server" /> 
            <sf:PersonProfileView runat="server" /> | <sf:FieldListView ID="PublicationDate" runat="server" 
                Format="{PublicationDate.ToLocal():MMM dd, yyyy}" />
        </div>
        <sf:ContentBrowseAndEditToolbar ID="BrowseAndEditToolbar" runat="server" Mode="Edit,Delete,Unpublish"></sf:ContentBrowseAndEditToolbar>
        <sf:FieldListView ID="summary" runat="server" Text="{0}" Properties="Summary" WrapperTagName="div" WrapperTagCssClass="sfproductsSummary"  /> 
        <div class="sfproductsContent">
            <asp:Literal ID="Literal3" Text='<%# Eval("Content") %>' runat="server" />
        </div>

        <sf:ImageField ID="predefinedImageField" runat="server" DataFieldType="Telerik.Sitefinity.Model.ContentLinks.ContentLink" 
			DisplayMode="Read" ShowDeleteImageButton="false" CssClass="sfprofileField sfprofileAvatar" DefaultSrc="~/SFRes/images/ProductCatalogSample/Images.NoProductImage.png" DataFieldName="ProductImage" />


        <sf:FieldListView ID="priceControl" runat="server" Text="Price: {0}" Properties="Price" WrapperTagName="div" WrapperTagCssClass="sfproductsSummary"  />
        <sf:FieldListView ID="quantityInStock" runat="server" Text="Quantity in stock: {0}" Properties="QuantityInStock" WrapperTagName="div" WrapperTagCssClass="sfproductsSummary"  />
        <sf:FieldListView ID="whatsInTheBox" runat="server" Text="What is in the box: {0}" Properties="WhatIsInTheBox" WrapperTagName="div" WrapperTagCssClass="sfproductsSummary"  />

         <div class='sfitemFlatTaxon sfitemTaxonWrp'>        
            <sf:SitefinityLabel runat="server" Text='Color:' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />
            <sf:FlatTaxonField runat="server" DisplayMode="Read" WebServiceUrl="~/Sitefinity/Services/Taxonomies/FlatTaxon.svc" TaxonomyId="47fc0316-0f9b-4d6d-a606-6cdc1977e10d" AllowMultipleSelection="false" 
          TaxonomyMetafieldName="Colors" Expanded="false" BindOnServer="true" WrapperTagCssClass="sfproductsSummary" />
        </div>

        <div class='sfitemFlatTaxon sfitemTaxonWrp'>        
            <sf:SitefinityLabel runat="server" Text='Tags:' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />
            <sf:FlatTaxonField runat="server" DisplayMode="Read" WebServiceUrl="~/Sitefinity/Services/Taxonomies/FlatTaxon.svc" TaxonomyId="cb0f3a19-a211-48a7-88ec-77495c0f5374" AllowMultipleSelection="true" TaxonomyMetafieldName="Tags" Expanded="false" BindOnServer="true" />
        </div>
        
        <div class='sfitemHierarchicalTaxon sfitemTaxonWrp'>        
            <sf:SitefinityLabel runat="server" Text='Categories:' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />
            <sf:HierarchicalTaxonField runat="server" DisplayMode="Read" WebServiceUrl="~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc" TaxonomyId="e5cd6d69-1543-427b-ad62-688a99f5e7d4" Expanded="false" TaxonomyMetafieldName="Category" BindOnServer="true" />
        </div>

    
<comments:CommentsWidget runat="server"
    ThreadKey='<%# ControlUtilities.GetLocalizedKey((Guid)Eval("Id")) %>'
    AllowComments='true'
    ThreadTitle='<%# Eval("Title") %>'
    ThreadType='<%# Container.DataItem.GetType().FullName %>'
    GroupKey='<%# ControlUtilities.GetUniqueProviderKey("ProductCatalogSample.Data.ProductsManager", Eval("Provider.Name").ToString()) %>' />

    </itemtemplate>
</telerik:RadListView>