using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitruc.DAL;
using System.Web.Mvc;
using System.Configuration;
using Twitruc.Models;

namespace Twitruc.Controllers {
	public class TweetController : Controller {
		//
		// GET: /Tweet/
		private UserService userManager { get; set; }
		private TweetService tweetManager { get; set; }
		public TweetController() {
			userManager = new UserService();
			tweetManager = new TweetService();
		}

		public ActionResult Index() {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new {returnUrl=this.HttpContext.Request.RawUrl});
			else
				usr = userManager.getUser(Session["nickname"] as string);

			ViewBag.Tweets = tweetManager.getTimeline(usr);
			return View(new TweetModel());
		}

		[HttpPost]
		public ActionResult Index(TweetModel model) {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new {ReturnUrl = HttpContext.Request.RawUrl });
			else
				usr = userManager.getUser(Session["nickname"] as string);

			ViewBag.Tweets = tweetManager.getTimeline(usr);
			return View(model);
		}

		public ActionResult Search() {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new { ReturnUrl = HttpContext.Request.RawUrl });
			else
				usr = userManager.getUser(Session["nickname"] as string);

			var tokens = TwitrucHelpers.getTokens(usr);
			string search_text = HttpContext.Request.QueryString["search-text"];
			Twitterizer.SearchOptions o = new Twitterizer.SearchOptions();
			o.NumberPerPage = 50;
			Twitterizer.TwitterResponse<Twitterizer.TwitterSearchResultCollection> userResponse = Twitterizer.TwitterSearch.Search(tokens, search_text, o);
			ViewBag.Tweets = userResponse.ResponseObject.ToList();
			return View();
		}

		public ActionResult FromUser(string id) {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new { ReturnUrl = HttpContext.Request.RawUrl });
			else
				usr = userManager.getUser(Session["nickname"] as string);

			ViewBag.Tweets = tweetManager.getTweetsFrom(id, usr); 
			return View();
		}

		public ActionResult apiRetweet(string id) {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new { ReturnUrl = HttpContext.Request.RawUrl });
			else
				usr = userManager.getUser(Session["nickname"] as string);

			var tokens = TwitrucHelpers.getTokens(usr);
			Twitterizer.TwitterResponse<Twitterizer.TwitterStatus> userResponse = Twitterizer.TwitterStatus.Retweet(tokens, decimal.Parse(id));
			return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
		}

	}
}
