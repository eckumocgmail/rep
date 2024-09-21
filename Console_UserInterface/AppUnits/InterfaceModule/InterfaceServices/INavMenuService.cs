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
    public NavLinkNavLink[] GetNavLinks()
    {
        return _context.NavLinks.ToArray();
    }
    public class NavLinkNavLink: BaseEntity
    {
        public string Icon { get; internal set; }
        public string Label { get; internal set; }
        public string Href { get; internal set; }
    }
    public NavLinkNavLink CreateNavLink()
    {
        var result = new NavLinkNavLink() { Icon = "oi-home", Label = "Домашняя", Href = "/" };
        _context.NavLinks.Add(result);
        _context.SaveChanges();
        return result;
    }

    public NavLinkNavLink CreateNavLink(string icon, string label, string href)
    {
        var result = new NavLinkNavLink() { Icon = icon, Label = label, Href = href };
        _context.NavLinks.Add(result);
        _context.SaveChanges();
        return result;
    }

    public void UpdateNavLink(NavLinkNavLink link)
    {
        _context.Update(link);
        _context.SaveChanges();
    }
    public void RemoveNavLink(NavLinkNavLink link)
    {
        _context.NavLinks.Remove(link);
        _context.SaveChanges();
    }
    public void RemoveNavLink(int id)
    {
        _context.NavLinks.Remove(_context.NavLinks.Find(id));
        _context.SaveChanges();
    }



    public NavLinkNavLink[] GetLinks(string uri)
    {
        _logger.LogInformation(uri);
        var navList = new List<NavLinkNavLink>();
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

    public NavLinkNavLink[] GetDefaultNavLinks()
    {
        return new NavLinkNavLink[] {
            new NavLinkNavLink(){ Icon="home", Label="Домашняя", Href="/" },
            new NavLinkNavLink(){ Icon="plus", Label="Версии", Href="/counter" },
            new NavLinkNavLink(){ Icon="list-rich", Label="Редактор форм", Href="/input-forms" },
            new NavLinkNavLink(){ Icon="person", Label="Редактор меню", Href="/nav-menu" }

        };
    }

    public IEnumerable<NavLinkNavLink> GetPageLinks() => _context.NavLinks.ToList();

    public IEnumerable<NavLinkNavLink> GetPageLinks(string uri)
    {
        return _context.NavLinks.Where(link => link.Href.StartsWith(uri) ).ToList();
    }

}

public interface INavMenuService
{
    IEnumerable<NavLinkNavLink> GetPageLinks(string uri);
    IEnumerable<NavLinkNavLink> GetPageLinks();
    void UpdateNavLink(NavLinkNavLink link);
    void RemoveNavLink(int id);
    NavLinkNavLink CreateNavLink(string icon, string label, string href);


}
public class NavMenuDbContext : DbContext
{
    public DbSet<NavLinkNavLink> NavLinks { get; set; }
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
