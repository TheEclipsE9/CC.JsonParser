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
            TokenType.LeftBrace => ParseObject(tokens, ref position),
            _ => throw new Exception($"Unexpected token: {token.TokenType}")
        };
    }

    private JsonObject ParseObject(List<Token> tokens, ref int position)
    {
        JsonObject jsonObject = new();
        //Perfect world

        string key = null;

        while (position < tokens.Count)
        {
            var curToken = tokens[position];
            if (curToken.TokenType == TokenType.RightBrace)
            {
                return jsonObject;
            }
            if (key is null && curToken.TokenType == TokenType.String)
            {
                var stringToken = ParseValue(tokens, ref position);
                key = ((JsonString)stringToken).Value;
                continue;
            }
            if (curToken.TokenType == TokenType.Colon)
            {
                position++;
                continue;
            }

            if (curToken.TokenType == TokenType.Comma)
            {
                key = null;
                position++;
                continue;
            }

            if (string.IsNullOrEmpty(key)) throw new Exception("Key cant be null or emptry");
            jsonObject.KeyValuePairs.Add(key, ParseValue(tokens, ref position));
        }

        throw new ArgumentException("Must exit on right brace case. not here! hahaha. ps object was not closed");
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
