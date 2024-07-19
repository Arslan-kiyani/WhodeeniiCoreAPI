﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Requests;
using WhoDeenii.DTO.Response;
using WhoDeenii.Infrastructure.DataAccess.Entities;
using WhoDeenii.Infrastructure.Repository.Interfaces;


namespace WhoDeenii.Domain.Services.Services
{
    public class RegistrationCardService : IRegistrationCardService
    {
        private readonly IRegistrationCardRepository _repository;
        private readonly IMapper _mapper;
        private readonly ImageSettings _imageSettings;

        public RegistrationCardService(IRegistrationCardRepository repository, IMapper mapper,IOptions<ImageSettings> options)
        {
            _repository = repository;
            _mapper = mapper;
            _imageSettings = options.Value;
        }

        public async Task<ApiResponse<string>> AddRegistrationCardAsync(RegistrationCardRequest registrationCardRequest)
        {
            var response = new ApiResponse<string>();

            
            var reservationExists = await _repository.CheckReservationIdAsync(registrationCardRequest.ReservationId);
            if (!reservationExists)
            {
                response.IsRequestSuccessful = false;
                response.Errors = new List<string> { "Reservation ID does not exist in Reservation Table" };
                return response;
            }

            var existingRegistrationCard = await _repository.GetRegistrationCardByReservationIdAsync(registrationCardRequest.ReservationId);

            if (existingRegistrationCard != null)
            {
                existingRegistrationCard.ModifiedDate = DateTime.Now;
                if (registrationCardRequest.Imagepath != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                    var fileName = $"{timestamp}_{registrationCardRequest.Imagepath.FileName}";
                    var imagePath = Path.Combine(_imageSettings.BasePath, fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await registrationCardRequest.Imagepath.CopyToAsync(fileStream);
                        }

                        existingRegistrationCard.Imagepath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        response.IsRequestSuccessful = false;
                        response.Errors = new List<string> { "Error saving registration card image" };
                        return response;
                    }
                }

                await _repository.UpdateRegistrationCardAsync(existingRegistrationCard);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Registration card updated successfully.";
                return response;
            }
            else
            {
                    
                var registrationCard = _mapper.Map<RegistrationCard>(registrationCardRequest);
                registrationCard.CreatedDate = DateTime.Now;
                registrationCard.ModifiedDate = DateTime.Now;

                if (registrationCardRequest.Imagepath != null)
                {
                    string timestamp = DateTime.Now.ToString("yyyy MM dd HHmmss");
                    var fileName = $"{timestamp}_{registrationCardRequest.Imagepath.FileName}";
                    var imagePath = Path.Combine(_imageSettings.BasePath, fileName);

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await registrationCardRequest.Imagepath.CopyToAsync(fileStream);
                        }

                        registrationCard.Imagepath = imagePath;
                    }
                    catch (Exception ex)
                    {
                        response.IsRequestSuccessful = false;
                        response.Errors = new List<string> { "Error saving registration card image" };
                        return response;
                    }
                }

                await _repository.AddRegistrationCardAsync(registrationCard);

                response.IsRequestSuccessful = true;
                response.SuccessResponse = "Registration card added successfully.";
                return response;
            } 
        }
    }
}
