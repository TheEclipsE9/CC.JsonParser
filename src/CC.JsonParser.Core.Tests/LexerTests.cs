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

        [Fact]
        public void Lexer_SingleComma()
        {
            string input = ",";

            var sut = new Lexer();

            var result = sut.Tokenize(input);

            Assert.Single(result);
            Assert.Equal(TokenType.Comma, result[0].TokenType);
            Assert.Equal(',', result[0].Value);
        }

        

        #region GPT generated tests

        [Fact]
        public void Lexer_EmptyInput_ReturnsEmptyList()
        {
            var sut = new Lexer();
            var result = sut.Tokenize("");
            Assert.Empty(result);
        }

        [Fact]
        public void Lexer_SingleWhitespace_Ignored()
        {
            var sut = new Lexer();
            var result = sut.Tokenize(" ");
            Assert.Empty(result);
        }

        [Fact]
        public void Lexer_IntegerTokens_SingleAndMultipleDigits()
        {
            var sut = new Lexer();

            var singleDigit = sut.Tokenize("5");
            Assert.Single(singleDigit);
            Assert.Equal(TokenType.Integer, singleDigit[0].TokenType);
            Assert.Equal("5", singleDigit[0].Value);

            var multiDigit = sut.Tokenize("12345");
            Assert.Single(multiDigit);
            Assert.Equal(TokenType.Integer, multiDigit[0].TokenType);
            Assert.Equal("12345", multiDigit[0].Value);
        }

        [Fact]
        public void Lexer_BooleanTokens_TrueAndFalse()
        {
            var sut = new Lexer();

            var trueToken = sut.Tokenize("true");
            Assert.Single(trueToken);
            Assert.Equal(TokenType.Boolean, trueToken[0].TokenType);
            Assert.Equal("true", trueToken[0].Value);

            var falseToken = sut.Tokenize("false");
            Assert.Single(falseToken);
            Assert.Equal(TokenType.Boolean, falseToken[0].TokenType);
            Assert.Equal("false", falseToken[0].Value);
        }

        [Fact]
        public void Lexer_NullToken()
        {
            var sut = new Lexer();

            var nullToken = sut.Tokenize("null");
            Assert.Single(nullToken);
            Assert.Equal(TokenType.Null, nullToken[0].TokenType);
            Assert.Equal("null", nullToken[0].Value);
        }

        [Fact]
        public void Lexer_ArrayWithIntegers()
        {
            string input = "[1, 2, 3]";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.LeftBracket, '['),
                new Token(TokenType.Integer, "1"),
                new Token(TokenType.Comma, ','),
                new Token(TokenType.Integer, "2"),
                new Token(TokenType.Comma, ','),
                new Token(TokenType.Integer, "3"),
                new Token(TokenType.RightBracket, ']'),
            };

            var sut = new Lexer();
            var result = sut.Tokenize(input);

            AssertHelper.AssertCountAndEachToken(expectedTokens, result);
        }

        [Fact]
        public void Lexer_StringWithEscapedQuotes_FailsOrHandled()
        {
            // If your lexer doesn't yet handle escape sequences, this might fail or need to be handled later.
            string input = "\"Hello \\\"world\\\"\"";

            var sut = new Lexer();

            // If no escape handling implemented yet, expect failure:
            Assert.Throws<Exception>(() => sut.Tokenize(input));
        }

        [Fact]
        public void Lexer_InvalidBoolean_Throws()
        {
            var sut = new Lexer();
            Assert.Throws<Exception>(() => sut.Tokenize("tru"));
            Assert.Throws<Exception>(() => sut.Tokenize("fals"));
            Assert.Throws<Exception>(() => sut.Tokenize("ture"));
        }

        [Fact]
        public void Lexer_InvalidNull_Throws()
        {
            var sut = new Lexer();
            Assert.Throws<Exception>(() => sut.Tokenize("nul"));
            Assert.Throws<Exception>(() => sut.Tokenize("nall"));
        }

        [Fact]
        public void Lexer_UnexpectedCharacter_Throws()
        {
            var sut = new Lexer();

            // Assuming lexer throws on unexpected chars like @ or #
            Assert.Throws<Exception>(() => sut.Tokenize("@"));
            Assert.Throws<Exception>(() => sut.Tokenize("#"));
        }

        #endregion
    }
}