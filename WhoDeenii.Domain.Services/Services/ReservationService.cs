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
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(IReservationRepository repository, IMapper mapper, ILogger<ReservationService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogError(ex, "An error occurred while adding reservation");

                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };

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
                _logger.LogError(ex, $"An error occurred while retrieving reservation details for id: {id}");

                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };

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
                _logger.LogError(ex, "An error occurred while retrieving reservation details");

                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };
                return response;
            }
        }
    }

}
