using BookingModel.ServiceDataModel;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ServiceDbContext : DbContext
{
    public DbSet<CityGroup> CityGroups { get; set; }
    public DbSet<ServiceDepartment> ServiceDepartments { get; set; }
    public DbSet<ServiceWork> ServiceWorks { get; set; }
    public DbSet<ServicePrice> ServicePrices { get; set; }
    public DbSet<BookingSlot> BookingSlots { get; set; }
    public DbSet<CustomerInfo> CustomerInfos { get; set; }

    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }

    public ServiceDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if(optionsBuilder.IsConfigured==false)
        {
            Configure(optionsBuilder);
        }
    }

    public static void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = $@"Data Source=DESKTOP-IHJM9RD;" +
            $"Initial Catalog={typeof(ServiceDbContext).Name};" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;" +
            "Multi Subnet Failover=False";
        optionsBuilder.UseSqlServer(connectionString);
    }
}
