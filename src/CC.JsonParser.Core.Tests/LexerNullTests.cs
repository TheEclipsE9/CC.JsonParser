namespace CC.JsonParser.Core.Tests;

public class LexerNullTests
{
    [Fact]
    public void Lexer_Null()
    {
        string input = "null";

        var sut = new Lexer();

        var result = sut.Tokenize(input);

        Assert.Single(result);
        Assert.Equal(TokenType.Null, result[0].TokenType);
        Assert.Equal("null", result[0].Value);
    }

    [Fact]
    public void Lexer_Null_Fails_WhenIncorrectTokenValue()
    {
        string input = "nbue";

        var sut = new Lexer();

        Assert.Throws<Exception>(() => sut.Tokenize(input));
    }

    // [Fact]
    // public void Lexer_Null_Fails_WhenTokenValueContinsNull()
    // {
    //     string input = "nullabcd";

    //     var sut = new Lexer();

    //     Assert.Throws<Exception>(() => sut.Tokenize(input));
    // }
}
