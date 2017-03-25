using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Withings.API.Portable

{
    public class WithingsAppAuthenticator
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public WithingsAppAuthenticator (string consumerKey, string consumerSecret)
        {
            consumerKey = ConsumerKey;
            consumerSecret = ConsumerSecret;
        }

        //public string GenerateAuthUrlFromRequestToken (RequestToken token, bool forceLogoutBeforeAuth)
        //{
        //    var url = Constants.BaseApiUrl + (forceLogoutBeforeAuth ? Constants.LogoutAndAuthorizeUri : Constants.AuthorizeUri);
        //    return string.Format("{0}?oauth_token={1}", url, token.Token);
        //}
        
        
    }
}
