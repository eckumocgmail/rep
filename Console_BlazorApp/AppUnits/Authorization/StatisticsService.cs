using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


public class StatisticsService
{
    private readonly AuthorizationDbContext _context;
    private readonly ILogger<StatisticsService> _logger;

    public StatisticsService(ILogger<StatisticsService> logger, AuthorizationDbContext db)
    {
        this._context = db;
        _logger = logger;
    }


    public async Task SaveStatisticsOLAP()
    {
        foreach (Type entityType in GetFactsTables(_context))
        {

            IEnumerable<INavigation> navs = GetNavigationProperties(entityType);
            foreach (INavigation nav in navs)
            {
                _logger.LogInformation(entityType.FullName + " " + nav.ForeignKey);
            }
            //await SaveStatistics(_context, entityType);
        }

        await _context.SaveChangesAsync();
    }

    private IEnumerable<Type> GetFactsTables(AuthorizationDbContext context)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<INavigation> GetNavigationProperties(Type t)
    {
        IEntityType entity = (from navs in _context.Model.GetEntityTypes() where navs.Name == t.FullName select navs).SingleOrDefault();
        return entity.GetNavigations();
    }


    /// <summary>
    /// Формирование и сохранение статистики
    /// </summary>
    public async Task SaveStatistics( )
    {
        await Task.CompletedTask;
    }
    public async Task SaveStatistics( int day, int month, int year )
    {
        await Task.CompletedTask;

    }

    

    



    /// <summary>
    /// Нехороший способ извеления наименований сущностей
    /// </summary>
    /// <param name="subject"> контекст данных </param>
    /// <returns> множество наименований сущностей </returns>
    private HashSet<Type> GetEntitiesTypes(DbContext subject)
    {
        Type type = subject.GetType();
        HashSet<Type> entities = new HashSet<Type>();
        foreach (MethodInfo info in type.GetMethods())
        {
            if (info.Name.StartsWith("get_") == true && info.ReturnType.Name.StartsWith("DbSet"))
            {
                if (info.Name.IndexOf("MigrationHistory") == -1)
                {
                    entities.Add(info.ReturnType);
                }
            }
        }
        return entities;
    }
}
