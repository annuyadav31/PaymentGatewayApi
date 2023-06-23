# PaymentGatewaySolution

This is a payment gateway API built with clean architecture, using .NET Core 6.0. It includes three main layers: Core, Infrastructure, and WebAPI. The Core layer contains all the business logic and domain models, while the Infrastructure layer contains the implementation of database and external services. The WebAPI layer is responsible for handling incoming requests and responses.

## Built With
1. NET Core 6.0 
2. Entity Framework Core - Used for storing data to database
3. Swagger
4. xUnit - lightweight, extensible, and supports parallel testing framework.
5. FluentAssertions - makes it easier to understand what is being tested.
6. AutoFixture - used for generating test objects.
7. Moq - to create mock objects for testing.
8. Serilog - supports structured logging, multiple sinks, and filtering, which makes it easier to diagnose issues in the application. (We have used file sink to store logs inside logs folder of webAPI).

## Features
1. Processing a payment request.
2. Retrieving payment details by transaction ID.
3. Masking the card number of a payment.
4. Exception handling with custom exceptions and logging.

## Getting Started
To run the Payment Gateway API locally, you'll need to have .NET Core 6.0 installed on your machine. Follow the steps below to get started:

1. Clone the repository to your local machine.
```bash
git clone https://github.com/annuyadav31/PaymentGatewayApi.git
```
2. Open the solution in Visual Studio or your preferred IDE or Navigate to root directory of project in command line.
```
cd {ProjectPath}
```
3. Set the PaymentGateway.Api project as the startup project through Visual Studio or command line.
```
dotnet sln set-startup-project src/PaymentGatewaySolution.Api/PaymentGatewaySolution.Api.csproj

```
4. Open Package Manager Console in Visual Studio and Navigate to the PaymentGatewaySolution.Infrastructure project in Visual Studio or Command Line.
```
cd {ProjectPath}
```

5. Set the database for the project using EntityFramework Command.
```
Add-Migration Initial
Update-Database
```

6. Run the project using the F5 key or the "Start" button in Visual Studio or run this command to start the project in command line.
```
dotnet run PaymentGatewaySolution.Api.csproj
```

## API Documentation

The Payment Gateway API uses Swagger to provide API documentation. Once you have the API running, navigate to ```https://localhost:7261/swagger/index.html``` in your web browser to view the documentation. You can also test the API endpoints from the Swagger UI.

## Testing
The Payment Gateway API includes service tests and controller tests using xUnit, FluentAssertion, AutoFixture, Moq. You can run the tests from Visual Studio or by using the ```dotnet test``` command in the terminal.

## Approach
The project was built using a test-driven development approach while implementing services, where tests were written first and then the implementation was developed to satisfy those tests. This approach helped ensure that the code was well-designed and met the requirements, and also provided a safety net to catch any regressions as changes were made.

Before starting the implementation, a visual structure of the application was created using ```draw.io```, which helped provide an overview of the application architecture and ensure that the components were well-defined and properly connected.

The project uses a clean architecture pattern with core, infrastructure, and webApi layers. Logging, API documentation, custom exceptions, EntityFramework DbContext, service tests, and controller tests were also implemented to ensure the quality and reliability of the code.

To start the implementation of service methods, tests were written to cover all the requirements and edge cases. These tests were run initially, and they all failed, providing the "red" signal. The service implementation was then developed to make the tests pass, providing the "green" signal. Finally, the code was refactored and optimized while ensuring that all tests remained passing.

By using this red-green-refactor cycle, we ensured that the code was always working correctly, and that it met the requirements while minimizing the possibility of introducing bugs or regressions.

![VisualStructure](https://github.com/annuyadav31/PaymentGatewayApi/blob/master/src/PaymentGatewaySolution.Api/wwwroot/VisualStructure.jpeg?raw=true)
