using CC.JsonParser.Console;

namespace CC.JsonParser.Tests;

public class ParserTests
{
    [Theory]
    [InlineData("\"\"", "")]
    [InlineData("\"Hello\"", "Hello")]
    [InlineData("\"Hello \" World\"", "Hello \" World")]
    [InlineData("\"\n Hello World\"", "\n Hello World")]
    [InlineData("\"Hello \nWorld\n\"", "Hello \nWorld\n")]
    [InlineData("\"\n\\\"Hello\n\\\" \n\\\" \n\\\"World\n\\\"\"", "\n\\\"Hello\n\\\" \n\\\" \n\\\"World\n\\\"")]
    public void ParseString_ReturnsValue_WhenStringIsValid(string input, string expectedValue)
    {
        //Arrange
        var charArray = input.ToCharArray();
        var sut = new Parser();

        //Act
        var result = sut.ParseString(charArray);
        

        //Assert
        Assert.Equal(expectedValue, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("\"")]
    [InlineData("\"Hello")]
    [InlineData("Hello\"")]
    public void ParseString_ThrowException_WhenStringIsInvalid(string input)
    {
        //Arrange
        var charArray = input.ToCharArray();
        var sut = new Parser();

        //Act
        var methodCall = () => sut.ParseString(charArray);

        
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
        var sut = new Parser();

        //Act
        var methodCall = () => sut.ParseString(input);

        
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
}