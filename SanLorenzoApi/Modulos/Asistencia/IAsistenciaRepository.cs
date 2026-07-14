using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Asistencia
{
    public interface IAsistenciaRepository
    {
        Task<List<Alumno>> ObtenerAlumnosPorGradoSeccionAsync(int gradoSeccionId);
        Task<List<Dominio.Asistencia>> ObtenerPorFechaAsync(int asignaturaId, DateTime fecha, List<int> alumnoIds);
        Task<Dominio.Asistencia?> ObtenerRegistroAsync(int alumnoId, int asignaturaId, DateTime fecha);
        Task CrearAsync(Dominio.Asistencia asistencia);
        Task GuardarCambiosAsync();
        Task<List<Dominio.Asistencia>> ObtenerPorAlumnoAsync(int alumnoId);
    }
}