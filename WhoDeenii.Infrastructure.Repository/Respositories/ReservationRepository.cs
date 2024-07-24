using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.Intrinsics.Arm;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;


namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly WhoDeeniiDbContext _whoDeeniiDbContext;
       
        public ReservationRepository(WhoDeeniiDbContext whoDeeniiDbContext)
        {
            _whoDeeniiDbContext = whoDeeniiDbContext;
        }

        public async Task AddReservationAsync(Reservation basicDetails)
        {
            await _whoDeeniiDbContext.AddAsync(basicDetails);
            await _whoDeeniiDbContext.SaveChangesAsync();
         
        }

        public async Task<Reservation?> GetByReservationIdAsync(string reservationId)
        {
            return await _whoDeeniiDbContext.Reservations
                               .FirstOrDefaultAsync(pd => pd.ReservationId == reservationId);
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {

            return await _whoDeeniiDbContext.Reservations.FindAsync(id);
        }

        public async Task<GetReservationDetailsResponse> GetReservationDetailsByReservationIdAsync(string reservationId)
        {
            var reservationDetails = await (from rc in _whoDeeniiDbContext.RegistrationCards
                                            join idd in _whoDeeniiDbContext.IDDocuments on rc.ReservationId equals idd.ReservationId into iddGroup
                                            from idd in iddGroup.DefaultIfEmpty()
                                            join pd in _whoDeeniiDbContext.ProfileDetails on rc.ReservationId equals pd.ReservationId into pdGroup
                                            from pd in pdGroup.DefaultIfEmpty()
                                            where rc.ReservationId == reservationId
                                            select new GetReservationDetails
                                            {
                                                ReservationId = reservationId,
                                                ModifiedDateRegistrationCards = rc.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                IDDocumentsimagePath = idd.ImagePath ?? string.Empty,
                                                ModifiedDateIDDocuments = idd.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                RegistrationCardsimagePath = rc.Imagepath ?? string.Empty,
                                                IsProfileUpdated = pd != null ? pd.IsProfileUpdated : false,
                                                ProfileCreatedDate = /*pd != null && */ pd.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                            }).FirstOrDefaultAsync();

            return new GetReservationDetailsResponse
            {
                ReservationDetails = reservationDetails
            };
        }


        public async Task<bool> ReservationExistsAsync(string? reservationId)
        {
            return await _whoDeeniiDbContext.Reservations
             .AnyAsync(r => r.ReservationId == reservationId);
        }
    }
}

