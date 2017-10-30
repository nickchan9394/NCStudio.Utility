# NCStudio.Utility
This project contains common utilities for coding and testing.

# Utilites:

### Mocking
+ `DbSet<T> GetMockDbSet<T>(List<T> sourceList)` : Get a DbSet mocking object for testing.

### Jsonning
+ `string SerializeJsonObjectWithCamelCasePropertyNames(object target)`: Serial an object to json string with camel case properties.
+ `JObject ConvertToCamelCasePropertyJObject(object target)`:Convert an object to JObject with camel case properties.
+ `JObject ConvertToJObject(object target)`:Convert an object to JObject