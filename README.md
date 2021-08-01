# SolutionName: ApplicatonProcess.Application
This Project includes a .net 5 api and a aurelia app.
## Projects:
### ApplicatonProcess.Data 
    - .Net 5.0 Class Library handles every DataAccess also HTTPDataAccess for assets, Uses RepositoryPattern and UnitOfWork Pattern
### ApplicatonProcess.Web 
    - .Net 5.0 Kestrel Host, Hosts the static content as well as a WebAPI for the Frontend
### ApplicatonProcess.Domain 
    – .Net 5.0 Class Library Containing Business Logic, Validators, Interfaces and Models
## The Api should have the following actions:
### POST for Creating an Object 
    – returning an 201 on successful creation of the object and the url were the object can be called
### GET with id parameter 
    – to ask for an object by id
### PUT 
    – to update the object with the given id
### DELETE 
    – to delete the object with the given id
## Rules:
- The WebApi accepts and returns application/json data.
- The object and the properties should be validated by fluentValidation ( nuget ) with the following rules:
AssetName – must be an existing asset (Show the user in the frontend only assets which are existing on the api endpoint: https://api.coincap.io/v2/assets but the ui shouldn’t be slow)
- If the object is invalid ( on post and put ) – return 400 and an information what property does not fullyfy the requirements and which requirement is not fullyfied.
- Describe the API with swagger therefore use Swashbuckle host the swaggerUI under [localhost]/swagger.
- Provide example data in the SwaggerUI, so when someone click on try it out there is already useful valid data in the object that can be posted.
- For all strings, use localization and a Jsonfile as resource file.
- To save the data use entityframework core 5.0 and entityframework in memory database.
- Use autofac for dependency injection
## Frontend:
The including Form must be an Aurelia Application which uses the API to Post Data AND Validate all the inputs with the exact same parameters as the API does.
- use Typescript
- use Webpack
- Form can only be send if the data is valid
- Use Boostrap for the UI
- Use aurelia-validation
- Use a Bootstrap FormRenderer
- invalid fields must be marked with an red border and an explanation why the date is invalid
- all strings must be using i18next
- the form has two buttons- send and reset.
- clicking the reset button an aurelia-dialog is shown - which ask if the user is really sure to reset all the data
- the reset button is only enabled if the user has typed in data -> if all fields are empty the reset button is not enabled.
- when the user has touched a field but afterwards deleted all entries, the reset button is also not enabled.
- The send button is only active if all required fields are filled out and are valid.
- after sending the data, the aurelia router redirects to a view which confirms the sending and shows the user all his created assets.
- if the sending was not successful an error message is shown in a aurelia-dialog. Describing what was going wrong.

## Run the app with docker
### Installation
For the project shoud be installed docker and nodejs
- Docker installation, go to https://www.docker.com/get-started
- nodejs installation, go to https://nodejs.org/en/download/

### Build for production
- Go to folder "ApplicatonProcess.Application\AureliaApp", then run `npm install`, then run `npm run build`
### Run 
- Go to folder "ApplicatonProcess.Application", then run `docker-compose up`, then open `http://localhost:5000` 

