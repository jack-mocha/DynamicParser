using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DynamicParser.Tests
{
    public class DictionaryParserTests
    {
        private readonly DictionaryParser _subject;
        public DictionaryParserTests()
        {
            this._subject = new DictionaryParser();
        }

        [Fact]
        public void Parse_WhenCalled_ShouldParseInput()
        {
            //arange
            var config = @"FirstName: John;  

                LastName: Doe;
                IsValid: true;
                Age: 100";

            var shouldParsed = new Configuration(new Dictionary<string, dynamic>()
            {
                {"FirstName", "John"},
                {"LastName", "Doe"},
                {"IsValid", true},
                {"Age", 100},
            });

            //act
            var actualParsed = _subject.Parse(config);
            //assert
            Assert.IsType<Configuration>(actualParsed);
            Assert.IsType<bool>(actualParsed["IsValid"]);
            Assert.IsType<int>(actualParsed["Age"]);
            Assert.Equal(shouldParsed["FirstName"], actualParsed["FirstName"]);
            Assert.Equal(shouldParsed["LastName"], actualParsed["LastName"]);
            Assert.Equal(shouldParsed["IsValid"], actualParsed["IsValid"]);
            Assert.Equal(shouldParsed["Age"], actualParsed["Age"]);
        }
    }
}
