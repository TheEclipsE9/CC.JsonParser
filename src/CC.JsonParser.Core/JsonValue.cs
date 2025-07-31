using System;

namespace CC.JsonParser.Core;

public abstract class JsonValue
{
}

public class JsonObject : JsonValue
{
    public Dictionary<string, JsonValue> KeyValuePairs { get; set; } = new();
}

public class JsonArray : JsonValue
{
    public List<JsonValue> Values { get; set; } = new();
}

public class JsonString : JsonValue
{
    public string Value { get; set; }
}

public class JsonInteger : JsonValue
{
    public int Value { get; set; }
}

public class JsonBoolean : JsonValue
{
    public bool Value { get; set; }
}

public class JsonNull : JsonValue
{
    public static readonly JsonNull Instance = new JsonNull();
    private JsonNull() { }
}
