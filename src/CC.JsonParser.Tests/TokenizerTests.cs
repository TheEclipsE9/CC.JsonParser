using CC.JsonParser.Console;

namespace CC.JsonParser.Tests;

public class TokenizerTests
{
    private readonly string Step1ValidFile = Path.Combine(AppContext.BaseDirectory, "tests/tests/step1/valid.json");
    private readonly string Step1InalidFile = Path.Combine(AppContext.BaseDirectory, "tests/tests/step1/invalid.json");
    
    [Fact]
    public void GetTokens_ReturnsValidTokens()
    {
        var stream = new StreamReader(Step1ValidFile);

        var sut = new Tokenizer();
        
        var result = sut.GetTokens(stream);
        
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public void GetTokens_ReturnsEmptyList_WhenFileIsEmpty()
    {
        var stream = new StreamReader(Step1InalidFile);

        var sut = new Tokenizer();
        
        var result = sut.GetTokens(stream);
        
        Assert.Empty(result);
    }
}