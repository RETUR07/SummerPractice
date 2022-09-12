using AutoMapper;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Services;
using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Mapping
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            CreateMap<MessageForm, Message>()
                .ForMember(m => m.Blobs, opt => opt.MapFrom(x => x.Content.FormFilesToBlobs()));

            CreateMap<Message, MessageForResponseDTO>()
                .ForMember(m => m.From, opt => opt.MapFrom(x => x.User.Id));

            CreateMap<Chat, ChatForResponseDTO>()
                .ForMember(ch => ch.Users, opt => opt.MapFrom(x => x.Users.Select(x=>x.Id)))
                .ForMember(ch => ch.Messages, opt => opt.MapFrom(x => x.Messages));
        }
    }
}
