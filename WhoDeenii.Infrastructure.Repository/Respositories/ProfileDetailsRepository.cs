using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhoDeenii.Infrastructure.DataAccess;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;


namespace WhoDeenii.Infrastructure.Repository.Respositories
{
    public class ProfileDetailsRepository : IProfileDetailsRepository
    {
        private readonly WhoDeeniiDbContext _whoDeeniiDbContext;
        private readonly ILogger<ProfileDetailsRepository> _logger;

        public ProfileDetailsRepository(WhoDeeniiDbContext whoDeeniiDbContext, ILogger<ProfileDetailsRepository> logger)
        {
            _whoDeeniiDbContext = whoDeeniiDbContext;
            _logger = logger;
        }

        public async Task AddProfileDetailsAsync(ProfileDetails profile)
        {
            await _whoDeeniiDbContext.AddAsync(profile);
            await _whoDeeniiDbContext.SaveChangesAsync();
        }

        public async Task<ProfileDetails?> GetByReservationIdAsync(string reservationId)
        {
            return await _whoDeeniiDbContext.ProfileDetails
                                .FirstOrDefaultAsync(pd => pd.ReservationId == reservationId);
        }
    }

}
