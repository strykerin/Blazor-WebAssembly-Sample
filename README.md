# Blazor-WebAssembly-Sample
This repo contains a Blazor WebAssembly application that calls a Web API backend to send an email using SendGrid.

The solution contains 3 projects:
* ```SiteWebAssembly.csproj```: The Blazor client application
* ```SiteWebAssemply.Api.csproj```: The Asp.net core backend
* ```SiteWebAssembly.Model.csproj```: The model that is shared across both projects

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

### Create components to better organize a page
In order to better organize this application, I have created multiple components and then referenced them in order to build a page instead of having a very large component. An example of this can be seen here below in the ```Index.razor``` page where it references 3 components:

```
@page "/"

<Banner />
<Content />
<SendEmail />
```

### Blazor Animations
For the 

## Reference
https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts?view=aspnetcore-5.0