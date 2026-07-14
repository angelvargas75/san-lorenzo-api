using Microsoft.AspNetCore.Mvc;
using SanLorenzoApi.Modulos.Auth.DTOs;

namespace SanLorenzoApi.Modulos.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var resultado = await _authService.LoginAsync(request);

            if (resultado == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(resultado);
        }
    }
}