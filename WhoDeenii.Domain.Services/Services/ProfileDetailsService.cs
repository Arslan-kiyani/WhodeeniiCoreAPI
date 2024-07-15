using AutoMapper;
using Microsoft.Extensions.Logging;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using WhoDeenii.Infrastructure.Repository.Mappers;


namespace WhoDeenii.Domain.Services.Services
{
    public class ProfileDetailsService : IProfileDetailsService
    {
        private readonly IProfileDetailsRepository _profileDetailsRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        
        public ProfileDetailsService(IReservationRepository reservationRepository, IProfileDetailsRepository profileDetailsRepository, IMapper mapper)
        {
            _profileDetailsRepository = profileDetailsRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            
        }

        public async Task<ApiResponse<string>> AddProfileDetailsAsync(ProfileDetailRequest profileDetailRequest)
        {
            var response = new ApiResponse<string>();

            try
            {

                var reservationExists = await _reservationRepository.ReservationExistsAsync(profileDetailRequest.ReservationId);
                if (!reservationExists)
                {
                    response.IsRequestSuccessful = false;
                    response.Errors = new List<string> { "Reservation ID does not exist." };
                    return response;
                }

                var profile = _mapper.Map<ProfileDetails>(profileDetailRequest);
                await _profileDetailsRepository.AddProfileDetailsAsync(profile);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Profile details added successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };

                return response;
            }
        }

    }

}
