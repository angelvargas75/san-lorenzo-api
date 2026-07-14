namespace SanLorenzoApi.Dominio
{
    public enum DiaSemana
    {
        Lunes, Martes, Miercoles, Jueves, Viernes
    }

    public class Horario
    {
        public int Id { get; set; }

        public int GradoSeccionId { get; set; }
        public GradoSeccion GradoSeccion { get; set; } = null!;

        public int AsignaturaId { get; set; }
        public Asignatura Asignatura { get; set; } = null!;

        public int DocenteId { get; set; }
        public Docente Docente { get; set; } = null!;

        public DiaSemana Dia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}