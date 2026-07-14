namespace SanLorenzoApi.Modulos.Usuarios.DTOs
{
    public class ActualizarUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}