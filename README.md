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

## ASP.NET Core backend Web API
### Enable CORS in Backend Web API
Browsers have a security feature that prevents an application from making requests to a different domain than the one that served the application. If you try to call the backend from the blazor webassembly without configuring the server you'll get an error. In order to solve this, the endpoint in the backend must enable cross-origin resource sharing (CORS).

In order to enable it on the backend, you add in the ```Configure``` method in ```Startup.cs``` in the the folowing middleware **after** the ```UseRouting```, but **before** the UseAuthorization as shown below:

```
app.UseRouting();

// Enable cors
app.UseCors(policyName => policyName.WithOrigins("https://localhost:5001")
                                    .AllowAnyMethod()
                                    .WithHeaders(HeaderNames.ContentType));

app.UseAuthorization();
```

### Add SendGrid Nuget Package
The service we'll be using to send email is SendGrid. We will use from SendGrid two nuget packages, one to send the emails and the other one to allow us to have access to the ```ISendGridClient``` interface that is injected in the DI container.

To download both nuget package, run this dotnet cli commands inside the Web Api project folder location:

```
dotnet add package Sendgrid --version 9.21.1
dotnet add package SendGrid.Extensions.DependencyInjection --version 1.0.0
```

Now that we have both of this packages, first we register the service in ```Startup.cs```:
```
services.AddSendGrid(opt => opt.ApiKey = Configuration["SendGrid:ApiKey"]);
```

To create the ```SendEmail``` service, we will need the ```ISendGridClient``` implementation that we will have access via the constructor:

```
public class SendEmailService : ISendEmailService
{
    private readonly ISendGridClient _sendGridClient;

    public SendEmailService(ISendGridClient sendGridClient)
    {
        _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
    }
    ...
}
```

With this interface, we can then create the method that will be sending email when the requests arrives from the Blazor WebAssembly:

```
public async Task<bool> SendEmail(Contact contact)
{
    SendGridMessage msg = new SendGridMessage();
    EmailAddress from = new EmailAddress(contact.Email, contact.Name);
    List<EmailAddress> recipients = new List<EmailAddress> { new EmailAddress("your@email.com", "Your Name") };

    msg.SetSubject("A new user has registered");
    msg.SetFrom(from);
    msg.AddTos(recipients);
    msg.PlainTextContent = contact.Message;

    Response response = await _sendGridClient.SendEmailAsync(msg);
    if (Convert.ToInt32(response.StatusCode) >= 400)
    {
        return false;
    }
    return true;
}
```


## Reference
Blazor Layouts:

https://docs.microsoft.com/en-us/aspnet/core/blazor/layouts?view=aspnetcore-5.0

Blazor.Animate:

https://github.com/mikoskinen/Blazor.Animate

Blazor dependency injection

https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0


Call a web API from ASP.NET Core Blazor

https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-3.1#cross-origin-resource-sharing-cors