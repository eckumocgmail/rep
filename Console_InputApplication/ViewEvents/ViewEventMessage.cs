
using System.Collections.Generic;
/// <summary>
/// Базовое сообщение передаваемое в котроллер через связку с сгенерированным 
/// отражением операций контроллера на TypeScript.
/// </summary>
public class ViewEventMessage
{
    public string Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, object> Data { get; set; }
}