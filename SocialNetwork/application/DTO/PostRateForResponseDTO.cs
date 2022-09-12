using SocialNetwork.Entities.Models;
using System;

namespace SocialNetwork.Application.DTO
{
    public class PostRateForResponseDTO
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string LikeStatus { get; set; }
    }
}
