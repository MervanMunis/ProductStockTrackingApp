# ProductStockTracking App Project

## Introduction

Welcome to the ProductStockTracking App project! This project is designed to provide a comprehensive system for tracking products and their associated stock levels. It offers a robust solution for managing inventory, including product creation, stock management, and product-based stock reporting. The project is built with a focus on maintainability, scalability, and adherence to software development best practices, including the use of a layered architecture.

## Project Overview

The ProductStockTracking App is a RESTful API developed using ASP.NET Core. It provides endpoints for managing products and stocks, allowing users to create, update, and delete products and their associated stock entries. The project also includes advanced features such as automatic cascading deletion of stock entries when a product is marked as deleted, and filtering capabilities for products and stocks based on their deletion status.

## Technologies Used

This project leverages a range of modern technologies to ensure a powerful and flexible system. Below is an overview of the key technologies used:

### ASP.NET Core

ASP.NET Core is a cross-platform, high-performance framework for building modern, cloud-based, and internet-connected applications. It serves as the backbone for the ProductStockTracking App, providing a robust and scalable platform for building the API.

### Entity Framework Core

Entity Framework Core (EF Core) is an open-source Object-Relational Mapper (ORM) for .NET. It allows developers to work with databases using .NET objects, reducing the need for manual data access code. EF Core is used in this project to handle database interactions seamlessly.

### Identity Framework

The ASP.NET Core Identity framework is used to manage authentication and authorization. It provides a complete, customizable system for handling users, roles, and claims, ensuring secure access to the API.

### JWT (JSON Web Tokens)

JSON Web Tokens are used for securely transmitting information between parties as a JSON object. In this project, JWT is employed for authentication and authorization, allowing secure access to the API's endpoints.

### SQL Server

SQL Server is a relational database management system developed by Microsoft. It is used as the primary database for the ProductStockTracking App, storing all the product and stock data.

### Swagger

Swagger is an open-source tool for documenting APIs. It provides a user-friendly interface for exploring and testing API endpoints. The ProductStockTracking App includes Swagger for API documentation and testing.

## Layered Architecture

### Importance of Layered Architecture

The layered architecture is a crucial design pattern that helps organize the codebase into distinct sections, each responsible for a specific aspect of the application. This separation of concerns leads to a more maintainable and scalable system, where each layer can be developed, tested, and maintained independently. 

### Why It Was Used

In this project, the layered architecture is used to ensure that the business logic, data access, and presentation concerns are neatly separated. This approach not only makes the codebase more manageable but also enhances testability and flexibility, allowing for future extensions and modifications with minimal impact on existing code.

### Project Structure Design

The ProductStockTracking App is organized into several key layers, each with a specific role:

- **Entities Layer**: Contains the model classes representing the database schema and the Data Transfer Objects (DTOs) used for data encapsulation.
- **Repositories Layer**: Provides the data access layer, including contracts and implementations for interacting with the database.
- **Services Layer**: Implements the business logic, including product and stock management services.
- **Presentation Layer**: Contains the API controllers that handle HTTP requests and map them to the appropriate service calls.
- **WebAPI Layer**: The entry point of the application, where configurations, middleware, and dependency injection are set up.

## Project Structure

The project is organized into several folders, each representing a different layer of the architecture. Below is an overview of the project structure:

```maths
ProductStockTrackingApp/
├── Entities/
│ ├── Models/
│ ├── DTOs/
├── Repositories/
│ ├── Contracts/
│ ├── EFCore/
├── Services/
│ ├── Contracts/
│ ├── Implementations/
├── Presentation/
│ ├── Controllers/
├── WebAPI/
│ ├── Extensions/
│ ├── Program.cs
```

### Key Folders and Files

- **Entities/Models**: Contains the model classes, such as `Product` and `Stock`, that represent the entities in the database.
- **Entities/DTOs**: Contains Data Transfer Objects like `ProductRequest`, `ProductResponse`, `StockRequest`, and `StockResponse`.
- **Repositories/Contracts**: Contains the interfaces for the repository pattern, defining the methods for data access.
- **Repositories/EFCore**: Contains the implementations of the repository interfaces and the DbContext class for database access.
- **Services/Contracts**: Contains the service interfaces, defining the methods for business logic.
- **Services/Implementations**: Contains the implementations of the service interfaces, including the business logic for managing products and stocks.
- **Presentation/Controllers**: Contains the API controllers that expose the endpoints for managing products and stocks.
- **WebAPI/Extensions**: Contains extension methods for configuring services and middleware in the `Program.cs`.

## Model Classes

The model classes represent the entities in the database. Here are some key model classes used in the project:

### Product

Represents a product in the system, including properties like `UUID`, `Name`, `CreationTime`, `DeletionTime`, and `IsDeleted`.

### Stock

Represents the stock associated with a product, including properties like `UUID`, `ProductId`, `Quantity`, `CreationTime`, and `IsDeleted`.

## Endpoints

The ProductStockTracking App provides various endpoints for managing products and stocks. Here is a table of the key endpoints and their purposes:

### Product Endpoints

| HTTP Method | Endpoint                          | Purpose                                              |
|-------------|-----------------------------------|------------------------------------------------------|
| GET         | /api/products                     | Retrieves all products                               |
| GET         | /api/products/{id}                | Retrieves a specific product by ID                   |
| POST        | /api/products                     | Creates a new product                                |
| PUT         | /api/products/{id}                | Updates a product's details                          |
| DELETE      | /api/products/{id}                | Deletes a product by setting `IsDeleted` to true     |
| GET         | /api/products/deleted             | Retrieves all products where `IsDeleted` is true     |
| GET         | /api/products/active              | Retrieves all products where `IsDeleted` is false    |

### Stock Endpoints

| HTTP Method | Endpoint                          | Purpose                                              |
|-------------|-----------------------------------|------------------------------------------------------|
| GET         | /api/stocks                       | Retrieves all stock entries                          |
| GET         | /api/stocks/{id}                  | Retrieves a specific stock entry by ID               |
| POST        | /api/stocks                       | Creates a new stock entry                            |
| PUT         | /api/stocks/{id}                  | Updates a stock entry's details                      |
| DELETE      | /api/stocks/{id}                  | Deletes a stock entry by setting `IsDeleted` to true |
| GET         | /api/stocks/deleted               | Retrieves all stock entries where `IsDeleted` is true|
| GET         | /api/stocks/active                | Retrieves all stock entries where `IsDeleted` is false|

## Product-Based Stock Entry Report

This feature allows generating a report that includes the following information:

- **Product ID**: The unique identifier of the product.
- **Product Name**: The name of the product.
- **Stock ID**: The unique identifier of the stock entry.
- **Stock Quantity**: The quantity of the stock entry.
- **Stock Creation Time**: The time when the stock entry was created.

### Example Endpoint

| HTTP Method | Endpoint                           | Purpose                                              |
|-------------|------------------------------------|------------------------------------------------------|
| GET         | /api/products/{productId}/stockreport | Generates a report of stock entries for a product |

## Testing the Project

To test the project, follow these steps:

### Authentication and Authorization

1. **Login as Admin**:
   * Endpoint: `POST /api/authentication/login`
   * Copy the token from the response.
   * JSON Body:
```json
   {
       "email": "admin@admin.com",
       "password": "Admin123!"
   }
```

2. **Authorize**:
   * Click on the "Authorize" button in Swagger.
   * Paste the token and click "Authorize".

3. **Create a Product**:
   * Endpoint: POST /api/products
   * JSON Body:
```json
    {
        "name": "Sample Product"
    }
```

4. **Create a Stock Entry**:
   * Endpoint: POST /api/stocks
   * JSON Body:
```json
    {
        "productId": "uuid-of-sample-product",
        "quantity": 100
    }
```

5. **Generate Product-Based Stock Entry Report**:
   * Endpoint: GET /api/products/{productId}/stockreport

## Running the Project
To run the project, follow these steps:

1. **Clone the repository**:
```sh
git clone https://github.com/yourusername/ProductStockTrackingApp.git
```

2. **Open the solution in Visual Studio**:

3. **Update the connection string in appsettings.json to point to your local SQL Server instance.**

4. **Add migrations**: 
```sh
dotnet ef migrations add Initial
```

5. **Update the migration**:
```sh
dotnet ef database update
```

6. **Build and run the project**.

## Conclusion
The ProductStockTracking App provides a scalable and maintainable solution for tracking products and stock levels. With its layered architecture, modern technology stack, and detailed documentation, it is well-suited for real-world inventory management scenarios.