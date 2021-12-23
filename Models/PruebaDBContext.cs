using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RetoAPI.Models
{
    public partial class PruebaDBContext : DbContext
    {
        public PruebaDBContext()
        {
        }

        public PruebaDBContext(DbContextOptions<PruebaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PreguntasDb> PreguntasDbs { get; set; }
        public virtual DbSet<RespuestasDb> RespuestasDbs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2OCVSIC\\WINCCPLUSMIG2014;Initial Catalog=PruebaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<PreguntasDb>(entity =>
            {
                entity.ToTable("PreguntasDb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Preguntas).IsRequired();
            });

            modelBuilder.Entity<RespuestasDb>(entity =>
            {
                entity.ToTable("RespuestasDb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPregunta).HasColumnName("id_Pregunta");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Usuario).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
