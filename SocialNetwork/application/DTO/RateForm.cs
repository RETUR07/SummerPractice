using SocialNetwork.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Application.DTO
{
    public class RateForm
    {
        public int ObjectId { get; set; }

        [Range(0, 1)]
        public LikeStatus LikeStatus { get; set; }
    }
}
