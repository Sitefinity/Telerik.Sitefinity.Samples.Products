<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sfFields" %>


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
