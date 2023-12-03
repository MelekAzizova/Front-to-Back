using Microsoft.EntityFrameworkCore;

namespace DIANA.Contexts
{
    public class DianaDBContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= DESKTOP-RD495BO\SQLEXPRESS;Database = melek;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
