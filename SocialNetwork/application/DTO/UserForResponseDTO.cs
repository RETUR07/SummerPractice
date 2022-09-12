using System.Collections.Generic;

namespace SocialNetwork.Application.DTO
{
    public class UserForResponseDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<int> Friends { get; set; }
        public List<int> Subscribers { get; set; }
        public List<int> Subscribed { get; set; }
        public string DateOfBirth { get; set; }
    }
}
