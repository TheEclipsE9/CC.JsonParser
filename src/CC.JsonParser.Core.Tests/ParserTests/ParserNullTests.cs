using System;

namespace CC.JsonParser.Core.Tests.ParserTests;

public class ParserNullTests
{
    [Fact]
    public void ParsesSingleNullTokenWithNullValue()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.Null, null)
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonNull);
        var resultAsJsonNull = (JsonNull)result;
    }

    [Fact]
    public void ThrowExceptionWhenSingleNullTokenWithNonNullValue()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.Null, "null")
        };

        var sut = new Parser();


        //Act
        var resultAction = () => sut.Parse(tokens);


        //Assert
        Assert.Throws<ArgumentException>(resultAction);
    }
}
