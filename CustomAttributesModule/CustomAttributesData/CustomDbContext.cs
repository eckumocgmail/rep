using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomDbContext : DbContext
{

    public DbSet<CustomAttribute> Attributes { get; set; } 

    public CustomDbContext()
    {
    }

    public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
    {
    }

    public static void Build()
    {
        using (var db = new CustomDbContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }


    public static void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            string connectionString = $@"Data Source=DESKTOP-IHJM9RD;"+
                $"Initial Catalog={typeof(CustomDbContext).Name};" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "Trust Server Certificate=False;" +
                "Application Intent=ReadWrite;" +
                "Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        Configure(optionsBuilder);
    }
}