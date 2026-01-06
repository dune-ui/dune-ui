# DuneUI 

DuneUI Tag Helpers is a collection of beautifully designed components based on [shadcn/ui](https://ui.shadcn.com/) which you can use to create CRUD screens in [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) MVC and Razor Pages applications.

<div style="text-align:center;">
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
@addTagHelper *, DuneUI.TagHelpers
```

### 4. Include stylesheet

In your `_Layout.cshtml` file (or whichever layout file you use), include the DuneUI stylesheet in the `head` section.

```razor
<head>
    <!-- ... -->
    <link rel="stylesheet" href="/_content/DuneUI.TagHelpers/dune-ui.css"/>
</head>
```

## Documentation

Complete documentation can be [found online](https://www.duneui.com/docs/tag-helpers) along with previews and code examples of all the included components.
