using SanLorenzoApi.Modulos.Usuarios.DTOs;

namespace SanLorenzoApi.Modulos.Usuarios
{
    public interface IUsuariosService
    {
        Task<List<UsuarioDto>> ObtenerTodosAsync();
        Task<UsuarioDto?> ObtenerPorIdAsync(int id);
        Task<(bool exito, string mensaje, UsuarioDto? usuario)> CrearAsync(CrearUsuarioDto dto);
        Task<(bool exito, string mensaje)> ActualizarAsync(int id, ActualizarUsuarioDto dto);
    }
}