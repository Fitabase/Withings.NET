using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable
{
    public class WithingsClient
    {
        public class WithingsClient
        {
            public WithingsAppCredentials AppCredentials { get; private set; }

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
                    if (HttpClient != null)
                        ConfigureAuthorizationHeader();
                }
            }
            /// <summary>
            /// The httpclient which will be used for the api calls through the FitbitClient instance
            /// </summary>
            public HttpClient HttpClient { get; private set; }



        }
    }
}
}
