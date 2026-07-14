using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Usuarios
{
    public interface IUsuariosRepository
    {
        Task<List<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<Usuario> CrearAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task<bool> ExisteEmailAsync(string email);
    }
}