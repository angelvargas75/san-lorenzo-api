namespace SanLorenzoApi.Modulos.Usuarios.DTOs
{
    public class CrearUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;   // "Alumno", "Docente", "Coordinador"

        // Solo se usan si Rol = "Alumno"
        public int? GradoSeccionId { get; set; }

        // Solo se usa si Rol = "Docente"
        public string? Especialidad { get; set; }
    }
}