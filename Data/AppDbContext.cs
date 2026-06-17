using Microsoft.EntityFrameworkCore;
using EcoRAEE_UAC.Models;

namespace EcoRAEE_UAC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Campaña> Campañas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<MaterialEducativo> MaterialesEducativos { get; set; }
        public DbSet<RecoleccionRaee> RecoleccionesRaee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Campaña>(entity =>
            {
                entity.ToTable("Campanas");
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Estado).HasConversion<string>();
            });

            modelBuilder.Entity<Participante>(entity =>
            {
                entity.ToTable("Participantes");
                entity.Property(e => e.TipoParticipante).HasConversion<string>();
                entity.Property(e => e.CampañaId).HasColumnName("CampanaId");
                entity.HasOne(p => p.Campaña)
                      .WithMany(c => c.Participantes)
                      .HasForeignKey(p => p.CampañaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RecoleccionRaee>(entity =>
            {
                entity.ToTable("RecoleccionRaee");
                entity.Property(e => e.CantidadKg).HasPrecision(10, 2);
                entity.Property(e => e.CampañaId).HasColumnName("CampanaId");
                entity.HasOne(r => r.Campaña)
                      .WithMany(c => c.RecoleccionesRaee)
                      .HasForeignKey(r => r.CampañaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MaterialEducativo>(entity =>
            {
                entity.ToTable("MaterialEducativo");
                entity.Property(e => e.Tipo).HasConversion<string>();
                entity.Property(e => e.CampañaId).HasColumnName("CampanaId");
            });
        }
    }
}
