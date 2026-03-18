using SimpleNoteApp.DTOs.Auth;

namespace SimpleNoteApp.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}
