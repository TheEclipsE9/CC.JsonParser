using System;

namespace CC.JsonParser.Core.Tests.LexerTests;

public class LexerStringTests
{
    [Fact]
    public void Lexer_SingleString()
    {
        string input = "\"value\"";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.String, result[0].TokenType);
        Assert.Equal("value", result[0].Value);
    }

    [Fact]
    public void Lexer_SimpleJsonObject_ReturnsExpectedTokens()
    {
        string input = "{ \"key\": \"value\" }";
        List<Token> expectedTokens = new List<Token>
        {
            new Token(TokenType.LeftBrace, '{'),
            new Token(TokenType.String, "key"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.String, "value"),
            new Token(TokenType.RightBrace, '}'),
        };

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        AssertHelper.AssertCountAndEachToken(
            expectedTokens: expectedTokens,
            actualTokens: result
        );
    }
}
