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
        /// <summary>
        /// {
        /// </summary>
        LeftBrace,//    '{'
        /// <summary>
        /// }
        /// </summary>
        RightBrace,//   '}'
        /// <summary>
        /// :
        /// </summary>
        Colon,//        ':'
        /// <summary>
        /// [
        /// </summary>
        LeftBracket,//  '['
        /// <summary>
        /// ]
        /// </summary>
        RightBracket,// ']'
        /// <summary>
        /// ,
        /// </summary>
        Comma,//        ','

        /// <summary>
        /// String value, like "value"
        /// </summary>
        String,//       '"value"'

        /// <summary>
        /// Integer value, like 101
        /// </summary>
        Integer,//      '101'

        /// <summary>
        /// Boolean value, like 'true'
        /// </summary>
        Boolean,//      'true' or 'false'

        /// <summary>
        /// Null value, like 'null'
        /// </summary>
        Null,//         'null'
    }
}