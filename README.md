Telerik.Sitefinity.Samples.Products
===================================

The Products sample project is a module that provides product listings built on top of a native content-based module in Sitefinity. Content-based modules themselves are based on _Generic Content_ and reuse much of the functionality available for generic content. 

The Products sample demonstrates comprehensively the features of a content-based module that has all built-in functionality of a native Sitefinity module. 

Using the Products sample, you can:

* Set up the solution 
* Build the model 
* Implement the data access layer 
* Implement the module class 
* Implement services 
* Implement module configuration 
* Provide localizable resources 
* Create the UI of ContentView 
* Creat Widget definitions  

### Requirements

* Sitefinity 6.3 license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions


### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.

### Installation instructions: SDK Samples from GitHub



1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.

The project refers to the following NuGet packages:

**ProductsCatalogSample** library

* OpenAccess.Core.nupkg

* OpenAccess.CodeFirst.nupkg

* Telerik.Sitefinity.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg



**SitefinityWebApp** library

* Telerik.Sitefinity.All.nupkg


**Telerik.Sitefinity.Samples.Common** library

* Telerik.Sitefinity.Core

* OpenAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg


You can find the packages in the official [Sitefinity Nuget Server](http://nuget.sitefinity.com).




### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Developers Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)

[Create Porduct module](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/creating-custom-modules/creating-products-module)
