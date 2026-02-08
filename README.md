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

## 📁 Project Structure
