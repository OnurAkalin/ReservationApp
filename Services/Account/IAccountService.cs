using Core.Jwt;
using Core.Utilities.Results;
using Domain.Entities;

namespace Services;

public interface IAccountService
{
    Task<Result> RegisterAsync(RegisterRequestDto requestDto);
    Task<DataResult<TokenResponseDto>> LoginAsync(LoginRequestDto requestDto);
    Task<Result> CreateRoleAsync(string roleName);
    Task<Result> UpdateRoleAsync(Guid id, string roleName);
    Task<DataResult<List<RoleResponseDto>>> GetRolesAsync();
}