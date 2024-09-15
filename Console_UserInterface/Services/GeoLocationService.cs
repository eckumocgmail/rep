namespace Console_UserInterface.Services
{
    /// <summary>
    /// Сервис работы с гео координатами
    /// </summary>
    public class GeoLocationService
    {
        private readonly SigninUser signin;
        private readonly DbContextUser dbu;

        public GeoLocationService( SigninUser signin, DbContextUser dbu )
        {
            this.signin = signin;
            this.dbu = dbu;
        }

        [Label("Обновление сведения о геолокации для авторизованного пользователя")]
        public void UpdateGeoLocation( string lat, string lng )
        {
            if(signin.IsSignin())
            {
                int userId = signin.Verify().Id;
                var user = dbu.UserContexts_.Find(userId);
                user.GeoUpdated = DateTime.Now;
                user.Latitude = lat;
                user.Longitude = lng;
                dbu.SaveChanges();
            }
        }
    }
}
