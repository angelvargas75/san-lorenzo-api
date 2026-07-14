using SanLorenzoApi.Modulos.Asistencia.DTOs;

namespace SanLorenzoApi.Modulos.Asistencia
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly IAsistenciaRepository _repository;
        private static readonly string[] ESTADOS_VALIDOS = { "Presente", "Tardanza", "Falta" };

        public AsistenciaService(IAsistenciaRepository repository)
        {
            _repository = repository;
        }

        // ===== Vista del Docente: tabla de alumnos para una fecha =====
        public async Task<List<AlumnoAsistenciaDto>> ObtenerPorSeccionAsync(int gradoSeccionId, int asignaturaId, DateTime fecha)
        {
            var alumnos = await _repository.ObtenerAlumnosPorGradoSeccionAsync(gradoSeccionId);
            var alumnoIds = alumnos.Select(a => a.Id).ToList();

            var registros = await _repository.ObtenerPorFechaAsync(asignaturaId, fecha, alumnoIds);
            var registrosPorAlumno = registros.ToDictionary(r => r.AlumnoId);

            return alumnos.Select(alumno =>
            {
                registrosPorAlumno.TryGetValue(alumno.Id, out var registro);

                return new AlumnoAsistenciaDto
                {
                    AlumnoId = alumno.Id,
                    AlumnoNombre = alumno.Usuario.Nombre,
                    AsistenciaId = registro?.Id,
                    Estado = registro?.Estado.ToString()
                };
            }).ToList();
        }

        // ===== Guardar Asistencia (lote) =====
        public async Task<(bool exito, string mensaje)> GuardarLoteAsync(GuardarAsistenciaLoteDto dto)
        {
            if (dto.Registros == null || dto.Registros.Count == 0)
                return (false, "No se enviaron registros de asistencia");

            foreach (var registro in dto.Registros)
            {
                if (!ESTADOS_VALIDOS.Contains(registro.Estado))
                    return (false, $"Estado inválido: '{registro.Estado}'. Debe ser Presente, Tardanza o Falta");
            }

            foreach (var registroDto in dto.Registros)
            {
                var existente = await _repository.ObtenerRegistroAsync(registroDto.AlumnoId, dto.AsignaturaId, dto.Fecha);
                var estadoEnum = Enum.Parse<Dominio.EstadoAsistencia>(registroDto.Estado);

                if (existente != null)
                {
                    // Ya existe un registro ese día -> se actualiza (evita duplicados)
                    existente.Estado = estadoEnum;
                }
                else
                {
                    await _repository.CrearAsync(new Dominio.Asistencia
                    {
                        AlumnoId = registroDto.AlumnoId,
                        AsignaturaId = dto.AsignaturaId,
                        Fecha = dto.Fecha.Date,
                        Estado = estadoEnum
                    });
                }
            }

            await _repository.GuardarCambiosAsync();
            return (true, "Asistencia guardada correctamente");
        }

        // ===== Vista del Alumno =====
        public async Task<List<MiAsistenciaDto>> ObtenerMiAsistenciaAsync(int alumnoId)
        {
            var registros = await _repository.ObtenerPorAlumnoAsync(alumnoId);

            return registros.Select(r => new MiAsistenciaDto
            {
                AsignaturaNombre = r.Asignatura.Nombre,
                Fecha = r.Fecha,
                Estado = r.Estado.ToString()
            }).ToList();
        }
    }
}