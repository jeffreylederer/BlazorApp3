This was my experiment to learn Blazer Web app. I had a number of goals. This application is a redo of an older application [Tournament](https://github/jeffreylederer/tournament) I created this older app in .Net 4.6 with MVC. This application is used by the Frick Park Lawn Bowling club to track season long leagues. The goal was not to fully duplicate the older app, but just enough to figuure out how to determine how to do the important functions of the older app.

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
* QuestPDF" Version="2022.12.15" 
* System.ComponentModel.Annotations" Version="5.0.0" 

## Add a database context ##
I already had an existing data, so i just used Scaffold-DbContext to create a Model directory with DB context. I also add a connection string to appseting.json file.

The major issues I had with the .Net Core entityframework were:
* It really does not support stored procedures like the older version. It does support views.
*  

## Authorization and Authentication
This is one of the goals for writing this application: a database authentication and authorization services. I played around some services I found on the web. The breakthrough was figuring out how to use cookies to make log ins persistent. Users, Roles, and the link 
between users and roles is stored in table userrole in the database.

## Force users to login
If you look in the file Routes.razer, you can see the construct that forces unauthorized users to only be able to see the Login page. The app does not let users self-register though that would be easy. I did not setup pages the user to reset password since I already have that logic in the older MVC application.

## CRUD ##
I wanted to create the CRUD pages for major tables in the existing database.



