using Newtonsoft.Json;

using System.Collections.Generic;

public class DataResponseMessage   
{
    public string SerialKey { get; set; }
    public object MessageObject { get; set; }
    public string ActionStatus { get; set; }
    public string StatusText { get; set; }

    public Dictionary<string, object> GetDictionary() {
        return null;
    }
}