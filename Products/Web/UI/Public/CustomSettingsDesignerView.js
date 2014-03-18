/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.4.2-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("CustomSettingsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("ProductCatalogSample.Web.UI.Public");


ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView = function (element) {
    ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.initializeBase(this, [element]);
    this._controldataFieldNameMap = {};
    this._parentDesigner = null;
    this._refreshing = false;
    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;
    this._hidePriceControlId = null;
    this._hidePriceControlDataFieldName = null;
}

ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.callBaseMethod(this, 'initialize');

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        if (this._onUnloadDelegate == null) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        }
        Sys.Application.add_load(this._onLoadDelegate);
        Sys.Application.add_unload(this._onUnloadDelegate);

        // prevent memory leaks for jQuery
        $(this).unload(function () {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        //Add custom dispose actions here
        ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.callBaseMethod(this, 'dispose');
        if (this._valueUpdatedDelegate) {
            delete this._valueUpdatedDelegate;
        }
        if (this._templateValueChangedDelegate) {
            delete this._templateValueChangedDelegate;
        }
        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }
        if (this._onUnloadDelegate) {
            delete this._onUnloadDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    refreshUI: function () {
        this._refreshing = true;
        var control = this.get_controlData();
        var field = $find(this._hidePriceControlId);
        field.set_value(control.HidePrices);

        this._refreshing = false;
    },

    applyChanges: function () {
        var control = this.get_controlData();
        var field = $find(this._hidePriceControlId);
        control.HidePrices = field.get_value();

    },

    /* --------------------------------- private methods --------------------------------- */

    // this method is executed when the page loads
    _onLoad: function () {

    },

    _onUnload: function () {

    },


    get_hidePriceControlId: function () {
        return this._hidePriceControlId;
    },

    set_hidePriceControlId: function (value) {
        this._hidePriceControlId = value;
    },

    get_hidePriceControlDataFieldName: function () {
        return this._hidePriceControlDataFieldName;
    },

    set_hidePriceControlDataFieldName: function (value) {
        this._hidePriceControlDataFieldName = value;
    },

    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },


    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        var currentViewName = this.get_currentViewName();
        var data = this.get_controlData();
        var views = data.ControlDefinition.Views;
        if (views.hasOwnProperty(currentViewName)) {
            return views[currentViewName];
        }
        else {
            var views = data.ControlDefinition.Views;
            for (var v in views) {
                var current = views[v];
                if (current.IsMasterView) {
                    return current;
                }
            }
            return null;
        }
    },

    // this fixes the data if there are some incompatible values set in advanced mode
    _adjustControlData: function (data) {
        var view = data.ControlDefinition.Views[this.get_currentViewName()];
        if (!view) {
            var views = data.ControlDefinition.Views;
            var viewName;
            for (var key in views) {
                if (views[key].IsMasterView) {
                    viewName = key;
                    break;
                }
            }
            data.MasterViewName = viewName;
        }
    },

    _resolvePropertyPath: function (fieldControl) {
        var dataFieldName = this._hidePriceControlDataFieldName;
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        return viewPath;
    },

    // gets the object that represents the client side representation of the control 
    // being edited
    get_controlData: function () {
        var parent = this.get_parentDesigner();
        if (parent) {
            var pe = parent.get_propertyEditor();
            if (pe) {
                return pe.get_control();
            }
        }
        alert('Control designer cannot find the control properties object!');
    }

}
ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView.registerClass('ProductCatalogSample.Web.UI.Public.CustomSettingsDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();