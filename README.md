# blacktau-openauth
An IOC/DI friendly OpenAuth client library for creating consumer applications written in dotnet core.

Currently only OpenAuth 1.0a is supported but OAuth 2.0 will be along shortly. 

On the To Do list in no particular order:

* Support OpenAuth 2.0 as a client/consumer
* Publish nuget packages
* Add libraries supporting common DI/IOC containers.
* Add an Asp.net Core Middleware for obtaining authorization and/or authentication
* Improve exception handling.

It is not intended for this library to include support for creating a server (service provider/resource) implementation of OAuth. There are plenty of those. 
This project strives to be a simple to use lightweight wrapper aroung HttpClient providing access to OpenAuth protected resources. 

## Projects

### Blacktau.OpenAuth

The main library. This can be used directly but it is much easier to use on of the IOC container integration libraries that ensure everything is wired correctly. 

### Blacktau.OpenAuth.Basic 

A library to use when you don't want an IOC container. This is a hard wired library with an implementation of OpenAuthClientFactory that creates its own dependencies. 

### Blacktau.OpenAuth.TestHarness 

A sample command line application using Blacktau.OpenAuth.Basic in order to make simple calls to [Tumblr API](https://www.tumblr.com/docs/en/api/v2) and [Twitter API](https://dev.twitter.com/rest/public).
You will need to supply your own Consumer Keys, Consumer Secrets, Access Tokens and AccessTokenSecrets. 
The first two you will need to obtain from the respective resouces by registering your application and the second two by obtaining Authorization from those APIs. 

## Usage

### Inversion of Control Container Free Usage

Use the Blacktau.OpenAuth.Basic package. 

1. Reference and add usings for *Blacktau.OpenAuth.Basic* and *Blacktau.OpenAuth*
2. New up an instance of *ApplicationCredentials* and set the *ApplicationKey* and *ApplicationSecret*
3. New up an instance of *AuthorizationInformation* and set its *AccessToken* and *AccessTokenSecret* properties.
4. Using the results of steps 1 and 2 new up an *OpenAuthClientFactory*.
5. Using the *OpenAuthClientFactory* instance create an *OpenAuthClient*.
6. Add Body (for POST requests) or Query (for GET requests) parameters
7. Invoke the async Execute method and do something with the result. 

```cs

var applicationCredentials = new ApplicationCredentials();
applicationCredentials.ApplicationKey = "YourApplicationKey";
applicationCredentials.ApplicationSecret = "YourApplicationSecret";

var authorizationInformation = new AuthorizationInformation("YourAccessToken");
authorizationInformation.AccessTokenSecret = "YourAccessTokenSecret";

var openAuthClientFactory = new OpenAuthClientFactory(applicationCredentials, authorizationInformation);

var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/statuses/user_timeline.json", HttpMethod.Get, OpenAuthVersion.OneA);

openAuthClient.AddQueryParameter("screen_name", "blacktau");
openAuthClient.AddQueryParameter("count", "2");

var result = await openAuthClient.Execute();

Console.WriteLine(result);

```