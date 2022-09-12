using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTO
{
    public class ChatForResponseDTO
    {
        public int Id { get; set; }
        public List<string> Users { get; set; }
        public List<MessageForResponseDTO> Messages { get; set; }
    }
}
