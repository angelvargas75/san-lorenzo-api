using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanLorenzoApi.Modulos.Academico.DTOs;
using SanLorenzoApi.Shared.Common;

namespace SanLorenzoApi.Modulos.Academico
{
    [ApiController]
    [Route("api/academico/docente-asignaturas")]
    [Authorize]
    public class DocenteAsignaturasController : ControllerBase
    {
        private readonly IAcademicoService _service;

        public DocenteAsignaturasController(IAcademicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var lista = await _service.ObtenerDocenteAsignaturasAsync();
            return Ok(ApiResponse<List<DocenteAsignaturaDto>>.Ok(lista));
        }

        [HttpPost]
        [Authorize(Roles = "Coordinador")]
        public async Task<IActionResult> Crear([FromBody] CrearDocenteAsignaturaDto dto)
        {
            var (exito, mensaje, asignacion) = await _service.CrearDocenteAsignaturaAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<DocenteAsignaturaDto>.Fail(mensaje));

            return Created(string.Empty, ApiResponse<DocenteAsignaturaDto>.Ok(asignacion, mensaje));
        }
    }
}