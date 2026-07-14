namespace SanLorenzoApi.Modulos.Asistencia.DTOs
{
    public class AsistenciaIndividualDto
    {
        public int AlumnoId { get; set; }
        public string Estado { get; set; } = string.Empty;   // "Presente", "Tardanza", "Falta"
    }

    public class GuardarAsistenciaLoteDto
    {
        public int AsignaturaId { get; set; }
        public DateTime Fecha { get; set; }
        public List<AsistenciaIndividualDto> Registros { get; set; } = new();
    }
}