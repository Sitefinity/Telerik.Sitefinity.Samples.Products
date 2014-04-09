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


### Installation instructions: SDK Samples from GitHub



1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.

The project refers to the following NuGet packages:

**ProductsCatalogSample** library

* OpenAccess.Core.nupkg

* Telerik.Sitefinity.Core

* Telerik.Sitefinity.Content

* Telerik.Web.UI

* SitefinityWebApp

* Telerik.Sitefinity.All

* Telerik.Sitefinity.Samples.Common

**Telerik.Sitefinity.Core** library

* OpenAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg





### Integrate the OpenAccess enhancer

Sitefinity ships with OpenAccess ORM. To use OpenAccess in the data provider of a module, you must integrate the OpenAccess enhancer. To do this:

1. From the context menu of the project **Products**, click _Unload Project_.
2. From the context menu of the unloaded project, click _Edit ProductCatalogSample.csproj_.
3. Find the **<ProjectExtensions>** tag and replace it with the following lines of code: 
```xml
<ProjectExtensions>
  <VisualStudio>
    <UserProperties OpenAccess_EnhancementOutputLevel="1" OpenAccess_UpdateDatabase="False" OpenAccess_Enhancing="False" OpenAccess_ConnectionId="DatabaseConnection1" OpenAccess_ConfigFile="App.config" />
  </VisualStudio>
</ProjectExtensions>
<PropertyGroup>
  <OpenAccessPath>C:\GitHub\Telerik.Sitefinity.Samples.Dependencies</OpenAccessPath>
</PropertyGroup>
<Import Condition="Exists('$(OpenAccessPath)\OpenAccess.targets')" Project="$(OpenAccessPath)\OpenAccess.targets" />
```

4. In the **OpenAccessPath** element, place the path to the folder containing the **OpenAccess.targets** file. 

    This is the location of the **Telerik.Sitefinity.Samples.Dependencies** repository that you cloned locally. In the example above, the repository is cloned in **C:\GitHub**.
    
5. Save the changes.
6. From the context menu of the project, click _Reload Project_.

    **NOTE**: If you are using Sitefinity 4.0 SP1 or prior, see [Custom Modules for Sitefinity 4.0](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/creating-custom-modules/custom-modules-for-sitefinity-4-0).
7. Build the solution.

### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Developers Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)

[Create Porduct module](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/creating-custom-modules/creating-products-module)
