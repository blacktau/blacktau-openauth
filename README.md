# blacktau-openauth
An IOC/DI friendly OpenAuth client library for creating consumer applications written in dotnet core.

Currently only OpenAuth 1.0a is supported but OAuth 2.0 will be along shortly. 

On the To Do list:

* Support OpenAuth 2.0 as a client/consumer
* Publish nuget packages
* Add libraries supporting common DI/IOC containers.
* Add an Asp.net Core Middleware for obtaining authorization and/or authentication
* learn markdown to make this nicer.  

It is not intended for this library to include support for creating a server (service provider/resource) implementation of OAuth. There are plenty of those. 
This project strives to be a simple to use lightweight wrapper aroung HttpClient providing access to OpenAuth protected resources. 