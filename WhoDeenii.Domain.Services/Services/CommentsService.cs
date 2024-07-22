using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;
using WhoDeenii.Infrastructure.Repository.Respositories;

namespace WhoDeenii.Domain.Services.Services
{
    public class CommentsService : ICommentsService
    {

        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public CommentsService(ICommentsRepository commentsRepository,IMapper mapper, ILoggerService logger)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<string>> AddCommentAsync(CommentsRequest request)
        {
            var response = new ApiResponse<string>();

            try
            {
                var reservationExists = await _commentsRepository.CheckReservationIdAsync(request.ReservationId);
                if (reservationExists)
                {

                    response.IsRequestSuccessful = true;
                    response.SuccessResponse = "Comment added successfully.";
                    return response;
                }

                var Comments = _mapper.Map<Comments>(request);

                await _commentsRepository.AddCommentAsync(Comments);
            }catch (Exception ex)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Invalid Reservation Id" };

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

                await _logger.LogAsync(logEntry);

            }
            return response;
        }

        public async Task<ApiResponse<List<Comments>>> GetCommentsByReservationIdAsync(string reservationId)
        {
            var response = new ApiResponse<List<Comments>>();

            try
            {
                var comments = await _commentsRepository.GetCommentsByReservationIdAsync(reservationId);
                if (!comments.Any())
                {
                    response.IsRequestSuccessful = false;
                    response.Errors = new List<string> { "Invalid  Reservation Id." };
                    return response;
                }

                response.IsRequestSuccessful = true;
                response.SuccessResponse = comments;
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
                    MethodInfo = System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Message = ex.Message,
                    Exception = ex.ToString(),
                    Timestamp = DateTime.Now,
                    TransactionId = ex.Message,
                    Context = "Additional context if needed"
                };

                await _logger.LogAsync(logEntry);

            }
            return response;
        }
    }
}
