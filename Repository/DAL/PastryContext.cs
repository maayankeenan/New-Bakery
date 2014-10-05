using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class PastryContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetailes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pastery> Pastries { get; set; }
        public DbSet<Branch> Branches { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public PastryContext()
            : base("RanConnection")
        {
            Database.SetInitializer<PastryContext>(null);
        }

    }    
}
