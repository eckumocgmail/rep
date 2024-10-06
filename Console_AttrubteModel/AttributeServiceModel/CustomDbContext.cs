using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контекст хранения динамических аттрибутов
/// </summary>
public class CustomDbContext : DbContext
{
    public DbSet<CustomAttribute> CustomAttributes { get; set; } 

    public CustomDbContext()
    {
    }

    public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Обновление базы данных
    /// </summary>
    public static void Build()
    {
        Console.WriteLine("Выполняю пересоздание базы данных");
        using (var db = new CustomDbContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }

    /// <summary>
    /// Настройки соединения
    /// </summary>
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