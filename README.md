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

### Behaviors
+ **`ExceptionBehavior`**:

    Implement Exception handling in Mediatr pipeline

+ **`ValidationBehavior`**:

    Implement Command Validation in Mediatr pipeline

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

### DockerStartupFixture

Base class for xUnit Startup Fixture

+ **`string GetApi(string path,string port)`**:

    Construct Api string

+ **`void ActOnDb<T>(DbContextOptions dbOptions,Action<T> act)`**:

    Inject data to Database

+ **`void runSSHCommand(string commandText)`**:

    Run ssh commands

+ **`void runProcess(string fileName, string arguments)`**:

    Run process

+ **`async Task<bool> WaitForService(string uri,int timeoutSeconds=60)`**:

    Check service availability

+ Usage:
    + Set Envirionment Parameters:
        + INTEGRATIONTEST_DOCKERHOST: docker host ip address
        + INTEGRATIONTEST_DOCKERUSER: docker host ssh login user
        + INTEGRATIONTEST_DOCKERPASSWORD: docker host ssh login password
    + Override InitTestEnv() and Dispose()
****