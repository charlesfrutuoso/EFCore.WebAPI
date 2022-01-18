using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public class HeroiContext : DbContext
    {
        //construtor do DbContext deve receber a conexão SQL Server (options), que foi passada lá pelo Startup.cs da WebAPI
        public HeroiContext(DbContextOptions<HeroiContext> options) : base(options)
        {

        }
        
        //anteriormente, estávamos fazendo a configuração da conexão SQL Server pelo método "OnConfiguring" abaixo,
        //mas agora estamos injetando essa conexão direto no construtor acima
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        //nomes em plural, pois representam listas 
        public DbSet<Heroi> Herois { get; set; }

        public DbSet<Batalha> Batalhas { get; set; }

        public DbSet<Arma> Armas { get; set; }

        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; } //precisamos "falar" pro EF que essa relação é N to N e tem chave composta, lá embaixo no "OnModelCreating"

        public DbSet<IdentidadeSecreta> IdentidadesSecretas { get; set; }

        

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
