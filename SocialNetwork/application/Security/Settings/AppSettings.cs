using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Security.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
