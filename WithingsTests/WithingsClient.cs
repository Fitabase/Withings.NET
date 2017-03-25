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
        public WithingsAppAuthenticator AppCredentials { get; private set; }

        private OAuth1RequestToken _requestToken;

        public OAuth1RequestToken RequestToken
        {
            get
            {
                return _requestToken;

            }
            set
            {
                _requestToken = value;
                //If we update the AccessToken after HttpClient has been created, then reconfigure authorization header
                if (HttpClient != null) ;
                    //ConfigureAuthorizationHeader();
            }
        }
        /// <summary>
        /// The httpclient which will be used for the api calls through the FitbitClient instance
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        private void ConfigureAutoRefresh(bool enableOAuth1TokenRefresh)
        {
            //this.OAuth1TokenAutoRefresh = enableOAuth1TokenRefresh;
            //if (OAuth1TokenAutoRefresh)
            //    this.FitbitInterceptorPipeline.Add(new OAuth1AutoRefreshInterceptor());
            //return;
        }

    }
}

