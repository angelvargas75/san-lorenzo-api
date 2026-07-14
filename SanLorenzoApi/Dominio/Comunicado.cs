namespace SanLorenzoApi.Dominio
{
    public enum DestinatarioTipo
    {
        Todos,
        PorRol,
        PorGradoSeccion
    }

    public class Comunicado
    {
        public int Id { get; set; }

        public int AutorId { get; set; }
        public Usuario Autor { get; set; } = null!;

        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;

        public DestinatarioTipo DestinatarioTipo { get; set; }
        public RolUsuario? RolDestino { get; set; }         // usado si DestinatarioTipo = PorRol
        public int? GradoSeccionId { get; set; }             // usado si DestinatarioTipo = PorGradoSeccion
        public GradoSeccion? GradoSeccion { get; set; }

        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
    }
}