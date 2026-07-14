namespace SanLorenzoApi.Dominio
{
    public enum EstadoAsistencia
    {
        Presente,
        Tardanza,
        Falta
    }

    public class Asistencia
    {
        public int Id { get; set; }

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int AsignaturaId { get; set; }
        public Asignatura Asignatura { get; set; } = null!;

        public DateTime Fecha { get; set; }
        public EstadoAsistencia Estado { get; set; }
    }
}