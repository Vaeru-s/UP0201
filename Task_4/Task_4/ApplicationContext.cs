using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Task_4
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Accounts> accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ADCLG1;Database=hotel;Trusted_Connection=True;");
        }
    }

}
