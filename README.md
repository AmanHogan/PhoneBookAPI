# Aman Hogan-Bailey
#### April 15, 2023
## API PhoneBook Project

This is a simple API for managing a phone book. It is built with ASP.NET Core and utilizes Swagger/OpenAPI for API documentation.

### Prerequisites
To run this project, you will need:
- Visual Studio 2019 or later
- .NET 5.0 or later

### Getting Started
To get started, clone the project and open it in Visual Studio. Once opened, build the project and run it.
You can build the project by clicking on the `.sln` file in the solution explorer.

### How to Run Using Docker
1. Make sure the Docker Daemon is running.
2. Open the terminal.
3. Navigate to the folder containing the `Dockerfile`.
4. Run the following commands in the terminal:
   - `docker build -f Dockerfile --force-rm -t myapp/test .`
   - `docker run -d -p 8080:80 myapp/test`
5. Navigate to the webpage: http://localhost:8080/swagger/index.html
6. You should then see the Swagger UI.

#### Alternate way to run docker
1. Open up the terminal in the directory containing the `myapptest.tar` file.
2. Run this command: `docker load -i myapptest.tar`
3. Navigate to the webpage: http://localhost:8080/swagger/index.html
4. You should then see the Swagger UI.

### Major Components
- PhoneBook
  - `DictionaryPhoneBookService.cs`
  - `Program.cs`
  - `Logger.cs`
  - `PhoneBookController.cs`
  - `LogFile.txt`
  - `myDatabase.txt`

### Usage
The API has the following endpoints:

- GET /PhoneBook/list: This endpoint returns a list of all phone book entries.
- POST /PhoneBook/add: This endpoint adds a new phone book entry to the phone book.
- PUT /PhoneBook/deleteByName: This endpoint deletes a phone book entry by name.
- PUT /PhoneBook/deleteByNumber: This endpoint deletes a phone book entry by phone number.

**You can test these endpoints using a tool such as Postman or by using the Swagger UI. The Swagger UI can be accessed by navigating to https://localhost:<port>/swagger/index.html, where <port> is the port number on which the API is running.**

### API Documentation
API documentation is generated using Swagger/OpenAPI. To view the documentation, navigate to https://localhost:<port>/swagger/index.html, where <port> is the port number on which the API is running.

### Dependencies
This project has the following dependencies:
- Microsoft.AspNetCore.Mvc version 5.0.0
- Microsoft.Extensions.DependencyInjection version 5.0.0
- Microsoft.OpenApi.Models version 1.0.0
- PhoneBook.Services (custom package) version 1.0.0
- PhoneBook.Exceptions
- PhoneBook.Model
- PhoneBook.Services
- PhoneBook.Logging

### Side Note
The tar file was not included in the repository to reduce size, so the project cannot be run with Docker.
  

# Unit Testing How To
### Folder Contents
This folder contains two collection files:
- PhoneBookUnitTest
  - PhoneBook API.postman_collection.json
  - PhoneBook Tests Demo.postman_collection.json

A Collection Format JSON file in Postman is a file that contains a collection of HTTP requests, each with its own set of properties and test scripts, organized into folders and sub-folders. It allows you to share your collection of requests with others, backup your requests, or import them into another Postman instance.

### How to Run
To run the API tests using Postman, follow these steps:

1. Open Postman and click on the "Import" button located in the top left corner of the window.
2. Select the "Import From File" option.
3. Browse your computer to find the Collection JSON file that you want to import.
4. Select the file and click on "Open".
5. Postman will import the collection and display it in the "Collections" tab.
6. You can now use the imported collection to send requests to the API and run tests on the responses.

Enjoy testing your API using Postman!
