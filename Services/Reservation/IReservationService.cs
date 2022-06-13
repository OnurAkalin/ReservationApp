namespace Services;

public interface IReservationService
{
    Task<Result> InsertAsync(ReservationRequestDto requestDto);
    Task<Result> UpdateAsync(ReservationRequestDto requestDto);
    Task<DataResult<List<ReservationResponseDto>>> ListAsync();
    Task<DataResult<ReservationResponseDto>> GetAsync(int id);
    Task<Result> CancelAsync(int id);
}