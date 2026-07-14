using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanLorenzoApi.Modulos.Academico.DTOs;
using SanLorenzoApi.Shared.Common;

namespace SanLorenzoApi.Modulos.Academico
{
    [ApiController]
    [Route("api/academico/grados-secciones")]
    [Authorize]
    public class GradoSeccionesController : ControllerBase
    {
        private readonly IAcademicoService _service;

        public GradoSeccionesController(IAcademicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _service.ObtenerGradoSeccionesAsync();
            return Ok(ApiResponse<List<GradoSeccionDto>>.Ok(lista));
        }

        [HttpPost]
        [Authorize(Roles = "Coordinador")]
        public async Task<IActionResult> Crear([FromBody] CrearGradoSeccionDto dto)
        {
            var (exito, mensaje, gradoSeccion) = await _service.CrearGradoSeccionAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<GradoSeccionDto>.Fail(mensaje));

            return Created(string.Empty, ApiResponse<GradoSeccionDto>.Ok(gradoSeccion, mensaje));
        }
    }
}