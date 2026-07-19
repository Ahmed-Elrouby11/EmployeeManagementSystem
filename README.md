# Employee Management System API

<p align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge)
![C#](https://img.shields.io/badge/C%23-12-blue?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red?style=for-the-badge)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core-green?style=for-the-badge)
![JWT](https://img.shields.io/badge/JWT-Authentication-orange?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-success?style=for-the-badge)

</p>

## Overview

Employee Management System is a modern RESTful Web API built with **ASP.NET Core 8** following **Clean Architecture** principles.

The project demonstrates enterprise-level backend development practices including authentication, authorization, repository pattern, unit of work, API versioning, logging, refresh tokens, and SQL Server integration.

This project was developed as part of my backend engineering portfolio to demonstrate production-ready API development using Microsoft's modern technology stack.

---

# Features

* Employee CRUD Operations
* Department Management
* JWT Authentication
* Refresh Token Authentication
* ASP.NET Core Identity
* Role-Based Authorization
* API Versioning
* Repository Pattern
* Unit of Work Pattern
* Entity Framework Core
* SQL Server
* Dependency Injection
* Global Exception Handling
* Serilog Logging
* Swagger/OpenAPI Documentation
* Clean Architecture
* DTO Pattern
* AutoMapper
* FluentValidation

---

# Project Architecture

```text
EmployeeManagementSystem
│
├── EmployeeManagementSystem.API
│      Controllers
│      Middlewares
│      Configurations
│
├── EmployeeManagementSystem.Application
│      DTOs
│      Interfaces
│      Services
│      Validators
│
├── EmployeeManagementSystem.Domain
│      Entities
│
├── EmployeeManagementSystem.Infrastructure
│      Persistence
│      Repositories
│      Identity
│
└── EmployeeManagementSystem.Shared
```

---

# Technologies

* ASP.NET Core 8
* C#
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* JWT Authentication
* Refresh Tokens
* Swagger
* Serilog
* AutoMapper
* FluentValidation
* Dependency Injection

---

# Authentication Flow

```text
Register
      │
      ▼
Create User
      │
      ▼
Login
      │
      ▼
Generate JWT Access Token
      │
      ▼
Generate Refresh Token
      │
      ▼
Save Refresh Token in Database
      │
      ▼
Access Protected APIs
      │
      ▼
Access Token Expired
      │
      ▼
POST /api/auth/refresh
      │
      ▼
Generate New Access Token
```

---

# API Endpoints

## Authentication

| Method | Endpoint           |
| ------ | ------------------ |
| POST   | /api/auth/register |
| POST   | /api/auth/login    |
| POST   | /api/auth/refresh  |

---
## Users

| Method | Endpoint              |
| ------ | --------------------- |
| GET    | /api/users            |
| GET    | /api/users/{id}       |
| PUT    | /api/users/{id}/role  |


---

## Employees

| Method | Endpoint               |
| ------ | ---------------------- |
| GET    | /api/v1/employees      |
| GET    | /api/v1/employees/{id} |
| POST   | /api/v1/employees      |
| PUT    | /api/v1/employees/{id} |
| DELETE | /api/v1/employees/{id} |

---

## Departments

| Method | Endpoint                                 |
| ------ | ---------------------------------------- |
| GET    | /api/v1/departments                      |
| GET    | /api/v1/departments/{id}                 |
| POST   | /api/v1/departments                      |
| PUT    | /api/v1/departments/{id}                 |
| DELETE | /api/v1/departments/{id}                 |
| GET    | /api/v1/departments/{id}/employees/count |
| GET    | /api/v1/departments/{id}/employees       |
---

# Database

The application uses **Microsoft SQL Server** with **Entity Framework Core Code First**.

Main tables include:

* Employees
* Departments
* AspNetUsers
* AspNetRoles
* AspNetUserRoles
* RefreshTokens

---

# Getting Started

## Clone Repository

```bash
git clone https://github.com/Ahmed-Elrouby11/EmployeeManagementSystem.git
```

## Navigate to Project

```bash
cd EmployeeManagementSystem
```

## Restore Packages

```bash
dotnet restore
```

## Apply Database Migrations

```bash
dotnet ef database update
```

## Run Application

```bash
dotnet run --project EmployeeManagementSystem.API
```

Swagger will be available at

```
https://localhost:7187/swagger
```

---

# Configuration

Update your SQL Server connection string inside:

```text
EmployeeManagementSystem.API/appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

# Screenshots

> Add screenshots here after uploading them.

```
screenshots/
    swagger-home.png
    login.png
    register.png
    employees.png
```

---

# Future Improvements

* Docker Support
* Redis Caching
* Background Jobs
* Email Notifications
* Unit Testing
* Integration Testing
* Azure Deployment
* CI/CD Pipeline
* File Upload
* Generic Repository
* CQRS + MediatR

---

# Author

**Ahmed Elrouby**

Backend Developer

GitHub:
https://github.com/Ahmed-Elrouby11

LinkedIn:
https://www.linkedin.com/in/ahmed-elrouby-950a37285

---

If you found this project helpful, consider giving it a ⭐ on GitHub.
