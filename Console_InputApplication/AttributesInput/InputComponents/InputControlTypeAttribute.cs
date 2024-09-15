namespace Console_InputApplication.AttributesInput.InputComponents
{
    public class InputControlTypeAttribute : InputSelectAttribute
    {
        public static IEnumerable<string> GetControlTypes() => AttrsUtil.GetControlTypes();
        public static string GetControlTypesString() 
        {
            string text = "";
            foreach (string controlType in AttrsUtil.GetControlTypes())
            {
                text += $"{controlType},";
            }
            if(text.EndsWith(","))
                text = text.Substring(0, text.Length -1);
            return text;
        }
        

        public InputControlTypeAttribute(): base(GetControlTypesString())
        {
        }
    }
}
