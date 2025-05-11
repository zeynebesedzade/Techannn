﻿using Microsoft.EntityFrameworkCore;
using Techan.Models;

namespace Techan.DataAcessLayer
{
    public class TechanDbContext:DbContext
    {
        public TechanDbContext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Techan;Trusted_Connection=true;TrustServerCertificate=true");
        //    base.OnConfiguring(optionsBuilder);     
        //}
    }
}
