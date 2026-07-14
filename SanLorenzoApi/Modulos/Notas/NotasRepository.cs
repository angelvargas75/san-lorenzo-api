using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Data;
using SanLorenzoApi.Dominio;

namespace SanLorenzoApi.Modulos.Notas
{
    public class NotasRepository : INotasRepository
    {
        private readonly AppDbContext _context;

        public NotasRepository(AppDbContext context)
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

        public async Task<List<RegistroNota>> ObtenerRegistrosAsync(int asignaturaId, int bimestreId, List<int> alumnoIds)
        {
            return await _context.RegistrosNotas
                .Include(r => r.Detalles)
                .Where(r => r.AsignaturaId == asignaturaId
                         && r.BimestreId == bimestreId
                         && alumnoIds.Contains(r.AlumnoId))
                .ToListAsync();
        }

        public async Task<RegistroNota?> ObtenerRegistroConDetallesAsync(int alumnoId, int asignaturaId, int bimestreId)
        {
            return await _context.RegistrosNotas
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(r => r.AlumnoId == alumnoId
                                        && r.AsignaturaId == asignaturaId
                                        && r.BimestreId == bimestreId);
        }

        public async Task<RegistroNota> CrearRegistroAsync(RegistroNota registro)
        {
            _context.RegistrosNotas.Add(registro);
            await _context.SaveChangesAsync();
            return registro;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<RegistroNota>> ObtenerNotasPorAlumnoAsync(int alumnoId)
        {
            return await _context.RegistrosNotas
                .Include(r => r.Detalles)
                .Include(r => r.Asignatura)
                .Include(r => r.Bimestre)
                .Where(r => r.AlumnoId == alumnoId)
                .OrderByDescending(r => r.Bimestre.AnioEscolar)
                .ThenBy(r => r.Bimestre.Nombre)
                .ToListAsync();
        }
    }
}