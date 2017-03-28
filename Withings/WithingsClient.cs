using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Withings.API.Portable;
using Withings.API.Portable.OAuth1;


namespace Withings.API.Portable
{
   public class WithingsClient 
    {
        public WithingsAppCredentials AppCredentials { get; private set; }

        private OAuth1AccessToken _accessToken;

        public OAuth1AccessToken AccessToken
        {
            get
            {
                return _accessToken;

            }
            set
            {
                _accessToken = value;
                //If we update the AccessToken after HttpClient has been created, then reconfigure authorization header
                if (HttpClient != null) ;
                    //ConfigureAuthorizationHeader();
            }
        }
        /// <summary>
        /// The httpclient which will be used for the api calls through the FitbitClient instance
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        public WithingsClient(WithingsAppCredentials credentials, OAuth1AccessToken accessToken)
        {
            this.AppCredentials = credentials;
            this.AccessToken = accessToken;
        }

        public async Task<Activity>GetDayActivityAsync(DateTime activityDate, string encodedUserId)
        {
            string userId = Request.QueryString["userid"]; //todo: Find out how to assign the real user id from OAuth call

            var result = new AuthCredential { userId = accessToken.ExtraData["encoded_user_id"].FirstOrDefault() };

            var client = OAuthUtility.CreateOAuthClient(AppCredentials, AccessToken);


            //string withingsDateApiUrl = "&date=";

            //string withingsStartDateApiUrl = "&startdateymd=";

            //string withingsEndDateApiUrl = "&enddateymd=";
            //DateTime date = DateTime.Now;
            //string dateFormat = date.ToString("yyyy-MM-dd");
            //string startDateFormat = "2017-03-10";

            //string endDateFormat = "2017-03-21";

            // string dateFormat = "2017-03-13";

            //string oauthenticator = "&"+consumerSecret+"&"+accessToken;
            var oAuth_params = OAuthUtility.BuildBasicParameters(ConsumerKey, ConsumerSecret, "https://wbsapi.withings.net", HttpMethod.Get, accessTokens)
                .Where(p => p.Key != "oauth_signature")
                .OrderBy(p => p.Key);


            string requestUri = $"https://wbsapi.withings.net/measure?action=getmeas&userid={userId}&";

            requestUri += string.Join("&", oAuth_params.Select(kvp => kvp.Key + "=" + kvp.Value));

            var signature = OAuthUtility.BuildBasicParameters(ConsumerKey, ConsumerSecret, requestUri, HttpMethod.Get, accessTokens)
                .First(p => p.Key == "oauth_signature").Value;

            string json = await client.GetStringAsync(requestUri + "&oauth_signature=" + signature);

            var o = JObject.Parse(json);

            int updateTime = (int)o["body"]["updatetime"];



            return (o);


        }

    }
}

