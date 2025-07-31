using System;

namespace CC.JsonParser.Core.Tests.ParserTests;

public class ParserStringTests
{
    [Fact]
    public void ParsesSingleStringToken()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.String, "test1")
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonString);
        var resultAsJsonString = (JsonString)result;
        Assert.Equal("test1", resultAsJsonString.Value);
    }
}
