# Simple Fleet Management - BackEnd

## Requirements
- Visual Studio 2017 +
- SQL Server Database

## Description
- An API developed with a .NET CORE architecture;
- Applied Entity Framework as ORM;
- Enabled CORS, you can configure it in **Startup.cs**;
- To be able to access your database, in **FleetManagementAPI/appsetings.json** adjust the following words to your database information.
~~~
"ConnectionStrings": {
    "CONNECTION_NAME": "server=YOUR_CONNECTRION_STRING,PORT;database=DATABASENAME;User ID=USERNAME;password=PASSWORD;"
  },
~~~

## Setting up your database
### Running migrations
- In Visual Studio terminal:
> Update-Database
- If you have setted up your appsetings.json with the correct connection string, this command will build your database structure.

## Tests
- Created tests for:
  - Vehicles endpoints
  - Chassis endpoints
- To run them, you just need to go to *Test* menu on Visual Studio and click on **Run all the tests**.
 
