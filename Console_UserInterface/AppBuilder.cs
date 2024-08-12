namespace Console_UserInterface
{
    public class AppBuilder
    {
  
        /// <summary>
        /// Модель меню навигации по типам из пространства имён
        /// </summary>        
        public Dictionary<string, string> GetNavModel(string snamespace, Dictionary<string, string> mapping = null)
        {
            Dictionary<string, string> result = new();
            var types = GetType().Assembly.GetTypes().Where(t => t.Namespace == snamespace && t.GetAttributes().ContainsKey("RouteAttribute"));
            result = new Dictionary<string, string>(types.Select(type =>
            {
                var route = type.GetAttributes()["RouteAttribute"];
                if(mapping is not null)
                {
                    if( route.IndexOf("{") != -1 )
                    {
                        var exp = route.Substring(route.IndexOf("{")+1, route.IndexOf("}") - route.IndexOf("{")-1);
                        exp = exp.Replace(":int", "");
                        if(mapping.ContainsKey(exp))
                        {
                            route = route.Substring(0, route.IndexOf("{")) + mapping[exp] + route.Substring(route.IndexOf("}") + 1);
                        }                        
                    }                    
                }
                return new KeyValuePair<string, string>
                (
                    type.GetAttributes().ContainsKey("LabelAttribute") ? type.GetAttributes()["LabelAttribute"] : type.GetTypeName(),
                    route
                );
            }).ToList());
            return result;
        }
    }
}
