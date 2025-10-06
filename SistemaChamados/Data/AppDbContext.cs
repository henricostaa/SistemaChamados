using Microsoft.EntityFrameworkCore;
using SistemaChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamados.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=SistemaChamados;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chamado>()
                .HasOne(c => c.Solicitante)
                .WithMany(u => u.ChamadosCriados)
                .HasForeignKey(c => c.SolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chamado>()
                .HasOne(c => c.AtribuidoA)
                .WithMany(u => u.ChamadosAtribuidos)
                .HasForeignKey(c => c.AtribuidoAId)
                .OnDelete(DeleteBehavior.SetNull);

        }

    }

}
