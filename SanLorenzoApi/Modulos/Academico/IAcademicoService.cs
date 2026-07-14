using SanLorenzoApi.Modulos.Academico.DTOs;

namespace SanLorenzoApi.Modulos.Academico
{
    public interface IAcademicoService
    {
        Task<List<CursoDto>> ObtenerCursosAsync();
        Task<(bool exito, string mensaje, CursoDto? curso)> CrearCursoAsync(CrearCursoDto dto);

        Task<List<AsignaturaDto>> ObtenerAsignaturasAsync();
        Task<(bool exito, string mensaje, AsignaturaDto? asignatura)> CrearAsignaturaAsync(CrearAsignaturaDto dto);

        Task<List<GradoSeccionDto>> ObtenerGradoSeccionesAsync();
        Task<(bool exito, string mensaje, GradoSeccionDto? gradoSeccion)> CrearGradoSeccionAsync(CrearGradoSeccionDto dto);

        Task<List<DocenteAsignaturaDto>> ObtenerDocenteAsignaturasAsync();
        Task<(bool exito, string mensaje, DocenteAsignaturaDto? asignacion)> CrearDocenteAsignaturaAsync(CrearDocenteAsignaturaDto dto);
    }
}