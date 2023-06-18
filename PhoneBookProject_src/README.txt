Aman Hogan-Bailey
04/15/2023
API PhoneBook Project

******PhoneBook API **************************************************************************************
This is a simple API for managing a phone book. It is built with ASP.NET Core and utilizes Swagger/OpenAPI for API documentation. 

Prerequisites *******************************************************************************************
To run this project, you will need:
	- Visual Studio 2019 or later
	- .NET 5.0 or later

Getting Started *****************************************************************************************
To get started, clone the project and open it in Visual Studio. Once opened, build the project and run it.
You can build the project by clicking on the .sln file in the solution explorer.

How to Run Using Docker
1.	Make sure the Docker Daemon is running.
2.	Open the terminal.
3.	Navigate to the folder containing the Dockerfile
4.	Run the following commands in the terminal:
a.	docker build -f Dockerfile --force-rm -t myapp/test .
b.	docker run -d -p 8080:80 myapp/test
5.	Navigate to the webpage: http://localhost:8080/swagger/index.html
6.	You should then see the swagger UI
Alternate way to run docker
1.	Open up terminal in the directory containing the myapptest.tar
2.	Run this command: docker load -i myapptest.tar
3.	Navigate to the webpage: http://localhost:8080/swagger/index.html
4.	You should then see the swagger UI.



Major Components ****************************************************************************************
	- PhoneBook
		- DictionaryPhoneBookService.cs
		- Program.cs
		- Logger.cs
		- PhoneBookController.cs
		-LogFile.txt
		-myDatabase.txt

Usage ****************************************************************************************

The API has the following endpoints:

	- GET /PhoneBook/list: This endpoint returns a list of all phone book entries.
	- POST /PhoneBook/add: This endpoint adds a new phone book entry to the phone book.
	- PUT /PhoneBook/deleteByName: This endpoint deletes a phone book entry by name.
	- PUT /PhoneBook/deleteByNumber: This endpoint deletes a phone book entry by phone number.


*** You can test these endpoints using a tool such as Postman or by using the Swagger UI. The Swagger UI can be accessed by navigating to https://localhost:<port>/swagger/index.html, where <port> is the port number on which the API is running. ***


API Documentation ****************************************************************************************

API documentation is generated using Swagger/OpenAPI. To view the documentation, navigate to https://localhost:<port>/swagger/index.html, where <port> is the port number on which the API is running.

Dependencies ****************************************************************************************

This project has the following dependencies:

	- Microsoft.AspNetCore.Mvc version 5.0.0
	- Microsoft.Extensions.DependencyInjection version 5.0.0
	- Microsoft.OpenApi.Models version 1.0.0
	- PhoneBook.Services (custom package) version 1.0.0
	- PhoneBook.Exceptions
	- PhoneBook.Model
	- PhoneBook.Services
	- PhoneBook.Logging


