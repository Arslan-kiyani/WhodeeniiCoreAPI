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
        private readonly ILogger<ReservationRepository> _logger;
        public ReservationRepository(WhoDeeniiDbContext whoDeeniiDbContext,ILogger<ReservationRepository> logger)
        {
            _whoDeeniiDbContext = whoDeeniiDbContext;
            _logger = logger;
        }

        public async Task AddReservationAsync(Reservation basicDetails)
        {
            try
            {
                await _whoDeeniiDbContext.AddAsync(basicDetails);
                await _whoDeeniiDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding reservation details");
               
            }
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
            var missingTables = new List<string>();

            var registrationCard = await _whoDeeniiDbContext.RegistrationCards
                .FirstOrDefaultAsync(id => id.ReservationId == reservationId);

            if (registrationCard == null)
            {
                missingTables.Add("RegistrationCard");
            }

            var idDocument = await _whoDeeniiDbContext.IDDocuments
                .FirstOrDefaultAsync(id => id.ReservationId == reservationId);

            if (idDocument == null)
            {
                missingTables.Add("IDDocument");
            }
           
            var profileDetails = await _whoDeeniiDbContext.ProfileDetails
                .FirstOrDefaultAsync(id => id.ReservationId == reservationId);

            if (profileDetails == null)
            {
                missingTables.Add("ProfileDetails");
            }

            var reservationDetails = new GetReservationDetails
            {
                ReservationId = reservationId,
                ModifiedDateRegistrationCards = registrationCard?.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") ?? ("N/A"),
                IDDocumentsimagePath = idDocument?.ImagePath ?? string.Empty,
                ModifiedDateIDDocuments = idDocument?.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") ?? ("N/A"),
                RegistrationCardsimagePath = registrationCard?.Imagepath ?? string.Empty,
                IsProfileUpdated = profileDetails?.IsProfileUpdated ?? false,
                ProfileCreatedDate = profileDetails?.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss") ?? ("N/A"),
            };

            var response = new GetReservationDetailsResponse
            {
                ReservationDetails = reservationDetails,
                MissingTablesMessage = missingTables.Count > 0
            ? $"ReservationId {reservationId} does not exist in the following table(s): {string.Join(", ", missingTables)}."
            : null
            };

            return response;
        }
        public async Task<bool> ReservationExistsAsync(string? reservationId)
        {
            return await _whoDeeniiDbContext.Reservations
             .AnyAsync(r => r.ReservationId == reservationId);
        }
    }
}

