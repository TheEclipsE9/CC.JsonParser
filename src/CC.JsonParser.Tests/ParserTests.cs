using CC.JsonParser.Console;

namespace CC.JsonParser.Tests;

public class ParserTests
{
    [Theory]
    [InlineData("\"\"", "")]
    [InlineData("\"Hello\"", "Hello")]
    [InlineData("\"Hello \\\" World\"", "Hello \\\" World")]
    [InlineData("\"\\n Hello World\"", "\\n Hello World")]
    [InlineData("\"Hello \\nWorld\\n\"", "Hello \\nWorld\\n")]
    [InlineData("\"\\n\\\"Hello\\n\\\" \\n\\\" \\n\\\"World\\n\\\"\"", "\\n\\\"Hello\\n\\\" \\n\\\" \\n\\\"World\\n\\\"")]
    public void ParseString_ReturnsValue_WhenStringIsValid(string input, string expectedValue)
    {
        //Arrange
        var charArray = input.ToCharArray();
        int curPosition = 0;
        var sut = new Parser();

        //Act
        var result = sut.ParseString(charArray, ref curPosition);
        

        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [InlineData("\"")]
    [InlineData("\"Hello")]
    [InlineData("Hello\"")]
    public void ParseString_ThrowException_WhenStringIsInvalid(string input)
    {
        //Arrange
        var charArray = input.ToCharArray();
        int curPosition = 0;
        var sut = new Parser();

        //Act
        var methodCall = () => sut.ParseString(charArray, ref curPosition);

        
        //Assert
        Assert.Throws<ArgumentException>(methodCall);
    }
    
    [Theory]
    [InlineData(new []{'"','H','i','\\','z','"',})]
    [InlineData(new []{'"','\\','z','H','i','"'})]
    [InlineData(new []{'"','\\','z','H','i','\\','z','"',})]
    [InlineData(new []{'"','H','i','\\','"',})]
    [InlineData(new []{'"','\\','"',})]
    [InlineData(new []{'"','\\','z','"',})]
    [InlineData(new []{'"','\\', ' ', ' ',' ', ' ', 'z','"',})]
    public void ParseString_ThrowException_WhenEscapeSequenceIsInvalid(char[] input)
    {
        //Arrange
        int curPosition = 0;
        var sut = new Parser();

        //Act
        var methodCall = () => sut.ParseString(input, ref curPosition);

        
        //Assert
        Assert.Throws<ArgumentException>(methodCall);
    }

    [Fact]
    public void ParseNull_ReturnsNull_WhenInputIsValid()
    {
        //Arrange
        var input = "null";
        var charArray = input.ToCharArray();
        var sut = new Parser();
        
        
        //Act
        var result = sut.ParseNull(charArray);

        //Assert
        Assert.Null(result);
    }
    
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void ParseBoolean_ReturnsValue_WhenInputIsValid(string input, bool expected)
    {
        //Arrange
        var charArray = input.ToCharArray();
        var sut = new Parser();
        
        
        //Act
        var result = sut.ParseBoolean(charArray);

        //Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("tru")]
    [InlineData("truf")]
    [InlineData("ture")]
    [InlineData("fals")]
    [InlineData("falze")]
    [InlineData("faLse")]
    [InlineData("felse")]
    [InlineData("atrue")]
    [InlineData("truea")]
    [InlineData("afalse")]
    [InlineData("falsea")]
    public void ParseBoolean_ThrowsException_WhenInputIsInvalid(string input)
    {
        //Arrange
        var charArray = input.ToCharArray();
        var sut = new Parser();
        
        
        //Act
        var methodCall = () => sut.ParseBoolean(charArray);

        //Assert
        Assert.Throws<ArgumentException>(methodCall);
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("10", 10)]
    [InlineData("123", 123)]
    [InlineData("987654", 987654)]
    public void ParseNumber_ReturnsValue_WhenInputIsValid(string input, int expected)
    {
        //Arrange
        var charArray = input.ToCharArray();
        var sut = new Parser();
        
        
        //Act
        var result = sut.ParseNumber(charArray);

        //Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("01")]
    [InlineData("001")]
    [InlineData("123a")]
    [InlineData("a123")]
    [InlineData("-1")]
    [InlineData("3.14")]
    [InlineData("1e3")]
    public void ParseNumber_ThrowsException_WhenInputIsInvalid(string input)
    {
        //Arrange
        var charArray = input.ToCharArray();
        var sut = new Parser();
        
        
        //Act
        var methodCall = () => sut.ParseNumber(charArray);

        //Assert
        Assert.Throws<ArgumentException>(methodCall);
    }
}