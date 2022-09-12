using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.SecurityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Security.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
