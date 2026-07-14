namespace SanLorenzoApi.Dominio
{
    public class DocenteAsignatura
    {
        public int Id { get; set; }

        public int DocenteId { get; set; }
        public Docente Docente { get; set; } = null!;

        public int AsignaturaId { get; set; }
        public Asignatura Asignatura { get; set; } = null!;

        public int GradoSeccionId { get; set; }
        public GradoSeccion GradoSeccion { get; set; } = null!;
    }
}