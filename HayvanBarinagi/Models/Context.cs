﻿using Microsoft.EntityFrameworkCore;

namespace HayvanBarinagi.Models
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=HayvanlarDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.UseSqlServer("Server=localhost; Database=HayvanBarinagiSon; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;");

        }
        public DbSet<Hayvan> Hayvanlar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set;}
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Sahiplenme> Sahiplenme { get; set; }
        public DbSet<HayvanVerme> HayvanVerme { get;set; }

    }
}
