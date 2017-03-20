using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WithingsOAuth1SampleSite.Models
{
    public class HttpClient
    {
        var client = OAuthUtility.CreateOAuthClient("consumerKey", "consumerSecret", new AccessToken("accessToken", "accessTokenSecret"));

    }
}