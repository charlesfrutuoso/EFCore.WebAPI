using EFCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Data
{
    public class HeroiContext : DbContext
    {
        //nomes em plural, pois representam listas 
        public DbSet<Heroi> Herois { get; set; }

        public DbSet<Batalha> Batalhas { get; set; }

        public DbSet<Arma> Armas { get; set; }

        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; } //precisamos "falar" pro EF que essa relação é N to N e tem chave composta, lá embaixo no "OnModelCreating"

        public DbSet<IdentidadeSecreta> IdentidadesSecretas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Password=pr0d@p123;Persist Security Info=True;User ID=sa;Initial Catalog=HeroApp;Data Source=.\SQLEXPRESS;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //"explicando" pro EF que os Ids da "HeroiBatalha" formam uma chave composta (e.BatalhaId, e.HeroiId)
            modelBuilder.Entity<HeroiBatalha>(entity =>
            {
                entity.HasKey(e => new { e.BatalhaId, e.HeroiId });
            });
        }

    }
}
