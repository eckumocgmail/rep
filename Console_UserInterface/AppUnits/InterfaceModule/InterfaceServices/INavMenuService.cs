using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Components.Routing;
using static NavMenuService;

public class NavMenuService : INavMenuService
{
    private readonly ILogger<NavMenuService> _logger;
    private readonly NavMenuDbContext _context;
    public NavMenuService(ILogger<NavMenuService> logger, NavMenuDbContext context)
    {
        _logger = logger;
        _context = context;
        if (GetNavLinks().Length == 0)
        {
            foreach (var navLink in GetDefaultNavLinks())
            {
                _context.NavLinks.Add(navLink);
                _context.SaveChanges();
            }
        }
    }
    public NavLink[] GetNavLinks()
    {
        return _context.NavLinks.ToArray();
    }
    public class NavLinkNavLink: NavLink
    {
        public string Icon { get; internal set; }
        public string Label { get; internal set; }
        public string Href { get; internal set; }
    }
    public void CreateNavLink()
    {
        _context.NavLinks.Add(new NavLinkNavLink() { Icon = "oi-home", Label = "Домашняя", Href = "/" });
        _context.SaveChanges();
    }

    public void Update(NavLink link)
    {
        _context.Update(link);
        _context.SaveChanges();
    }
    public void Remove(NavLink link)
    {
        _context.NavLinks.Remove(link);
        _context.SaveChanges();
    }

 

    public NavLink[] GetPageLinks(string uri)
    {
        _logger.LogInformation(uri);
        var navList = new List<NavLink>();
        string ns = "BlazorHospital.Client.Pages";
        var ids =
            uri.ReplaceAll("/", ".").ReplaceAll("\\", ".").Split(".").ToList().Where(s => string.IsNullOrEmpty(s) == false).Select(s => s.ToCapitalStyle()).ToList();
        ids.ForEach(id => ns += "." + id);
        _logger.LogInformation($"Namespace: {ns}");
        foreach (var page in Assembly.GetCallingAssembly().GetPages(ns))
        {
            var attrs = page.GetAttributes();
            if (attrs.ContainsKey("RouteAttribute"))
            {
                navList.Add(new NavLinkNavLink() { Icon = "home", Label = page.GetName(), Href = attrs["RouteAttribute"] });
            }
            if (attrs.ContainsKey("Route"))
            {
                navList.Add(new NavLinkNavLink() { Icon = "home", Label = page.GetName(), Href = attrs["Route"] });
            }

        };
        return navList.ToArray();
    }

    public NavLink[] GetDefaultNavLinks()
    {
        return new NavLink[] {
            new NavLinkNavLink(){ Icon="home", Label="Домашняя", Href="/" },
            new NavLinkNavLink(){ Icon="plus", Label="Версии", Href="/counter" },
            new NavLinkNavLink(){ Icon="list-rich", Label="Редактор форм", Href="/input-forms" },
            new NavLinkNavLink(){ Icon="person", Label="Редактор меню", Href="/nav-menu" }

        };
    }


    public void OnNavTreeInit(object evt)
    {
        _logger.LogInformation("OnNavTreeInit(evt)");
        //NavTree tree = (NavTree)evt;
        //tree.Reset(GetPageLinks());
    }

    public NavLink[] GetPageLinks()
    {
        throw new System.NotImplementedException();
    }

    Task INavMenuService.OnNavTreeInit(object evt)
    {
        throw new System.NotImplementedException();
    }

    NavLinkNavLink[] INavMenuService.GetPageLinks(string uri)
    {
        throw new NotImplementedException();
    }

    NavLinkNavLink[] INavMenuService.GetPageLinks()
    {
        throw new NotImplementedException();
    }

    NavLinkNavLink[] INavMenuService.GetNavLinks()
    {
        throw new NotImplementedException();
    }

    public void Update(NavLinkNavLink link)
    {
        throw new NotImplementedException();
    }

    public void Remove(NavLinkNavLink link)
    {
        throw new NotImplementedException();
    }
}

public interface INavMenuService
{

    public Task OnNavTreeInit(object evt);
    public NavLinkNavLink[] GetPageLinks(string uri);
    public NavLinkNavLink[] GetPageLinks();
    public NavLinkNavLink[] GetNavLinks();
    public void Update(NavLinkNavLink link);
    public void Remove(NavLinkNavLink link);
    public void CreateNavLink();

}
public class NavMenuDbContext : DbContext
{
    public DbSet<NavLink> NavLinks { get; set; }
    public NavMenuDbContext(DbContextOptions<NavMenuDbContext> options) : base(options) { }
}
public static class NavMenuServiceExtension
{
    public static IServiceCollection AddNavMenu(this IServiceCollection services)
    {
        services.AddOpenIcons();
        services.AddScoped<INavMenuService, NavMenuService>();
        services.AddDbContext<NavMenuDbContext>(options => options.UseInMemoryDatabase(nameof(NavMenuDbContext)));
        return services;
    }
}
