namespace Console_UserInterface.Location
{
    public class LocationUnit: TestingElement
    {
        public LocationUnit(IServiceProvider provider) : base(provider)
        {
        }

        public override void OnTest()
        {
            this.Info("OnTest()");
            this.AssertService<LocationDbContext>(db => {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                var location = this.provider.Get<LocationService>();
                var page = this.provider.Get<PageService>();
                var router = this.provider.Get<RouteService>();

                router.AddNavigation();
                router.AddRoute<UserAccount>();
                router.AddRoute<UserPerson>();
                return true;
            },"","");
            
        }
    }
}
