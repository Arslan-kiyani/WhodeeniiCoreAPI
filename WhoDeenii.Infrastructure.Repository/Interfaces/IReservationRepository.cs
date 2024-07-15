using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Interfaces
{
    public interface IReservationRepository 
    {
        Task AddReservationAsync(Reservation basicDetails);
        Task<Reservation> GetReservationByIdAsync(int id);

        Task<GetReservationDetailsResponse> GetReservationDetailsByReservationIdAsync(string reservationId);
        Task<bool> ReservationExistsAsync(string? reservationId);
    }
}
