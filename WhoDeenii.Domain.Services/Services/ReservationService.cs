using AutoMapper;
using Microsoft.Extensions.Logging;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;

namespace WhoDeenii.Domain.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        public ReservationService(IReservationRepository repository, IMapper mapper, ILoggerService loggerService)
        {
            _repository = repository;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<string>> AddReservationAsync(ReservationRequest reservationRequest)
        {
            var response = new ApiResponse<string>();

            try
            {
                var reservation = _mapper.Map<Reservation>(reservationRequest);
                await _repository.AddReservationAsync(reservation);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Reservation added successfully.";
               
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

        public async Task<ApiResponse<string>> GetReservationByIdAsync(int id)
        {
            var response = new ApiResponse<string>();

            try
            {
                var reservation = await _repository.GetReservationByIdAsync(id);
                if (reservation == null)
                {
                    response.IsRequestSuccessful = false;
                    response.Errors = new List<string> { $"Reservation with id {id} not found." };
                }
                else
                {
                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = "Get Reservation Id successfully ";
                }

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

        public async Task<ApiResponse<GetReservationDetailsResponse>> GetReservationDetailsByReservationIdAsync(string reservationId)
        {
            var response = new ApiResponse<GetReservationDetailsResponse>();

            try
            {
                var reservationDetails = await _repository.GetReservationDetailsByReservationIdAsync(reservationId);
                if (reservationDetails == null)
                {
                    response.IsRequestSuccessful = false;
                    response.Errors = new List<string> { "Reservation ID does not exist in either table." };
                    return response;
                }

                response.IsRequestSuccessful = true;
                response.SuccessResponse = reservationDetails;
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

        public async Task<ApiResponse<Reservation>> ReservationByReservationIdAsync(string reservationId)
        {
            var response = new ApiResponse<Reservation>();
            try
            {
                var profileDetails = await _repository.GetByReservationIdAsync(reservationId);
                if (profileDetails != null)
                {
                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = new Reservation
                    {
                        ReservationId = profileDetails.ReservationId,
                        GuestName = profileDetails.GuestName,
                        HotelName = profileDetails.HotelName,

                    };
                }
                else
                {
                    response.IsRequestSuccessful = false;
                    response.ErrorMessage = "Reservation details not found.";
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
