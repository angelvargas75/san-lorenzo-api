using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Data;
using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Academico
{
    public class AcademicoRepository : IAcademicoRepository
    {
        private readonly AppDbContext _context;

        public AcademicoRepository(AppDbContext context)
        {
            _context = context;
        }

        // ===== Cursos =====
        public async Task<List<Curso>> ObtenerCursosAsync()
        {
            return await _context.Cursos.OrderBy(c => c.Nombre).ToListAsync();
        }

        public async Task<Curso?> ObtenerCursoPorIdAsync(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<Curso> CrearCursoAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        // ===== Asignaturas =====
        public async Task<List<Asignatura>> ObtenerAsignaturasAsync()
        {
            return await _context.Asignaturas
                .Include(a => a.Curso)
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        public async Task<Asignatura?> ObtenerAsignaturaPorIdAsync(int id)
        {
            return await _context.Asignaturas
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Asignatura> CrearAsignaturaAsync(Asignatura asignatura)
        {
            _context.Asignaturas.Add(asignatura);
            await _context.SaveChangesAsync();
            return asignatura;
        }

        // ===== Grados y Secciones =====
        public async Task<List<GradoSeccion>> ObtenerGradoSeccionesAsync()
        {
            return await _context.GradoSecciones
                .OrderBy(gs => gs.Grado).ThenBy(gs => gs.Seccion)
                .ToListAsync();
        }

        public async Task<GradoSeccion?> ObtenerGradoSeccionPorIdAsync(int id)
        {
            return await _context.GradoSecciones.FindAsync(id);
        }

        public async Task<GradoSeccion> CrearGradoSeccionAsync(GradoSeccion gradoSeccion)
        {
            _context.GradoSecciones.Add(gradoSeccion);
            await _context.SaveChangesAsync();
            return gradoSeccion;
        }

        // ===== Docente-Asignatura =====
        public async Task<List<DocenteAsignatura>> ObtenerDocenteAsignaturasAsync()
        {
            return await _context.DocenteAsignaturas
                .Include(da => da.Docente).ThenInclude(d => d.Usuario)
                .Include(da => da.Asignatura)
                .Include(da => da.GradoSeccion)
                .ToListAsync();
        }

        public async Task<DocenteAsignatura> CrearDocenteAsignaturaAsync(DocenteAsignatura docenteAsignatura)
        {
            _context.DocenteAsignaturas.Add(docenteAsignatura);
            await _context.SaveChangesAsync();
            return docenteAsignatura;
        }

        public async Task<bool> ExisteAsignacionAsync(int docenteId, int asignaturaId, int gradoSeccionId)
        {
            return await _context.DocenteAsignaturas.AnyAsync(da =>
                da.DocenteId == docenteId &&
                da.AsignaturaId == asignaturaId &&
                da.GradoSeccionId == gradoSeccionId);
        }
    }
}