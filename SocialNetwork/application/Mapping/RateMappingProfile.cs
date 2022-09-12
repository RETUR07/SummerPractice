using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;

namespace SocialNetwork.Application.Mapping
{
    public class RateMappingProfile : Profile
    {
        public RateMappingProfile()
        {
            CreateMap<Rate, PostRateForResponseDTO>()
                .ForMember(r => r.UserId, opt => opt.MapFrom(x => x.UserId))
                .ForMember(r => r.LikeStatus, opt => opt.MapFrom(x => x.LikeStatus.ToString()))
                .ForMember(r => r.PostId, opt => opt.MapFrom(x => x.PostId));

            CreateMap<RateForm, Rate>()
                .ForMember(r => r.LikeStatus, opt => opt.MapFrom(x => x.LikeStatus))
                .ForMember(r => r.PostId, opt => opt.MapFrom(x => x.ObjectId));
                
        }
    }
}
