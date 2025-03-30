namespace CC.JsonParser.Console
{
    public class Token
    {
        public TokenType TokenType { get; init; }
        public string? Value { get; init; }

        public Token(TokenType tokenType, string? value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }
}