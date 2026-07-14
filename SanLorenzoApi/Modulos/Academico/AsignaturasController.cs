using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanLorenzoApi.Modulos.Academico.DTOs;
using SanLorenzoApi.Shared.Common;

namespace SanLorenzoApi.Modulos.Academico
{
    [ApiController]
    [Route("api/academico/asignaturas")]
    [Authorize]
    public class AsignaturasController : ControllerBase
    {
        private readonly IAcademicoService _service;

        public AsignaturasController(IAcademicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var asignaturas = await _service.ObtenerAsignaturasAsync();
            return Ok(ApiResponse<List<AsignaturaDto>>.Ok(asignaturas));
        }

        [HttpPost]
        [Authorize(Roles = "Coordinador")]
        public async Task<IActionResult> Crear([FromBody] CrearAsignaturaDto dto)
        {
            var (exito, mensaje, asignatura) = await _service.CrearAsignaturaAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<AsignaturaDto>.Fail(mensaje));

            return Created(string.Empty, ApiResponse<AsignaturaDto>.Ok(asignatura, mensaje));
        }
    }
}