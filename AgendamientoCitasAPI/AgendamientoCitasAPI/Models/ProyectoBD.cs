using Microsoft.EntityFrameworkCore;

namespace AgendamientoCitasAPI.Models
{
    internal class ProyectoBD : DbContext
    {
        public ProyectoBD(DbContextOptions<ProyectoBD> options)
            : base(options)
        {
        }

        // Define tus DbSets aquí basados en las tablas de tu base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<EmpleadoEspecialidad> EmpleadosEspecialidades { get; set; }
        public DbSet<HorarioDisponible> HorariosDisponibles { get; set; }
        public DbSet<Cita> Citas { get; set; }
    }

    // Modelos para las entidades correspondientes a las tablas
    internal class Usuario
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    internal class Empleado
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Profesion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    internal class Especialidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    internal class EmpleadoEspecialidad
    {
        public string EmpleadoDocumento { get; set; }
        public int EspecialidadId { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    internal class HorarioDisponible
    {
        public int Id { get; set; }
        public string EmpleadoDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    internal class Cita
    {
        public int Id { get; set; }
        public string PacienteDocumento { get; set; }
        public string EmpleadoDocumento { get; set; }
        public int? EspecialidadId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}