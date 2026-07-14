using SanLorenzoApi.Dominio;
using SanLorenzoApi.Modulos.Academico.DTOs;

namespace SanLorenzoApi.Modulos.Academico
{
    public class AcademicoService : IAcademicoService
    {
        private readonly IAcademicoRepository _repository;

        public AcademicoService(IAcademicoRepository repository)
        {
            _repository = repository;
        }

        // ===== Cursos =====
        public async Task<List<CursoDto>> ObtenerCursosAsync()
        {
            var cursos = await _repository.ObtenerCursosAsync();
            return cursos.Select(c => new CursoDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }).ToList();
        }

        public async Task<(bool, string, CursoDto?)> CrearCursoAsync(CrearCursoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return (false, "El nombre del curso es obligatorio", null);

            var curso = new Curso { Nombre = dto.Nombre, Descripcion = dto.Descripcion };
            await _repository.CrearCursoAsync(curso);

            return (true, "Curso creado correctamente", new CursoDto
            {
                Id = curso.Id,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion
            });
        }

        // ===== Asignaturas =====
        public async Task<List<AsignaturaDto>> ObtenerAsignaturasAsync()
        {
            var asignaturas = await _repository.ObtenerAsignaturasAsync();
            return asignaturas.Select(a => new AsignaturaDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                CursoId = a.CursoId,
                CursoNombre = a.Curso.Nombre
            }).ToList();
        }

        public async Task<(bool, string, AsignaturaDto?)> CrearAsignaturaAsync(CrearAsignaturaDto dto)
        {
            var curso = await _repository.ObtenerCursoPorIdAsync(dto.CursoId);
            if (curso == null)
                return (false, "El curso especificado no existe", null);

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return (false, "El nombre de la asignatura es obligatorio", null);

            var asignatura = new Asignatura { Nombre = dto.Nombre, CursoId = dto.CursoId };
            await _repository.CrearAsignaturaAsync(asignatura);

            return (true, "Asignatura creada correctamente", new AsignaturaDto
            {
                Id = asignatura.Id,
                Nombre = asignatura.Nombre,
                CursoId = curso.Id,
                CursoNombre = curso.Nombre
            });
        }

        // ===== Grados y Secciones =====
        public async Task<List<GradoSeccionDto>> ObtenerGradoSeccionesAsync()
        {
            var lista = await _repository.ObtenerGradoSeccionesAsync();
            return lista.Select(gs => new GradoSeccionDto
            {
                Id = gs.Id,
                Grado = gs.Grado,
                Seccion = gs.Seccion
            }).ToList();
        }

        public async Task<(bool, string, GradoSeccionDto?)> CrearGradoSeccionAsync(CrearGradoSeccionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Grado) || string.IsNullOrWhiteSpace(dto.Seccion))
                return (false, "Grado y Sección son obligatorios", null);

            var gradoSeccion = new GradoSeccion { Grado = dto.Grado, Seccion = dto.Seccion };
            await _repository.CrearGradoSeccionAsync(gradoSeccion);

            return (true, "Grado y Sección creados correctamente", new GradoSeccionDto
            {
                Id = gradoSeccion.Id,
                Grado = gradoSeccion.Grado,
                Seccion = gradoSeccion.Seccion
            });
        }

        // ===== Docente-Asignatura =====
        public async Task<List<DocenteAsignaturaDto>> ObtenerDocenteAsignaturasAsync()
        {
            var lista = await _repository.ObtenerDocenteAsignaturasAsync();
            return lista.Select(da => new DocenteAsignaturaDto
            {
                Id = da.Id,
                DocenteId = da.DocenteId,
                DocenteNombre = da.Docente.Usuario.Nombre,
                AsignaturaId = da.AsignaturaId,
                AsignaturaNombre = da.Asignatura.Nombre,
                GradoSeccionId = da.GradoSeccionId,
                GradoSeccionTexto = $"{da.GradoSeccion.Grado} {da.GradoSeccion.Seccion}"
            }).ToList();
        }

        public async Task<(bool, string, DocenteAsignaturaDto?)> CrearDocenteAsignaturaAsync(CrearDocenteAsignaturaDto dto)
        {
            bool yaExiste = await _repository.ExisteAsignacionAsync(dto.DocenteId, dto.AsignaturaId, dto.GradoSeccionId);
            if (yaExiste)
                return (false, "Este docente ya tiene asignada esta asignatura en este grado y sección", null);

            var entidad = new DocenteAsignatura
            {
                DocenteId = dto.DocenteId,
                AsignaturaId = dto.AsignaturaId,
                GradoSeccionId = dto.GradoSeccionId
            };

            await _repository.CrearDocenteAsignaturaAsync(entidad);

            // Recargar con los datos relacionados para el DTO de respuesta
            var lista = await _repository.ObtenerDocenteAsignaturasAsync();
            var creada = lista.First(x => x.Id == entidad.Id);

            return (true, "Asignación creada correctamente", new DocenteAsignaturaDto
            {
                Id = creada.Id,
                DocenteId = creada.DocenteId,
                DocenteNombre = creada.Docente.Usuario.Nombre,
                AsignaturaId = creada.AsignaturaId,
                AsignaturaNombre = creada.Asignatura.Nombre,
                GradoSeccionId = creada.GradoSeccionId,
                GradoSeccionTexto = $"{creada.GradoSeccion.Grado} {creada.GradoSeccion.Seccion}"
            });
        }
    }
}