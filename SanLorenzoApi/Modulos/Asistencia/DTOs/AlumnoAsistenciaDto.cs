namespace SanLorenzoApi.Modulos.Asistencia.DTOs
{
    public class AlumnoAsistenciaDto
    {
        public int AlumnoId { get; set; }
        public string AlumnoNombre { get; set; } = string.Empty;
        public int? AsistenciaId { get; set; }   // null si aún no se registró ese día
        public string? Estado { get; set; }       // "Presente", "Tardanza", "Falta", o null
    }
}