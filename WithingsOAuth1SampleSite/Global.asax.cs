using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WithingsOAuth1SampleSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Silverlight, Windows Phone, Console, Web, etc...
            OAuthUtility.ComputeHash = (key, buffer) => { using (var hmac = new HMACSHA1(key)) { return hmac.ComputeHash(buffer); } };

            // Windows Store Apps
            AsyncOAuth.OAuthUtility.ComputeHash = (key, buffer) =>
            {
                var crypt = Windows.Security.Cryptography.Core.MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");
                var keyBuffer = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(key);
                var cryptKey = crypt.CreateKey(keyBuffer);

                var dataBuffer = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(buffer);
                var signBuffer = Windows.Security.Cryptography.Core.CryptographicEngine.Sign(cryptKey, dataBuffer);

                byte[] value;
                Windows.Security.Cryptography.CryptographicBuffer.CopyToByteArray(signBuffer, out value);
                return value;
            };
        }
    }
}
