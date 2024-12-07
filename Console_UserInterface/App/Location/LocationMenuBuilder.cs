using BlazorContextMenu;

using Console_UserInterface.Services.Location;

using static MainMenuBuilder;

namespace Console_UserInterface.App.Location
{
    public class LocationMenuBuilder
    {
        private readonly LocationDbContext locationDbContext;
        private readonly IServiceProvider provider;

        public LocationMenuBuilder(  IServiceProvider provider , LocationDbContext locationDbContext ) {

            this.locationDbContext = locationDbContext;
            this.provider = provider;
        }

        public List<MenuItemModel> Test()
        {
            var builder = new MenuBuilder();
            builder.CreateMenu("Файл", new MenuItemModel[] {
            builder.CreateButtonMenu("Close"),
            builder.CreateButtonMenu("New"),
            builder.CreateButtonMenu("Open")
            });
                builder.CreateMenu("Вид", new MenuItemModel[] {
                builder.CreateMenuSelect(null, "Размер", new MenuItemModel[]{
                    builder.CreateButtonMenu("Максимальный"),
                    builder.CreateButtonMenu("Стандарт"),
                    builder.CreateButtonMenu("Свернутый")
                })

            }); 
            this.Info(builder.Menus.ToJsonOnScreen());
            return builder.Menus;
        }

        public List<MenuItemModel> Build()
        {
            var location = provider.Get<LocationService>();
            var page = provider.Get<PageService>();
            var router = provider.Get<RouteService>();

            router.AddNavigation();
            router.AddRoute<UserAccount>();
            router.AddRoute<UserPerson>();

            MenuBuilder mainMenuBuilder = new();

            var routes = locationDbContext.AppRoutes.ToList();
            Func<string, string, int> CompareUris = (r1, r2) =>
            {
                var list = new List<string>() { r1, r2 };
                list.Sort();
                return list[0] == r1 ? 1 : -1;
            };
            routes.Sort((r1, r2) => CompareUris(r2.Uri, r1.Uri));
            var uriList = routes.Select(r => r.Uri).ToList();
            Dictionary<string, IMenuItemModel> elements = new();
            foreach (var route in routes)
            {
                if (uriList.Any(uri => uri.StartsWith(route.Uri)))
                {
                    elements[route.Uri] = mainMenuBuilder.CreateButtonMenu(route.Uri);
                }
            }
            foreach (var route in routes)
            {
                if(uriList.Any(uri => uri.StartsWith(route.Uri)))
                {
                    List<IMenuItemModel> items = new();
                    foreach(var kv in elements)
                    {
                        if (kv.Key.StartsWith(route.Uri))
                        {
                            items.Add(kv.Value);
                        }
                    }
                    elements[route.Uri] = mainMenuBuilder.CreateMenu(route.Uri,items.ToArray());             
                }
            }
            mainMenuBuilder.Menus = elements.Values.Select(item => (MenuItemModel)item).ToList();
            return mainMenuBuilder.Menus;
              
        }
    }
}
