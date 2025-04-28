namespace CC.JsonParser.Console;

public class Parser
{
    public object ParseString(char[] input)
    {
        if (input.Length == 0 || input.Length == 1) throw new ArgumentException($"Parameter {nameof(input)}: Incorrect length - {input.Length}");

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
}