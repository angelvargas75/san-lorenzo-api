using SanLorenzoApi.Data;
using SanLorenzoApi.Dominio;
using SanLorenzoApi.Modulos.Usuarios.DTOs;

namespace SanLorenzoApi.Modulos.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _repository;
        private readonly AppDbContext _context;

        public UsuariosService(IUsuariosRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            var usuarios = await _repository.ObtenerTodosAsync();
            return usuarios.Select(MapearADto).ToList();
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _repository.ObtenerPorIdAsync(id);
            return usuario == null ? null : MapearADto(usuario);
        }

        public async Task<(bool exito, string mensaje, UsuarioDto? usuario)> CrearAsync(CrearUsuarioDto dto)
        {
            // Validar email único
            if (await _repository.ExisteEmailAsync(dto.Email))
                return (false, "El email ya está registrado", null);

            // Validar que el rol sea válido
            if (!Enum.TryParse<RolUsuario>(dto.Rol, ignoreCase: true, out var rolParseado))
                return (false, "Rol inválido. Debe ser Alumno, Docente o Coordinador", null);

            // Validaciones específicas por rol
            if (rolParseado == RolUsuario.Alumno && dto.GradoSeccionId == null)
                return (false, "Debe especificar el Grado y Sección para un Alumno", null);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = rolParseado,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();  

            // Crear el registro relacionado según el rol
            if (rolParseado == RolUsuario.Alumno)
            {
                var alumno = new Alumno
                {
                    UsuarioId = usuario.Id,
                    GradoSeccionId = dto.GradoSeccionId!.Value
                };
                _context.Alumnos.Add(alumno);
            }
            else if (rolParseado == RolUsuario.Docente)
            {
                var docente = new Docente
                {
                    UsuarioId = usuario.Id,
                    Especialidad = dto.Especialidad ?? string.Empty
                };
                _context.Docentes.Add(docente);
            }
            // Si es Coordinador, no se crea registro adicional

            await _context.SaveChangesAsync();

            return (true, "Usuario creado correctamente", MapearADto(usuario));
        }

        public async Task<(bool exito, string mensaje)> ActualizarAsync(int id, ActualizarUsuarioDto dto)
        {
            var usuario = await _repository.ObtenerPorIdAsync(id);
            if (usuario == null)
                return (false, "Usuario no encontrado");

            // Si cambia el email, validar que no choque con otro usuario existente
            if (usuario.Email != dto.Email && await _repository.ExisteEmailAsync(dto.Email))
                return (false, "El email ya está en uso por otro usuario");

            usuario.Nombre = dto.Nombre;
            usuario.Email = dto.Email;
            usuario.Activo = dto.Activo;

            await _repository.ActualizarAsync(usuario);
            return (true, "Usuario actualizado correctamente");
        }

        private static UsuarioDto MapearADto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol.ToString(),
                Activo = usuario.Activo,
                FechaCreacion = usuario.FechaCreacion
            };
        }
    }
}