namespace CC.JsonParser.Console;

public class Parser
{
    /// <summary>
    /// Parse String value. Expects input to include opening/closing quotes.
    /// </summary>
    /// <param name="input">Array of input chars.</param>
    /// <param name="curPosition">Current position.</param>
    /// <returns>String value or empty String.</returns>
    /// <exception cref="ArgumentException">JSON Format is invalid.</exception>
    public object ParseString(char[] input, ref int curPosition)
    {
        int startPosition = curPosition;
        curPosition++;
        if (curPosition >= input.Length) throw new ArgumentException("String is not closed, overwlow");
        
        if (input[startPosition] != '"') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Expected '\"' at the beginning, but found: {input[startPosition]}");

        bool isPrevEscape = false;
        bool isClosingQuoteFound = false;
        //ToDo: Handle all types od escaped seq, including unicode \u1234
        for (; curPosition < input.Length; curPosition++)
        {
            var curChar = input[curPosition];
            
            if (curChar == '"' && !isPrevEscape)
            {
                isClosingQuoteFound = true;
                break;
            }
            
            if (curChar != '\\') continue;
            
            if (curPosition == input.Length - 2) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Escape sequence contains only '\\'");

            curPosition++;//go to next char
            var nextChar = input[curPosition];
            if (nextChar == '\\' || nextChar == '"' || nextChar == 'n') continue;
            
            throw new ArgumentException(
                $"Parameter {nameof(input)}: Invalid JSON format - Expected valid escape sequence, but found: {input[curPosition]}");
        }

        if (!isClosingQuoteFound)
            throw new ArgumentException(
                $"Parameter {nameof(input)}: Invalid JSON format - Expected '\"' at the end");

        if (curPosition - startPosition == 1) return String.Empty;
        
        return new String(input[(startPosition + 1)..(curPosition)]);//ToDo: Build unescaped string, not just slice???
    }

    public object ParseNull(char[] input)
    {
        if (input.Length != 4) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Incorrect length: {input.Length}");
        
        if (input[0] != 'n') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'n', but was {input[0]}");
        if (input[1] != 'u') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'u', but was {input[1]}");
        if (input[2] != 'l') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'l', but was {input[2]}");
        if (input[3] != 'l') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'l', but was {input[3]}");
        
        return null!;
    }

    public object ParseBoolean(char[] input)
    {
        if (input[0] == 't') return ParseTrue();
        if (input[0] == 'f') return ParseFalse();
        
        throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Expected 't' or 'f', but was {input[0]}");
        
        bool ParseTrue()
        {
            if (input.Length != 4) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Incorrect length, expected 4, but was {input.Length}");
            
            if (input[1] != 'r') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'r', but was {input[1]}");
            if (input[2] != 'u') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'u', but was {input[2]}");
            if (input[3] != 'e') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'e', but was {input[3]}");

            return true;
        }
        
        bool ParseFalse()
        {
            if (input.Length != 5) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Incorrect length, expected 5, but was {input.Length}");
            
            if (input[1] != 'a') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'a', but was {input[1]}");
            if (input[2] != 'l') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'l', but was {input[2]}");
            if (input[3] != 's') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 's', but was {input[3]}");
            if (input[4] != 'e') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected 'e', but was {input[3]}");

            return false;
        }
    }

    public object ParseNumber(char[] input)
    {
        if (input.Length == 0) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected length not '0', but was {input.Length}");
        
        if (input[0] == '0')
        {
            if(input.Length != 1) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected length '1', but was {input.Length}");

            return 0;
        }

        for (int i = 0; i < input.Length; i++)
        {
            if (!Char.IsDigit(input[i])) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON Format - Expected digit, but was {input[i]}");
        }

        return Convert.ToInt32(new String(input));
    }

    public object ParseArray(char[] input)
    {
        var array = new List<object>();
        for (int curPosition = 0; curPosition < input.Length; curPosition++)
        {
            var curChar = input[curPosition];

            if (curChar == '[')
                throw new ArgumentException(
                    $"Parameter {nameof(input)}: Invalid JSON Format - Array containing arrays is not supported");
            
            if (curChar == '"')
            {
                var value = ParseString(input, ref curPosition);
                array.Add(value);
            }
        }

        return array;
    }
}