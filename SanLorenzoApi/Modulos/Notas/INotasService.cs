using SanLorenzoApi.Modulos.Notas.DTOs;

namespace SanLorenzoApi.Modulos.Notas
{
    public interface INotasService
    {
        Task<List<AlumnoNotaDto>> ObtenerNotasPorSeccionAsync(int gradoSeccionId, int asignaturaId, int bimestreId);
        Task<(bool exito, string mensaje)> GuardarLoteAsync(GuardarNotasLoteDto dto);
        Task<List<MiNotaDto>> ObtenerMisNotasAsync(int alumnoId);
    }
}