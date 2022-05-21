namespace Services;

public interface IRoleService
{
    Task<Result> CreateAsync(string roleName);
    Task<Result> UpdateAsync(int id, string roleName);
    Task<DataResult<List<RoleResponseDto>>> ListAsync();
}