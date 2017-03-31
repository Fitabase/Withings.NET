using System;
using System.Collections.Generic;
using System.Linq;
using Withings.API.Portable.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Withings.API.Portable;
using AsyncOAuth;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Withings.API.Portable
{
   public class WithingsClient 
    {
        public WithingsAppCredentials AppCredentials { get; private set; }

        private AccessToken _accessToken;

        public AccessToken AccessToken
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



        private HttpClient _httpClient;
        /// <summary>
        /// The httpclient which will be used for the api calls through the FitbitClient instance
        /// </summary>
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient ?? (_httpClient = new HttpClient());
            }
        }

        public WithingsClient(WithingsAppCredentials credentials, AccessToken accessToken)
        {
            this.AppCredentials = credentials;
            this.AccessToken = accessToken;
        }

        public async Task<Activity>GetDayActivityAsync(string Currentdate, string userId)
        {
            var appCredentials = AppCredentials.ToString();


            var oAuth_params = OAuthUtility.BuildBasicParameters(AppCredentials.ConsumerKey, AppCredentials.ConsumerSecret, "https://wbsapi.withings.net", HttpMethod.Get, this.AccessToken)
                .Where(p => p.Key != "oauth_signature")
                .OrderBy(p => p.Key);

            string startdate = ("2017-02-30");
            string date = ("2017-03-24");

            string requestUri = $"https://wbsapi.withings.net/v2/measure?action=getactivity&userid={userId}&date={date}&";

            requestUri += string.Join("&", oAuth_params.Select(kvp => kvp.Key + "=" + kvp.Value));

            var signature = OAuthUtility.BuildBasicParameters(AppCredentials.ConsumerKey, AppCredentials.ConsumerSecret, requestUri, HttpMethod.Get, this.AccessToken)
                .First((KeyValuePair<string, string> p) => p.Key == "oauth_signature").Value;

            string json = await HttpClient.GetStringAsync(requestUri + "&oauth_signature=" + signature);

            var o = JObject.Parse(json);

           
            return (new Activity
            {
                   
                Calories = (float)o["body"]["calories"],
                Date = (string)o["body"]["date"],
                Distance = (float)o["body"]["distance"],
                Elevation = (float)o["body"]["elevation"],
                Intense = (int)o["body"]["intense"],
                Moderate = (int)o["body"]["moderate"],
                Soft = (int)o["body"]["soft"],
                Steps = (int)o["body"]["steps"],
                TimeZone = (string)o["body"]["timezone"],
                TotalCalories = (float)o["body"]["totalcalories"]
            });

        }
        public async Task<IEnumerable<MeasureGroup>> GetBodyMeasureAsync(string userId, string deviceType)
        {
            var appCredentials = AppCredentials.ToString();


            var oAuth_params = OAuthUtility.BuildBasicParameters(AppCredentials.ConsumerKey, AppCredentials.ConsumerSecret, "https://wbsapi.withings.net", HttpMethod.Get, this.AccessToken)
                .Where(p => p.Key != "oauth_signature")
                .OrderBy(p => p.Key);
            

            string requestUri = $"https://wbsapi.withings.net/measure?action=getmeas&userid={userId}&devtype={deviceType}&";

            requestUri += string.Join("&", oAuth_params.Select(kvp => kvp.Key + "=" + kvp.Value));

            var signature = OAuthUtility.BuildBasicParameters(AppCredentials.ConsumerKey, AppCredentials.ConsumerSecret, requestUri, HttpMethod.Get, this.AccessToken)
                .First((KeyValuePair<string, string> p) => p.Key == "oauth_signature").Value;

            string json = await HttpClient.GetStringAsync(requestUri + "&oauth_signature=" + signature);

            var o = JObject.Parse(json);

            return JsonConvert.DeserializeObject<IEnumerable<MeasureGroup>>(o["body"]["measuregrps"].ToString());
           
        }

        //string withingsStartDateApiUrl = "&startdateymd=";

        //string withingsEndDateApiUrl = "&enddateymd=";
        //DateTime date = DateTime.Now;
        //string dateFormat = date.ToString("yyyy-MM-dd");
        //string startDateFormat = "2017-03-10";

        //string endDateFormat = "2017-03-21";

        // string dateFormat = "2017-03-13";

        //string oauthenticator = "&"+consumerSecret+"&"+accessToken;
    }
}

