using AsyncOAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Withings.API.Portable;


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
            //Will appear as a link on the webpage after requestToken is passed
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
            //saving consumerKey and Secret into session 
            Session["AppCredentials"] = appCredentials;

            //oAuthVerifier taken from url to string to be made into a Key Value pair
            var oAuthVerifier = Request.QueryString["oauth_verifier"].ToString();
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("oauth_verifier", oAuthVerifier));

            //Using AppCredentials in session into instanticiating WithingsAppAUthenticator
            var withingsAppConstructor = new WithingsAppAuthenticator((WithingsAppCredentials)Session["AppCredentials"]);

            //Calling request token from previous method to inject into new AccessToken Call
            var requestTokenSession = Session["requestToken"] as RequestToken;
            
            //Awaiting Method from WithingsAppAuthenticator Class to return 
            AccessToken accessTokenResponse = await withingsAppConstructor.AccessTokenFlow(requestTokenSession, oAuthVerifier);

            Session["accessToken"] = accessTokenResponse;

            string userId = Request.QueryString["userid"];
            Session["UserId"] = userId;
            
            WithingsClient client = new WithingsClient(appCredentials, accessTokenResponse);
           
            return View("AccessTokenFlow");
        }

        public async Task<ActionResult> GetWithingsClient(string currentDate,string userid)
        {
            var accessToken = Session["accessToken"] as AccessToken;
            var appCredentials = new WithingsAppCredentials()
            {
                ConsumerKey = ConfigurationManager.AppSettings["WithingsConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["WithingsConsumerSecret"]
            };
            
            var userId = Session["UserId"].ToString();

            string date = DateTime.Now.ToString("yyyy-MM-dd");

            WithingsClient client = new WithingsClient(appCredentials,accessToken);

            var response = await client.GetDayActivityAsync(date,userId);

            ViewBag.ResponseData = response;

            return View("GetWithingsClient");

        }

        public async Task<ActionResult> GetWithingsBodyMeas(string userid, string devType, string measType)
        {
            var accessToken = Session["accessToken"] as AccessToken;
            var appCredentials = new WithingsAppCredentials()
            {
                ConsumerKey = ConfigurationManager.AppSettings["WithingsConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["WithingsConsumerSecret"]
            };

            var userId = Session["UserId"].ToString();

            WithingsClient client = new WithingsClient(appCredentials, accessToken);

            var response = await client.GetBodyMeasureAsync(userId, device, measure);

            ViewBag.ResponseData = response;

            return View("GetWithingsClient");

        }
    }
}