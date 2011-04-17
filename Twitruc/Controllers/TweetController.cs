using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitruc.DAL;
using System.Web.Mvc;
using System.Configuration;
using Twitruc.Models;

namespace Twitruc.Controllers
{
    public class TweetController : Controller
    {
        //
        // GET: /Tweet/
		public UserService userManager { get; set; }
		public TweetController() { userManager = new UserService(); }

        public ActionResult Index()
        {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new ReturnUrl(this.HttpContext.Request.RawUrl));
			else
				usr = userManager.getUser(Session["nickname"] as string);

			var tokens = new Twitterizer.OAuthTokens();
			tokens.AccessToken = usr.Token;
			tokens.AccessTokenSecret = usr.TokenSecret;
			tokens.ConsumerKey = ConfigurationManager.AppSettings["consumerkey"];
			tokens.ConsumerSecret = ConfigurationManager.AppSettings["consumersecret"];
			Twitterizer.TwitterResponse<Twitterizer.TwitterStatusCollection> userResponse = Twitterizer.TwitterTimeline.HomeTimeline(tokens);
			ViewBag.Tweets = userResponse.ResponseObject.Select(st => new TweetExtended(st)).ToList();
			return View(new TweetModel());
        }

    }
}
