using System;

namespace CC.JsonParser.Core.Tests.ParserTests;

public class ParserArrayTests
{
    [Fact]
    public void ParsesArrayOfTokens()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.LeftBracket, '['),
            new Token(TokenType.String, "val1"),
            new Token(TokenType.Comma, ','),
            new Token(TokenType.String, "val2"),
            new Token(TokenType.RightBracket, ']'),
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonArray);
        var resultAsJsonArray = (JsonArray)result;
        Assert.Equal(2, resultAsJsonArray.Values.Count);


        Assert.True(resultAsJsonArray.Values[0] is JsonString);
        var val1 = (JsonString)resultAsJsonArray.Values[0];
        Assert.Equal("val1", val1.Value);

        Assert.True(resultAsJsonArray.Values[1] is JsonString);
        var val2 = (JsonString)resultAsJsonArray.Values[1];
        Assert.Equal("val2", val2.Value);
    }

    [Fact]
    public void ParsesSingleBoolTokenAsFalse()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.LeftBracket, '['),
            new Token(TokenType.String, "val1"),
            new Token(TokenType.Comma, ','),
            new Token(TokenType.String, "val2")
        };

        var sut = new Parser();


        //Act
        var resultAction = () => sut.Parse(tokens);


        //Assert
        Assert.Throws<ArgumentException>(resultAction);
    }
}
