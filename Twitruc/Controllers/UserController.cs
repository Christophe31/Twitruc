using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitterizer;
using Twitruc.DAL;
using Twitruc.Models;
using System.Web.Routing;
using System.Configuration;

namespace Twitruc.Controllers {
	public class UserController : Controller {
		public UserService userManager { get; set; }

		protected override void Initialize(RequestContext requestContext) {
			userManager = userManager ?? new UserService();
			base.Initialize(requestContext);
		}

		// **************************************
		// URL: /User/LogOn
		// **************************************

		public ActionResult LogOn() {
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnForm model, string returnUrl) {
			if (ModelState.IsValid) {
				if (userManager.ValidateUser(model.Nickname, model.Password)) {
					userManager.SignIn(model.Nickname, model.RememberMe);
					Session["nickname"] = model.Nickname;
					Session.Timeout = 100000;
					if (Url.IsLocalUrl(returnUrl))
						return Redirect(returnUrl);
					else
						return RedirectToAction("Index", "Home");
				} else
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		// **************************************
		// URL: /User/LogOff
		// **************************************

		public ActionResult LogOff() {
			userManager.SignOut();
			Session.Clear();

			return RedirectToAction("Index", "Home");
		}

		// **************************************
		// URL: /User/Register
		// **************************************

		public ActionResult Register() {
			//			ViewBag.PasswordLength = userManager.MinPasswordLength;
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterForm model) {
			if (ModelState.IsValid) {
				if (userManager.CreateUser(model.Name, model.Nickname, model.Password, model.Email)) {
					userManager.SignIn(model.Nickname, false /* createPersistentCookie */);
					return RedirectToAction("TwitterAuth", "User");
				}
			}

			// If we got this far, something failed, redisplay form
			ViewBag.PasswordLength = UserService.PassLength;
			return View(model);
		}


		// **************************************
		// URL: /User/TwitterAuth
		// **************************************

		public ActionResult TwitterAuth() {
			TwUser usr;
			if (String.IsNullOrEmpty(Session["nickname"] as String))
				return RedirectToAction("LogOn", "User", new {ReturnUrl=this.HttpContext.Request.RawUrl});
			else
				usr = userManager.getUser(Session["nickname"] as string);

			if (!String.IsNullOrEmpty(HttpContext.Request.QueryString["oauth_token"])) {
				var atoken = OAuthUtility.GetAccessTokenDuringCallback(ConfigurationManager.AppSettings["consumerkey"], ConfigurationManager.AppSettings["consumersecret"]);
				this.userManager.UpdateTwitterAccount(atoken, usr);

				return RedirectToAction("Index", "Tweet");
			}

			var tokens = OAuthUtility.GetRequestToken(ConfigurationManager.AppSettings["consumerkey"], ConfigurationManager.AppSettings["consumersecret"], "http://localhost/User/TwitterAuth");
			var link = OAuthUtility.BuildAuthorizationUri(tokens.Token).AbsoluteUri;
			
			return this.Redirect(link);
		}
	}
}
