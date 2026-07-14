using SanLorenzoApi.Modulos.Auth.DTOs;

namespace SanLorenzoApi.Modulos.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
