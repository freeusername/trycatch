      ____ TryCatch WebShop task ____

-=-=-=-=- Reference -=-=-=-=

During development process I didn’t simulate git flow, just was working in master branch, 
no release branches, no feature or bugfix bracnhes, I wanted to save some time on it. 
Usually I use this flow https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow
I son’t leave comments in the code usually cause I think the code must be self descriptive.
One week is a small amount of time to implement complete tested two clients (wpf, asp net mvc), 
auth server and resource server, so I concentrated on architecture of the application and 
did my best implementing JWT token based authentication server.
UI for WPF application is really poor, I concentrated on the code, sorry for that


-=-==-=-= How to Run -=-=-=-=-=

Startup projects: 
WebShop.API
WebShop.ResourceServer
WebShop.WPF or WebShop.MVC (two clients, both can be selected)

Nuget packages must be autorestored
Database mdf file is present in AppData folder in WebShop.API project. You can delete it and 
create new one with the same name and EF will recreate the schema and seed data automatically.

you can register a new user or use existing one:
email: admin@trycatch.com
pwd: 123456

-=-=-=-=  Used technologies, patterns, tools -=-=-=-=-

Visual Studio 2015

Asp .Net MVC
Asp .Net API
JWT (https://auth0.com/learn/json-web-tokens/)
Owin
Entity Framework + CodeFirst
WPF
Unity (Asp .Net MVC)
Ninject (WPF client)
RestSharp
Bootstrap
jQuery

Patterns and principles: REST, IOC, MVC, MVVM, DIP, Repository, Proxy, Onion

-=-=-=-=- A bit about architecture -=-=-=-=

Only Api has access to data access layer which has access to the DB where all information 
is stored, all operations including authentication are done via Api.
ApiProxy is a proxy are used to make WPF and MVC clients to be able make calls to the API 
(they use IApiProxy class). WebShop.API combines two servers in it - resource server and auth server.

If I have more time I would decouple Api on AuthServer assembly and ResourceServer assembly 
(now all the resource related logic is placed in WebShop.API).
WebShop.ResourceServer can be deployed on another machine and be used like independent server
 with the same auth type like in WebShop.API.

-=-=-=- Conclusion -=-=-=-=-
I didn't know what would be the main criterias of evaluation so I assumed that it would be 
architecture so I concentrated on it. So if you want me something done that I didn't pay 
attention to please contact me and I’ll do it according to best practises. I have a lot of
 ideas how to improve it, I’m just limited in time. Many thanks for your time.