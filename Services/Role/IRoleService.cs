namespace Services;

public interface IRoleService
{
    Task<Result> CreateRoleAsync(string roleName);
    Task<Result> UpdateRoleAsync(int id, string roleName);
    Task<DataResult<List<RoleResponseDto>>> GetRolesAsync();
}