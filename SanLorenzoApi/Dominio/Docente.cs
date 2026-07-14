namespace SanLorenzoApi.Dominio
{
    public class Docente
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public string Especialidad { get; set; } = string.Empty;

        // Navegación 1:N
        public ICollection<DocenteAsignatura> DocenteAsignaturas { get; set; } = new List<DocenteAsignatura>();
    }
}