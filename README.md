# LearnASP.NET
Learning resources for my focused learing on ASP.NET

## Learning Plan by Week
1. Reading material
   
   Collect relevant links, books, etc.
2. Tooling
   
   Ensure all required tools and resources for devoloping and hosting a database backed ASP.NET
   web application are available.
3. Sample site
   
   Create a sample site by following a step-by-step guide or tutorial.
4. Domain Model
   
   Design the core domain model for the "Record Collector" application.
5. Core implementation
   
   Implement core functionality. Focus should not be on UI or persistence.
6. Core implementation continued
   
   Finish up the implementation of the core model.
7. UI Design
   
   Add support for working with the core model via a web UI.
8. Persistence
   
   Add persistence support.
9. (Optional) Multi user
   
   Add support for multiple users.

## Reading material
- [The little ASP.NET Core Book](https://www.recaffeinate.co/book/)
- [A lesson in ASP.NET DI Scope](https://dotnetcoretutorials.com/2018/03/20/cannot-consume-scoped-service-from-singleton-a-lesson-in-asp-net-core-di-scopes/)
- [Typescript official docs](https://www.typescriptlang.org/docs/home.html)
- [Entity Framework Documentation](https://docs.microsoft.com/en-us/ef/#pivot=entityfmwk)
- [Microsoft's introduction to Entity Framework](https://msdn.microsoft.com/en-us/library/aa937723(v=vs.113).aspx)
- [Bootstrap front-end library](https://getbootstrap.com/)
- [jQuery JavaScript library](https://jquery.com/)

## Tools
- [.NET Core SDK for Windows](https://www.microsoft.com/net/download/windows)
- [Visual Studio Code](https://code.visualstudio.com/)

## Sample Site
The free online book [The little ASP.NET Core Book](https://www.recaffeinate.co/book/) is a
step by step guide, with explanations, on how to create a simple ASP.NET site from scratch.
Code is available here under LittleAspNetCoreBook.

*Note* The example in the book where Entity Framework is used to inject the ApplicationDbContext into
the TodoItemService singleton does not work for me. I had to make the TodoItemService scoped instead
of Singleton.

