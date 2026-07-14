namespace SanLorenzoApi.Modulos.Asistencia.DTOs
{
    public class MiAsistenciaDto
    {
        public string AsignaturaNombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}