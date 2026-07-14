using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Data;
using SanLorenzoApi.Modulos.Notas.DTOs;
using SanLorenzoApi.Shared.Common;
using System.Security.Claims;

namespace SanLorenzoApi.Modulos.Notas
{
    [ApiController]
    [Route("api/notas")]
    [Authorize]
    public class NotasController : ControllerBase
    {
        private readonly INotasService _service;
        private readonly AppDbContext _context;

        public NotasController(INotasService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET /api/notas?gradoSeccionId=1&asignaturaId=1&bimestreId=1
        [HttpGet]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> ObtenerPorSeccion(
            [FromQuery] int gradoSeccionId,
            [FromQuery] int asignaturaId,
            [FromQuery] int bimestreId)
        {
            var resultado = await _service.ObtenerNotasPorSeccionAsync(gradoSeccionId, asignaturaId, bimestreId);
            return Ok(ApiResponse<List<AlumnoNotaDto>>.Ok(resultado));
        }

        // POST /api/notas/lote  (Guardar Cambios / Carga Masiva)
        [HttpPost("lote")]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> GuardarLote([FromBody] GuardarNotasLoteDto dto)
        {
            var (exito, mensaje) = await _service.GuardarLoteAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<object>.Fail(mensaje));

            return Ok(ApiResponse<object>.Ok(new { }, mensaje));
        }

        // GET /api/notas/mis-notas  (Alumno consulta las suyas)
        [HttpGet("mis-notas")]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> ObtenerMisNotas()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.UsuarioId == userId);
            if (alumno == null)
                return NotFound(ApiResponse<object>.Fail("No se encontró el registro de alumno asociado a este usuario"));

            var notas = await _service.ObtenerMisNotasAsync(alumno.Id);
            return Ok(ApiResponse<List<MiNotaDto>>.Ok(notas));
        }
    }
}