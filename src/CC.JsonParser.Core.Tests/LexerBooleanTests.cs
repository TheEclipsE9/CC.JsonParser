namespace CC.JsonParser.Core.Tests;

public class LexerBooleanTests
{
    [Fact]
    public void Lexer_True()
    {
        string input = "true";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.Boolean, result[0].TokenType);
        Assert.Equal("true", result[0].Value);
    }

    [Fact]
    public void Lexer_True_Fails_WhenTokenValueIncorrect()
    {
        string input = "tbue";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }

    [Fact]
    public void Lexer_True_Fails_WhenTokenValueShort()
    {
        string input = "tru";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }

    [Fact]
    public void Lexer_True_Fails_WhenTokenValueLong()
    {
        string input = "tru";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }

    [Fact]
    public void Lexer_False()
    {
        string input = "false";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.Boolean, result[0].TokenType);
        Assert.Equal("false", result[0].Value);
    }

    [Fact]
    public void Lexer_False_Fails_WhenTokenValueIncorrect()
    {
        string input = "ftrue";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }

    [Fact]
    public void Lexer_False_Fails_WhenTokenValueShort()
    {
        string input = "fals";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }

    [Fact]
    public void Lexer_False_Fails_WhenTokenValueLong()
    {
        string input = "falseabc";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }
}
