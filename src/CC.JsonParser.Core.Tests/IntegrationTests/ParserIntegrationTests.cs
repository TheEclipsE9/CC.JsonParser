using System;

namespace CC.JsonParser.Core.Tests.IntegrationTests;

public class ParserIntegrationTests
{
    [Theory]
    [InlineData("tests/step1/valid.json", true)]
    [InlineData("tests/step2/valid.json", true)]
    [InlineData("tests/step2/valid2.json", true)]
    [InlineData("tests/step3/valid.json", true)]
    [InlineData("tests/step4/valid.json", true)]
    [InlineData("tests/step4/valid2.json", true)]
    public void ParserIntegration_FromJsonFile_NoException(string path, bool shouldSucceed)
    {
        var exeLocation = AppContext.BaseDirectory;
        var src = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var pathToFile = Path.Combine(src, path);

        var input = File.ReadAllText(pathToFile);

        var lexer = new Lexer();
        var tokens = lexer.Tokenize(input);

        var parser = new Parser();
        var jsonObject = parser.Parse(tokens);
    }

    [Theory]
    [InlineData("tests/step1/invalid.json", false)]
    [InlineData("tests/step2/invalid.json", false)]
    [InlineData("tests/step2/invalid2.json", false)]
    [InlineData("tests/step3/invalid.json", false)]
    [InlineData("tests/step4/invalid.json", false)]
    public void ParserIntegration_FromJsonFile_WithException(string path, bool shouldSucceed)
    {
        var exeLocation = AppContext.BaseDirectory;
        var src = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var pathToFile = Path.Combine(src, path);

        var input = File.ReadAllText(pathToFile);

        var sutAction = () =>
        {
            var lexer = new Lexer();
            var tokens = lexer.Tokenize(input);

            var parser = new Parser();
            var jsonObject = parser.Parse(tokens);
        };

        Assert.ThrowsAny<Exception>(sutAction);
    }
}
