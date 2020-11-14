# Blazor-WebAssembly-Sample
This repo contains a Blazor WebAssembly application that calls a Web API backend to send an email using SendGrid.

The solution contains 3 projects:
* ```SiteWebAssembly.csproj```: The Blazor client application
* ```SiteWebAssemply.Api.csproj```: The Asp.net core backend
* ```SiteWebAssembly.Model```: The model that is shared across both projects

## Blazor WebAssembly
For every Blazor WebAssembly project, the entry point for the application is the ```App.razor``` component. If the path the application being requested exists, the page is then rendered in the ```@Body``` property in the ```MainLayout.razor``` component.

The ```MainLayout.razor``` component is where we can specify the components that will be shared across multiple pages, such a website header and footer. In our case we create a ```Header``` component that is referenced in the ```MainLayout.razor``` as show below:

```
@inherits LayoutComponentBase

<div>
    <MainHeader />

    <div>
        @Body
    </div>
</div>
```

## Reference
https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts?view=aspnetcore-5.0