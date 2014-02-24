<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>

<telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemContainer" AllowPaging="False" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate> 
        <div class="sfnewsDetails">
            <div class="sfnewsLinksWrp">
                <sf:MasterViewHyperLink class="sfnewsBack" Text="<%$ Resources:NewsResources, AllNews %>" runat="server" />
            </div>
            <asp:PlaceHolder ID="ItemContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <h1 class="sfnewsTitle">
            <asp:Literal Text='<%# Eval("Title") %>' runat="server" />
        </h1>
        <div class="sfnewsAuthorAndDate">
            <asp:Literal Text="<%$ Resources:Labels, By %>" runat="server" /> 
            <sf:PersonProfileView runat="server" /> | <sf:FieldListView ID="PublicationDate" runat="server" 
                Format="{PublicationDate.ToLocal():MMM dd, yyyy}" />
        </div>
        <sf:ContentBrowseAndEditToolbar ID="BrowseAndEditToolbar" runat="server" Mode="Edit,Delete,Unpublish"></sf:ContentBrowseAndEditToolbar>
        <sf:FieldListView ID="summary" runat="server" Text="{0}" Properties="Summary" WrapperTagName="div" WrapperTagCssClass="sfnewsSummary"  /> 
        <div class="sfnewsContent">
            <asp:Literal ID="Literal1" Text='<%# Eval("Content") %>' runat="server" />
        </div>

        <sf:FieldListView ID="priceControl" runat="server" Text="Price: {0}" Properties="Price" WrapperTagName="div" WrapperTagCssClass="sfnewsSummary"  />
        <sf:FieldListView ID="quantityInStock" runat="server" Text="Quantity in stock: {0}" Properties="QuantityInStock" WrapperTagName="div" WrapperTagCssClass="sfnewsSummary"  />
        <sf:FieldListView ID="whatsInTheBox" runat="server" Text="What is in the box: {0}" Properties="WhatIsInTheBox" WrapperTagName="div" WrapperTagCssClass="sfnewsSummary"  />
    </ItemTemplate>
</telerik:RadListView>