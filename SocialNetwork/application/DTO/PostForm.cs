using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Application.DTO
{
    public class PostForm
    {
        public string Header { get; set; }
        [Required]
        public int ParentPostId { get; set; }
        public string Text { get; set; }
        public IEnumerable<IFormFile> Content { get; set; }

    }
}
