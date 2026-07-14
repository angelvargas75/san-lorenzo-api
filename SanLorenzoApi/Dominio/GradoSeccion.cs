namespace SanLorenzoApi.Dominio
{
    public class GradoSeccion
    {
        public int Id { get; set; }
        public string Grado { get; set; } = string.Empty;    // ej: "5to"
        public string Seccion { get; set; } = string.Empty;  // ej: "A"

        public ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
        public ICollection<DocenteAsignatura> DocenteAsignaturas { get; set; } = new List<DocenteAsignatura>();
        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    }
}