using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Domain.Contracts.Interfaces
{
    public interface IReservationService
    {
        Task<ApiResponse<string>> AddReservationAsync(ReservationRequest request);
        Task<ApiResponse<string>> GetReservationByIdAsync(int id);
        Task<ApiResponse<Reservation>> ReservationByReservationIdAsync(string reservationId);

        Task<ApiResponse<GetReservationDetailsResponse>> GetReservationDetailsByReservationIdAsync(string reservationId);
    }
}
