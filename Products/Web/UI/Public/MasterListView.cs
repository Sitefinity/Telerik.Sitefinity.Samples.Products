using ProductCatalogSample.Data;
using ProductCatalogSample.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Comments;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views;

namespace ProductCatalogSample.Web.UI.Public
{
    /// <summary>
    /// Represents master view that displays a collection content items as list.
    /// </summary>
    [ControlTemplateInfo("ProductsResources", "MasterListViewFriendlyName", "ModuleTitle")]
    public class MasterListView : MasterViewBase
    {
        #region Properties
        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
        protected override string LayoutTemplateName
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the layout template path.
        /// </summary>
        /// <value>The layout template path.</value>
        public override string LayoutTemplatePath
        {
            get
            {
                return "~/SFRes/" + titlesDatesLayoutTemplateName;
            }

            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        protected ProductsManager Manager
        {
            get
            {
                if (this.manager == null)
                    this.manager = ProductsManager.GetManager(this.Host.ControlDefinition.ProviderName);

                return this.manager;
            }
        }

        #endregion

        #region Control References

        /// <summary>
        /// Gets the repeater for news list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView NewsList
        {
            get
            {
                return this.Container.GetControl<RadListView>("NewsList", true);
            }
        }

        /// <summary>
        /// Gets the pager.
        /// </summary>
        /// <value>The pager.</value>
        protected internal virtual Pager Pager
        {
            get
            {
                return this.Container.GetControl<Pager>("pager", true);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The controls container.</param>
        /// <param name="definition">The content view definition.</param>
        protected override void InitializeControls(GenericContainer container, IContentViewDefinition definition)
        {
            var masterDefinition = definition as IContentViewMasterDefinition;
            if (masterDefinition != null)
            {
                var query = this.Manager.GetProducts();
                if (masterDefinition.AllowUrlQueries.HasValue && masterDefinition.AllowUrlQueries.Value)
                {
                    query = this.EvaluateUrl(query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                    query = this.EvaluateUrl(query, "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                    query = this.EvaluateUrl(query, "Taxonomy", string.Empty, typeof(ProductItem), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                }

                int? totalCount = 0;
                int? itemsToSkip = 0;
                if (masterDefinition.AllowPaging.HasValue && masterDefinition.AllowPaging.Value)
                {
                    itemsToSkip = this.GetItemsToSkipCount(masterDefinition.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                }

                CultureInfo uiCulture = null;
                if (AppSettings.CurrentSettings.Multilingual)
                {
                    uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
                }
                //the filter is adapted to the implementation of ILifecycleDataItemGeneric, so the culture is taken in advance when filtering published items.
                this.FilterExpression = ContentHelper.AdaptMultilingualFilterExpression(this.FilterExpression);
                var filterExpression = DefinitionsHelper.GetFilterExpression(this.FilterExpression, this.AdditionalFilter);
                query = Telerik.Sitefinity.Data.DataProviderBase.SetExpressions(
                    query,
                    filterExpression,
                    masterDefinition.SortExpression,
                    uiCulture,
                    itemsToSkip,
                    masterDefinition.ItemsPerPage,
                    ref totalCount);

                this.IsEmptyView = (totalCount == 0);

                if (totalCount == 0)
                {
                    this.NewsList.Visible = false;
                }
                else
                {
                    this.ConfigurePager(totalCount.Value, masterDefinition);
                    this.NewsList.DataSource = query.ToList();
                    this.NewsList.PreRender += new EventHandler(NewsList_PreRender);
                }
            }
        }

        /// <summary>
        /// Configures the pager.
        /// </summary>
        /// <param name="vrtualItemCount">The vrtual item count.</param>
        /// <param name="masterDefinition">The master definition.</param>
        protected virtual void ConfigurePager(int vrtualItemCount, IContentViewMasterDefinition masterDefinition)
        {
            if (masterDefinition.AllowPaging.HasValue &&
                masterDefinition.AllowPaging.Value &&
                masterDefinition.ItemsPerPage.GetValueOrDefault() > 0)
            {
                this.Pager.VirtualItemCount = vrtualItemCount;
                this.Pager.PageSize = masterDefinition.ItemsPerPage.Value;
                this.Pager.QueryParamKey = this.Host.UrlKeyPrefix;
            }
            else
            {
                this.Pager.Visible = false;
            }
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the PreRender event of the EventsList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewsList_PreRender(object sender, System.EventArgs e)
        {
            string commentsControlName = "itemCommentsLink";
            //// In ItemDataBound NavigateUrl property of the link is still not set. That is the reason why this logic is implemented in PreRender.
            foreach (var item in this.NewsList.Items)
            {
                if (item.ItemType == RadListViewItemType.DataItem || item.ItemType == RadListViewItemType.AlternatingItem)
                {
                    var itemCommentsLink = item.FindControl("itemCommentsLink") as CommentsBox;
                    if (itemCommentsLink != null)
                    {
                        var dataItem = item.DataItem as Telerik.Sitefinity.GenericContent.Model.Content;
                        if (dataItem != null)
                        {
                            var query = this.GetCommentsQuery(dataItem);
                            var commentsCount = query.Count();
                            itemCommentsLink.CommentsCount = commentsCount;
                        }

                        var commentsControl = item.FindControl(commentsControlName) as CommentsBox;
                        if (commentsControl != null)
                        {
                            var allowComments = dataItem.AllowComments ?? false;
                            if (!allowComments)
                            {
                                commentsControl.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Helper methods

        private IQueryable<Comment> GetCommentsQuery(Telerik.Sitefinity.GenericContent.Model.Content dataItem)
        {
            var id = dataItem.Id;
            IQueryable<Comment> query = null;
            var commentsSettings = new CommentsSettingsWrapper(dataItem, this.MasterViewDefinition.CommentsSettingsDefinition);
            if ((bool)commentsSettings.HideCommentsAfterNumberOfDays)
            {
                var numberOfDaysToHideComments = (int)commentsSettings.NumberOfDaysToHideComments;
                var duration = new TimeSpan(numberOfDaysToHideComments, 0, 0, 0);
                query = this.Manager.GetComments().Where<Comment>(c => c.CommentedItemID == id &&
                                                                       c.CommentStatus == CommentStatus.Published &&
                                                                       c.DateCreated > DateTime.UtcNow.Subtract(duration));
            }
            else
            {
                query = this.Manager.GetComments().Where<Comment>(c => c.CommentedItemID == id && c.CommentStatus == CommentStatus.Published);
            }

            return query;
        }

        #endregion

        #region Private fields

        private ProductsManager manager;
        internal const string titlesOnlyLayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.News.TitlesOnlyListView.ascx";
        internal const string titlesDatesLayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.News.TitlesDatesListView.ascx";
        internal const string titlesDatesSummariesLayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.News.TitlesDatesSummariesListView.ascx";
        internal const string titlesDatesContentsLayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.News.TitlesDatesContentsListView.ascx";
        #endregion
    }
}
