namespace Services;

public interface IRoleService
{
    Task<Result> InsertAsync(string roleName);
    Task<Result> UpdateAsync(int id, string roleName);
    Task<DataResult<RoleResponseDto>> GetAsync(int id);
    Task<DataResult<List<RoleResponseDto>>> ListAsync();
    Task<Result> DeleteAsync(int id);
}