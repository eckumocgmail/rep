using Console_UserInterface.App.Location;
using Console_UserInterface.Services.Location;

namespace Console_UserInterface.App
{
    public class LocationUnit : TestingElement
    {
        public LocationUnit(IServiceProvider provider) : base(provider)
        {
        }

        public override void OnTest()
        {
            this.Info("OnTest()");
            AssertService<LocationDbContext>(db =>
            {

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var location = provider.Get<LocationService>();
                var page = provider.Get<PageService>();
                var router = provider.Get<RouteService>();

                router.AddNavigation();
                router.AddRoute<UserAccount>();
                router.AddRoute<UserPerson>();
                return true;
            },
            "",
            "");
            AssertService<LocationMenuBuilder>(builder =>
            {
                var db = provider.Get<LocationDbContext>();
                if(db.AppRoutes.Count()==0)
                {

                }
                var location = provider.Get<LocationService>();
                var page = provider.Get<PageService>();
                var router = provider.Get<RouteService>();

                router.AddNavigation();
                router.AddRoute<UserAccount>();
                router.AddRoute<UserPerson>();

                var menus = builder.Build();

                return menus.Count() != 0;
            },
            "",
            "");
        }
    }
}
