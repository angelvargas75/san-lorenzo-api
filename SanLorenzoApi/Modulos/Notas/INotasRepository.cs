using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Notas
{
    public interface INotasRepository
    {
        Task<List<Alumno>> ObtenerAlumnosPorGradoSeccionAsync(int gradoSeccionId);
        Task<List<RegistroNota>> ObtenerRegistrosAsync(int asignaturaId, int bimestreId, List<int> alumnoIds);
        Task<RegistroNota?> ObtenerRegistroConDetallesAsync(int alumnoId, int asignaturaId, int bimestreId);
        Task<RegistroNota> CrearRegistroAsync(RegistroNota registro);
        Task GuardarCambiosAsync();
        Task<List<RegistroNota>> ObtenerNotasPorAlumnoAsync(int alumnoId);
    }
}