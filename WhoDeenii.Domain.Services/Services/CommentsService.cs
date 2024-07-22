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

        public CommentsService(ICommentsRepository commentsRepository,IMapper mapper)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> AddCommentAsync(CommentsRequest request)
        {
            var response = new ApiResponse<string>();

            var reservationExists = await _commentsRepository.CheckReservationIdAsync(request.ReservationId);
            if (!reservationExists)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Reservation ID does not exist in the Reservation Table." };
                return response;
            }

            var Comments= _mapper.Map<Comments>(request);

            await _commentsRepository.AddCommentAsync(Comments);

            response.IsRequestSuccessful = true;
            response.SuccessResponse = "Comment added successfully.";
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
                return response;
            }
        }
    }
}
