using System;
using System.Collections.Generic;


namespace SocialNetwork.Application.DTO
{
    public class MessageForResponseDTO
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int FileCount { get; set; }
        public IEnumerable<Uri> Content { get; set; }
    }
}
