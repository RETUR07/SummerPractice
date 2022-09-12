using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Application.DTO
{
    public class PostForResponseDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ParentPostId { get; set; }
        public string Header { get; set; }

        public string Text { get; set; }

        public IEnumerable<Uri> Content { get; set; }
    }
}
