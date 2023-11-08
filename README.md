# TaskManager
This repository contains the Task Manager project developed using the Onion Architecture. The project is built on .NET MAUI (Multi-platform App UI) and utilizes SQLite database with Entity Framework ORM.

## Project Description
Task Manager is a simple task management application that allows users to create, view, edit, and delete tasks. The application provides the following functionalities:
* Sign up & Sign in.
* View account details.
* Ability to delete account.
* Create a task with a title, description, and due date.
* View a list of all tasks.
* Edit a task including the title, description, and due date.
* Delete a task.
  
## Installation and Setup
To install and run the Task Manager project, follow these steps:
* Make sure you have the .NET 6 SDK installed.
* Clone the repository to your local machine.
* Open the project in your favorite development environment (e.g., Visual Studio or Visual Studio Code).
* Ensure that all required NuGet packages are installed. If not, restore the packages.
* Configure the SQLite database connection in the appsettings.json file. Specify the path to the database file.
* Run Entity Framework migrations to create the necessary database schema. Execute the command dotnet ef database update in the project directory.
* Run the project.

## Project Structure
The Task Manager project is organized into the following layers based on the Onion Architecture:
* **Domain Layer:** This layer contains the core business logic of the application. It defines the data models, repository interfaces, and services.
* **Infrastructure Layer:** This layer implements the infrastructure-related code, such as repositories and database contexts. It also includes Entity Framework migrations.
* **Application Layer:** The application layer provides the implementation of the services and application logic. It utilizes the repositories and models from the Domain Layer to perform the business operations.
* **Presentation Layer:** This layer is responsible for data presentation and user interaction. It includes user interfaces and views.
  
## IRepository and Unit of Work Pattern
The Task Manager project employs the IRepository pattern for abstracting data access and the Unit of Work pattern for managing database transactions.

* **IRepository:** In the Domain Layer, an IRepository<T> interface is defined for repositories. The repositories in the Infrastructure Layer implement this interface to work with the corresponding database entities. The IRepository provides an abstraction over the data access mechanism and defines standard CRUD (Create, Read, Update, Delete) operations for entities.
* **Unit of Work:** In the Infrastructure Layer, an IUnitOfWork interface is defined as the contract for the Unit of Work. The UnitOfWork class implements this interface and provides methods for managing database transactions. The Unit of Work allows grouping multiple database operations into a single transaction, ensuring data consistency and the ability to rollback changes in case of errors.
