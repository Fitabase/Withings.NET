using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WithingsOAuth1SampleSite
{
    internal class Constants
    {
        public const string BaseApiUrl = "https://wbsapi.withings.net/";
        public const string BaseAuthUrl = "https://oauth.withings.com/account/";
        public const string TemporaryCredentialsRequestTokenUri = "request_token";
        public const string TemporaryCredentialsAccessTokenUri = "access_token";
        public const string AuthorizeUri = "authorize";
        public const string LogoutAndAuthorizeUri = "logout_and_authorize";

        public class Headers
        {
           public const string XWithingsSubsciberId = "X-Fitbit-Subscriber-Id";
        }

        public class Formatting
        {
            public const string TrailingSlash = "{0}/";
            public const string LeadingDash = "-{0}";
        }
    }
}