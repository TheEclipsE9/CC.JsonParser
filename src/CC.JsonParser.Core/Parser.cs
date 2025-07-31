using System;

namespace CC.JsonParser.Core;

public class Parser
{
    private List<Token> _tokens;
    private int _position;

    public JsonValue Parse(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;

        return ParseValue();
    }

    private Token Peek() => _tokens[_position];
    private Token Consume() => _tokens[_position++];

    private JsonValue ParseValue()
    {
        var token = Peek();

        return token.TokenType switch
        {
            TokenType.String => new JsonString { Value = Consume().Value.ToString() },
            _ => throw new Exception($"Unexpected token: {token.TokenType}")
        };
    }
}
