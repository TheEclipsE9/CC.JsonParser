using System;

namespace CC.JsonParser.Core.Tests.ParserTests;

public class ParserBooleanTests
{
    [Fact]
    public void ParsesSingleBoolTokenAsTrue()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.Boolean, "true")
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonBoolean);
        var resultAsJsonBoolean = (JsonBoolean)result;
        Assert.True(resultAsJsonBoolean.Value);
    }

    [Fact]
    public void ParsesSingleBoolTokenAsFalse()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.Boolean, "false")
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonBoolean);
        var resultAsJsonBoolean = (JsonBoolean)result;
        Assert.False(resultAsJsonBoolean.Value);
    }
}
