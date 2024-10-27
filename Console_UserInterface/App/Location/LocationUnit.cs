namespace Console_UserInterface.Services.Location
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

            }, "", "");
        }
    }
}
