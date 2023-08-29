# MicroserviceExample
Very simple demonstration of Microservice architecture using ASP.NET Core.
For building and deploying into Docker Containers on Windows on your local machine, use the following commands in PowerShell:

In /PlayerManagementService/:
  docker build -t player-management-service .
  docker run -d -p 5224:80 --name player-service player-management-service
  
In /GameManagementService/:
  docker build -t game-management-service .
  docker run -d -p 5074:80 --name game-service game-management-service

Prerequisites: You have already installed and set up Docker Desktop.

Refer to the endpointmethods on how to call them with Postman or similar in each Controllerclass of each Service, there is only 1 per Service.
