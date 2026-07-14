using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanLorenzoApi.Modulos.Usuarios.DTOs;
using SanLorenzoApi.Shared.Common;

namespace SanLorenzoApi.Modulos.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Coordinador")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _service;

        public UsuariosController(IUsuariosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarios = await _service.ObtenerTodosAsync();
            return Ok(ApiResponse<List<UsuarioDto>>.Ok(usuarios));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuario = await _service.ObtenerPorIdAsync(id);
            if (usuario == null)
                return NotFound(ApiResponse<UsuarioDto>.Fail("Usuario no encontrado"));

            return Ok(ApiResponse<UsuarioDto>.Ok(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearUsuarioDto dto)
        {
            var (exito, mensaje, usuario) = await _service.CrearAsync(dto);
            if (!exito)
                return BadRequest(ApiResponse<UsuarioDto>.Fail(mensaje));

            return CreatedAtAction(nameof(ObtenerPorId), new { id = usuario!.Id },
                ApiResponse<UsuarioDto>.Ok(usuario, mensaje));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarUsuarioDto dto)
        {
            var (exito, mensaje) = await _service.ActualizarAsync(id, dto);
            if (!exito)
                return BadRequest(ApiResponse<object>.Fail(mensaje));

            return Ok(ApiResponse<object>.Ok(new { }, mensaje));
        }
    }
}