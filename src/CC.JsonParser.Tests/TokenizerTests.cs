using CC.JsonParser.Console;

namespace CC.JsonParser.Tests;

public class TokenizerTests
{
    [Theory]
    [InlineData("{}", 2)]
    [InlineData("{{[]}}", 6)]//Should fail later
    [InlineData("{[{}]}", 6)]////Should fail later
    public void GetTokens_ReturnsTokens_WhenValidJson(string input, int expectedTokenCount)
    {
        //Arrange
        using var stream = new StreamReader(input.ToStream());

        var sut = new Tokenizer();
        

        //Act
        var result = sut.GetTokens(stream);
        

        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(expectedTokenCount, result.Count);
    }
    
    
    [Theory]
    [InlineData("")]
    [InlineData("{")]
    [InlineData("}")]
    [InlineData("[]")]
    [InlineData("[")]
    [InlineData("]")]
    public void GetTokens_ThrowsException_WhenInvalidJsonFormat(string input)
    {
        //Arrange
        using var stream = new StreamReader(input.ToStream());

        var sut = new Tokenizer();
        

        //Act
        Action act = () => sut.GetTokens(stream);
        

        //Assert
        Assert.Throws<ArgumentException>(act);
    }
}