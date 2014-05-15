Telerik.Sitefinity.Samples.Products
===================================

[![Build Status](http://sdk-jenkins-ci.cloudapp.net/buildStatus/icon?job=Telerik.Sitefinity.Samples.Products.CI)](http://sdk-jenkins-ci.cloudapp.net/job/Telerik.Sitefinity.Samples.Products.CI/)

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

* Sitefinity license

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

For version-specific details about the required Sitefinity NuGet packages for this sample application, click on [Releases]
 (https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Products/releases).


### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Developers Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)

[Create Porduct module](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/creating-custom-modules/creating-products-module)
