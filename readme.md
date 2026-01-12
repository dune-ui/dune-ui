# DuneUI

<div align="center">
    <img src="assets/logo/dune-ui-logo.svg">
</div>

DuneUI Tag Helpers is a collection of beautifully designed components based on [shadcn/ui](https://ui.shadcn.com/) which you can use to create CRUD screens in [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) MVC and Razor Pages applications.

<div align="center">

[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/DuneUI.TagHelpers)](https://www.nuget.org/packages/DuneUI.TagHelpers/)

</div>

## Quick start

### 1. Install package

Install the `DuneUI.TagHelpers` NuGet package:

```bash
dotnet add package DuneUI.TagHelpers
```

### 2. Register services

Update your `Program.cs` (or `Startup.cs`) to register the DuneUI services.

```cs
using DuneUI;

builder.Services.AddDuneUI();
```

### 3. Update imports

Update your `_ViewImports.cshtml` to register the DuneUI Tag Helpers.

```razor
@using DuneUI.TagHelpers
@addTagHelper *, DuneUI.TagHelpers
```

### 4. (Optional) Remove 3rd party stylesheets

Using DuneUI along with 3rd party CSS libraries like Bootstrap will almost certainly result in incorrect rendering of the DuneUI components, since these libraries apply their own styling which may override the styling applied by DuneUI.

As such, we **strongly recommend** that you remove 3rd party stylesheets and only depend on the CSS styling applied by DuneUI.

### 5. Start using the Tag Helpers

Start using the DuneUI Tag Helpers inside your Razor Pages or MVC Views. For example, the code snippet below adds an alert to your page. 

```razor
<dui-alert>
    <dui-alert-title>Success! You have configured DuneUI correctly.</dui-alert-title>
</dui-alert>
```

## Documentation

Documentation and code examples for all the Tag Helpers [can be found online](https://www.duneui.com/docs/tag-helpers/components/avatar).