# EHI Contact API 

The solution is develop using Visual Studio 2019 Community Free Edition. It uses .Net Core 3.1 Framework.
Dependancy Injection has been used to inject the Dependancies.
Unit Tests has been added for Controller and Service methods.

###### Code Flow

Controller calls the Service class -> Service calls the Data provider class -> Data Provider fetches the Actual Data from Database. 
All these dependancies have been injected using DI engine.

###### Project Structure -

###### EHIContactAPI - 
WEB API Project having two Controllers viz. Healthcheck Controller and Contact Controller.  
HealthCheck Controller has only one method which returns Status OK. This endpoint is useful to check the Service Status in Production. Also can be used by Production Monitoring tools the check Health of the Service.
ContactController has multiple endpoints to Get, update, add and delete contacts. Contact Service has been injected in this controller to do the various operations. Also has .Net Core Logger service to do basic logging. 

###### EHIContactAPI.Services -
Class Library Project having Contact Service which implements IContactService Interface. Contact Data Provider has been injected into this service to get data from Contact Database.

###### EHIContactAPI.Providers -
Class Library Poject having InMemory Contact Data Provider and SQL Contact Data Provider. Both implements IContactDataProvider Interface.
For testing purpose In Memory Provider has been implemented fully which uses MemoryCache. As I don't get SQL server install on local machine I did not implement SQL server provider.

###### EHIContactAPI.Providers.Models - 
This contains Contact Model class and Status Enum.

###### EHIContactAPI.Tests -
Unit Test Projects having multiple Tests methods to test the Contact controller endpoints.

###### Endpoints Details -
For Testing purpose All the endpoints are Not having any authentication/authorization. But we can easily implement various type authentication like Oauth, Jwt, SAML depennds on client requirement.

###### How to Run - 

Press F5 to run the Web API Project using inbuilt IIS Express. Accept the SSL certificate warning to proceed further. Default Healtcheck endpoint will be invovked - https://localhost:44311/healthcheck

###### Contact Endpoints -

###### Get All Contacts - 
Request Type - GET.
Url - https://localhost:44311/api/contact/getall
Response - All the Active Contacts available in the Memory Cache.

###### Add New Contact -
Request Type - POST
URL - https://localhost:44311/api/contact/addnew
Sample Payload - {
  "FirstName":"Test fname",
  "ddd":"dd",
  "LastName":"test lname",
  "Email" : "email@ema.com",
  "PhoneNumber" : 1234567,
  "Status" : 0
}

Response - Returns the Added Contact Object along with random generated Contact ID.
{
"contactId": 1744924498,
"firstName": "Test fname",
"lastName": "test lname",
"email": "email@ema.com",
"phoneNumber": 1234567,
"status": 0
}

###### Delete Contact - Basically it sets the Status to Inactive (1)
Request Type - PATCH
Request URL - https://localhost:44311/api/contact/delete
PayLoad - Contact ID
Sample Payload - { 1 }
Response - Returns Contact deleted successfully response message.

###### Update Contact - Update the existing contact
Request Type - PATCH
Route - "update/{contactId}"
Request URL - https://localhost:44311/api/contact/update/1
Sample payload - 
{
  "FirstName" : "from Update"
}

Sample Response - Returns the entire updated contact object
{
"contactId": 1,
"firstName": "from Update",
"lastName": "Boner",
"email": "john.b@ehi.com",
"phoneNumber": 730456000,
"status": 0
}
