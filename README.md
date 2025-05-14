# Simple-Auth-System

# Authentication System

A simple authentication system built with ASP.NET Core 8.0, following Onion Architecture and Repository Pattern. This system provides user registration, login, and token validation, using JWT for secure authentication.

## Project Structure

This project follows the Onion Architecture with the following layers:

### Domain Layer (`Simple_Authentication_System_Domain`)
- Contains the core domain entities and interfaces
- Defines contracts for repositories and services
- No dependencies on other project layers

### Application Layer (`Simple_Authentication_System_Application`)
- Contains application business logic and services
- DTOs (Data Transfer Objects) for communication
- Depends on the Domain layer

### Infrastructure Layer (`Simple_Authentication_System_Infrastructure`)
- Implementation of domain interfaces
- Database context and migrations
- External service integrations (Password hashing, JWT token services)
- Depends on Domain and Application layers

### API Layer (`Simple_Authentication_System_API`)
- ASP.NET Core Web API controllers
- Middleware configuration
- Depends on Application and Infrastructure layers

## Features

1. **User Registration**
   - Securely stores user credentials with BCrypt password hashing
   - Validates user input
   - Returns JWT token upon successful registration

2. **User Login**
   - Validates credentials against stored data
   - Returns JWT token upon successful authentication
   - Updates last login timestamp

3. **Token Validation**
   - JWT middleware to protect endpoints
   - Endpoint to validate token status

4. **Current User Details**
   - Protected endpoint to retrieve current user information

## Technical Implementation

### Database
- PostgreSQL database via Entity Framework Core

### Authentication
- JWT (JSON Web Tokens) for stateless authentication
- Configurable token expiration
- BCrypt for secure password hashing

### API Security
- Input validation
- Protected routes with authentication middleware
- Proper error handling

## Setup and Configuration

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL server
- Visual Studio 2022 or VS Code

### Setup Steps

1. **Clone the repository**
   ```
   git clone <repository-url>
   cd AuthSystem
   ```

2. **Update connection string**
   
   Edit the connection string in `AuthSystem.API/appsettings.json` to match your PostgreSQL server:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=Simple_Auth_Service;Username=postgres;Password=your_password"
   }
   ```

3. **Update JWT settings**
   
   Update the JWT configuration in `AuthSystem.API/appsettings.json`:
   ```json
   "Jwt": {
     "Key": "Add secret key here, the one in the appsettings can as well serve",
     "Issuer": "AuthSystem",
     "Audience": "AuthSystemClient",
     "ExpiryInDays": 7
   }
   ```

4. **Swagger**
   ```
   Update to 0 or 1 to call up swagger on the application or not
     "PUBLISH_API_SWAGGER": 1,

   ```

5. **Run the application**
   ```
   dotnet run
   ```

   The API will be available at:
   - https://localhost:7001/swagger - for Swagger UI
   - https://localhost:7001/api/auth/ - for direct API access

## API Endpoints

### Public Endpoints

- **POST /api/auth/register**
  - Register a new user
  - Request body: `{ "username": "user", "email": "user@example.com", "password": "password", "confirmPassword": "password" }`
  - Returns: User details and authentication token

- **POST /api/auth/login**
  - Authenticate a user
  - Request body: `{ "email": "user@example.com", "password": "password" }`
  - Returns: User details and authentication token

### Protected Endpoints (Require Authentication)

- **GET /api/auth/me**
  - Get current user details
  - Headers: `Authorization: Bearer {token}`
  - Returns: User details

- **GET /api/auth/validate**
  - Validate the current token
  - Headers: `Authorization: Bearer {token}`
  - Returns: Token validity status

## Testing

A Postman collection is included in the repository to test all endpoints. Import the collection to Postman and use the following steps:
## Postman route is below, check this file path in the project to get to postman collection

## Simple-Auth-System\Simple_Authentication_System\Auth System API.postman_collection.json"
1. Register a new user using the "Register User" request
2. Login using the "Login" request (token will be automatically saved to the collection variables)
3. Test protected endpoints using the "Get Current User" and "Validate Token" requests

