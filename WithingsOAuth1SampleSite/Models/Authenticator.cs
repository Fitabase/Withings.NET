using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WithingsOAuth1SampleSite.Models
{
    public class Authenticator
    {
        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }

        public Authenticator(string consumerKey, string consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public string GenerateAuthUrlFromRequestToken(RequestToken token, bool forceLogoutBeforeAuth)
        {
            var url = Constants.BaseApiUrl + (forceLogoutBeforeAuth ? Constants.LogoutAndAuthorizeUri : Constants.AuthorizeUri);
            return string.Format("{0}?oauth_token={1}", url, token.Token);
        }
    }
}