using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanLorenzoApi.Modulos.Academico.DTOs;
using SanLorenzoApi.Shared.Common;

namespace SanLorenzoApi.Modulos.Academico
{
    [ApiController]
    [Route("api/academico/cursos")]
    [Authorize]  
    public class CursosController : ControllerBase
    {
        private readonly IAcademicoService _service;

        public CursosController(IAcademicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var cursos = await _service.ObtenerCursosAsync();
            return Ok(ApiResponse<List<CursoDto>>.Ok(cursos));
        }

        [HttpPost]
        [Authorize(Roles = "Coordinador")]   // solo Coordinador puede crear
        public async Task<IActionResult> Crear([FromBody] CrearCursoDto dto)
        {
            var (exito, mensaje, curso) = await _service.CrearCursoAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<CursoDto>.Fail(mensaje));

            return Created(string.Empty, ApiResponse<CursoDto>.Ok(curso, mensaje));
        }
    }
}