using DynamicParser;

var config = @"FirstName: John;  

LastName: Doe;
IsValid: true;
Age: 100";

//Using Dictionary
IParser parser = new DictionaryParser();
var parsed = parser.Parse(config);
var lastname = parsed["LastName"];
Console.WriteLine(lastname);
//var something = parsed3["Something"]; //Throws unknown key exception

//Using ExpandoObject
parser = new ExpandoObjectParser();
parsed = parser.Parse(config);
lastname = parsed.LastName;
Console.WriteLine(lastname);

//Using DynamicObject
parser = new DynamicObjectParser();
parsed = parser.Parse(config);
lastname = parsed.LastName;
Console.WriteLine(lastname);
var isValid = parsed["IsValid"];
Console.WriteLine(isValid);

Console.WriteLine("END");