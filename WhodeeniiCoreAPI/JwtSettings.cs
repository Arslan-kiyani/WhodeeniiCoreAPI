using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhodeeniiCoreAPI
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        //public string AccessToken { get; set; }

    }
}
