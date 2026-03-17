# Vitalis
The Vitalis project is a web application that allows users to track their nutrition goals. It provides a user-friendly interface for logging recipes and creating an extensive database showcasing a vast amount of foods and their nutrient profiles.


![alt tag](https://imgs.search.brave.com/4x5pNDTZT0c0mL5RdbSBx7vis3Lmf8GyUBj14kRjims/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9zdGF0/aWMudmVjdGVlenku/Y29tL3N5c3RlbS9y/ZXNvdXJjZXMvdGh1/bWJuYWlscy8wNTMv/ODEyLzAyMi9zbWFs/bC8zZC1yZWQtYXBw/bGUtaWxsdXN0cmF0/aW9uLXNoaW55LWZy/dWl0LXJlbmRlci1w/bmcucG5n)

## Prerequisites

.Net SDK 8.0 or later

Visual Studio 2022 (optional / for development)

SQL Server

NuGet packages: 
   > Microsoft.EntityFrameworkCore							   8.0.23     
   > Microsoft.AspNetCore.Identity.UI                          8.0.23
   > Microsoft.Extensions.Identity.Core						   8.0.23     
   > Microsoft.EntityFrameworkCore.Tools                       8.0.23
   > Microsoft.EntityFrameworkCore.Sqlite                      8.0.23 
   > Microsoft.EntityFrameworkCore.SqlServer                   8.0.23     
   > Microsoft.VisualStudio.Web.CodeGeneration.Design          8.0.23 
   > Microsoft.AspNetCore.Identity.EntityFrameworkCore         8.0.23  
   > Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore      8.0.23 

 

## Installation
1. Clone the repository:
   ```bash
   git clone
2. Navigate to the project directory:
   ```bash
   cd Vitalis
3. Restore dependencies:
   ```bash
   dotnet restore
4. Update the database connection string in `appsettings.development.json` to point to your SQL Server instance.
5. Apply database migrations:
   ```bash
   dotnet ef database update --project Vitalis.Data
6. Run the application:
   ```bash
   dotnet run --project Vitalis.Web
   ```
## Features
- Recipe logging with detailed nutrient profiles
- Extensive food database with nutrient information

## License
See LICENSE for details.






