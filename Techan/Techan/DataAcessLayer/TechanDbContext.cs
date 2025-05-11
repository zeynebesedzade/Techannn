using Microsoft.EntityFrameworkCore;
using Techan.Models;

namespace Techan.DataAcessLayer
{
    public class TechanDbContext:DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Techan;Trusted_Connection=true;TrustServerCertificate=true");
         base.OnConfiguring(optionsBuilder);     
     }
    }
}
