namespace CC.JsonParser.Core.Tests;

public class LexerNumberTests
{
    [Fact]
    public void Lexer_SingleDigitInteger()
    {
        string input = "1";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.Integer, result[0].TokenType);
        Assert.Equal("1", result[0].Value);
    }

    [Fact]
    public void Lexer_MultipleDigitInteger()
    {
        string input = "1123";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.Integer, result[0].TokenType);
        Assert.Equal("1123", result[0].Value);
    }

    [Fact]
    public void Lexer_LeadingZeroDigitInteger()
    {
        string input = "01";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.Integer, result[0].TokenType);
        Assert.Equal("01", result[0].Value);
    }

    [Fact]
    public void Lexer_SimpleJsonObject_ReturnsExpectedTokens()
    {
        string input = "{ \"key\": 1 }";
        List<Token> expectedTokens = new List<Token>
        {
            new Token(TokenType.LeftBrace, '{'),
            new Token(TokenType.String, "key"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.Integer, "1"),
            new Token(TokenType.RightBrace, '}'),
        };

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        AssertHelper.AssertCountAndEachToken(
            expectedTokens: expectedTokens,
            actualTokens: result
        );
    }

    [Fact]
    public void Lexer_SimpleJsonObject_MultipleDigitsInteger_ReturnsExpectedTokens()
    {
        string input = "{ \"key\": 1301 }";
        List<Token> expectedTokens = new List<Token>
        {
            new Token(TokenType.LeftBrace, '{'),
            new Token(TokenType.String, "key"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.Integer, "1301"),
            new Token(TokenType.RightBrace, '}'),
        };

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        AssertHelper.AssertCountAndEachToken(
            expectedTokens: expectedTokens,
            actualTokens: result
        );
    }

    [Fact]
    public void Lexer_SimpleJsonObject_LeadingZeroInteger_ReturnsExpectedTokens()
    {
        string input = "{ \"key\": 01 }";
        List<Token> expectedTokens = new List<Token>
        {
            new Token(TokenType.LeftBrace, '{'),
            new Token(TokenType.String, "key"),
            new Token(TokenType.Colon, ':'),
            new Token(TokenType.Integer, "01"),
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
