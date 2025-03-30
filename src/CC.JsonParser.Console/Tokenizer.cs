namespace CC.JsonParser.Console;

public class Tokenizer
{
    private const char LeftBracket = '[';
    private const char RightBracket = ']';
    private const char LeftBrace = '{';
    private const char RightBrace = '}';
    private const char DoubleQuotes = '"';

    public List<Token> GetTokens(StreamReader stream)
    {
        List<Token> tokens = new ();
        Stack<char> stack = new();

        var firstChar = (char)stream.Read();
        var firstTokenType = firstChar.ToTokenType();
        if (firstTokenType != TokenType.LeftBrace)
        {
            throw new ArgumentException();
        }
        else
        {
            
            stack.Push(firstChar);
            tokens.Add(new Token(firstTokenType, null));
        }
        

        int curCharIntValue;
        while ((curCharIntValue = stream.Read()) != -1)
        {
            char curChar =  (char)curCharIntValue;

            if (IsOpening(curChar))
            {
                stack.Push(curChar);
                tokens.Add(new Token(curChar.ToTokenType(), null));
                continue;
            }

            if (IsClosing(curChar))
            {
                if (!stack.TryPop(out char openingChar)) throw new ArgumentException();

                if (!AreOpeningClosinMatched(openingChar, curChar)) throw new ArgumentException();

                tokens.Add(new Token(curChar.ToTokenType(), null));
                continue;
            }

        }
        
        if(tokens.Count == 0 || stack.Count != 0) throw new ArgumentException();

        return tokens;
    }

    private bool IsOpening(char c)
    {
        return c == LeftBrace || c == LeftBracket;
    }

    private bool IsClosing(char c)
    {
        return c == RightBrace || c == RightBracket;
    }

    private bool AreOpeningClosinMatched(char openingChar, char closingChar)
    {
        return (openingChar, closingChar) switch
        {
            ('[', ']') => true,
            ('{', '}') => true,
            _ => false
        };
    }
}