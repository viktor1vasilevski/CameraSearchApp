# CameraSearchApp

A multi-layered .NET 9 application for searching camera data, featuring both a Web API and a Command-Line Interface (CLI). Built with clean architecture principles for maintainability and extensibility.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Sample Data](#sample-data)
- [Usage](#usage)
  - [Command-Line Interface (CLI)](#command-line-interface-cli)
  - [Web API](#web-api)
- [Error Handling](#error-handling)
- [Extending the Application](#extending-the-application)
- [Design Decisions](#design-decisions)
- [Contact](#contact)

---

## Overview

**CameraSearchApp** allows users to search data sourced from a CSV file. The application is accessible via both a command-line interface and a API, making it versatile for different user needs and integration scenarios.

---

## Architecture

The solution follows a **clean architecture** approach, separating concerns into distinct layers:

- **Domain**: Core business entities and logic.
- **Application**: Business use cases, DTOs, service interfaces, and request/response models.
- **Infrastructure**: Data access (CSV repository), configuration, and external dependencies.
- **Presentation**: User interfaces (Web API and CLI).
- **IoC**: Dependency injection and service registration.
- **Exceptions**: Custom exception handling for robust error management.

This structure ensures maintainability and scalability.

---

## Project Structure

```
CameraSearchApp/
  ├── Application/                  # Business logic, DTOs, service interfaces
  ├── CLI/                          # Command-line interface
  ├── Domain/                       # Core domain entities
  ├── Infrastructure/               # Data access, repositories
  ├── Infrastructure.Exceptions/    # Custom exceptions
  ├── Infrastructure.IoC/           # Dependency injection setup
  ├── WebAPI/                       # ASP.NET Core Web API
  └── README.md                     # Project documentation
```

---

## Configuration

- **CSV Data Path:**  
  The path to the camera data CSV file is configured in `appsettings.json` for the CLI and in `appsettings.Development.json` in WebAPI project.

**Example (`CLI/appsettings.json`):**
```json
{
  "CsvFilePath": "../../../../Infrastructure/cameras-defb.csv"
}
```

**Example (`WebAPI/appsettings.Development.json`):**
```json
{
  "CsvFilePath": "../Infrastructure/cameras-defb.csv"
}
```

---

## Sample Data

- The file `cameras-defb.csv` contains sample camera data used by both the CLI and WebAPI.
- You can replace or extend this file with your own data as needed.

---

## Usage

### Command-Line Interface (CLI)

1. **Navigate to the CLI directory:**
   ```sh
   cd CLI
   ```

2. **Run the CLI application:**
   ```sh
   dotnet run --project SearchConsole.csproj
   ```
   or
   ```sh
   dotnet run SearchConsole --name mari
   ```

4. **Follow the on-screen prompts** to search and display camera data.

**Features:**
- Search cameras by street name
- Display results in a user-friendly format.

---

### Web API

1. **Navigate to the WebAPI directory:**
   ```sh
   cd WebAPI
   ```

2. **Example Endpoints:**

   - **Get all cameras grouped:**
     ```
     GET /api/camera/grouped
     ```

---

## Error Handling

- The application uses custom exceptions for configuration and CSV parsing errors.
- The Web API includes global exception middleware to return meaningful error responses.
- The CLI displays user-friendly error messages for invalid input or data issues.

---

## Extending the Application

- **Add new data sources:**  
  Implement a new repository in `Infrastructure/Repositories/` and register it in the IoC container.
- **Add new API endpoints:**  
  Create new controllers or actions in `WebAPI/Controllers/`.
- **Add new CLI features:**  
  Extend or add helper classes in `CLI/Helpers/`.
- **Add new domain logic:**  
  Update or add entities and services in the `Domain` and `Application` layers.

---

## Design Decisions

- **Clean Architecture:**  
  Chosen for maintainability, testability, and clear separation of concerns.
- **CSV as Data Source:**  
  Simple, portable, and easy to replace with a database in the future.
- **Multiple Interfaces:**  
  Both CLI and Web API provided to demonstrate versatility and adaptability.
- **Custom Exceptions:**  
  Ensures robust error handling and clear communication of issues.

---
