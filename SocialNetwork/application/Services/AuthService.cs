//using AutoMapper;
//using Microsoft.Extensions.Options;
//using SocialNetwork.Application.Contracts;
//using SocialNetwork.Entities.Models;
//using SocialNetwork.Entities.SecurityModels;
//using SocialNetwork.Security.Authorization;
//using SocialNetwork.Security.DTO;
//using SocialNetwork.Security.Settings;
//using SocialNetworks.Repository.Contracts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BCryptNet = BCrypt.Net.BCrypt;

//namespace SocialNetwork.Application.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly IRepositoryManager _repository;
//        private IJwtUtils _jwtUtils;
//        private readonly AppSettings _appSettings;

//        public AuthService(IRepositoryManager repository, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings)
//        {
//            _repository = repository;
//            _jwtUtils = jwtUtils;
//            _appSettings = appSettings.Value;
//        }

//        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, string ipAddress)
//        {
//            var user = _repository.User.FindByCondition(x => x.UserName == model.Username, true).SingleOrDefault();

//            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
//                throw new Exception("Username or password is incorrect");

//            var jwtToken = _jwtUtils.GenerateJwtToken(user);
//            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
//            user.RefreshTokens.Add(refreshToken);

//            removeOldRefreshTokens(user);

//            await _repository.SaveAsync();

//            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
//        }

//        public async Task<AuthenticateResponse> RefreshTokenAsync(string token, string ipAddress)
//        {
//            var user = getUserByRefreshToken(token);
//            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

//            if (refreshToken.IsRevoked)
//            {
//                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
//                _repository.User.Update(user);
//                await _repository.SaveAsync();
//            }

//            if (!refreshToken.IsActive)
//                throw new Exception("Invalid token");

//            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
//            user.RefreshTokens.Add(newRefreshToken);

//            removeOldRefreshTokens(user);

//            _repository.User.Update(user);
//            await _repository.SaveAsync();

//            var jwtToken = _jwtUtils.GenerateJwtToken(user);

//            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
//        }

//        public async Task RevokeTokenAsync(string token, string ipAddress)
//        {
//            var user = getUserByRefreshToken(token);
//            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

//            if (!refreshToken.IsActive)
//                throw new Exception("Invalid token");

//            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
//            _repository.User.Update(user);
//            await _repository.SaveAsync();
//        }

//        private User getUserByRefreshToken(string token)
//        {
//            var user = _repository.User.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token), false).SingleOrDefault();

//            if (user == null)
//                throw new Exception("Invalid token");

//            return user;
//        }

//        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
//        {
//            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
//            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
//            return newRefreshToken;
//        }

//        private void removeOldRefreshTokens(User user)
//        {
//            user.RefreshTokens.RemoveAll(x =>
//                !x.IsActive &&
//                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
//        }

//        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
//        {
//            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
//            {
//                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
//                if (childToken.IsActive)
//                    revokeRefreshToken(childToken, ipAddress, reason);
//                else
//                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
//            }
//        }

//        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
//        {
//            token.Revoked = DateTime.UtcNow;
//            token.RevokedByIp = ipAddress;
//            token.ReasonRevoked = reason;
//            token.ReplacedByToken = replacedByToken;
//        }

//        public async Task<List<RefreshToken>> GetUserRefreshTokensAsync(int id)
//        {
//            var user = await _repository.User.GetUserAsync(id, false);
//            if (user == null || user.RefreshTokens == null) return null;
//            return user.RefreshTokens;
//        }
//    }
//}
