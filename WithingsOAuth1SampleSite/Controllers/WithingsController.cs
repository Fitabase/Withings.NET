using System;
using System.Configuration;
using System.Web.Mvc;
using System.Threading.Tasks;
using Withings.API.Portable;
using Withings.API.Portable.OAuth1;

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
        //Setup - Redirects to Withings.com to authorize this app
        public ActionResult Authorize()
        {
            var appCredentials = new WithingsAppCredentials()
            {
                ConsumerKey = ConfigurationManager.AppSettings["WithingsConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["WithingsConsumerSecret"]
            };
            //make sure you've set these up in Web.Config under <appSettings>:

            Session["AppCredentials"] = appCredentials;

            //Provide the App Credentials. You get those by registering your app at oauth.withings.com
            //Configure Withings authenticaiton request to perform a callback to this constructor's Callback method
            var authenticator = new OAuth1Helper(appCredentials, Request.Url.GetLeftPart(UriPartial.Authority) + "/Withings/Callback");
            string[] scopes = new string[] { "profile" };

            //string authUrl = authenticator.GenerateAuthUrl(scopes, null);
            string authUrl = "http://www.localhost";

            return Redirect(authUrl);
        }
        //Final step. Take this authorization information and use it in the app
        public async Task<ActionResult> Callback()
        {
            WithingsAppCredentials appCredentials = (WithingsAppCredentials)Session["AppCredentials"];

            var authenticator = new OAuth1Helper(appCredentials, Request.Url.GetLeftPart(UriPartial.Authority) + "/Withings/Callback");

            string code = Request.Params["code"];

            OAuth1RequestToken requestToken = await authenticator.ExchangeAuthCodeForRequestTokenAsync(code);

            //Store credentials in FitbitClient. The client in its default implementation manages the Refresh process
            var withingsClient = GetWithingsClient(requestToken);

            ViewBag.RequestToken = requestToken;

            return View();

        }
        //        /// <summary>
        //        /// HttpClient and hence FitbitClient are designed to be long-lived for the duration of the session. This method ensures only one client is created for the duration of the session.
        //        /// More info at: http://stackoverflow.com/questions/22560971/what-is-the-overhead-of-creating-a-new-httpclient-per-call-in-a-webapi-client
        //        /// </summary>
        //        /// <returns></returns>

        //        private WithingsClient GetWithingsClient(OAuth1RequestToken requestToken = null)
        //        {
        //            if (Session["WithingsClient"] == null)
        //            {
        //                if (requestToken != null)
        //                {
        //                    var appCredentials = (WithingstAppCredentials)Session["AppCredentials"];
        //                    WithingsClient client = new WithingsClient(appCredentials, requestToken);
        //                    Session["WithingsClient"] = client;
        //                    return client;
        //                }
        //                else
        //                {
        //                    throw new Exception("First time requesting a WithingsClient from the session you must pass the AccessToken.");
        //                }

        //            }
        //            else
        //            {
        //                return (WithingsClient)Session["WithingsClient"];
        //            }
        //        }
    }
}