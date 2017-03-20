using System;
using System.Configuration;
using System.Web.Mvc;
using System.Threading.Tasks;
using Withings.API.Portable;
using Withings.API.Portable.OAuth1;
using AsyncOAuth;
using WithingsOAuth1SampleSite.Models.Authenticator;

namespace WithingsOAuth1SampleSite.Controllers
{
    public class WithingsController : Controller
    {
        //Get:/Withings/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //GET : /WithingsAuth/
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
}