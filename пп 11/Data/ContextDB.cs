using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using пп_11.Models;

namespace пп_11.Data
{
    internal class ContextDB : DbContext
    {
        public DbSet<GroundPlace> GroundPlaces { get; set; }

        public DbSet<Obremenenia> Obremenenia { get; set; }

        public DbSet<Pravoobladateli> Pravoobladatelis { get; set; }

        public DbSet<Right> Rights { get; set; }


        public DbSet<TypeOfPravoobladateli> TypeOfPravoobladatelis { get; set; }

        public DbSet<TypeOfRight> TypeOfRight { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O44JPRI\SQLEXPRESS;Database=PP5;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
