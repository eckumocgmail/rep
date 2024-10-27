
using Console_DataConnector.DataModule.DataModels.MessageModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



[Label("Контекст бизнес модели")]
public class BusinessDataModel : MessageDbContext
{
 
    public BusinessDataModel() { }
    public BusinessDataModel(DbContextOptions<MessageDbContext> options) : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false)
            optionsBuilder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={GetType().GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" );


        
    }

    public int Save()
    {
        return SaveChanges();
    }

    public IEnumerable<INavigation> GetNavFor(Type type)
    {
        return null;
    }

    internal static void Configure(DbContextOptionsBuilder builder)
    {
        throw new NotImplementedException();
    }

    public DbSet<BusinessDatasource> BusinessDatasources { get ; set ; }
    public DbSet<BusinessFunction> BusinessFunctions { get ; set ; }
    public DbSet<BusinessLogic> BusinessLogics { get ; set; }

    
    public DbSet<BusinessReport> BusinessReports { get ; set ; }
    public DbSet<BusinessResource> BusinessResources { get ; set ; }
     
    public DbSet<BusinessIndicator> BusinessIndicators { get ; set ; }
    public DbSet<BusinessDataset> BusinessDatasets { get ; set ; }
    public DbSet<BusinessGranularities> Granularities { get ; set ; }
    public DbSet<BusinessData> BusinessData { get; set; }
}