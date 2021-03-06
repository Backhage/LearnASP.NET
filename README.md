# LearnASP.NET
Learning resources for my focused learning on ASP.NET

## Learning Plan by Week
1. Reading material
   
   Collect relevant links, books, etc.
2. Tooling
   
   Ensure all required tools and resources for devoloping and hosting a database backed ASP.NET
   web application are available.
3. Sample sites
   
   Create sample sites by following step-by-step guides or tutorials.
   Be sure to understand the details and how these can be applied when creating your own web applications.
4. Requirements for The Record Collector application

   Write a small requirement document for the record collector application.

5. Architecture and Domain Model
   
   Design the core domain model for the "Record Collector" application.
6. Core implementation
   
   Implement core functionality. Focus should not be on UI or persistence.
7. Core implementation continued
   
   Finish up the implementation of the core model.
8. UI Design
   
   Add support for working with the core model via a web UI.
9. Persistence
   
   Add persistence support.
10. (Optional) Multi user
   
   Add support for multiple users.

## Reading material
- [The little ASP.NET Core Book](https://www.recaffeinate.co/book/)
- [Pro ASP.NET Core MVC](https://www.apress.com/gp/book/9781484231494)
- [LibMan Bower replacement](https://blogs.msdn.microsoft.com/webdev/2018/04/17/library-manager-client-side-content-manager-for-web-apps/)
- [Packman client side package manager](https://github.com/madskristensen/Packman)
- [A lesson in ASP.NET DI Scope](https://dotnetcoretutorials.com/2018/03/20/cannot-consume-scoped-service-from-singleton-a-lesson-in-asp-net-core-di-scopes/)
- [Typescript official docs](https://www.typescriptlang.org/docs/home.html)
- [Entity Framework Documentation](https://docs.microsoft.com/en-us/ef/#pivot=entityfmwk)
- [Microsoft's introduction to Entity Framework](https://msdn.microsoft.com/en-us/library/aa937723(v=vs.113).aspx)
- [Introduction to Identity on ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.1&tabs=visual-studio%2Caspnetcore2x)
- [Bootstrap front-end library](https://getbootstrap.com/)
- [jQuery JavaScript library](https://jquery.com/)

## Tools
- [.NET Core SDK for Windows](https://www.microsoft.com/net/download/windows)
- [Visual Studio Code](https://code.visualstudio.com/)

## Sample Site
### Created using "The little ASP.NET Core Book"
The free online book [The little ASP.NET Core Book](https://www.recaffeinate.co/book/) is a
step by step guide, with explanations, on how to create a simple ASP.NET site from scratch.
Code is available here under LittleAspNetCoreBook.

**NOTE:** There have been some updates to ASP.NET Core 2.1 in regards to Identity and Roles.
Everywhere where it in the book says ApplicationUser I had to use IdentityUser instead.
Also, the RoleManager is no longer available as a service by default. See [this post](https://stackoverflow.com/questions/50426278/how-to-use-roles-in-asp-net-core-2-1) on StackOverflow for some tips.
I was not able to get the Authorize attribute with Roles parameter to work as described in the book, even though the
UserManager correctly identifies the user as having the administrator role.

```csharp
[Authorize(Roles = "Administrator"] // This did not work for me
public class ManageUsersController : Controller
{
   ...
}
```

**UPDATE:** I received a tip on how to make the authorization work again (thanks @RoBak42). It should now work as expected in the code in this repository. What I had to do was to add the line

```csharp
services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();
```
to the startup.cs file.

**NOTE:** In the Unittest chapter: According to the book the unit test template should add everything needed. However I had to add two packages to the test project 
in order to get the code to compile. These were `Microsoft.AspNetCore.Identity.EntityFrameworkCore` and `Microsoft.EntityFrameworkCore.InMemory`.

**NOTE:** In the Integration test chapter: I had to manually add the following packages `Microsoft.Extensions.Configuaration.Json` and `Microsoft.AspNetCore.App`. Also the expected URL in the test in the book actually differs from the actual URL returned.
```csharp
// I had to change the assert to look like this
Assert.Equal(
    "http://localhost:8888/Identity/Account/Login?ReturnUrl=%2Ftodo",
    response.Headers.Location.ToString());
```

### Created using "Pro ASP.NET Core MVC 2"
#### PartyInvites
The first sample site from this book is called "PartyInvites". It is built using the MVC template, however upcoming sample sites will be built from scratch.
Code for "PartyInvites" can be found under the PartyInvites folder.
#### Razor
The second sample site are intended to show Razor features and is named 'Razor'. Here the site is created using a blank template.
Razor is the viewengine that parses .cshtml views and outputs the HTML that is sent to the client. It replaces the C# code in the
views with HTML that the browser is able to parse.
#### WorkingWithVisualStudio
This sample site is designed to show how to efficiently work with Visual Studio during development of your ASP.NET Core MVC project.

**NOTE:** The book shows how to use a tool called Bower for managing client side packages. Bower is deprecated and the replacement
seems to be a tool called Library manager, or LibMan for short. I have added a link to an info page on LibMan and will try to use
that instead of Bower for the sample sites. UPDATE: LibMan is not available in the Visual Studio version I am using now, 15.7.4 and
neither is Bower. I will try to use npm instead.

**NOTE:** The book instructs you to enable the developer exception page during development. However in later versions of ASP.NET Core this
is enabled by default, so there is no need to enable it. If you want to see how it looks without the developer exception page you can
comment out this line:

```csharp
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Comment out this line to see the effect 
}
```

**NOTE:** If you want to use Typescript instead of Javascript you can do that. Visual Studio has great support for Typescript so you can treat the TS files
just like you would treat JS files. The compilation from TS to JS happen automatically without any user interaction.

#### SportStore
This is a sample site that quite closely resembles a real production site. It includes a database, user login, administration etc.
It is written from a blank template. All code except the absolute basic is written manually.

**NOTE:** I have used ASP.NET Core 2.1 instead of 2.0. The book informs you to add a DotNetCliToolReference for the Entity Framework command line tools
to your project file. This is however not necessary when using version 2.1 of the framework since it is included by default in the bundle.

**NOTE:** This is a really large sample site. It throws you in on the deep side
of the pool and forces you through a lot of new concepts. If you are not pretty
well familiar with ASP.NET you will have a tough time following along. But the
author promises to go through all the new concepts in detail in later chapters.

**NOTE:** There is a bad bug in the IdentitySeedData.cs code in the book. It uses the *FindByIdAsync* method to try to find the admin user in the database. However, it uses the admin username for lookup, which has the effect that the lookup always fails. The correct method to use is *FindByNameAsync*.

**NOTE:** I am fully aware of that there is login credentials for an Admin account in the source code. These are however just seed data and there is no live site where they can be used.