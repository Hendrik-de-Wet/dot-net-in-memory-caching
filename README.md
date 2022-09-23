# In-Memory Caching in .NET

<img src="https://github.com/Hendrik-de-Wet/dot-net-in-memory-caching/blob/main/dot-net-in-memory-caching/Resources/Images/dot-net-in-memory-caching.jpg"/>

## Introduction

I recently explored how to implement In-Memory caching in an ASP.NET Core application to improve the performance of data access. This works best with data that doesn’t change frequently but takes some time to populate. It has some limitations that one needs to be aware of and one should also consider the pros and cons of In-Memory caching before implementing it.

With ASP.NET Core, it is now possible to cache data within the application. This is known as In-Memory Caching in ASP.NET Core. The Application stores the data on to the server’s instance which in turn drastically improves the application’s performance. This is probably the easiest way to implement caching in your application.

## Getting Started

Whilst there are many detailed tutorials available on this topic, my aim is to only highlight the steps taken to implement this feature with minimal explanation. Refer to the solution for more details.

In-Memory Caching in ASP.NET Core is a Service that should be registered in the service container of the application in the Startup.cs class. 

```
services.AddMemoryCache();
```
In the Trail Controller we invoke a service to get all the trail records from a list.

We define IMemoryCache to access the in-memory cache implementation and inject the IMemoryCache to the constructor.
 
Next, we create a simple endpoint that can configure the cache options, set and get the cache. 

### Configuring the Cache Options

<b>MemoryCacheEntryOptions</b> – This class is used to define the crucial properties of the concerned caching technique; 

* <b>Priority</b> – Sets the priority of keeping the cache entry in the cache.  Other options are High, Low and Never Remove.

* <b>Size</b> – Allows you to set the size of this particular cache entry, so that it doesn’t start consuming the server resources.

* <b>Sliding Expiration</b> – A defined Timespan within which a cache entry will expire if it is not used by anyone for this particular time period.

* <b>Absolute Expiration</b> –  With Absolute expiration, we can set the actual expiration of the cache entry.


### Endpoint to Get / Set Cache in Memory

GetCache method of the CacheController is a GET Method that accepts key as the parameter. It’s a simple method where you access the memoryCache object and try to get a value that corresponds to the entered key. 

```
_memoryCache.TryGetValue("trail", out Trail trail)
```

The POST Method will be responsible for setting the cache. Now how cache works is quite similar to a C# dictionary. That means you will need 2 parameters, a key, and a value. We will use the key to identify the value (data).

```
_memoryCache.Set(id, trail, cacheEntryOptions); 
```

## Testing

In order to test the In-Memory Caching, set a break point inside the GetTrail method in our GetTrailService.cs service and test the API end point via Postman / Swagger. Take note of the expiration timespan values whilst testing. Supply the Id parameter value e.g. 1 and watch as it reaches the service to retrieve the data.  Since the entry was not found in the cache, we proceed to fetch the data from the list. Do this again and you will notice the data is retrieved from the cache.


