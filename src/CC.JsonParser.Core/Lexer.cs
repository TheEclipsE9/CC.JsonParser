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
                    return (new Token(TokenType.String, input.Substring(valueStart, curPosition - 1)), curPosition);
                }
            }

            throw new Exception("String has not closed.");
        }
    }
}
