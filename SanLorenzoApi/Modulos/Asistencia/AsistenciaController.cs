using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanLorenzoApi.Data;
using SanLorenzoApi.Modulos.Asistencia.DTOs;
using SanLorenzoApi.Shared.Common;
using System.Security.Claims;

namespace SanLorenzoApi.Modulos.Asistencia
{
    [ApiController]
    [Route("api/asistencia")]
    [Authorize]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _service;
        private readonly AppDbContext _context;

        public AsistenciaController(IAsistenciaService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET /api/asistencia?gradoSeccionId=1&asignaturaId=1&fecha=2026-05-15
        [HttpGet]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> ObtenerPorSeccion(
            [FromQuery] int gradoSeccionId,
            [FromQuery] int asignaturaId,
            [FromQuery] DateTime fecha)
        {
            var resultado = await _service.ObtenerPorSeccionAsync(gradoSeccionId, asignaturaId, fecha);
            return Ok(ApiResponse<List<AlumnoAsistenciaDto>>.Ok(resultado));
        }

        // POST /api/asistencia
        [HttpPost]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> Guardar([FromBody] GuardarAsistenciaLoteDto dto)
        {
            var (exito, mensaje) = await _service.GuardarLoteAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<object>.Fail(mensaje));

            return Ok(ApiResponse<object>.Ok(new { }, mensaje));
        }

        // GET /api/asistencia/mi-asistencia
        [HttpGet("mi-asistencia")]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> ObtenerMiAsistencia()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.UsuarioId == userId);
            if (alumno == null)
                return NotFound(ApiResponse<object>.Fail("No se encontró el registro de alumno asociado a este usuario"));

            var registros = await _service.ObtenerMiAsistenciaAsync(alumno.Id);
            return Ok(ApiResponse<List<MiAsistenciaDto>>.Ok(registros));
        }
    }
}