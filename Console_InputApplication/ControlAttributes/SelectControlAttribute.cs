namespace Console_UserInterface.ControlAttributes
{
    public class SelectControlAttribute: ControlAttribute
    {
        public SelectControlAttribute(string ControlType) : base(ControlTypes.Select)
        {
        }

        public string Value { get; set; }
        public List<string> Options { get; set; }

    }

    public class SelectControlModel: ViewItem
    {
        public string Value { get; set; }
        public List<string> Options { get; set; }
    }
}
