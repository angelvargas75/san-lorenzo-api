using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Academico
{
    public interface IAcademicoRepository
    {
        // Cursos
        Task<List<Curso>> ObtenerCursosAsync();
        Task<Curso?> ObtenerCursoPorIdAsync(int id);
        Task<Curso> CrearCursoAsync(Curso curso);

        // Asignaturas
        Task<List<Asignatura>> ObtenerAsignaturasAsync();
        Task<Asignatura?> ObtenerAsignaturaPorIdAsync(int id);
        Task<Asignatura> CrearAsignaturaAsync(Asignatura asignatura);

        // Grados y Secciones
        Task<List<GradoSeccion>> ObtenerGradoSeccionesAsync();
        Task<GradoSeccion?> ObtenerGradoSeccionPorIdAsync(int id);
        Task<GradoSeccion> CrearGradoSeccionAsync(GradoSeccion gradoSeccion);

        // Docente-Asignatura
        Task<List<DocenteAsignatura>> ObtenerDocenteAsignaturasAsync();
        Task<DocenteAsignatura> CrearDocenteAsignaturaAsync(DocenteAsignatura docenteAsignatura);
        Task<bool> ExisteAsignacionAsync(int docenteId, int asignaturaId, int gradoSeccionId);
    }
}