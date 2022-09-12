using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class RateService : IRateService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public RateService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PostRateForResponseDTO> GetPostRateAsync(string userId, int postId, bool trackChanges)
        {
            var rate = await _repository.Rate.GetPostRateAsync(userId, postId, trackChanges);
            return _mapper.Map<Rate, PostRateForResponseDTO>(rate);
        }

        public async Task<List<PostRateForResponseDTO>> GetRatesByPostIdAsync(int postId, bool trackChanges)
        {
            var rates = await _repository.Rate.GetRatesByPostIdAsync(postId, trackChanges);
            return _mapper.Map<List<Rate>, List<PostRateForResponseDTO>>(rates);
        }

        public async Task<List<List<PostRateForResponseDTO>>> GetRatesByPostsIdsAsync(List<int> postIds, bool trackChanges)
        {
            List<List<PostRateForResponseDTO>> PostsRates = new List<List<PostRateForResponseDTO>>();
            foreach (var postId in postIds)
            {
                var rates = await _repository.Rate.GetRatesByPostIdAsync(postId, trackChanges);
                var responseRates = _mapper.Map<List<Rate>, List<PostRateForResponseDTO>>(rates);
                PostsRates.Add(responseRates);
            }
            
            return PostsRates;
        }

        public async Task UpdatePostRateAsync(RateForm ratedto, string userId)
        {
            if (ratedto == null) return;
            var rate = await _repository.Rate.GetPostRateAsync(userId, ratedto.ObjectId, true);
            if (rate == null)
            {
                var post = await _repository.Post.GetPostAsync(ratedto.ObjectId, true);
                if (post == null) return;
                rate = _mapper.Map<RateForm, Rate>(ratedto);
                rate.UserId = userId;
                _repository.Rate.Create(rate);
            }
            else
            {
                if (ratedto.LikeStatus == rate.LikeStatus)
                {
                    rate.LikeStatus = LikeStatus.Viewed;
                }
                else
                   rate.LikeStatus = ratedto.LikeStatus;               
            }
            await _repository.SaveAsync();
            return;
        }
    }
}
