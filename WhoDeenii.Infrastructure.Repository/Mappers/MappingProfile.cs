using AutoMapper;
using WhoDeenii.DTO.Requests;
using WhoDeenii.Infrastructure.DataAccess.Entities;

namespace WhoDeenii.Infrastructure.Repository.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReservationRequest, Reservation>();

            CreateMap<ProfileDetailRequest, ProfileDetails>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<IDDocumentRequest, IDDocument>()
             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
             .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
             .ForMember(dest => dest.ImagePath, opt => opt.Ignore());


            CreateMap<RegistrationCardRequest, RegistrationCard>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Imagepath, opt => opt.Ignore());

            CreateMap<WhatsAppMessageRequest, WhatsAppMessage>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<WhatsAppMessageRequest, SmsMessage>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CommentsRequest, Comments>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<RoomDetailsRequest, RoomDetails>()
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
