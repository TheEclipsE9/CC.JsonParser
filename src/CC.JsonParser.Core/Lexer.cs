namespace CC.JsonParser.Core
{
    public class Lexer
    {
        private static readonly List<Token> EmptyTokenList = new();

        public List<Token> Tokenize(string input)
        {
            if (String.IsNullOrEmpty(input))
                return EmptyTokenList;

            var tokens = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                char curChar = input[i];

                if (curChar == '{')
                {
                    tokens.Add(new Token(TokenType.LeftBrace, '{'));
                    continue;
                }

                if (curChar == '}')
                {
                    tokens.Add(new Token(TokenType.RightBrace, '}'));
                    continue;
                }

                if (curChar == '[')
                {
                    tokens.Add(new Token(TokenType.LeftBracket, '['));
                    continue;
                }

                if (curChar == ']')
                {
                    tokens.Add(new Token(TokenType.RightBracket, ']'));
                    continue;
                }

                if (curChar == ':')
                {
                    tokens.Add(new Token(TokenType.Colon, ':'));
                    continue;
                }

                if (curChar == '"')
                {
                    (Token token, int newPosition) result = TokenizeString(input, i + 1);

                    tokens.Add(result.token);
                    i = result.newPosition;

                    continue;
                }

                if (Char.IsDigit(curChar))
                {
                    (Token token, int newPosition) result = TokenizeInteger(input, i);

                    tokens.Add(result.token);
                    i = result.newPosition;

                    continue;
                }
            }

            return tokens;
        }

        private (Token, int) TokenizeString(string input, int curPosition)
        {
            int valueStart = curPosition;

            for (; curPosition < input.Length; curPosition++)
            {
                var curChar = input[curPosition];

                if (curChar == '"')
                {
                    return (new Token(TokenType.String, input.Substring(valueStart, curPosition - valueStart)), curPosition);
                }
            }

            throw new Exception("String has not closed.");
        }

        private (Token, int) TokenizeInteger(string input, int curPosition)
        {
            int from = curPosition;
            int to = curPosition + 1;

            for (; to < input.Length; to++)
            {
                var curChar = input[to];

                if (Char.IsDigit(curChar) == false)
                    break;
            }

            return (new Token(TokenType.Integer, input.Substring(from, to - from)), to - 1);
        }
    }
}
