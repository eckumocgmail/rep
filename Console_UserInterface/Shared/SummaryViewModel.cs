namespace Console_UserInterface.Shared
{
    public class SummaryViewModel
    {
        public string Description { get; internal set; }
        public string Title { get; internal set; }
        internal LinkViewModel[] Links { get; set; }
    }
}
