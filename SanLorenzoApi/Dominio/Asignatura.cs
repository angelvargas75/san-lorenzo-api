using System.Threading;

namespace SanLorenzoApi.Dominio
{
    public class Asignatura
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;

        public ICollection<DocenteAsignatura> DocenteAsignaturas { get; set; } = new List<DocenteAsignatura>();
        public ICollection<RegistroNota> RegistrosNotas { get; set; } = new List<RegistroNota>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}