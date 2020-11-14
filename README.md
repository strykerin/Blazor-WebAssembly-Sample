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
To create animations in the application I used the ```Blazor.Animate``` nuget package craeted by mikoskinen. In order to add this nuget package simply run this dotnet cli command inside the Blazor WebAssembly folder: ```dotnet add package BlazorAnimate --version 3.0.0```

To add animation to a Component or html element, simply wrap it inside the Animate component as below:
```
<Animate Animation="Animations.SlideUp" DurationMs="1000">
    <p>
        Lorem ipsum dolor sit amet 
    </p>
</Animate>
```

### Call Backend Web API
Blazor supports dependency injection. Dependency injection will be used in order to have a single instance of the the service across all components and to decouple the components from the concrete service class.

In our blazor wasm application we have created the ```EmailService.cs``` class that will be injected in the DI container in ```Program.cs``` by doing:

```
builder.Services.AddHttpClient<IEmailService, EmailService>();
```

The service class above requires the ```HttpClient``` class, so we will add it as a Scoped. In the configuration we will also set the Backend BaseAddress. It is important to say that in a deployed application the base address should be configured in a configuration file such as appsettings.json or have the value coming from the Azure KeyVault service when this application is running in Azure App Service for example.

```
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44345") });
```


## Reference
Blazor Layouts:

https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts?view=aspnetcore-5.0

Blazor.Animate:

https://github.com/mikoskinen/Blazor.Animate

Blazor dependency injection

https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0