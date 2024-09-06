using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace Console_InputApplication.EntityFasade
{

    [Label("Тестирование методов управление веб-приложением")]

    public class WebAppUnit
    {
        public IEntityFasade<WebPage> WebPages { get; }
        private WebAppDbContext dbContext = new WebAppDbContext();
        public WebAppUnit()
        {
            this.WebPages = new DbEntityFasade<WebPage>(dbContext);
        }

        public static void Test()
        {
            var unit = new WebAppUnit();
            unit.WebPages.Create(new WebPage()
            {
                Url = "test"
            });
            unit.WebPages.GetAll().ToJsonOnScreen().WriteToConsole();
        }
    }



    public class WebPage
    {
        public int Id { get; set; } 
        public string Url { get; set; }
    }

    [Label("База веб-приложения")]
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

        public static void ConfigureWebAppDbContext(IServiceCollection services)
        {

            services.AddDbContext<WebAppDbContext>();

        }
    }
}
