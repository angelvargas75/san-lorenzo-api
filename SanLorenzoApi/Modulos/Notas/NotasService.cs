using SanLorenzoApi.Dominio;
using SanLorenzoApi.Modulos.Notas.DTOs;

namespace SanLorenzoApi.Modulos.Notas
{
    public class NotasService : INotasService
    {
        private readonly INotasRepository _repository;
        private const int NUMERO_MAXIMO_NOTAS = 5;

        public NotasService(INotasRepository repository)
        {
            _repository = repository;
        }

        // ===== Vista del Docente: tabla de alumnos con sus notas =====
        public async Task<List<AlumnoNotaDto>> ObtenerNotasPorSeccionAsync(int gradoSeccionId, int asignaturaId, int bimestreId)
        {
            var alumnos = await _repository.ObtenerAlumnosPorGradoSeccionAsync(gradoSeccionId);
            var alumnoIds = alumnos.Select(a => a.Id).ToList();

            var registros = await _repository.ObtenerRegistrosAsync(asignaturaId, bimestreId, alumnoIds);
            var registrosPorAlumno = registros.ToDictionary(r => r.AlumnoId);

            var resultado = new List<AlumnoNotaDto>();

            foreach (var alumno in alumnos)
            {
                var dto = new AlumnoNotaDto
                {
                    AlumnoId = alumno.Id,
                    AlumnoNombre = alumno.Usuario.Nombre,
                    Detalles = new List<DetalleNotaDto>()
                };

                if (registrosPorAlumno.TryGetValue(alumno.Id, out var registro))
                {
                    dto.RegistroNotaId = registro.Id;
                    dto.Promedio = registro.Promedio;

                    // Llenar las 5 posiciones, con null si esa nota específica no existe aún
                    for (int i = 1; i <= NUMERO_MAXIMO_NOTAS; i++)
                    {
                        var detalle = registro.Detalles.FirstOrDefault(d => d.NumeroNota == i);
                        dto.Detalles.Add(new DetalleNotaDto
                        {
                            NumeroNota = i,
                            Valor = detalle?.Valor
                        });
                    }
                }
                else
                {
                    // El alumno aún no tiene registro de notas para esta asignatura/bimestre
                    dto.RegistroNotaId = null;
                    dto.Promedio = null;
                    for (int i = 1; i <= NUMERO_MAXIMO_NOTAS; i++)
                        dto.Detalles.Add(new DetalleNotaDto { NumeroNota = i, Valor = null });
                }

                resultado.Add(dto);
            }

            return resultado;
        }

        // ===== Guardar Cambios / Carga Masiva =====
        public async Task<(bool exito, string mensaje)> GuardarLoteAsync(GuardarNotasLoteDto dto)
        {
            if (dto.Notas == null || dto.Notas.Count == 0)
                return (false, "No se enviaron notas para guardar");

            foreach (var nota in dto.Notas)
            {
                if (nota.NumeroNota < 1 || nota.NumeroNota > NUMERO_MAXIMO_NOTAS)
                    return (false, $"El número de nota debe estar entre 1 y {NUMERO_MAXIMO_NOTAS}");

                if (nota.Valor < 0 || nota.Valor > 20)
                    return (false, $"La nota debe estar entre 0 y 20 (alumno {nota.AlumnoId})");
            }

            // Agrupamos por alumno, porque cada alumno puede traer varias notas en el mismo lote
            var notasPorAlumno = dto.Notas.GroupBy(n => n.AlumnoId);

            foreach (var grupo in notasPorAlumno)
            {
                int alumnoId = grupo.Key;

                var registro = await _repository.ObtenerRegistroConDetallesAsync(alumnoId, dto.AsignaturaId, dto.BimestreId);

                if (registro == null)
                {
                    // Primera vez que se le pone una nota a este alumno en esta asignatura/bimestre
                    registro = new RegistroNota
                    {
                        AlumnoId = alumnoId,
                        AsignaturaId = dto.AsignaturaId,
                        BimestreId = dto.BimestreId,
                        Detalles = new List<DetalleNota>(),
                        FechaActualizacion = DateTime.UtcNow
                    };
                    registro = await _repository.CrearRegistroAsync(registro);
                }

                foreach (var notaIndividual in grupo)
                {
                    var detalleExistente = registro.Detalles.FirstOrDefault(d => d.NumeroNota == notaIndividual.NumeroNota);

                    if (detalleExistente != null)
                    {
                        // Ya existía esa posición de nota (ej. Nota 1) -> se actualiza
                        detalleExistente.Valor = notaIndividual.Valor;
                    }
                    else
                    {
                        // Nota nueva en esa posición -> se agrega
                        registro.Detalles.Add(new DetalleNota
                        {
                            RegistroNotaId = registro.Id,
                            NumeroNota = notaIndividual.NumeroNota,
                            Valor = notaIndividual.Valor
                        });
                    }
                }

                // ===== Cálculo automático del promedio =====
                // Se basa SOLO en las notas que realmente existen (no cuenta posiciones vacías)
                registro.Promedio = registro.Detalles.Count > 0
                    ? Math.Round(registro.Detalles.Average(d => d.Valor), 2)
                    : 0;

                registro.FechaActualizacion = DateTime.UtcNow;
            }

            await _repository.GuardarCambiosAsync();

            return (true, "Notas guardadas correctamente");
        }

        // ===== Vista del Alumno: Mis Calificaciones =====
        public async Task<List<MiNotaDto>> ObtenerMisNotasAsync(int alumnoId)
        {
            var registros = await _repository.ObtenerNotasPorAlumnoAsync(alumnoId);

            return registros.Select(r => new MiNotaDto
            {
                AsignaturaNombre = r.Asignatura.Nombre,
                BimestreNombre = r.Bimestre.Nombre,
                Promedio = r.Promedio,
                Detalles = r.Detalles
                    .OrderBy(d => d.NumeroNota)
                    .Select(d => new DetalleNotaDto { NumeroNota = d.NumeroNota, Valor = d.Valor })
                    .ToList()
            }).ToList();
        }
    }
}