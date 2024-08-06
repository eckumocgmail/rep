using Microsoft.EntityFrameworkCore;

public class DbContextService: DbContext 
{
    public virtual DbSet<ServiceGroup> ServiceGroups { get; set; }
    public virtual DbSet<ServiceGroupMessage> ServiceGroupMessages { get; set; }
    public virtual DbSet<ServiceGroups> ServiceGroups_ServiceContext { get; set; }
    public virtual DbSet<ServiceMessage> ServiceMessages { get; set; }
    public virtual DbSet<ServiceInfo> ServiceInfos { get; set; }
    public virtual DbSet<ServiceLogin> ServiceLogins { get; set; }
    public virtual DbSet<ServiceSertificate> ServiceSertificates { get; set; }
    public virtual DbSet<ServiceContext> ServiceContexts { get; set; }
    public virtual DbSet<ServiceSettings> ServiceSettings { get; set; }
    
    public DbContextService(){}
    public DbContextService(DbContextOptions<DbContextService> options) : base(options){}
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={GetType().GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" );
    }

} 
