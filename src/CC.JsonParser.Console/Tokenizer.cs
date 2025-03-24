namespace CC.JsonParser.Console;

public class Tokenizer
{
    public List<string> GetTokens(StreamReader stream)
    {
        List<string> tokens = new List<string>();
        
        int charValue;
        while ((charValue = stream.Read()) != -1)
        {
            char c =  (char)charValue;
            System.Console.WriteLine(c);
            tokens.Add(c.ToString());
        }
        
        return tokens;
    }
}