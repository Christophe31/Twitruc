using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitterizer;
using Twitruc.DAL;
using System.Configuration;

namespace Twitruc.Models {
	public static class TwitrucHelpers {
		public static OAuthTokens getTokens(TwUser usr) {
			var tokens = new Twitterizer.OAuthTokens();
			tokens.AccessToken = usr.Token;
			tokens.AccessTokenSecret = usr.TokenSecret;
			tokens.ConsumerKey = ConfigurationManager.AppSettings["consumerkey"];
			tokens.ConsumerSecret = ConfigurationManager.AppSettings["consumersecret"];
			return tokens;
		}
	}
}