using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Security.DTO
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
