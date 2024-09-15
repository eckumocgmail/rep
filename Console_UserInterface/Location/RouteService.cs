using AngleSharp.Dom;

using Console_UserInterface.AppUnits;

using System;
using System.Linq;

using static Console_UserInterface.Location.LocationDbContext;

namespace Console_UserInterface.Location
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
            var ppage = this.page.CreatePage<TEntity>();
            
            this.db.PageComponents.AddRange(ppage.PageComponents);            
            this.db.AppPages.Add(ppage);
            this.db.AppRoutes.Add(new() { 
                Uri = uri,
                AppPage = ppage
            });
            return this.db.SaveChanges();
        }

        public void AddNavigation()
        {
            var navigation = new Dictionary<string, string>(this.db.AppRoutes.ToList().Select(route => new KeyValuePair<string, string>(this.db.AppPages.First(p => p.Id == route.AppPageId).Name, route.Uri)));
            AppPage navPage  = this.page.CreateNavPage("Меню", navigation);
            this.db.AppPages.Add(navPage);
            this.db.PageComponents.AddRange(navPage.PageComponents);

            this.db.AppRoutes.Add(new()
            {
                Uri = "/api/",
                AppPage = navPage
            });
        }
    }
}
