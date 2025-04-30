namespace CC.JsonParser.Console;

public class Parser
{
    public object ParseString(char[] input)
    {
        if (input.Length == 0 || input.Length == 1) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Incorrect length: {input.Length}");

        if (input[0] != '"') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Expected '\"' at the beginning, but found: {input[0]}");
        if (input[^1] != '"') throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Expected '\"' at the end, but found: {input[^1]}");
        
        //handle \ sequence
        //ToDo: Handle all types od escaped seq, including unicode \u1234
        for (int i = 1; i < input.Length - 1; i++)
        {
            if (input[i] != '\\') continue;
            
            if (i == input.Length - 2) throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Escape sequence contains only '\\'");

            i++;//go to next char
            var nextChar = input[i];
            if (nextChar == '\\' || nextChar == '"' || nextChar == 'n') continue;
            
            throw new ArgumentException($"Parameter {nameof(input)}: Invalid JSON format - Expected valid escape sequence, but found: {input[i]}");
        }
        
        return new String(input[1..^1]);//ToDo: Build unescaped string, not just slice???
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
}