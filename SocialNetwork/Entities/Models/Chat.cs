using SocialNetwork.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.Entities.Models
{
    public class Chat : ParentModel
    {
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}
