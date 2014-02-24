<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>


<telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemContainer" AllowPaging="False" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="sfproductsDetails">
            <%--<div class="sfproductsLinksWrp">
                <sf:MasterViewHyperLink class="sfproductsBack" Text="<%$ Resources:ProductsResources, AllProducts%>" runat="server" />
            </div>--%>
            <asp:PlaceHolder ID="ItemContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <h1 class="sfproductsTitle">
            <asp:Literal ID="Literal1" Text='<%# Eval("Title") %>' runat="server" />
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

        <sf:ContentView 
             id="commentsListView" 
             ControlDefinitionName="ProductsCommentsFrontend"
             MasterViewName="CommentsMasterView" 
             ContentViewDisplayMode="Master"
             runat="server" />
         <sf:ContentView 
             id="commentsDetailsView" 
             ControlDefinitionName="ProductsCommentsFrontend" 
             DetailViewName="CommentsDetailsView"
             ContentViewDisplayMode="Detail"
             runat="server" />
    </ItemTemplate>
</telerik:RadListView>