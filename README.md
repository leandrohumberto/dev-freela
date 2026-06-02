# DevFreela
> A robust freelance project management API built with ASP.NET Core, facilitating project creation, user management, skill assignment, and secure authentication.

## 🚀 Features
-   **User Management:** Register, retrieve, and manage user profiles, including assigning skills.
-   **Project Management:** Create, update, delete, search, start, complete, and comment on freelance projects.
-   **Authentication & Authorization:** Secure user login with JWT (JSON Web Token) and role-based authorization (Client/Freelancer roles).
-   **Password Reset:** Functionality to request and reset user passwords securely.
-   **API Documentation:** Interactive API documentation powered by Swagger/OpenAPI.
-   **Global Error Handling:** Centralized exception handling for consistent API responses.
-   **Correlation ID:** Middleware for tracking requests across logs.
-   **Email Service Integration:** Placeholder for sending emails (e.g., password reset notifications) via SendGrid.

## 🛠️ Tech Stack
-   **Core Technologies:** C# 12, .NET 8, ASP.NET Core
-   **Data & Storage:**
    -   Entity Framework Core (ORM)
    -   SQL Server (Primary Database)
    -   In-Memory Database (for development/testing environments)
-   **Key Libraries:**
    -   **MediatR:** For implementing CQRS (Command Query Responsibility Segregation) pattern.
    -   **FluentValidation:** For robust request validation.
    -   **Serilog:** For structured logging across the application.
    -   **Swashbuckle.AspNetCore:** Generates interactive API documentation (Swagger UI).
    -   **Microsoft.AspNetCore.Authentication.JwtBearer:** Handles JWT-based authentication.
    -   **BCrypt.Net-Next:** Secure password hashing.
    -   **SendGrid.Extensions.DependencyInjection:** Integration for email sending.
    -   **Bogus:** (Dev Dependency) For generating realistic fake data in tests.
    -   **FluentAssertions:** (Dev Dependency) For readable assertion syntax in tests.
    -   **Moq/NSubstitute:** (Dev Dependencies) Popular mocking frameworks for unit testing.
    -   **xunit:** (Dev Dependency) The unit testing framework.

## 📋 Prerequisites
Before you begin, ensure you have the following installed:
-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Docker Desktop](https://www.docker.com/products/docker-desktop) (Optional, but recommended for containerized development)
-   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any compatible SQL database, for production environment)

## ⚙️ Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd DevFreela/src/
```

### 2. Database Setup
The application can be configured to use either SQL Server or an In-Memory database.

#### Using SQL Server (Recommended for Development/Production)
1.  **Update Connection String:** Open `DevFreela.API/appsettings.json` and ensure the `DevFreelaCs` connection string points to your SQL Server instance.
    ```json
    "ConnectionStrings": {
      "DevFreelaCs": "Server=localhost\\SQLSERVER17_DEV;Database=DevFreelaDb;Trusted_Connection=True;trustServerCertificate=true"
    }
    ```
    *Note: Adjust `Server` and `Database` as per your setup.*

2.  **Apply Migrations:** Navigate to the `DevFreela.API` project directory and run the following commands to create the database schema:
    ```bash
dotnet ef database update --project ../DevFreela.Infrastructure/DevFreela.Infrastructure.csproj
```

#### Using In-Memory Database (for quick local testing)
If no connection string is provided, the application will default to an in-memory database. No further setup is required for this option, but data will not persist between runs.

### 3. API Configuration
-   **JWT Secret Key:** In `DevFreela.API/appsettings.json`, update the `Jwt:Key` with a strong, secret key.
    ```json
    "Jwt": {
      "Key": "YOUR_SUPER_SECRET_KEY_HERE_MIN_16_CHARS",
      "Issuer": "DevFreela.API",
      "Audience": "DevFreela.App"
    }
    ```
-   **SendGrid API Key:** If you plan to use email functionalities (e.g., password reset), configure your SendGrid API key in `DevFreela.API/appsettings.json`.
    ```json
    "SendGrid": {
      "ApiKey": "YOUR_SENDGRID_API_KEY",
      "FromEmail": "no-reply@devfreela.com",
      "FromName": "DevFreela"
    }
    ```

### 4. Run the Application

#### Local Run
Navigate to the `DevFreela.API` project directory:
```bash
cd DevFreela/src/DevFreela.API
dotnet run
```
The API will typically run on `https://localhost:7214` and `http://localhost:5264`. Swagger UI will be available at `https://localhost:7214/swagger`.

#### Docker
To run the application using Docker:
1.  Navigate to the solution root directory (`DevFreela/src/`).
2.  Build the Docker image:
    ```bash
docker build -t devfreelaapi -f DevFreela.API/Dockerfile .
```
3.  Run the Docker container:
    ```bash
docker run -p 8080:8080 -p 8081:8081 devfreelaapi
```
    The API will be available on `http://localhost:8080` (HTTP) and `https://localhost:8081` (HTTPS). Swagger UI will be at `https://localhost:8081/swagger`.

## 🧪 Running Tests
The project includes unit tests. To run them:
Navigate to the `DevFreela.UnitTests` project directory:
```bash
cd DevFreela/src/DevFreela.UnitTests
dotnet test
```

## 📚 API Endpoints
The API documentation is available via Swagger UI. Once the application is running, navigate to `/swagger` (e.g., `https://localhost:7214/swagger`) in your browser to explore all available endpoints and interact with the API.

## 🤝 Contributing
Contributions are welcome! Please feel free to open issues or submit pull requests.

## 📄 License
This project is licensed under the terms of the MIT license.