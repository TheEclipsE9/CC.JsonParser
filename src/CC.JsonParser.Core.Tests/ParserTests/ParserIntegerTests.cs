using System;

namespace CC.JsonParser.Core.Tests.ParserTests;

public class ParserIntegerTests
{
    [Fact]
    public void ParsesSingleIntegerToken()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.Integer, "32")
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonInteger);
        var resultAsJsonInteger = (JsonInteger)result;
        Assert.Equal(32, resultAsJsonInteger.Value);
    }
}
