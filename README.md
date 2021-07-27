# Payment Gateway

This project exposes API gateway that returns pokemon information.

## Framework and Tools
ASP.NET Core 5.0 
Visual Studio 2019


## WebAPI
	Contains controller class and API setup 

## Domain
	Contains data contracts, Services and Helper classes.

## Tests
	Contains test cases for services and controllers. 

## Running in Docker
  - Go to project root directory 
  - run "docker build -t gateway -f Dockerfile ."
  - Once image is ready, run "docker run -d -p 8080:80 --name pokedex gateway"
  
## Running in Visual Studio 
- Open solution file in Admin Mode 
- Hit F5 to run application in debug mode