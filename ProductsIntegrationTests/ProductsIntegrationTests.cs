using MbUnit.Framework;
using ProductCatalogSample.Data;
using ProductCatalogSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Workflow;

namespace ProductsIntegrationTests
{
    [TestFixture]
    [Description("Integration tests for the Products catalog custom module.")]
    public class ProductsIntegrationTests
    {
        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a Product item")]
        [Author("SDK")]
        public void CreateProduct()
        {
            ProductsManager productsManager = ProductsManager.GetManager();

            var productItem = productsManager.CreateProduct(ProductsIntegrationTests.productItemId);
            productItem.Title = "Test Product";
            productItem.Content = "<h1>Simple content goes here ...</h1>";

            var taxManager = TaxonomyManager.GetManager();

            AddTaxaToProduct(productItem, taxManager, "Colors", ProductsIntegrationTests.colorName);

            productItem.WhatIsInTheBox = "Summary goes here...";
            productItem.QuantityInStock = 1;
            productItem.Price = 10;

            AddImageToProductItem(productItem);

            productsManager.SaveChanges();
            var contextBag = new Dictionary<string, string>();
            contextBag.Add("ContentType", productItem.GetType().FullName);

            string workflowOperation = "Publish";

            WorkflowManager.MessageWorkflow(
                                            productItem.Id,
                                            productItem.GetType(),
                                            "OpenAccessDataProvider",
                                            workflowOperation,
                                            false,
                                            contextBag);

            var product = productsManager.GetProduct(ProductsIntegrationTests.productItemId);

            Assert.IsNotNull(product);
            Assert.IsNotNull(product.GetValue("Colors"));
        }

        #region Helper methods

        private static void AddTaxaToProduct(ProductItem productItem, TaxonomyManager taxManager, string taxaName, string taxonName)
        {
            var taxon = taxManager.GetTaxa<FlatTaxon>().SingleOrDefault(t => t.Name == taxonName);

            // Check if a tag with the same name is already added
            var tagExists = productItem.Organizer.TaxonExists(taxaName, taxon.Id);

            if (!tagExists)
            {
                // Add the tag and save the changes
                productItem.Organizer.AddTaxa(taxaName, taxon.Id);
            }
        }

        private void AddImageToProductItem(ProductItem product)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            if (product == null)
            {
                return;        // Product does not exist
            }

            var defaultAlbum = librariesManager.GetAlbums().FirstOrDefault(a => a.Id == LibrariesModule.DefaultImagesLibraryId);

            Telerik.Sitefinity.Libraries.Model.Image image = UploadImage(librariesManager, "imageTitle1", "imageTitle1", "Some description", 0, Guid.Empty, defaultAlbum);

            product.CreateRelation(image, "ProductImage");
        }

        private Image UploadImage(LibrariesManager manager, string iTitle, string iUrlName, string iDescription, long iTotalSize, Guid iOwner, Album album)
        {
            var mediaItem = manager.CreateImage(ProductsIntegrationTests.imageId);
            mediaItem.Title = iTitle;
            mediaItem.UrlName = iUrlName;
            mediaItem.Description = iDescription;
            mediaItem.Parent = album;
            manager.RecompileItemUrls<Image>(mediaItem);

            UploadImageFile(manager, mediaItem);
            iTotalSize = mediaItem.TotalSize;
            iOwner = mediaItem.Owner;

            manager.SaveChanges();

            return mediaItem;
        }

        private void UploadImageFile(LibrariesManager libManager, MediaContent mediaItem)
        {
            var imageResource = "ProductsIntegrationTests.Images.1.jpg";
            var assembly = this.GetType().Assembly;
            var stream = assembly.GetManifestResourceStream(imageResource);

            libManager.Upload(mediaItem, stream, ".jpg");
        }

        #endregion

        #region TearDown

        [TearDown]
        public void TearDown()
        {
            ProductsManager productsManager = ProductsManager.GetManager();

            var productItem = productsManager.GetProducts()
                .FirstOrDefault(p => p.Id == ProductsIntegrationTests.productItemId);

            if (productItem != null)
            {
                ProductsIntegrationTests.DeleteProduct(productItem, productsManager);
                ProductsIntegrationTests.DeleteImage();
            }
        }

        private static void DeleteProduct(ProductItem product, ProductsManager productsManager)
        {
            productsManager.DeleteProduct(product);
            productsManager.SaveChanges();
        }

        private static void DeleteImage()
        {
            LibrariesManager manager = LibrariesManager.GetManager();

            var imageToDelete = manager.GetImages().FirstOrDefault(i => i.Id == ProductsIntegrationTests.imageId);

            if (imageToDelete != null)
            {
                manager.DeleteImage(imageToDelete);
                manager.SaveChanges();
            }
        }

        #endregion

        #region Private members and constants

        public static readonly Guid productItemId = Guid.Parse("49C0F775-EC9F-4DA1-B89B-4A1FAC50F540");
        public static readonly Guid imageId = Guid.Parse("C3266584-34EC-41C0-A213-0B821C7591A7");

        public static readonly string taxonName = "taxa1";
        public static readonly string colorName = "Blue";

        #endregion
    }
}
