using System.Text;

namespace CC.JsonParser.Console;

public static class StreamHelper
{
    public static Stream ToStream(this string str)
        => new MemoryStream(Encoding.UTF8.GetBytes((str)));
}