using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.OAuth1
{
    public class OAuth1Helper
    {
        private const string WithingsWebAuthBaseUrl = "https://www.withings.com";
        private const string WithingsApiBaseUrl = "https://oauth.withings.com";

        private string ConsumerKey;
        private string ConsumerSecret;

        private string RedirectUri;
        
        public OAuth1Helper(WithingsAppCredentials credentials, string redirectUri)
        {
            this.ConsumerKey = credentials.ConsumerKey;
            this.ConsumerSecret = credentials.ConsumerSecret;
            this.RedirectUri = redirectUri;
        }


        //Omitting GernateAuthUrl and Task AccessToken/RequestToken 


    }
}
