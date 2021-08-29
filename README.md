# ChemWareTest


Code test put together for a 
backend developer role
Using C# .net core, sql server

Approaching this as a single microservice / web API responsible for managing Products
Service is broken into the top layer which receives and responds to API calls
A middle / business / logic layer which applies any specific rules or requirements
And a base / data access layer which focuses on how to query the data out of the sql server.

It is assumed the productService would exist alongside other microservices such as CustomerService, or CheckoutService, 
which would handle user account management and ordering/payment processing respectively.

I've also included a brief database file structure containing schema files, 
a place for updates to be kept as the schema changes, 
and a folder for a 'test' set of data which could be deployed as a known base state to populate a new database

I've included some unit tests for the ProductManager , and it is assumed more tests could be added to get full coverage over the productManagers other methods.
Would also be beneficial to add tests at the data access layer, which I assume would require a slightly more complex database driver to manage the database state
to allow repeatable testing. I've however only had experience with unit tests and UI tests via an extensive Selenium setup.
Ideally for test driven development the tests should be written first, which is a bit easier when you already have a testing project setup.
I wanted to rearrange the code to a form that could be tested, before setting up a test framework to ensure they were compatible to start with.

It was assumed that the ProductService and it's API would mainly be feeding a specific website, and that adding 'view modals' to the backend would be appropriate.
If this API was to be more generic and feed multiple consumers then the 'view modal' approach could be stripped out, 
and the login of matching product details and productType details could be offloaded to the consumer, leaving the API's much thinner.

It is also assumed that 'base classes' would be established, for example moving the DBConnection details to a 'DataAccessBaseClass' 
which other data access classes would inherit from.

Other notable improvements/requirements before this could be deemed production ready would include adding authorisation headers to all the API endpoints
Running auth tokens against a database of user logins, and restricting actions such as edit/delete to only users with the appropriate roles/permissions.
Adding steps to prevent raw strings sent to the API from being run directly against the database, and mechanisms for nicer error catching and reporting, logging etc.
Again depending on who is expected to consume this API, most of the endpoints could be made to return a custom 'payload' which could include data, status messages, error messages, 
and any other additional info which might help to decipher if the 'true' response from a delete request is 'true we deleted it' or 'true, it wasn't there anyway'

Various versions might be a bit out of date, I've not used this laptop for development in the last 2 years and didn't want to spend the whole week updating everything.
But obviously in any work environment updating versions, packages, plugins, etc needs to be part of the scheduled dev cycle otherwise it never happens until it's urgent/too late.

I've used entity framework previously, but it can be a bit cumbersome, so I gave dapper a try. Not sure I've used it to it's fullest, but hopefully avoided lazy loading performance dramas. 

Didn't get around to adding the UI, managed to create a react 'hello world', and comfortable enough working away at it, but ran out of time to include anything worth showing.
Have worked as a full stack dev for a number of years and very capable of building basic / utilitarian UI , but usually have the advantage of not needing to start from scratch.

Regarding the other 'expectations' advised:
ProductService loads to swagger ui by default; obviously something to change in any prod environment.
ReSharper I thought was just a visual studio plugin , had colleagues that used it and usually they just complained it slowed down their IDE, but they did have a sizable monolith to load up.
Linting, not something i've explicitly used, but had a little experience with UI scripting languages and their 'build' process
No experience with prettier
Some experience with unit test code coverage and ensuring all paths of execution are tested
Currently working in devOps, not really sure what kind of devOps you want shown in a test project like this? CI/CD to a test server?


Many Thanks for you time
Comments, questions, criticism and advice welcome

