using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace SocialNetwork.Application.DTO
{
    public class MessageForm
    {
        public string Text { get; set; }
        public int FileCount { get; set; }
        public IEnumerable<IFormFile> Content { get; set; }
        public int ChatId { get; set; }
    }
}
