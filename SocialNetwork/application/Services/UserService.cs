using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IRepositoryManager repository, IMapper mapper, UserManager<User> userManageer)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManageer;
        }

        public async Task AddFriendAsync(string userId, string friendId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            var friend = await _repository.User.GetUserAsync(friendId, true);

            if (user == null || friend == null) return;
            if (user.Friends.Contains(friend) || friend.Friends.Contains(user)) return;

            if (user.Subscribers.Contains(friend))
            {
                user.Friends.Add(friend);
                user.Subscribers.Remove(friend);
            }
            else
            {
                friend.Subscribers.Add(user);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteFriendAsync(string userId, string friendId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            var friend = await _repository.User.GetUserAsync(friendId, true);

            if (user == null || friend == null) return;

            if (friend.Subscribers.Contains(user))
            {
                friend.Subscribers.Remove(user);
            }
            if (user.Friends.Contains(friend) || friend.Friends.Contains(user))
            {
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);

                user.Subscribers.Add(friend);
            }
            await _repository.SaveAsync();
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistrationForm userdto)
        {
            if (userdto == null)
            {
                return null;
            }

            var user = _mapper.Map<User>(userdto);
            var res = await _userManager.CreateAsync(user, userdto.Password);
            await _repository.SaveAsync();
            return res;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            _repository.User.Delete(user);
            await _repository.SaveAsync();
        }

        public async Task<UserForResponseDTO> GetUserAsync(string userId)
        {
            var user = await _repository.User.GetUserAsync(userId, false);
            if (user == null)
            {
                return null;
            }
            var userdto = _mapper.Map<UserForResponseDTO>(user);
            return userdto;
        }

        public async Task<List<UserForResponseDTO>> GetUsersAsync()
        {
            var users = await _repository.User.GetAllUsersAsync(false);
            var usersdto = _mapper.Map<List<UserForResponseDTO>>(users);
            return usersdto;
        }

        public async Task UpdateUserAsync(string userId, UserForm userdto)
        {
            if (userdto == null) throw new InvalidDataException("user dto is null");

            var user = await _repository.User.GetUserAsync(userId, true);

            if (user == null) throw new InvalidDataException("no such user");

            _mapper.Map(userdto, user);
            await _repository.SaveAsync();
        }
    }
}
