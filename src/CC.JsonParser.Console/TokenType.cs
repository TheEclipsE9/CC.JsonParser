namespace CC.JsonParser.Console
{
    public enum TokenType
    {
        // Structural tokens
        LeftBracket,   // [
        RightBracket,  // ]
        LeftBrace,     // {
        RightBrace,    // }

        // Separators
        Comma,         // ,
        Colon,         // :

        // Values
        String,        // "..."
        Number,        // integers & floats
        Boolean,       // true / false
        Null,          // null

        // Special
        EOF            // End of file/input
    }

    public static class TokenTypeExtensions
    {
        public static TokenType ToStructuralTokenType(this char c)
        {
            return c switch
            {
                '[' => TokenType.LeftBracket,
                ']' => TokenType.RightBracket,
                '{' => TokenType.LeftBrace,
                '}' => TokenType.RightBrace,
                ',' => TokenType.Comma,
                ':' => TokenType.Colon,
                _ => throw new ArgumentException()
            };
        }
    }
}