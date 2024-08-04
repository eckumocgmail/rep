using Microsoft.EntityFrameworkCore;

namespace Console_InputApplication.EntityFasade
{
    public class WebAppUnit
    {
        public IEntityFasade<WebPage> WebPages { get; }
        private WebAppDbContext dbContext = new WebAppDbContext();
        public WebAppUnit()
        {
            this.WebPages = new DbEntityFasade<WebPage>(dbContext);
        }
    }

    public class WebPage
    {
        public int Id { get; set; } 
        public string Url { get; set; }
    }
    public class WebAppDbContext : DbContext
    {
        public DbSet<WebPage> WebPages { get; set; }   

        public WebAppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(GetType().GetTypeName());
        }
    }
}
