using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGuideCookingWpfApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<GuideCooking> GuideCookings { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=GuideCookingDB;Trusted_Connection=True;");
        }
    }
}
