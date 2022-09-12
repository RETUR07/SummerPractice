using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Entities.Models
{
    public class Message : ParentModel
    {
        public User User { get; set; }
        public string Text { get; set; }
        public int FileCount { get; set; }
        public List<Blob> Blobs { get; set; }
        public Chat Chat { get; set; }
    }
}
