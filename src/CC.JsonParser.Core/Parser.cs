using System;

namespace CC.JsonParser.Core;

public class Parser
{
    public JsonValue Parse(List<Token> tokens)
    {
        int i = 0;
        while (i < tokens.Count)
        {
            return ParseValue(tokens, ref i);
        }

        throw new ArgumentException("i < tokens.Count returned false");
    }


    private JsonValue ParseValue(List<Token> tokens, ref int position)
    {
        var token = tokens[position];
        position++;

        return token.TokenType switch
        {
            TokenType.String => new JsonString { Value = token.Value.ToString() },
            TokenType.Integer => new JsonInteger { Value = int.Parse(token.Value.ToString()) },
            TokenType.Boolean => new JsonBoolean { Value = bool.Parse(token.Value.ToString()) },
            TokenType.Null => token.Value is null ? JsonNull.Instance : throw new ArgumentException("Token of type Null must contain null as value"),
            TokenType.LeftBracket => ParseArray(tokens, ref position),
            _ => throw new Exception($"Unexpected token: {token.TokenType}")
        };
    }

    private JsonArray ParseArray(List<Token> tokens, ref int position)
    {
        JsonArray jsonArray = new();
        //Perfect world

        while (position < tokens.Count)
        {
            var curToken = tokens[position];
            if (curToken.TokenType == TokenType.RightBracket)
            {
                return jsonArray;
            }

            if (curToken.TokenType == TokenType.Comma)
            {
                position++;
                continue;
            }

            jsonArray.Values.Add(ParseValue(tokens, ref position));
        }

        throw new ArgumentException("Must exit on right bracket case. not here! hahaha. ps array was not closed");
    }
}
