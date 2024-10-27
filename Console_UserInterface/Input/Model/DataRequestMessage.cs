using System.Collections.Generic;

public class DataRequestMessage
{
    public string AccessToken { get; set; }
    public string SerialKey { get; set; }
    public Dictionary<string,string> MessageObject { get; set; }
    public string ActionName { get; set; }
}