<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>


<ul class="sfRadioList sfTitledList">
  <li>
     <sfFields:ChoiceField ID="hidePrice" runat="server"
                         CssClass="sfInlineWrapper"
                         DataFieldName="HidePriceOnFrontend" 
                         DisplayMode="Write" 
                         RenderChoicesAs="SingleCheckBox">
     <Choices>
        <sfFields:ChoiceItem Text="Hide price on products" />
     </Choices>
    </sfFields:ChoiceField>
 </li>
</ul>
