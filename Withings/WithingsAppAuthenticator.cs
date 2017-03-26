using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Withings.API.Portable

{
    public class WithingsAppAuthenticator
    {
        //setting authorizer value to the keyvaluepair and IEnumberable key and secret
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public WithingsAppAuthenticator (string consumerKey, string consumerSecret)
        {
            consumerKey = ConsumerKey;
            consumerSecret = ConsumerSecret;
        }
        

        public string GenerateAuthUrlFromRequestToken(RequestToken token, bool forceLogoutBeforeAuth)
        {
            var url = Constants.BaseApiUrl + (forceLogoutBeforeAuth ? Constants.LogoutAndAuthorizeUri : Constants.AuthorizeUri);
            return string.Format("{0}?oauth_token={1}", url, token.Token);
        }


        public async Task<RequestToken> GetRequestTokenAsync()
        {
            // create authorizer
            var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("oauth_callback", Uri.EscapeUriString("http://localhost:49932/CallBack/AccessTokenFlow")));

            // get request token - once url reads http://localhost:49932/Withings/RequestTokenFlow Controller begins with action result HERE
            // get request token
      
            //Summary - Sends consumerKey and consumerSecret to withings oauth site with parameters of oauth callback valued above

            var tokenResponse = await authorizer.GetRequestToken(Constants.BaseApiUrl + Constants.TemporaryCredentialsRequestTokenUri, parameters);

            var requestToken = tokenResponse.Token;


            //requestUrl buildAuthroizeUrl is putting together a callback url
            var requestUrl = authorizer.BuildAuthorizeUrl("https://oauth.withings.com/account/authorize", requestToken);

            // return the request token
            return new RequestToken
            {
                Token = requestToken.Key,
                Secret = requestToken.Secret
            };
        }

    }
}
