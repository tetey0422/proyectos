using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Security.Cryptography;
using System.Text;

namespace TuProyecto.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(AppDbContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/usuarios/registro
        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistroDto usuarioDto)
        {
            try
            {
                // Validar que el modelo sea válido
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        mensaje = "Datos inválidos",
                        errores = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                // Verificar si el usuario ya existe (por documento o correo)
                var usuarioExistente = await _context.Usuarios
                    .Where(u => u.Documento == usuarioDto.Documento || u.Correo == usuarioDto.Correo)
                    .FirstOrDefaultAsync();

                if (usuarioExistente != null)
                {
                    string mensaje = usuarioExistente.Documento == usuarioDto.Documento
                        ? "Ya existe un usuario con este número de documento"
                        : "Ya existe un usuario con este correo electrónico";

                    return Conflict(new { mensaje });
                }

                // Crear nuevo usuario
                var nuevoUsuario = new Usuario
                {
                    Documento = usuarioDto.Documento,
                    Nombre = usuarioDto.Nombre.Trim(),
                    Apellido = usuarioDto.Apellido.Trim(),
                    TipoDocumento = ConvertirTipoDocumento(usuarioDto.TipoDocumento),
                    Genero = ConvertirGenero(usuarioDto.Genero),
                    Correo = usuarioDto.Correo.ToLower().Trim(),
                    Telefono = usuarioDto.Telefono,
                    Direccion = usuarioDto.Direccion?.Trim(),
                    Barrio = usuarioDto.Barrio?.Trim(),
                    FechaNacimiento = usuarioDto.FechaNacimiento,
                    Password = HashPassword(usuarioDto.Password),
                    Estado = "Activo",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };

                // Guardar en la base de datos
                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Usuario registrado exitosamente: {nuevoUsuario.Documento}");

                // Retornar respuesta exitosa (sin incluir la contraseña)
                var usuarioRespuesta = new
                {
                    documento = nuevoUsuario.Documento,
                    nombre = nuevoUsuario.Nombre,
                    apellido = nuevoUsuario.Apellido,
                    correo = nuevoUsuario.Correo,
                    fechaCreacion = nuevoUsuario.FechaCreacion
                };

                return CreatedAtAction(nameof(ObtenerUsuario),
                    new { documento = nuevoUsuario.Documento },
                    new { mensaje = "Usuario registrado exitosamente", usuario = usuarioRespuesta });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error al guardar usuario en la base de datos");
                return StatusCode(500, new { mensaje = "Error interno del servidor al registrar usuario" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar usuario");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        // GET: api/usuarios/{documento}
        [HttpGet("{documento}")]
        public async Task<IActionResult> ObtenerUsuario(string documento)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Where(u => u.Documento == documento && u.Estado == "Activo")
                    .Select(u => new
                    {
                        u.Documento,
                        u.Nombre,
                        u.Apellido,
                        u.TipoDocumento,
                        u.Genero,
                        u.Correo,
                        u.Telefono,
                        u.Direccion,
                        u.Barrio,
                        u.FechaNacimiento,
                        u.Estado,
                        u.FechaCreacion
                    })
                    .FirstOrDefaultAsync();

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario: {documento}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        // POST: api/usuarios/verificar-disponibilidad
        [HttpPost("verificar-disponibilidad")]
        public async Task<IActionResult> VerificarDisponibilidad([FromBody] VerificarDisponibilidadDto dto)
        {
            try
            {
                bool documentoExiste = false;
                bool correoExiste = false;

                if (!string.IsNullOrEmpty(dto.Documento))
                {
                    documentoExiste = await _context.Usuarios
                        .AnyAsync(u => u.Documento == dto.Documento);
                }

                if (!string.IsNullOrEmpty(dto.Correo))
                {
                    correoExiste = await _context.Usuarios
                        .AnyAsync(u => u.Correo == dto.Correo.ToLower());
                }

                return Ok(new
                {
                    documentoDisponible = !documentoExiste,
                    correoDisponible = !correoExiste
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar disponibilidad");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        #region Métodos Privados

        private string ConvertirTipoDocumento(int tipoDocumento)
        {
            return tipoDocumento switch
            {
                1 => "CC",
                2 => "TI",
                3 => "CE",
                4 => "RC",
                5 => "NIP",
                6 => "NIT",
                _ => "CC"
            };
        }

        private string ConvertirGenero(int genero)
        {
            return genero switch
            {
                1 => "Masculino",
                2 => "Femenino",
                3 => "Otro",
                _ => "Otro"
            };
        }

        private string HashPassword(string password)
        {
            // Implementación básica de hash (en producción usar BCrypt o similar)
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "TuSaltSecreto"));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        #endregion
    }

    #region DTOs (Data Transfer Objects)

    public class UsuarioRegistroDto
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int TipoDocumento { get; set; }
        public int Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Password { get; set; }
    }

    public class VerificarDisponibilidadDto
    {
        public string? Documento { get; set; }
        public string? Correo { get; set; }
    }

    #endregion
}