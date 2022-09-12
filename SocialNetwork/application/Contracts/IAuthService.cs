//using SocialNetwork.Entities.SecurityModels;
//using SocialNetwork.Security.DTO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SocialNetwork.Application.Contracts
//{
//    public interface IAuthService
//    {
//        public Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, string ipAddress);
//        public Task<AuthenticateResponse> RefreshTokenAsync(string token, string ipAddress);
//        public Task RevokeTokenAsync(string token, string ipAddress);
//        public Task<List<RefreshToken>> GetUserRefreshTokensAsync(string id);
//    }
//}
