using System;
using System.Collections;
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

        public class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 
                    @"FirstName: John;  

                    LastName: Doe;
                    IsValid: true;
                    Age: 100" };
                yield return new object[] {
                    @"FirstName: John     ;  

                        LastName  :   Doe;
                        IsValid   :   true;
                    Age: 100" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        [Theory]
        [ClassData(typeof(TestData))]
        public void Parse_WhenCalled_ShouldParseInput(string config)
        {
            //arange
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

        [Fact]
        public void Parse_WhenThereIsEmptyKey_ShouldThrowEmptyKeyException()
        {
            //arange
            var config = @"FirstName: John;  

                      : Doe;
                    IsValid: true;
                    Age: 100";
            
            //assert
            Assert.Throws<EmptyKeyException>(() => _subject.Parse(config));
        }


        [Fact]
        public void Parse_WhenThereIsDuplicateKey_ShouldThrowDuplicateKeyException()
        {
            //arange
            var config = @"FirstName: John;  

                     FirstName : Doe;
                    IsValid: true;
                    Age: 100";

            //assert
            Assert.Throws<DuplicateKeyException>(() => _subject.Parse(config));
        }

        [Fact]
        public void Parse_KeyDoesNotStartWhichLetter_ShouldThrowInvalidKeyException()
        {
            //arange
            var config = @"FirstName: John;  

                    2LastName : Doe;
                    IsValid: true;
                    Age: 100";

            //assert
            Assert.Throws<InvalidKeyException>(() => _subject.Parse(config));
        }

        [Fact]
        public void Parse_KeyContainsNonLetterOrDigit_ShouldThrowInvalidKeyException()
        {
            //arange
            var config = @"FirstName: John;  

                    Last$Name : Doe;
                    IsValid: true;
                    Age: 100";

            //assert
            Assert.Throws<InvalidKeyException>(() => _subject.Parse(config));
        }
    }
}
