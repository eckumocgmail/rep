using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static NavMenuService;


/// <summary>
/// Предоставляет данные в модель компонента меню навигации
/// </summary>
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


    public NavLinkNavLink[] GetNavLinks() => _context.NavLinks.ToArray();
    public IEnumerable<NavLinkNavLink> GetPageLinks() => _context.NavLinks.ToList();
    public IEnumerable<NavLinkNavLink> GetPageLinks(string uri)
        => _context.NavLinks.Where(link => link.Href.StartsWith(uri)).ToList();


    /// <summary>
    /// Модель ссылки
    /// </summary>
    public class NavLinkNavLink: BaseEntity
    {
        public string Icon { get; internal set; }
        public string Label { get; internal set; }
        public string Href { get; internal set; }
    }

    /// <summary>
    /// Создаст запись в базе и вернёт связанный объект
    /// </summary>
    public NavLinkNavLink CreateNavLink()
    {
        var result = new NavLinkNavLink() { Icon = "oi-home", Label = "Домашняя", Href = "/" };
        _context.NavLinks.Add(result);
        _context.SaveChanges();
        return result;
    }


    /// <summary>
    /// Создаст запись в базе и верёнт связанный объект
    /// </summary>
    public NavLinkNavLink CreateNavLink(string icon, string label, string href)
    {
        var result = new NavLinkNavLink() { Icon = icon, Label = label, Href = href };
        _context.NavLinks.Add(result);
        _context.SaveChanges();
        return result;
    }


    /// <summary>
    /// Обновление данных
    /// </summary>   
    public void UpdateNavLink(NavLinkNavLink link)
    {
        _context.Update(link);
        _context.SaveChanges();
    }


    /// <summary>
    /// Удаление данных
    /// </summary> 
    public void RemoveNavLink(NavLinkNavLink link)
    {
        _context.NavLinks.Remove(link);
        _context.SaveChanges();
    }


    // <summary>
    /// Удаление данных по ид
    /// </summary> 
    public void RemoveNavLink(int id)
    {
        _context.NavLinks.Remove(_context.NavLinks.Find(id));
        _context.SaveChanges();
    }



    /// <summary>
    /// Получение ссылок по маршрутизируемым представления относительно текущего адреса
    /// </summary>
    /// <param name="uri">текущий адрес</param>
    /// <returns>Ссылки</returns>
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
            var attrs = TypeAttributesExtensions.GetTypeAttributes(page);
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


    /// <summary>
    /// Тестовый данные
    /// </summary>   
    public NavLinkNavLink[] GetDefaultNavLinks()
    {
        return new NavLinkNavLink[] {
            new NavLinkNavLink(){ Icon="home",      Label="Домашняя",       Href="/" },
            new NavLinkNavLink(){ Icon="plus",      Label="Версии",         Href="/counter" },
            new NavLinkNavLink(){ Icon="list-rich", Label="Редактор форм",  Href="/input-forms" },
            new NavLinkNavLink(){ Icon="person",    Label="Редактор меню",  Href="/nav-menu" }
        };
    }


}


/// <summary>
/// Предоставляет модели ссылок
/// </summary>
public interface INavMenuService
{
    IEnumerable<NavLinkNavLink> GetPageLinks(string uri);
    IEnumerable<NavLinkNavLink> GetPageLinks();
    void UpdateNavLink(NavLinkNavLink link);
    void RemoveNavLink(int id);
    NavLinkNavLink CreateNavLink(string icon, string label, string href);
}


/// <summary>
/// Контекст данных
/// </summary>
public class NavMenuDbContext : DbContext
{
    public DbSet<NavLinkNavLink> NavLinks { get; set; }
    public NavMenuDbContext( ) : base( ) { }
    public NavMenuDbContext(DbContextOptions<NavMenuDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if(optionsBuilder.IsConfigured==false)
        {
            optionsBuilder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={GetType().GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}

/// <summary>
/// Расширение упрощает регистрацию зависимостей в приложении
/// </summary>
public static class NavMenuServiceExtension
{
    public static IServiceCollection AddNavMenu(this IServiceCollection services)
    {
        services.AddOpenIcons();
        services.AddScoped<INavMenuService, NavMenuService>();
        services.AddDbContext<NavMenuDbContext>(options => 
            options.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={typeof(NavMenuDbContext).GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        return services;
    }
}