using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataModels.MessageModel
{
    public class MessageDbContext : DbContext
    {
        public DbSet<MessageAttribute> MessageAttributes { get; set; }
        public DbSet<MessageProperty> MessageProperties { get; set; }
        public DbSet<MessageProtocol> MessageProtocols { get; set; }
        public DbSet<ValidationModel> ValidationModels { get; set; }

        public MessageDbContext() : base()
        {
        }

        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={GetType().GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
    }
}
