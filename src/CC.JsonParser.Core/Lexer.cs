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
            }

            return tokens;
        }
    }
}
