using System.Collections.Generic;

namespace SocialNetwork.Entities.Models
{
    public class Post : ParentModel
    {
        public string Header { get; set; }

        public string Text { get; set; }

        public User Author { get; set; }
        public Post ParentPost { get; set; }
        public List<Blob> BlobIds { get; set; }
        public List<Post> Comments { get; set; }
    }
}
