using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Withings.API.Portable;
using Withings.API.Portable.OAuth1;


namespace WithingsTest.Controllers
{
    public class WithingsController : Controller
    {
        //private string consumerSecret= "1f933dcc400ef51f63ffc40b8eeeb429cbbdc53198cf7c4249662c70f403b";
        
        //private string consumerKey="8ecb6d91d1ee66d5c3696c0c803197ed9e8cdff332ff40d5f964430ce9d1";
        
        // GET: Withings
        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult Authorize()
        {
            var appCredentials = new WithingsAppCredentials()
            {
                ConsumerKey = ConfigurationManager.AppSettings["WithingsConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["WithingsConsumerSecret"]
            };
            //make sure you've set these up in Web.Config under <appSettings>:

            Session["AppCredentials"] = appCredentials;

            //Provide the App Credentials. You get those by registering your app at dev.fitbit.com
            //Configure Fitbit authenticaiton request to perform a callback to this constructor's Callback method
         
            ViewBag.Response = appCredentials;
            return View();
        }

        public async Task<ActionResult> CallBack()
        {

            WithingsAppCredentials appCredentials = (WithingsAppCredentials)Session["AppCredentials"];


            var withingsAppConstructor = new WithingsAppAuthenticator((WithingsAppCredentials)Session["AppCredentials"]);

           RequestToken requestTokenResponse = await withingsAppConstructor.GetRequestTokenAsync();
            ViewBag.displayRequest = requestTokenResponse;

            Session["requestToken"] = requestTokenResponse;

            var redirectUrl = withingsAppConstructor.GenerateAuthUrlFromRequestToken(requestTokenResponse);

            ViewBag.RedirectUrl = redirectUrl;
            ViewBag.RedirectUrl.ToString();

            // "~/View/Withings/CallBack.aspx"
       
            
            return View();
        }
        
        public async Task<ActionResult> AccessTokenFlow()
        {
            //var accessToken = Request.QueryString["oauth_token"].ToString();

            //List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            //parameters.Add(new KeyValuePair<string, string>("oauth_token", accessToken));

            var appCredentials = new WithingsAppCredentials()
            {
                ConsumerKey = ConfigurationManager.AppSettings["WithingsConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["WithingsConsumerSecret"]
            };

            Session["AppCredentials"] = appCredentials;

            var oAuthVerifier = Request.QueryString["oauth_verifier"].ToString();
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("oauth_verifier", oAuthVerifier));

            var withingsAppConstructor = new WithingsAppAuthenticator((WithingsAppCredentials)Session["AppCredentials"]);
            var requestTokenSession = Session["requestToken"] as RequestToken;


            AccessToken accessTokenResponse = await withingsAppConstructor.AccessTokenFlow(requestTokenSession, oAuthVerifier);

            return View("AccessTokenFlow");
        }
    }
}