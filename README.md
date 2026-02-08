# 🔐 SafeVault  
A secure ASP.NET Core MVC application featuring JWT authentication, PBKDF2 password hashing, input validation, and a full NUnit test suite.  
SafeVault is designed as a clean, modern foundation for authentication‑based web applications.

---

## 🚀 Features

### 🔑 Authentication & Security
- JWT authentication with secure cookie storage  
- PBKDF2 password hashing with salt  
- Role‑based authorization (User, Admin)  
- Secure login & registration flow  
- Protection against SQL injection & XSS via strict validation  
- Token expiration handling  

### 🧩 Architecture
- ASP.NET Core MVC (Views, Controllers, Models)  
- Clean separation of concerns  
- Helper classes for hashing, validation, and JWT generation  
- MySQL database integration via EF Core  

### 🧪 Testing
- NUnit test project (`SafeVault.Tests`)  
- Validation tests for:
  - Username rules  
  - Password rules  
  - Allowed/disallowed characters  
  - Length constraints  
- Fully automated test suite using `dotnet test`



---
 ### Packages Requirement :
 For SafeVault :
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 8.0.0
dotnet add package Microsoft.Extensions.Caching.Memory --version 8.0.1
   For SafeVault.Test
   dotnet add package NUnit --version 4.2.2
   dotnet add package NUnit3TestAdapter --version 5.0.0
   dotnet add package Microsoft.NET.Test.Sdk --version 17.10.0

### Project configuration :
  MySQL Configuration :
    Database: SafeVault
    Table : 
    CREATE TABLE Users (
                                            ->     Id INT AUTO_INCREMENT PRIMARY KEY,
                                            ->     Username VARCHAR(50) NOT NULL UNIQUE,
                                            ->     PasswordHash VARCHAR(256) NOT NULL,
                                            ->     Role VARCHAR(20) NOT NULL DEFAULT 'User'
                                            -> );
---------------------------------------------------------------------------------
  appsettings.json :
  
  {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "AllowedHosts": "*",

  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SafeVault;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
  },

  "Jwt": {
    "SecretKey": "PLEASE_CREATE_YOUR_OWN_SECRET_KEY",
    "Issuer": "SafeVault",
    "Audience": "SafeVaultUsers",
    "ExpirationMinutes": 60
  }
}
------------------------------------------------------------------
appsettings.Development.json :
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SafeVault;User Id=root;Password=YOUR_PASSWORD;"
  }
}
-----------------------------------------------------------------------
To create admin user. Create new user and Update its role in table to Admin
-----------------------------------------------------------------------

