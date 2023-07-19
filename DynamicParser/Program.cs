using DynamicParser;

var config = @"FirstName: John;  

LastName: Doe;
IsValid: true;
Age: 100";

var parser = new DictionaryParser();
var parsed = parser.Parse(config);
var lastname = parsed["LastName"];
Console.WriteLine(lastname);
//var something = parsed3["Something"]; //Throws unknown key exception

Console.WriteLine("END");