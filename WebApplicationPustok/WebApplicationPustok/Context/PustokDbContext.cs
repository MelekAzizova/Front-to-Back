using Microsoft.EntityFrameworkCore;

using WebApplicationPustok.Models;

namespace WebApplicationPustok.Context
{
    public class PustokDbContext:DbContext
    {
        //public PustokDbContext(PustokDbContext context)
        //{
        //    _context = context;
        //}

        PustokDbContext _context {  get;  }
        public PustokDbContext(DbContextOptions<PustokDbContext> opt) : base(opt) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagProduct> TagsProduct { get; set; }
       // public DbSet<ProductImages> ProductImages { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-RD495BO\SQLEXPRESS;Database=WebApplicationPustok;Trusted_Connection=true");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
