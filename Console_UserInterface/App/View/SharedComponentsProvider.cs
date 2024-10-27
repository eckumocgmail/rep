using Console_UserInterface.Shared;
using Console_UserInterface.Shared.Cards;
using Console_UserInterface.Shared.Charts;
using Console_UserInterface.Shared.Controls;

public class SharedComponentsProvider
{




    public static Dictionary<string, Type> GetCardsComponents() => GetComponents<ProductCard>();
    public static Dictionary<string, Type> GetChartComponents() => GetComponents<AreaChart>();
    public static Dictionary<string, Type> GetContolsComponents() => GetComponents<AreaChart>();
    public static Dictionary<string, Type> GetControlsComponents() => GetComponents<Checkbox>();


    public static Dictionary<string, Type> GetComponents<TComponent>() where TComponent: BaseComponent
    {
        return typeof(TComponent).Assembly.GetTypes().Where(ptype => ptype.Namespace == typeof(TComponent).Namespace).ToDictionary<Type, string>(ptype => ptype.GetTypeName());        
    }
}
