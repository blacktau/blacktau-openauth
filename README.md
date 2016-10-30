blacktau-openauth
=================

An IOC/DI friendly OpenAuth client library for creating consumer applications written in dotnet core.

Currently only OpenAuth 1.0a is supported but OAuth 2.0 will be along shortly. 

On the To Do list in no particular order:

* Add OAuth 2.0 support to the Asp.net Core middelware. 
* Improve exception handling.
* Increase Test coverage
* Add more DI container support.
* better documentation. 
* more examples of usage in the WebTest project. 
* more complete examples of usage in the WebTest project. 
* more resource providers. 
* default IStateStorageManager implementation needed.

It is not intended for this library to include support for creating a server (service provider/resource) implementation of OAuth. There are plenty of those. 
This project strives to be a simple to use lightweight wrapper aroung HttpClient providing access to OpenAuth protected resources. 

Projects
--------

### Blacktau.OpenAuth.Client

The main library. This can be used directly but it is recommended that one of the IOC container integration libraries is used as they ensure everything is wired correctly. 

### Blacktau.OpenAuth.Containers.Basic 

A library to use when an IOC container is not available or needed. This is a hard wired library with an implementation of OpenAuthClientFactory that creates its own dependencies. 

### Blacktau.OpenAuth.Client.Containers.ServiceCollection 

A library making use of the ServiceCollection default IOC/DI implementation in asp.net core.  

### Blacktau.OpenAuth.AspNet.Authorization

The core Middleware implementation. This can be used directly by itself but all details for a specific resource provider will need to be supplied to the Options when calling UseOAuthAuthorization in IAppBuilder.

### Blacktau.OpenAuth.AspNet.Authorization.Twitter 

The Twitter specific Authorization example.  

### Blacktau.OpenAuth.AspNet.SessionStateStorage 

An implementation of the IStateStorageManager (required for the normal functioning of `Blacktau.OpenAuth.AspNet.Authorization`) that makes use of Asp.Net Core Sessions. 

Example Solution
---------------- 

### Blacktau.OpenAuth.Client.TestHarness 

A sample command line application using `Blacktau.OpenAuth.Containers.Basic` in order to make simple calls to [Tumblr API](https://www.tumblr.com/docs/en/api/v2), [Twitter API](https://dev.twitter.com/rest/public) and [Facebook GraphAPI](https://developers.facebook.com/docs/graph-api) 
You will need to supply your own Consumer Keys, Consumer Secrets, Access Tokens and Access Token Secrets. 
The first two you will need to obtain from the respective resources by registering your application and the second two by obtaining Authorization from those APIs. In the case of OAuth2 (Facebook/Google) you will need only an AccessToken as there is no access token secret in OAuth 2.

### Blacktau.OpenAuth.AspNet.WebTest

A simple Asp.Net core web application making use of `Blacktau.OpenAuth.AspNet.Authorization` and `Blacktau.OpenAuth.AspNet.Authorization.Twitter` in order to demonstrate the Asp.Net core Middleware integration and usage for obtaining authorization from twitter for accessing a users feed.
As with the TestHarness above, you will need to supply your own Consumer Keys and Consumer Secrets, but Access Tokens and Access Token Secrets are obtained by the middelware.

Usage of Client
---------------

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