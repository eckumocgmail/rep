public class AppViewBuilder
{
    public static List<string> GetGroups() => new List<string>() { "Charts", "Controls", "Cards", "Grids" };
    public static Dictionary<string, Type> GetComponents(string name)
    {
        switch(name)
        {
            case "Charts": return SharedComponentsProvider.GetChartComponents();
            case "Controls": return SharedComponentsProvider.GetControlsComponents();
            case "Cards": return SharedComponentsProvider.GetCardsComponents();
            default: throw new Exception();
        }
    }




    


}