# NCStudio.Utility
This project contains common utilities for coding and testing.

****
# Mvc:
### Middleware
+ **`ResponseExceptionMiddleware & UseResponseExceptionMiddlewareExtenstions`**:

    Catch exception thrown by internal server and response it to api client.
    
    Usage: `app.UseResponseException()`

### Data
+ **`NCDbContext & INCDbContext`**:

    Implement LoadNavigationProperty & MarkAsDeleted\<T\>
****
# Testing:
### Mocking
+ **`DbSet<T> GetMockDbSet<T>(List<T> sourceList)`** :
    
    Get a DbSet mocking object for testing.

### Jsonning
+ **`string SerializeJsonObjectWithCamelCasePropertyNames(object target)`**:
    
    Serial an object to json string with camel case properties.

+ **`JObject ConvertToCamelCasePropertyJObject(object target)`**:
    
    Convert an object to JObject with camel case properties.

+ **`JObject ConvertToJObject(object target)`**:
    
    Convert an object to JObject

+ **`bool EqualsOrThrows<T>(T expected,T actual)`**:
    
    Return true is json strings equal, throws if not.

### HttpTesting
+ **`Task<ResponseResult> GetAsync(string uri)`**:

    Make a Get request and return a ResponseResult.

+ **`Task<ResponseResult> PostAsync(string uri,string content,Encoding encoding=null,string mediaType="application/json")`**:
    
    Make a Post request and return a ResponseResult.

+ **`Task<ResponseResult> PutAsync(string uri, string content, Encoding encoding = null, string mediaType = "application/json")`**:

    Make a Put request and return a ResponseResult.

+ **`Task<ResponseResult> DeleteAsync(string uri)`**:

    Make a Delete request and return a ResponseResult.
****