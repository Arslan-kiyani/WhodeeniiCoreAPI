﻿using AutoMapper;
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
    public class RoomDetailsService : IRoomDetailsService
    {

        private readonly IRoomDetailsRepository _roomDetailsRepository;
        private readonly IMapper _mapper;

        public RoomDetailsService(IRoomDetailsRepository roomDetailsRepository, IMapper mapper)
        {
            _roomDetailsRepository = roomDetailsRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> CreateRoomDetailsAsync(RoomDetailsRequest request)
        {
            var response = new ApiResponse<string>();

            try
            {
                var room = _mapper.Map<RoomDetails>(request);
                await _roomDetailsRepository.CreateRoomDetailsAsync(room);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Room Details details added successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { ex.Message };

                return response;
            }
   
        }

        public async Task DeleteRoomDetailsAsync(int id)
        {
            await _roomDetailsRepository.DeleteRoomDetailsAsync(id);
        }

        public Task<RoomDetails> GetRoomDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoomDetailsAsync(int id, RoomDetails roomDetails)
        {
            var existingRoomDetails = await _roomDetailsRepository.GetRoomDetailsAsync(id);
            if (existingRoomDetails != null)
            {
                existingRoomDetails.RoomNumber = roomDetails.RoomNumber;
                existingRoomDetails.Name = roomDetails.Name;
                existingRoomDetails.StartDate = roomDetails.StartDate;
                existingRoomDetails.EndDate = roomDetails.EndDate;
                existingRoomDetails.ModifiedDate = DateTime.Now;

                await _roomDetailsRepository.UpdateRoomDetailsAsync(existingRoomDetails);
            }
        }

    }
}
