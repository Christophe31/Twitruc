using System;
using Twitterizer;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitruc.DAL;
using System.Text.RegularExpressions;

namespace Twitruc.Models {
	#region Models
	public class TweetModel {
		[Required]
		[DataType(DataType.Text)]
		[StringLength(140, ErrorMessage = "The {0} must have less than {1} characters.", MinimumLength = 1)]
		[Display(Name = "Tweet")]
		public string Content { get; set; }
	}
	public class TweetExtended {
		public TweetExtended(Twitterizer.TwitterStatus st) {
			using (var db = new dbContainer()) {
				try{
					this.DataBase = db.TweetSet.First(t => t.TweetId == st.Id);
				} catch(InvalidOperationException){
					this.DataBase = new Twitruc.DAL.Tweet();
					DataBase.Content = st.Text;
					DataBase.AuthorNick = st.User.ScreenName;
					DataBase.Date = st.CreatedDate;
					DataBase.TweetId = st.Id;
					DataBase.TwitrucUser = null;
					DataBase.Public = ! st.User.IsProtected;
					db.TweetSet.AddObject(DataBase);
					db.SaveChanges();
				}
				Tweeter = st;
			}
		}
		public Tweet DataBase { get; set; }
		public Twitterizer.TwitterStatus Tweeter { get; set; }

	}
	#endregion
	public static class ExtentionsMethods { 
		public static string Urlify(this string s){
			string regex = @"((www\.|(http|https|ftp)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
			Regex r = new Regex(regex, RegexOptions.IgnoreCase);
			return r.Replace(s, "<a href=\"$1\">$1</a>").Replace("href=\"www", "href=\"http://www");
		}
	}
	#region Services
	public class TweetService {
		private dbContainer db = new dbContainer();

		public Tweet CreateTweet(string content, TwUser u) {
			if (String.IsNullOrEmpty(content))
				throw new ArgumentException("Value cannot be null or empty.", "Content");
			var t = new Tweet();
			t.Content = content;
			t.TwitrucUser = u;
			t.AuthorNick = u.TwitterNick;
			t.Date = DateTime.Now;
			t.Sent = false;
			t.Public = false;
			db.TweetSet.AddObject(t);
			db.SaveChanges();
			return t;
		}
		public void SyncTweets() {
			db.UserSet.Where(u => !u.Tweets.Any(t => !t.Sent))
				.ToArray().Select(u => {
					var tok = TwitrucHelpers.getTokens(u);
					return u.Tweets.OrderBy(t=>t.Date).Select(t => {
						Twitterizer.TwitterResponse<Twitterizer.TwitterStatus> userResponse = Twitterizer.TwitterStatus.Update(tok, t.Content);
						t.TweetId = userResponse.ResponseObject.Id;
						t.Sent = true;
						t.Public = ! userResponse.ResponseObject.User.IsProtected;
						t.Date = userResponse.ResponseObject.CreatedDate;
						return t;
					});
				});
		}

	}
#endregion
}