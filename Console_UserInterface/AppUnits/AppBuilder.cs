﻿using System.Xml.Linq;

namespace Console_UserInterface.AppUnits
{
    public class AppBuilder
    {
        /// <summary>
        /// Модель меню навигации по типам из пространства имён
        /// </summary>        
        /// <param name="mapping">
        /// Маппинг значений для маршрута
        /// </param>
        public static Dictionary<string, string> CreateNavMenu<T>(Dictionary<string, string> mapping = null)
        {
            Dictionary<string, string> result = new();
            var snamespace = typeof(T).Namespace;
            var types = typeof(T).Assembly.GetTypes().Where(t => t.Namespace == snamespace && t.GetAttributes().ContainsKey("RouteAttribute"));
            result = new Dictionary<string, string>(types.Select(type =>
            {
                var route = type.GetAttributes()["RouteAttribute"];
                if (mapping is not null)
                {
                    if (route.IndexOf("{") != -1)
                    {
                        var exp = route.Substring(route.IndexOf("{") + 1, route.IndexOf("}") - route.IndexOf("{") - 1);
                        exp = exp.Replace(":int", "");
                        if (mapping.ContainsKey(exp))
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
        public static Dictionary<string, string> CreateNavMenu(string snamespace, Dictionary<string, string> mapping = null)
        {
            Dictionary<string, string> result = new();
            var types = AppProviderService.GetInstance().GetType().Assembly.GetTypes().Where(t => t.Namespace == snamespace && t.GetAttributes().ContainsKey("RouteAttribute"));
            result = new Dictionary<string, string>(types.Select(type =>
            {
                var route = type.GetAttributes()["RouteAttribute"];
                if (mapping is not null)
                {
                    if (route.IndexOf("{") != -1)
                    {
                        var exp = route.Substring(route.IndexOf("{") + 1, route.IndexOf("}") - route.IndexOf("{") - 1);
                        exp = exp.Replace(":int", "");
                        if (mapping.ContainsKey(exp))
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