# ModernNotes
This is how I solved the developer assignment.

## Running the project
Please ensure that both ModernNotes.Web and ModernNotes.WpfClient are set as startup projects. 
The client will try to connect to the web api using port 60194 (hardcoded).

## Project structure

* **ModernNotes.Web** is the RESTful web api. It uses Sqlite as a store.
* **ModernNotes.WpfClient** is a very simple MVVM client.
* **ModernNotes.IntegrationTests** are tests on the web api. It is currently 
using the first store implementation (an in-memory store).
* **ModernNotes.Specs** are BDD style tests using SpecFlow. The plan was to write 
GUI tests using White and have them running against the web api, but there was not enough
time. So I ended up testing the ViewModel instead.

## Swagger Documentation
I've used Swashbuckle to generate Swagger documentation. The JSON endpoint is 
available at http://localhost:60194/swagger/v1/swagger.json.

I've also added Swagger UI, available at http://localhost:60194/swagger.
