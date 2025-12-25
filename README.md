# TP2 - ASP.NET Core MVC Application

## Project Overview
This is a comprehensive ASP.NET Core MVC web application built with .NET 8.0 that implements a movie rental and customer management system with advanced features including user authentication, audit logging, and CRUD operations.

## Key Features

### 1. **Movie Management**
- Complete CRUD operations for movies
- Genre categorization
- Image upload functionality
- Stock tracking and release date management
- Advanced sorting (by name, genre)
- Pagination (5 items per page)

### 2. **Customer Management**
- Customer registration and management
- Membership type assignment
- Newsletter subscription tracking
- Full CRUD operations

### 3. **User Authentication & Authorization**
- ASP.NET Core Identity integration
- User registration and login
- Account management
- User cart functionality

### 4. **Audit Logging System**
- Automatic change tracking using Entity Framework interceptors
- Logs all database operations (Create, Update, Delete)
- Records entity changes with before/after values
- Viewable audit history through dedicated interface

### 5. **Repository Pattern Implementation**
- Generic repository for common operations
- Specific repositories for specialized queries (Movie, Customer, Genre)
- Service layer abstraction
- Dependency injection throughout

## Project Structure

```
TP2/
├── Controllers/          # MVC Controllers
│   ├── MovieController.cs
│   ├── CustomerController.cs
│   ├── GenreController.cs
│   ├── AccountController.cs
│   ├── AuditLogController.cs
│   └── MembershipTypeController.cs
├── Models/              # Data models
│   ├── Movie.cs
│   ├── Customer.cs
│   ├── Genre.cs
│   ├── MembershipType.cs
│   ├── AuditLog.cs
│   ├── ApplicationUser.cs
│   └── ApplicationDbContext.cs
├── Repositories/        # Data access layer
│   ├── GenericRepository.cs
│   ├── MovieRepository.cs
│   ├── CustomerRepository.cs
│   └── GenreRepository.cs
├── Services/           # Business logic layer
│   ├── MovieService.cs
│   ├── CustomerService.cs
│   ├── GenreService.cs
│   └── MembershipTypeService.cs
├── Interceptors/       # EF Core interceptors
│   └── AuditInterceptor.cs
├── Views/              # Razor views
├── Migrations/         # EF Core migrations
└── wwwroot/           # Static files (CSS, JS, images)
```

## Technologies Used

- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 9.0
- **Database**: SQL Server (LocalDB)
- **Authentication**: ASP.NET Core Identity
- **Architecture Patterns**: 
  - Repository Pattern
  - Service Layer Pattern
  - Dependency Injection
  - Interceptor Pattern (for audit logging)

## Database Schema

### Main Tables:
- **Movies**: Movie catalog with genre relationships, stock, and images
- **Genres**: Movie genre categories
- **Customers**: Customer information with membership types
- **MembershipTypes**: Different membership levels
- **AuditLogs**: Change tracking for all entities
- **AspNetUsers**: User authentication (Identity)
- **Products**: Product catalog
- **UserCarts**: User shopping cart functionality

## Setup Instructions

1. **Prerequisites**:
   - .NET 8.0 SDK
   - SQL Server or SQL Server Express
   - Visual Studio 2022 or VS Code

2. **Database Configuration**:
   - Update connection string in `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TestDatabaseConnection;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

3. **Database Migration**:
   ```powershell
   dotnet ef database update
   ```

4. **Run the Application**:
   ```powershell
   dotnet run
   ```

5. **Access the Application**:
   - Navigate to `https://localhost:5001` or `http://localhost:5000`

## Key Architectural Decisions

### 1. Repository Pattern
Implemented to separate data access logic from business logic, making the code more maintainable and testable.

### 2. Service Layer
Business logic is isolated in service classes, keeping controllers thin and focused on HTTP concerns.

### 3. Audit Interceptor
Uses EF Core's `SaveChangesInterceptor` to automatically track all database changes without modifying existing code.

### 4. Dependency Injection
All services, repositories, and the interceptor are registered in the DI container for loose coupling.

## Features Highlights

- **Dynamic Sorting**: Movies can be sorted by name or genre (ascending/descending)
- **Pagination**: Efficient page-based navigation through large datasets
- **File Upload**: Support for movie poster images
- **Change Tracking**: Complete audit trail of all database modifications
- **Identity Integration**: Secure user authentication and authorization
- **Clean Architecture**: Separation of concerns with Repository and Service patterns

## Assignment Requirements Fulfilled

✅ ASP.NET Core MVC application structure  
✅ Entity Framework Core with SQL Server  
✅ Repository Pattern implementation  
✅ Service Layer implementation  
✅ CRUD operations for multiple entities  
✅ User authentication with Identity  
✅ Audit logging functionality  
✅ File upload handling  
✅ Data validation  
✅ Sorting and pagination  

## Notes

- The application uses a test database named `TestDatabaseConnection`
- Initial data can be seeded from `Genres.json` and `Movies.json` files
- Audit logs are automatically created for all entity changes
- Password requirements: minimum 6 characters with at least one digit and one lowercase letter

## Author

Submitted as TP2 Assignment - December 2025
