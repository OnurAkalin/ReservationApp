namespace Services;

public interface IEmployeeService
{
    Task<Result> InsertAsync(EmployeeRequestDto requestDto);
    Task<DataResult<EmployeeResponseDto>> GetAsync(int id);
    Task<DataResult<List<EmployeeResponseDto>>> ListAsync();
    Task<Result> DeleteAsync(int id);
}