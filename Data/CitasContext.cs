using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CitasContext : DbContext
    {
        public CitasContext(DbContextOptions<CitasContext> options) : base(options)
        {
        }

        // DbSets para todas las entidades
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Enfermero> Enfermeros { get; set; }
        public DbSet<EPS> EPS { get; set; }
        public DbSet<Afiliacion> Afiliaciones { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Examen> Examenes { get; set; }
        public DbSet<Reprogramacion> Reprogramaciones { get; set; }
        public DbSet<EstadoCita> EstadosCita { get; set; }
        public DbSet<Resultado> Resultados { get; set; }
        public DbSet<HistorialCita> HistorialCitas { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<LogSistema> LogsSistema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales si son necesarias

            // Configurar índices únicos
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Documento)
                .IsUnique();

            modelBuilder.Entity<Afiliacion>()
                .HasIndex(a => new { a.TipoDocumento, a.Documento })
                .IsUnique();

            // Configurar relaciones uno a muchos explícitamente
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Pacientes)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioFK)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Enfermeros)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioFK)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar valores por defecto
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Estado)
                .HasDefaultValue("Activo");

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.Leida)
                .HasDefaultValue(false);

            // Configurar valores por defecto para fechas usando MySQL
            modelBuilder.Entity<Notificacion>()
                .Property(n => n.Fecha)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<LogSistema>()
                .Property(l => l.FechaHora)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Resultado>()
                .Property(r => r.FechaSubida)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<HistorialCita>()
                .Property(h => h.FechaCambio)
                .HasDefaultValueSql("NOW()");

        }
    }
}