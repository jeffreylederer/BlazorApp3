This was my experiment to learn Blazer Web app. I had a number of goals. This application is a redo of an older application [Tournament](https://github/jeffreylederer/tournament) I created this older app in .Net 4.6 with MVC. That application is used by the Frick Park Lawn Bowling club to track season long leagues. The goal was not to fully duplicate the older app, but just enough to figuure out how to determine how to do the important functions of the older app.

## Create a Blazer App ##
This app was created using the Blazer Server template and selecting no authentication

## Added the following Nuget Packages
* Ardalis.GuardClauses" Version="4.5.0" 
* AutoMapper" Version="13.0.1" 
* DNTCommon.Web.Core" Version="5.3.3" 
* EntityFramework" Version="6.4.4" 
* jQuery" Version="3.7.1" 
* Microsoft.AspNetCore.Components.QuickGrid" Version="8.0.6" 
* Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter" Version="8.0.6" 
* Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.6" 
* Microsoft.EntityFrameworkCore.Tools" Version="8.0.6">
* Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="8.0.2" 
* QuestPDF" Version="2022.12.15" <-- use this version, later versions require you to pay license
* System.ComponentModel.Annotations" Version="5.0.0" 

## Add a database context ##
I already had an existing data, so i just used Scaffold-DbContext to create a Model directory with DB context. I also add a connection string to appseting.json file.

The major issues I had with the .Net Core entityframework were:
* It really does not support stored procedures like the older version. It does support views.
* It does support lazy loading, but I really did not need this function.

## Authorization and Authentication
This is one of the goals for writing this application: database authentication and authorization services. I played around with some services I found on the web. The breakthrough was figuring out how to use cookies to make log ins persistent. Users, Roles, and the link 
between users and roles is stored in the table userrole in the database. Thought it would be easy to give users multiple roles, this application only needs to give each user just one role.

## Seed Database
Since you need a SiteAdmin role to create additional users, I seeded the database with roles and a SiteAdmin, Password = password. You can create addtional Users with the User CRUD pages. This used the DbInitializerService service to seed the database.

## Force users to login
If you look in the file Routes.razer, you can see the construct that forces unauthorized users to only be able to see the Login page. The app does not let users self-register though that would be easy. I did not setup pages for the user to reset password since I already have that logic in the older MVC application.

## CRUD ##
I wanted to create CRUD pages for major tables in the existing database. It was easy as creating this as in MVC. I did learn how to do select onchange callbacks in the matches index page and inside an EditForm on the Matches Scoring page.

## AuthorizeView ##
I want to limit users to certain pages and certain functions on the pages a user is authorized to view. This is all based on the user's role. I only implemented this in the User pages (limit page viewing to SiteAdmin) and the Membership index page 
(limiting user actions to SiteAdmin and Admin)

## PDF Documents ##
I used QuestPDF to create a single PDF document. It is accessable from the Teams index page. I did not bother to create additional ones that are avialable to the older app.

## Session Object ##
.Net Core does not support the Session Object. This application had to store from page to page the current league id.  I used ProtectedSessionStore. On the Welcome page, I created a ProtectedSessionStore object and then in each table index page I retrieved that object.
Since the object is stored using browser storage, you need to work with the object in a override of the OnAfterRenderAsync method and set the rendermode to InteractiveServer.





