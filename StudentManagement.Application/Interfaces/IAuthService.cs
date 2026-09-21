using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Interfaces;

public interface IAuthService
{
    Task<ResponseDto<RegisterResponseDto>> RegisterAsync(RegisterRequestDto dto);
    
    Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto dto);

    Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);

    Task<ResponseDto<bool>> LogoutAsync(RefreshTokenRequestDto dto);
}