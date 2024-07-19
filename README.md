# Aspire.Hosting.ExternalEndpoint
Adds the possibility to add external endpoints as resources which can be easily referenced.

## Overview
An external endpoint is just a URL which is reachable from wherever you are running your application. This can be a URL to a REST API, a website, a file, etc. The idea is to have a single place where you can define these URLs and then reference them in your code. This way, if the URL changes, you only have to update it in one place.

## Usage
The external endpoint takes part in service discovery, so any `HttpClient` can simply reference the external endpoint by name just as if you would reference another project.
Alternatively, you can use the `ServiceEndpointResolver` from `Microsoft.Extensions.ServiceDiscovery` to get the URL in your `MyApplication` if you do not want to use `HttpClient`.

```csharp
var externalApi = builder.AddExternalEndpoint("ExternalApi", "https://api.example.com");

var myProject = builder.addProject<MyApplication>("MyApplication")
	.WithReference(externalApi);

```
