namespace CC.JsonParser.Core.Tests
{
    public class LexerTests
    {
        //ToDo: Add more test cases
        //      - failing
        //      - edge cases
        //      - whitespaces between tokens
        //      - StringTokenizer
        //          - "
        //          - ""
        //          - "a"
        //          - "ab

        [Fact]
        public void Lexer_EmptyJsonObject_ReturnsExpectedTokens()
        {
            string input = "{}";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.LeftBrace, '{'),
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

        [Fact]
        public void Lexer_SingleLeftBrace()
        {
            string input = "{";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.LeftBrace, result[0].TokenType);
            Assert.Equal('{', result[0].Value);
        }

        [Fact]
        public void Lexer_SingleRightBrace()
        {
            string input = "}";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.RightBrace, result[0].TokenType);
            Assert.Equal('}', result[0].Value);
        }

        [Fact]
        public void Lexer_SingleColon()
        {
            string input = ":";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.Colon, result[0].TokenType);
            Assert.Equal(':', result[0].Value);
        }

        [Fact]
        public void Lexer_SingleLeftBracket()
        {
            string input = "[";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.LeftBracket, result[0].TokenType);
            Assert.Equal('[', result[0].Value);
        }

        [Fact]
        public void Lexer_SingleRightBracket()
        {
            string input = "]";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.RightBracket, result[0].TokenType);
            Assert.Equal(']', result[0].Value);
        }
    }
}