namespace CC.JsonParser.Core
{
    public class Token
    {
        public TokenType TokenType { get; init; }
        public object Value { get; init; }

        public Token(TokenType tokenType, object value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }

    public enum TokenType
    {
        LeftBrace,//    '{'
        RightBrace,//   '}'
        Colon,//        ':'
        LeftBracket,//  '['
        RightBracket,// ']'
        Comma,//        ','

        String,//       '"value"'

        Integer,//      '101'

        Boolean,//      'true' or 'false'

        Null,//         'null'
    }
}