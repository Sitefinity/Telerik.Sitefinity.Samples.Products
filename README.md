Progress.Sitefinity.Samples.Products
===================================

### Disclaimer
### The purpose of this sample is to show the creation of a Sitefinity content module. The content module similar to the built-in News module in Sitefinity. We highly recommend using [Module Builder](https://docs.sitefinity.com/overview-dynamic-modules-and-the-module-builder) to create modules that manage content. The built-in module builder can address all of the needs for managing content and updates will be provided to it with each Sitefinity release and is the best way to go in terms of managing content in the long run. To move the configurations for module builder modules between different deployment environments refer to [this](https://docs.sitefinity.com/export-and-deploy-code-changes#procedure) documentation or to setup continuous delivery refer to [this](https://docs.sitefinity.com/setup-the-continuous-delivery-process) guide.
### The sample presented here can be used as a reference to how Sitefinity content modules are created, but for actual use of content module it is best to use Module Builder module.

### This repository is not automatically upgraded to latest Sitefintiy version. The repository is monitored for pull requests and fixes. The latest official version of Sitefinity that supports this sample is 9.1. Be aware that using a higher version could cause unexpected behavior. If you successfully upgrade the example to a greater version, please share your work with the community by submitting your changes via pull request.

[![Build Status](http://sdk-jenkins-ci.cloudapp.net/buildStatus/icon?job=Telerik.Sitefinity.Samples.Products.CI)](http://sdk-jenkins-ci.cloudapp.net/job/Telerik.Sitefinity.Samples.Products.CI/)

The Products sample project is a module that provides product listings built on top of a native content-based module in Sitefinity CMS. Content-based modules themselves are based on _Generic Content_ and reuse much of the functionality available for generic content. 

The Products sample demonstrates comprehensively the features of a content-based module that has all built-in functionality of a native Sitefinity CMS module. 

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

* Sitefinity CMS license
* .NET Framework 4.5
* Visual Studio 2012
* Microsoft SQL Server 2008R2 or later versions


### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.

### Nuget package restoration
The solution in this repository relies on NuGet packages with automatic package restore while the build procedure takes place.   
For a full list of the referenced packages and their versions see the [packages.config](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Products/blob/master/SitefinityWebApp/packages.config) file.    
For a history and additional information related to package versions on different releases of this repository, see the [Releases page](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Products/releases).    


### Installation instructions: SDK Samples from GitHub

1. In Solution Explorer, navigate to _SitefinityWebApp_ » *App_Data* » _Sitefinity_ » _Configuration_ and select the **StartupConfig.config** file. 
2. Modify the **connectionString** value to match your server address.

For version-specific details about the required Sitefinity CMS NuGet packages for this sample application, click on [Releases](https://github.com/Sitefinity-SDK/Telerik.Sitefinity.Samples.Products/releases).


### Login

To login into the Sitefinity CMS backend, use the following credentials:  
**Username:** admin   
**Password:** password

### Additional resources
Progress Sitefinity CMS documentation  
* [Develop: Use and extend Sitefinity CMS functionality](http://docs.sitefinity.com/develop-create-and-manage-website-content) 
* [Tutorial: Create a custom Products module](http://docs.sitefinity.com/tutorial-create-a-custom-products-module)
