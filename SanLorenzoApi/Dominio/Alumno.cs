namespace SanLorenzoApi.Dominio
{
    public class Alumno
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int GradoSeccionId { get; set; }
        public GradoSeccion GradoSeccion { get; set; } = null!;

        // Navegación 1:N
        public ICollection<RegistroNota> RegistrosNotas { get; set; } = new List<RegistroNota>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}