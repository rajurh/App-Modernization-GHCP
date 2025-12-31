# .NET 9.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 9.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 9.0 upgrade.
3. Upgrade src\eShopLite.StoreFx\eShopLite.StoreFx.csproj

## Settings

This section contains settings and data used by execution steps.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                  | Current Version | New Version | Description                                    |
|:----------------------------------------------|:---------------:|:-----------:|:-----------------------------------------------|
| Antlr                                         | 3.5.0.2         | Remove      | Migrate to Antlr4 4.6.6                        |
| Antlr4                                        | -               | 4.6.6       | Replacement for Antlr                          |
| Autofac.Mvc5                                  | 6.1.0           | Remove      | No supported version, migrate to built-in DI   |
| Microsoft.AspNet.Mvc                          | 5.3.0           | Remove      | Functionality included with framework          |
| Microsoft.AspNet.Razor                        | 3.3.0           | Remove      | Functionality included with framework          |
| Microsoft.AspNet.Web.Optimization             | 1.1.3           | Remove      | No supported version, use direct file links    |
| Microsoft.AspNet.WebPages                     | 3.3.0           | Remove      | Functionality included with framework          |
| Microsoft.Bcl.AsyncInterfaces                 | 9.0.7           | 9.0.11      | Recommended for .NET 9.0                       |
| Microsoft.CodeDom.Providers.DotNetCompilerPlatform | 4.1.0      | Remove      | Functionality included with framework          |
| Microsoft.Web.Infrastructure                  | 2.0.0           | Remove      | Functionality included with framework          |
| Newtonsoft.Json                               | 13.0.3          | 13.0.4      | Recommended for .NET 9.0                       |
| System.Buffers                                | 4.6.1           | Remove      | Functionality included with framework          |
| System.Diagnostics.DiagnosticSource           | 9.0.7           | 9.0.11      | Recommended for .NET 9.0                       |
| System.Memory                                 | 4.6.3           | Remove      | Functionality included with framework          |
| System.Numerics.Vectors                       | 4.6.1           | Remove      | Functionality included with framework          |
| System.Threading.Tasks.Extensions             | 4.6.3           | Remove      | Functionality included with framework          |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### src\eShopLite.StoreFx\eShopLite.StoreFx.csproj modifications

Project properties changes:
  - Project file needs to be converted to SDK-style
  - Target framework should be changed from `net48` to `net9.0`

NuGet packages changes:
  - Antlr should be removed and replaced with Antlr4 4.6.6 (*migration to newer version*)
  - Autofac.Mvc5 should be removed (*no supported version, migrate to ASP.NET Core built-in DI*)
  - Microsoft.AspNet.Mvc should be removed (*functionality included with framework*)
  - Microsoft.AspNet.Razor should be removed (*functionality included with framework*)
  - Microsoft.AspNet.Web.Optimization should be removed (*no supported version*)
  - Microsoft.AspNet.WebPages should be removed (*functionality included with framework*)
  - Microsoft.Bcl.AsyncInterfaces should be updated from 9.0.7 to 9.0.11 (*recommended for .NET 9.0*)
  - Microsoft.CodeDom.Providers.DotNetCompilerPlatform should be removed (*functionality included with framework*)
  - Microsoft.Web.Infrastructure should be removed (*functionality included with framework*)
  - Newtonsoft.Json should be updated from 13.0.3 to 13.0.4 (*recommended for .NET 9.0*)
  - System.Buffers should be removed (*functionality included with framework*)
  - System.Diagnostics.DiagnosticSource should be updated from 9.0.7 to 9.0.11 (*recommended for .NET 9.0*)
  - System.Memory should be removed (*functionality included with framework*)
  - System.Numerics.Vectors should be removed (*functionality included with framework*)
  - System.Threading.Tasks.Extensions should be removed (*functionality included with framework*)


Feature upgrades:
  - System.Web.Optimization bundling and minification is not supported in .NET Core and should be replaced with actual html tags pointing to content files
  - Routes registration via RouteCollection is not supported in .NET Core and needs to be converted to the route mappings on the application object
  - GlobalFilterCollection is not supported in .NET Core and needs to be converted to the corresponding middleware registrations on the application object
  - Classic EntityFramework initialization needs to be adjusted in .NET Core
  - Convert from Autofac to ASP.NET Core dependency injection
  - Convert application initialization code from Global.asax.cs to .NET Core and clean up Global.asax.cs

- Update the front-end from the MVC to Blazor components
    - Take a look on the current front-end implementation
    - Recreate the components updating the design but keeping the same functionality
    - Ensure proper routing and navigation between components
    - Update any necessary services or APIs to support the new Blazor components
    - Remove any obsolete or unused code related to the old MVC front-end
    - Move scripts, images, and other assets to the new Blazor folder structure
- Transition from SQLExpress to SQLite
    - Update the database connection string in the appsettings.json file to use SQLite
    - Create a new SQLite database file and ensure it is included in the project
    - Migrate any existing data from SQLExpress to SQLite if necessary
    - Update any Entity Framework configurations to work with SQLite	
