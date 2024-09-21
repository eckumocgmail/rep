using AngleSharp.Dom;

using Console_UserInterface.AppUnits;

using System;
using System.Linq;

using static Console_UserInterface.Services.Location.LocationDbContext;

namespace Console_UserInterface.Services.Location
{
    public class RouteService
    {
        private readonly LocationDbContext db;
        private readonly PageService page;

        public RouteService(PageService page, LocationDbContext db)
        {
            this.db = db;
            this.page = page;
        }

        public int AddRoute<TEntity>()
        {
            return AddRoute<TEntity>($"/api/{typeof(TEntity).GetTypeName()}");
        }

        public int AddRoute<TEntity>(string uri)
        {
            var ppage = page.CreatePage<TEntity>();

            db.PageComponents.AddRange(ppage.PageComponents);
            db.AppPages.Add(ppage);
            db.AppRoutes.Add(new()
            {
                Uri = uri,
                AppPage = ppage
            });
            return db.SaveChanges();
        }

        public void AddNavigation()
        {
            var navigation = new Dictionary<string, string>(db.AppRoutes.ToList().Select(route => new KeyValuePair<string, string>(db.AppPages.First(p => p.Id == route.AppPageId).Name, route.Uri)));
            AppPage navPage = page.CreateNavPage("Меню", navigation);
            db.AppPages.Add(navPage);
            db.PageComponents.AddRange(navPage.PageComponents);

            db.AppRoutes.Add(new()
            {
                Uri = "/api/",
                AppPage = navPage
            });
        }
    }
}
