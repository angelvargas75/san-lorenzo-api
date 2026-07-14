using SanLorenzoApi.Modulos.Asistencia.DTOs;

namespace SanLorenzoApi.Modulos.Asistencia
{
    public interface IAsistenciaService
    {
        Task<List<AlumnoAsistenciaDto>> ObtenerPorSeccionAsync(int gradoSeccionId, int asignaturaId, DateTime fecha);
        Task<(bool exito, string mensaje)> GuardarLoteAsync(GuardarAsistenciaLoteDto dto);
        Task<List<MiAsistenciaDto>> ObtenerMiAsistenciaAsync(int alumnoId);
    }
}