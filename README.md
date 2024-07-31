<h1>Setup Instructions</h1>
Clone the Repository
bash
git clone 
cd BloggingPlatform
Configure Database Connection
Edit the appsettings.json file to configure your SQL Server connection string.

<br>

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BloggingPlatformDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
Apply Migrations
Run the following commands to create the database and apply the migrations.

bash

dotnet ef migrations add InitialCreate
dotnet ef database update
Run the Application
bash
dotnet run
By default, the application will run at http://localhost:5000 or https://localhost:5001.

Access Swagger UI
Navigate to http://localhost:5000/swagger to access the Swagger UI, where you can test the API endpoints.
