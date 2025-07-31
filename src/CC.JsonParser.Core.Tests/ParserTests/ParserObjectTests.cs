using System;

namespace CC.JsonParser.Core.Tests.ParserTests;

public class ParserObjectTests
{
    [Fact]
    public void ParsesObjectOfSingleKeyValuePair()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.LeftBrace, '{'),
            new Token(TokenType.String, "key1"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.String, "val1"),
            new Token(TokenType.Comma, ','),
            new Token(TokenType.String, "key2"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.String, "val2"),
            new Token(TokenType.RightBrace, '}'),
        };

        var sut = new Parser();


        //Act
        var result = sut.Parse(tokens);


        //Assert
        Assert.True(result is JsonObject);
        var resultAsJsonObject = (JsonObject)result;
        Assert.Equal(2, resultAsJsonObject.KeyValuePairs.Count);

        Assert.True(resultAsJsonObject.KeyValuePairs.ContainsKey("key1"));
        Assert.True(resultAsJsonObject.KeyValuePairs["key1"] is JsonString);
        var val1 = (JsonString)resultAsJsonObject.KeyValuePairs["key1"];
        Assert.Equal("val1", val1.Value);

        Assert.True(resultAsJsonObject.KeyValuePairs.ContainsKey("key2"));
        Assert.True(resultAsJsonObject.KeyValuePairs["key2"] is JsonString);
        var val2 = (JsonString)resultAsJsonObject.KeyValuePairs["key2"];
        Assert.Equal("val2", val2.Value);
    }

    [Fact]
    public void ThrowsErrorIfObjectNotClosed()
    {
        //Arrange
        List<Token> tokens = new List<Token>
        {
            new Token(TokenType.LeftBrace, '{'),
            new Token(TokenType.String, "key1"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.String, "val2")
        };

        var sut = new Parser();


        //Act
        var resultAction = () => sut.Parse(tokens);


        //Assert
        Assert.Throws<ArgumentException>(resultAction);
    }
}
