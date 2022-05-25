namespace Services;

public interface IReservationService
{
    Task<Result> InsertAsync(ReservationMainDto requestDto);
    Task<Result> UpdateAsync(ReservationMainDto requestDto);
    Task<DataResult<List<ReservationMainDto>>> ListAsync();
    Task<DataResult<ReservationMainDto>> GetAsync(int id);
    Task<Result> DeleteAsync(int id);
}