using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.OpenAccess;
using Telerik.Sitefinity;
using Telerik.Sitefinity.ContentViewAttributes;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Versioning.Serialization.Extensions;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.Sitefinity.Workflow.Model.Tracking;
using System;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model.ContentLinks;

namespace ProductCatalogSample.Model
{
    /// <summary>
    /// Product item model
    /// </summary>
    [DataContract(Namespace = "http://sitefinity.com/samples/productcatalogue", Name = "ProductItem")]
    [ManagerType("ProductCatalogSample.Data.ProductsManager, ProductCatalogSample")]
    [Persistent(IdentityField = "contentId")]
	public class ProductItem : Content, IApprovalWorkflowItem, ISecuredObject, ILocatable, ISitefinityCustomTypeSerialization, ILifecycleDataItemGeneric
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductItem" /> class.
        /// </summary>
        public ProductItem()
        {
            // set default values
            this.inheritsPermissions = true;
            this.canInheritPermissions = true;
            this.supportedPermissionSets = new string[] { ProductsConstants.Security.PermissionSetName };
        }

        /// <summary>
        /// Static initializer
        /// </summary>
        static ProductItem()
        {
            // set default values
            permissionsetObjectTitleResKeys = new Dictionary<string, string>() 
            {
                { ProductsConstants.Security.PermissionSetName, "NewsActionPermissionsListTitle" }
            };
        }

        #endregion

        #region Own properties

        /// <summary>
        /// Gets or sets product price
        /// </summary>
        [DataMember]
        [FieldAlias("price")]
        public decimal Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
            }
        }

        /// <summary>
        /// Gets or sets the Quantity in stock
        /// </summary>
        [DataMember]
        [FieldAlias("quantityInStock")]
        public int QuantityInStock
        {
            get
            {
                return this.quantityInStock;
            }

            set
            {
                this.quantityInStock = value;
            }
        }

        /// <summary>
        /// Description of the product's contents
        /// </summary>
        [DataMember]
        [MetadataMapping(true, false)]
        public virtual Lstring WhatIsInTheBox
        {
            get
            {
                if (this.whatIsInTheBox == null)
                {
                    this.whatIsInTheBox = this.GetString("WhatIsInTheBox");
                }

                return this.whatIsInTheBox;
            }

            set
            {
                this.whatIsInTheBox = value;
                this.SetString("WhatIsInTheBox", this.whatIsInTheBox);
            }
        }

        /// <summary>
        /// Contents of the product item
        /// </summary>        
        [DataMember]
        [MetadataMapping(true, true)]
        [UserFriendlyDataType(UserFriendlyDataType.LongText)]
        [CommonProperty]
        [ScaffoldInfo(@"<sitefinity:HtmlField runat=""server"" DisplayMode=""Read"" Value='<%# Eval(""Content"")%>' />")]
        public Lstring Content
        {
            get
            {
                if (this.content == null)
                {
                    this.content = this.GetString("Content");
                }

                return this.ApplyContentFilters(this.content);
            }

            set
            {
                this.content = value;
                this.SetString("Content", this.content);
            }
        }

        #endregion

        #region Sitefinity infrastucture

        #region IApprovalWorkflowItem members

        /// <summary>
        /// Gets or sets the approval tracking records
        /// </summary>
        [FieldAlias("approvalTrackingRecordMap")]
        [NonSerializableProperty]
        public ApprovalTrackingRecordMap ApprovalTrackingRecordMap
        {
            get
            {
                return this.approvalTrackingRecordMap;
            }

            set
            {
                this.approvalTrackingRecordMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the current state of the item in the approval workflow.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Note that item can be in different states depending on the culture.
        /// </remarks>
        [DataMember]
        [Database(DBType = "VARCHAR", DBSqlType = "NVARCHAR")]
        public virtual Lstring ApprovalWorkflowState
        {
            get
            {
                if (this.approvalWorkflowState == null)
                {
                    this.approvalWorkflowState = this.GetString("ApprovalWorkflowState");
                }

                return this.approvalWorkflowState;
            }

            set
            {
                this.approvalWorkflowState = value;
                this.SetString("ApprovalWorkflowState", this.approvalWorkflowState);
            }
        }

        #endregion

        #region ILocatable members

        /// <summary>
        /// Gets a value indicating whether to auto generate an unique URL.
        /// </summary>
        /// <value>
        /// <c>true</c> if to auto generate an unique URL otherwise, <c>false</c>.
        /// </value>
        [NonSerializableProperty]
        public bool AutoGenerateUniqueUrl
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a collection of URL data for this item.
        /// </summary>
        /// <value>The collection of URL data.</value>
        [FieldAlias("urls")]
        [NonSerializableProperty]
        public virtual IList<ProductItemUrlData> Urls
        {
            get
            {
                if (this.urls == null)
                {
                    this.urls = new ProviderTrackedList<ProductItemUrlData>(this, "Urls");
                }

                this.urls.SetCollectionParent(this);
                return this.urls;
            }
        }

        /// <summary>
        /// Gets a collection of URL data for this item.
        /// </summary>
        /// <value>The collection of URL data.</value>
        [NonSerializableProperty]
        IEnumerable<UrlData> ILocatable.Urls
        {
            get
            {
                return this.Urls.Cast<UrlData>();
            }
        }

        #endregion

        #region ISitefinityCustomTypeSerialization members

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="serializedObject">The serialized object.</param>
        public void Serialize(object obj, out Dictionary<string, object> serializedObject)
        {
            
            var unfilteredObjects = obj.GetSerializationPropertyValueCollection();
            serializedObject = new Dictionary<string, object>(unfilteredObjects.Count);

            //Removing of ContentLink objects is neccesary since we cannot handle deserialziation of this type
            //ContentLinks are persisted outside the ProductItem and deserialization causes side effects
            foreach (var item in unfilteredObjects)
            {
                if (!(item.Value is ContentLink))
                {
                    serializedObject.Add(item.Key, item.Value);
                }
            }

            if (Urls.Count > 0)
            {
                var urls = obj.GetListSerializationItemsItems("Urls");
                serializedObject.Add("URLS", urls);
            }
        }

        /// <summary>
        /// Deserializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="serializedObject">The serialized object.</param>
        public void Deserialize(ref object obj, Dictionary<string, object> serializedObject)
        {
            VersioningUtilities.SetSerializationPropertyValueCollection(ref obj, serializedObject);

            ((ProductItem)obj).Urls.Clear();
            if (serializedObject.ContainsKey("URLS"))
            {
                VersioningUtilities.SetListDeserializedItems(ref obj, "Urls", "Parent", this, serializedObject["URLS"]);
            }
        }

        #endregion

        #region ISecuredObject

        /// <summary>
        /// Gets or sets a value indicating whether this instance can inherit permissions.
        /// </summary>
        /// <value></value>
        [FieldAlias("canInheritPermissions")]
        [NonSerializableProperty]
        public bool CanInheritPermissions
        {
            get
            {
                return this.canInheritPermissions;
            }

            set
            {
                this.canInheritPermissions = value;
            }
        }

        /// <summary>
        /// Indicates if this profile inherits permissions
        /// </summary>
        [FieldAlias("inheritsPermissions")]
        [NonSerializableProperty]
        public bool InheritsPermissions
        {
            get
            {
                return this.inheritsPermissions;
            }

            set
            {
                this.inheritsPermissions = value;
            }
        }

        /// <summary>
        /// Get a set of permissions for the profile, as a secured object (as IList)
        /// </summary>
        [FieldAlias("permissions")]
        [NonSerializableProperty]
        public IList<Permission> Permissions
        {
            get
            {
                if (this.permissions == null)
                {
                    this.permissions = new ProviderTrackedList<Permission>(this, "Permissions");
                }

                this.permissions.SetCollectionParent(this);
                return this.permissions;
            }
        }

        /// <summary>
        /// Gets a dictionary:
        /// Key is a name of a permission set supported by this provider,
        /// Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders.
        /// </summary>
        /// <value>The permission set object titles.</value>
        [NonSerializableProperty]
        public virtual IDictionary<string, string> PermissionsetObjectTitleResKeys
        {
            get
            {
                return permissionsetObjectTitleResKeys;
            }

            set
            {
                permissionsetObjectTitleResKeys = value;
            }
        }

        /// <summary>
        /// Gets the permission sets relevant to this specific secured object.
        /// </summary>
        /// <value>The supported permission sets.</value>
        [NonSerializableProperty]
        public string[] SupportedPermissionSets
        {
            get
            {
                return supportedPermissionSets;
            }

            set
            {
                supportedPermissionSets = value;
            }
        }

        #endregion

        #endregion

		#region ILifecycleDataItem Members

		/// <summary>
		/// Gets a list of available translations (set for the Master and Live items).
		/// </summary>
		[Searchable(false)]
		[NonSerializableProperty]
		public virtual IList<string> PublishedTranslations
		{
			get
			{
				if (this.publishedTranslations == null)
					this.publishedTranslations = new TrackedList<string>();
				return this.publishedTranslations;
			}
		}

		/// <summary>
		/// Collection of culture specific data - publication date, scheduled date
		/// </summary>
		[Searchable(false)]
		[NonSerializableProperty]
		public IList<LanguageData> LanguageData
		{
			get
			{
				if (this.languageData == null)
					this.languageData = new TrackedList<LanguageData>();
				return this.languageData;
			}
		}
		#endregion

        #region Fields

        private decimal price;
        private int quantityInStock;
        [Transient]
        private Lstring whatIsInTheBox;

        #region Sitefinity infrastructure

        // persistent fields
        private ApprovalTrackingRecordMap approvalTrackingRecordMap;
        [Depend]
        private ProviderTrackedList<ProductItemUrlData> urls;
        private bool canInheritPermissions;
        private static IDictionary<string, string> permissionsetObjectTitleResKeys;
        private ProviderTrackedList<Permission> permissions;
        private bool inheritsPermissions;
        private IList<PermissionsInheritanceMap> permissionChildren;

        // transient fields
        [Transient]
        private Lstring content;
        [Transient]
        private Lstring approvalWorkflowState;
        [Transient]
        private string[] supportedPermissionSets;

        #endregion

		private TrackedList<string> publishedTranslations;
		private TrackedList<LanguageData> languageData;

        #endregion

        /// <summary>
        /// Clears the Urls collection for this item.
        /// </summary>
        /// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
        void ILocatable.ClearUrls(bool excludeDefault = false)
        {
            this.urls.ClearUrls(excludeDefault);
        }

        /// <summary>
        /// Removes all urls that satisfy the condition that is checked in the predicate function.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        void ILocatable.RemoveUrls(Func<UrlData, bool> predicate)
        {
            this.urls.RemoveUrls(predicate);
        }
    }
}
