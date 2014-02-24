namespace ProductCatalogSample.Model
{
    /// <summary>
    /// Various constants related to Products
    /// </summary>
    public static class ProductsConstants
    {
        /// <summary>
        /// Security-Related constants related to Products
        /// </summary>
        public static class Security
        {
            /// <summary>
            /// The main permission set name related to Products
            /// </summary>
            public const string PermissionSetName = "Products";

            /// <summary>
            /// View action name
            /// </summary>
            public const string View = "ViewProducts";

            /// <summary>
            /// Modify Products action name
            /// </summary>
            public const string Modify = "ModifyProducts";

            /// <summary>
            /// Create Products action name
            /// </summary>
            public const string Create = "CreateProducts";

            /// <summary>
            /// Delete Products action name
            /// </summary>
            public const string Delete = "DeleteProducts";

            /// <summary>
            /// ChangeOwner Products action name
            /// </summary>
            public const string ChangeOwner = "ChangeProductsOwner";

            /// <summary>
            /// ChangePermissions Products action name
            /// </summary>
            public const string ChangePermissions = "ChangeProductsPermissions";
        }
    }
}
