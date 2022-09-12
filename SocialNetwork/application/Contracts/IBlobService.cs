using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IBlobService
    {
        public Task<Blob> GetBlobAsync(int id);
        public Task<List<Uri>> GetBLobsAsync(IEnumerable<int> ids, bool trackChanges);
        public Task<List<Blob>> SaveBlobsAsync(IEnumerable<IFormFile> formFile, string uniqueID);
        public List<List<Uri>> GetBLobsAsync(IEnumerable<IEnumerable<int>> collection, bool trackChanges);
    }
}
