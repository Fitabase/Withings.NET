using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace WithingsOAuth1SampleSite.Models
{
    public class Authenticator
    {
        var client = OAuthUtility.CreateOAuthClient("consumerKey", "consumerSecret", new AccessToken("accessToken", "accessTokenSecret"));
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

        //Setup - Redirects to Withings.com to authorize this app First step in the OAuth process is to ask for a temporary request token. 
        /// From this you should store the RequestToken returned for later processing the auth token.

        public async Task<RequestToken> GetRequestTokenAsync()
        {
            // create authorizer
            var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);

            // get request token
            var tokenResponse = await authorizer.GetRequestToken(Constants.BaseApiUrl + Constants.TemporaryCredentialsRequestTokenUri);
            var requestToken = tokenResponse.Token;

            // return the request token
            return new RequestToken
            {
                Token = requestToken.Key,
                Secret = requestToken.Secret
            };
        }
        public async Task<AuthCredential> ProcessApprovedAuthCallbackAsync(RequestToken token)
        {
            if (token == null)
                throw new ArgumentNullException("token", "RequestToken cannot be null");

            if (string.IsNullOrWhiteSpace(token.Token))
                throw new ArgumentNullException("token", "RequestToken.Token must not be null");

            var oauthRequestToken = new AsyncOAuth.RequestToken(token.Token, token.Secret);
            var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);
            var accessToken = await authorizer.GetAccessToken(Constants.BaseApiUrl + Constants.TemporaryCredentialsAccessTokenUri, oauthRequestToken, token.Verifier);

            var result = new AuthCredential
            {
                AuthToken = accessToken.Token.Key,
                AuthTokenSecret = accessToken.Token.Secret,
                UserId = accessToken.ExtraData["encoded_user_id"].FirstOrDefault()
            };
            return result;
        }
    }
}