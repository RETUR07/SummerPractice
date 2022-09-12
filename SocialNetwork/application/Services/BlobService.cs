using Microsoft.AspNetCore.Http;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly IRepositoryManager _repository;

        public BlobService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<List<Uri>> GetBLobsAsync(IEnumerable<int> Ids, bool trackChanges)
        {
            List<Uri> formfiles = new List<Uri>(); 
            foreach (int id in Ids)
            {
                var blob = await _repository.Blob.GetBlob(id, trackChanges);
                if (blob == null)
                    continue;
                formfiles.Add(new Uri("http://localhost:5050/api/Blob/" + id));
            }
            return formfiles;

        }

        public async Task<Blob> GetBlobAsync(int blobId)
        {
            var blob = await _repository.Blob.GetBlob(blobId, false);
            return blob;
        }

        public List<List<Uri>> GetBLobsAsync(IEnumerable<IEnumerable<int>> collection, bool trackChanges)
        {
            List<List<Uri>> collectionfiles = new List<List<Uri>>();

            foreach (IEnumerable<int> Ids in collection)
            {
                var blobs = _repository.Blob.FindByCondition(x => Ids.Contains(x.Id), trackChanges);
                List<Uri> formfiles = new List<Uri>();

                foreach (Blob blob in blobs)
                {              
                    if (blob == null)
                        continue;

                    formfiles.Add(new Uri("http://localhost:5050/api/Blob/" + blob.Id));
                }
                collectionfiles.Add(formfiles);
            }
            return collectionfiles;
        }

        public async Task<List<Blob>> SaveBlobsAsync(IEnumerable<IFormFile> formFiles, string uniqueID)
        {
            if (formFiles == null)return new List<Blob>();
            List<Blob> blobs = new List<Blob>();
            foreach (IFormFile formfile in formFiles)
            {
                if (formfile == null)
                    continue;
                var blob = await BlobConverters.FormFileToBlobAsync(formfile);
                blob.Filename = uniqueID + "-" + formfile.FileName;
                blob.Data = new byte[formfile.Length];
                await formfile.OpenReadStream().ReadAsync(blob.Data, 0, (int)formfile.Length);
                _repository.Blob.Create(blob);

                //var blobContainer = _blobServiceClient.GetBlobContainerClient("files");
                //var blobClient = blobContainer.GetBlobClient(uniqueID + "-" + formfile.FileName);
                //await blobClient.UploadAsync(formfile.OpenReadStream());

                blobs.Add(blob);
            }
            return blobs;
        }
    }
}
