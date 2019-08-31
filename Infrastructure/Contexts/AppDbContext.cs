using Microsoft.EntityFrameworkCore;
using Seguradora.API.Domain.Models;

namespace Seguradora.API.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Cobertura> Coberturas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Cobertura> ().ToTable("Cobertura");
            builder.Entity<Cobertura> ().HasKey(a =>a.Id);
            builder.Entity<Cobertura> ().Property (a => a.Id).IsRequired ().ValueGeneratedOnAdd ();
            builder.Entity<Cobertura> ().Property (a => a.Nome).IsRequired ().HasMaxLength (50);
            builder.Entity<Cobertura> ().Property (a => a.Premio).IsRequired ();
            builder.Entity<Cobertura> ().Property (a => a.Valor).IsRequired ();
            builder.Entity<Cobertura> ().Property (a => a.Obrigatorio).IsRequired ();

            builder.Entity<Cobertura> ().HasData(
                new Cobertura{Id = 1, Nome = "Morte Acidental", Premio = 100, Valor = 50000, Obrigatorio = true},
                new Cobertura{Id = 2, Nome = "Quebra de Ossos", Premio = 30, Valor = 5000, Obrigatorio = false},
                new Cobertura{Id = 3, Nome = "Internacao Hospitalar", Premio = 50, Valor = 10000, Obrigatorio = false},
                new Cobertura{Id = 4, Nome = "Assistencia Funeraria", Premio = 10, Valor = 2500, Obrigatorio = false},
                new Cobertura{Id = 5, Nome = "Invalidez Permanente", Premio = 130, Valor = 90000, Obrigatorio = true},
                new Cobertura{Id = 6, Nome = "Assistencia Odontologia Emergencial", Premio = 10, Valor = 2500, Obrigatorio = false},
                new Cobertura{Id = 7, Nome = "Diária Incapacidade Temporária", Premio = 30, Valor = 5000, Obrigatorio = false},
                new Cobertura{Id = 8, Nome = "Invalidez Funcional", Premio = 80, Valor = 40000, Obrigatorio = true},
                new Cobertura{Id = 9, Nome = "Doenças Graves", Premio = 100, Valor = 50000, Obrigatorio = false},
                new Cobertura{Id = 10, Nome = "Diagnostico de Cancer", Premio = 50, Valor = 10000, Obrigatorio = false}
            );
        }
    }
}