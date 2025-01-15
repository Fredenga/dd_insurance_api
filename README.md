SETUP INSTRUCTIONS:

Git clone project

Install Packages: dotnet add package BCrypt.Net-Next Microsoft.AspNetCore.Authentication.JwtBearer Microsoft.EntityFrameworkCore Microsoft.EntityFrameworkCore.Design Microsoft.EntityFrameworkCore.SqlServer Microsoft.EntityFrameworkCore.Tools Swashbuckle.AspNetCore --version 4.0.3 --version 8.0.11 --version 9.0.0 --version 9.0.0 --version 9.0.0 --version 9.0.0 --version 7.2.0

Install and configure Microsoft SQL Server Database: Create new database called InsuranceDB

Add Initial Migration: dotnet ef migrations add InitialCreate

Update Database: dotnet ef database update

Run source code
