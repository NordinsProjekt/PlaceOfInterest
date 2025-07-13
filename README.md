# PlaceOfInterest
## Overview
**PlaceOfInterest** is a modular web application built using **ASP.NET Core**, **Blazor**, and **Entity Framework Core**. It is designed to manage and display points of interest, with a focus on scalability, maintainability, and modern web development practices.
The solution is structured into multiple projects, each serving a specific purpose, such as domain logic, application services, and data access.
## Features
- **Blazor Components**: Interactive and reusable UI components for a modern user experience.
- **Entity Framework Core**: Database access and ORM for seamless data management.
- **Radzen Blazor**: Pre-built components for rapid UI development.
- **MediatR**: Implementation of the mediator pattern for clean and decoupled architecture.
- **FluentValidation**: Validation of models and inputs.
- **ASP.NET Identity**: Authentication and authorization support.
## Technologies Used
- **Framework**: .NET 9.0
- **Languages**: C# 13.0, Razor
- **Frontend**: Blazor, Radzen Blazor
- **Backend**: ASP.NET Core
- **Database**: SQL Server (via Entity Framework Core)
- **Tools**: Visual Studio Community 2022, ReSharper 2024.3.5
## Project Structure
### Projects
1. **PlaceOfInterest.BackOffice**
   - Contains Blazor components and Razor pages for the back-office interface.
   - Uses Radzen Blazor for UI components.
   - References:
     - `PlaceOfInterest.Application`
     - `PlaceOfInterest.EFCore`
2. **PlaceOfInterest.Application**
   - Contains application-level services and interfaces.
   - Implements MediatR for request/response handling.
   - References:
     - `PlaceOfInterest.Domain`
3. **PlaceOfInterest.EFCore**
   - Implements the repository pattern for data access.
   - Uses Entity Framework Core for database operations.
   - References:
     - `PlaceOfInterest.Application`
     - `PlaceOfInterest.Domain`
4. **PlaceOfInterest.Domain**
   - Contains domain models and business logic.
   - Serves as the core of the application.
