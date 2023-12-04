using Microsoft.EntityFrameworkCore;
using Pustok_AzMB.Models;

namespace Pustok_AzMB.Context
{
    public class PustokDbContext:DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RD495BO\SQLEXPRESS;Database=Pustok_AzMB;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
