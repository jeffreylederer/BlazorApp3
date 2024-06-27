This was my experiment to learn Blazer Web app. I had a number of goals. This application is a redo of older application [Tournament](https://github/jeffreylederer/tournament)

## Create a Blazer App ##
This app was created using the Blazer Server template and selecting no authentication

## Add the following Nuget Packages
* Ardalis.GuardClauses" Version="4.5.0" 
* AutoMapper" Version="13.0.1" 
* Blazored.SessionStorage" Version="2.4.0"
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

## Add a database ##
I already had an existing data, so i just used Scaffold-DbContext to create a Model directory with DB context. I also add a connection string to appseting.json file.


