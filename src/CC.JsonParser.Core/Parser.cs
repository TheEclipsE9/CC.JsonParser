using System;

namespace CC.JsonParser.Core;

public class Parser
{
    public JsonValue Parse(List<Token> tokens)
    {
        int i = 0;
        while (i < tokens.Count)
        {
            var curToken = tokens[i];

            return ParseValue(curToken);
        }

        throw new ArgumentException();
    }


    private JsonValue ParseValue(Token token)
    {
        return token.TokenType switch
        {
            TokenType.String => new JsonString { Value = token.Value.ToString() },
            TokenType.Integer => new JsonInteger { Value = int.Parse(token.Value.ToString())},
            TokenType.Boolean => new JsonBoolean { Value = bool.Parse(token.Value.ToString())},
            TokenType.Null => token.Value is null ? JsonNull.Instance : throw new ArgumentException("Token of type Null must contain null as value"),
            _ => throw new Exception($"Unexpected token: {token.TokenType}")
        };
    }
}
