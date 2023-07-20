# DynamicParser
This project implements a parser that takes a input string as a config file and parses the data in the file dynamically. <br>
## For example 
After parsing the below string:

"FirstName: John;  
LastName: Doe;  
IsValid: true;  
Age: 100";

You can access the parsed data as the following:  
parsed.FirstName  
parsed["FirstName"]  

## Implementation
### DictionaryParser
* Implemented using a dictionary
* It can only access parsed data using parsed["FirstName"]
* It uses indexer to thow appropriate exceptions, when parsed data is accessed, e.g. UnknownKeyException
### ExpandoObjectParser
* Implemented using ExandoObject
* It can only access parsed data using parsed.FirstName
* It cannot throw UnknownKeyException, when parsed data is accessed
### DynamicObjectParser
* Implemented using DynamicObject
* It can access parsed data using parsed.FirstName and parsed["FirstName"]
* It can throw UnknownKeyException, when parsed data is accessed

## Unit Test
* Framework: XUnit
* There are test cases setup for all 3 implementations of the dynamic parser.

## Environment
* .Net 7.0
* Visual Studio 2022
