using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Data;
using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Asistencia
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly AppDbContext _context;

        public AsistenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Alumno>> ObtenerAlumnosPorGradoSeccionAsync(int gradoSeccionId)
        {
            return await _context.Alumnos
                .Include(a => a.Usuario)
                .Where(a => a.GradoSeccionId == gradoSeccionId)
                .OrderBy(a => a.Usuario.Nombre)
                .ToListAsync();
        }

        public async Task<List<Dominio.Asistencia>> ObtenerPorFechaAsync(int asignaturaId, DateTime fecha, List<int> alumnoIds)
        {
            return await _context.Asistencias
                .Where(a => a.AsignaturaId == asignaturaId
                         && a.Fecha.Date == fecha.Date
                         && alumnoIds.Contains(a.AlumnoId))
                .ToListAsync();
        }

        public async Task<Dominio.Asistencia?> ObtenerRegistroAsync(int alumnoId, int asignaturaId, DateTime fecha)
        {
            return await _context.Asistencias
                .FirstOrDefaultAsync(a => a.AlumnoId == alumnoId
                                        && a.AsignaturaId == asignaturaId
                                        && a.Fecha.Date == fecha.Date);
        }

        public async Task CrearAsync(Dominio.Asistencia asistencia)
        {
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Dominio.Asistencia>> ObtenerPorAlumnoAsync(int alumnoId)
        {
            return await _context.Asistencias
                .Include(a => a.Asignatura)
                .Where(a => a.AlumnoId == alumnoId)
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();
        }
    }
}