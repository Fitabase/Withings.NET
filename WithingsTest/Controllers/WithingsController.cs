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
            var authenticator = new OAuth1Helper(appCredentials, "localhost//");

            return View();
        }
   }
}