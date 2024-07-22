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
        private readonly ILoggerService _loggerService;
        
        public ProfileDetailsService(IReservationRepository reservationRepository, IProfileDetailsRepository profileDetailsRepository, IMapper mapper,ILoggerService loggerService)
        {
            _profileDetailsRepository = profileDetailsRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _loggerService = loggerService;
            
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

                var logEntry = new LogEntry
                {
                    Level = "Error",
                    Application = "WhoDeenii",
                    MethodInfo = "SomeService.DoSomethingAsync",
                    Message = ex.Message,
                    Exception = ex.ToString(),
                    Timestamp = DateTime.Now,
                    TransactionId = ex.Message,
                    Context = "Additional context if needed"
                };

                await _loggerService.LogAsync(logEntry);

                return response;
            }
        }

        public async Task<ApiResponse<ProfileDetails>> GetProfileDetailsByReservationIdAsync(string reservationId)
        {
            var response = new ApiResponse<ProfileDetails>();
            try
            {
                var profileDetails = await _profileDetailsRepository.GetByReservationIdAsync(reservationId);
                if (profileDetails != null)
                {
                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = new ProfileDetails
                    {
                        ReservationId = profileDetails.ReservationId,
                        IsProfileUpdated = profileDetails.IsProfileUpdated,
                        CreatedDate = profileDetails.CreatedDate
                       
                    };
                }
                else
                {
                    response.IsRequestSuccessful = false;
                    response.ErrorMessage = "Profile details not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };

                var logEntry = new LogEntry
                {
                    Level = "Error",
                    Application = "WhoDeenii",
                    MethodInfo = System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Message = ex.Message,
                    Exception = ex.ToString(),
                    Timestamp = DateTime.Now,
                    TransactionId = ex.Message,
                    Context = "Additional context if needed"
                };

                await _loggerService.LogAsync(logEntry);
            }

            return response;
        }
    }

}
