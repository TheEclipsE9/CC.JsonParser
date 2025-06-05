namespace CC.JsonParser.Core.Tests
{
    public class LexerTests
    {
        //ToDo: Add more test cases
        //      - whitespaces between tokens
        //      - StringTokenizer
        //          - "
        //          - ""
        //          - "a"
        //          - "ab

        [Fact]
        public void Lexer_return_left_brace()
        {
            string input = "{";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.LeftBrace, result[0].TokenType);
            Assert.Equal('{', result[0].Value);
        }

        [Fact]
        public void Lexer_return_right_brace()
        {
            string input = "}";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.RightBrace, result[0].TokenType);
            Assert.Equal('}', result[0].Value);
        }

        [Fact]
        public void Lexer_return_colon()
        {
            string input = ":";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.Colon, result[0].TokenType);
            Assert.Equal(':', result[0].Value);
        }

        [Fact]
        public void Lexer_return_string()
        {
            string input = "\"value\"";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.String, result[0].TokenType);
            Assert.Equal("value", result[0].Value);
        }
    }
}