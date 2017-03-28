using AsyncOAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Withings.API.Portable.OAuth1;

namespace Withings.API.Portable

{
    public class WithingsAppAuthenticator
    {
        //setting authorizer value to the keyvaluepair and IEnumberable key and secret
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        //Constructor
        public WithingsAppAuthenticator (WithingsAppCredentials appCredentials)
        {
            ConsumerKey = appCredentials.ConsumerKey;
            ConsumerSecret = appCredentials.ConsumerSecret;
        }
        

        public string GenerateAuthUrlFromRequestToken(RequestToken requestToken)
        {
            // create authorizer
            var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);
            //requestUrl buildAuthroizeUrl is putting together a callback url
            var requestUrl = authorizer.BuildAuthorizeUrl("https://oauth.withings.com/account/authorize", requestToken);
            return requestUrl;
        }


        public async Task<RequestToken> GetRequestTokenAsync()
        {
            // create authorizer
            var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("oauth_callback", Uri.EscapeUriString("http://localhost:49932/Withings/AccessTokenFlow")));

            // get request token - once url reads http://localhost:49932/Withings/CallBack Controller begins with action result HERE
            // get request token
           
            //Summary - Sends consumerKey and consumerSecret to withings oauth site with parameters of oauth callback valued above

            var tokenResponse = await authorizer.GetRequestToken(Constants.BaseApiUrl + Constants.TemporaryCredentialsRequestTokenUri, parameters);

            var requestToken = tokenResponse.Token;

            
            // return the request token
            return requestToken;

        }

    

        //public async Task<AuthCredential> ProcessApprovedAuthCallbackAsync(RequestToken token)
        //{
        //    if (token == null)
        //        throw new ArgumentNullException("token", "RequestToken cannot be null");

        //    if (string.IsNullOrWhiteSpace(token.Token))
        //        throw new ArgumentNullException("token", "RequestToken.Token must not be null");

        //    var oauthRequestToken = new AsyncOAuth.RequestToken(token.Token, token.Secret);
        //    var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);
        //    var accessToken = await authorizer.GetAccessToken(Constants.BaseApiUrl + Constants.TemporaryCredentialsAccessTokenUri, oauthRequestToken, token.Verifier);

        //    var result = new AuthCredential
        //    {
        //        AuthToken = accessToken.Token.Key,
        //        AuthTokenSecret = accessToken.Token.Secret,
        //        UserId = accessToken.ExtraData["encoded_user_id"].FirstOrDefault()
        //    };
        //    return result;
        //}
        public async Task<AccessToken> AccessTokenFlow(RequestToken requestToken, string oAuthVerifier)
        {
        //passing in the MVC where AccessTokenFlow=string oatuhtokens
            //seting the authorizer varable with consumerKey/Secret as in Withings. Will become variable for later injection
            //grab value in URL to place in varables

            var authorizer = new OAuthAuthorizer(ConsumerKey, ConsumerSecret);

            


            //send them out as access_tokens to get access granted by Withings 
           var accessTokenResponse = await authorizer.GetAccessToken(Constants.BaseApiUrl + Constants.TemporaryCredentialsAccessTokenUri, requestToken, oAuthVerifier);
            var accessToken = accessTokenResponse.Token;
            return accessToken;
        }

        //    string userId = Request.QueryString["userid"]; //todo: Find out how to assign the real user id from OAuth call

        //    var result = new AuthCredential { userId = accessToken.ExtraData["encoded_user_id"].FirstOrDefault()};

        //    var client = OAuthUtility.CreateOAuthClient(ConsumerKey, ConsumerSecret, accessTokens);


        //    //string withingsDateApiUrl = "&date=";

        //    //string withingsStartDateApiUrl = "&startdateymd=";

        //    //string withingsEndDateApiUrl = "&enddateymd=";
        //    //DateTime date = DateTime.Now;
        //    //string dateFormat = date.ToString("yyyy-MM-dd");
        //    //string startDateFormat = "2017-03-10";

        //    //string endDateFormat = "2017-03-21";

        //    // string dateFormat = "2017-03-13";

        //    //string oauthenticator = "&"+consumerSecret+"&"+accessToken;
        //    var oAuth_params = OAuthUtility.BuildBasicParameters(ConsumerKey, ConsumerSecret, "https://wbsapi.withings.net", HttpMethod.Get, accessTokens)
        //        .Where(p => p.Key != "oauth_signature")
        //        .OrderBy(p => p.Key);


        //    string requestUri = $"https://wbsapi.withings.net/measure?action=getmeas&userid={userId}&";

        //    requestUri += string.Join("&", oAuth_params.Select(kvp => kvp.Key + "=" + kvp.Value));

        //    var signature = OAuthUtility.BuildBasicParameters(ConsumerKey, ConsumerSecret, requestUri, HttpMethod.Get, accessTokens)
        //        .First(p => p.Key == "oauth_signature").Value;

        //    string json = await client.GetStringAsync(requestUri + "&oauth_signature=" + signature);

        //    var o = JObject.Parse(json);

        //    int updateTime = (int)o["body"]["updatetime"];

           

        //    return (o);

        
    }
}
