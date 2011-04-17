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

namespace Twitruc.Models {
	#region Models
	public class TweetModel {
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Tweet")]
		public string Content { get; set; }
		public List<Tweet> Tweets { get; set; }

	}

	#endregion

	#region Services
	public class TweetService {
		private dbContainer db = new dbContainer();

		public bool CreateTweet(string content, TwUser u) {
			if (String.IsNullOrEmpty(content))
				throw new ArgumentException("Value cannot be null or empty.", "Content");
			try {
				var t = new Tweet();
				t.Content = content;
				//t.User = 
				db.TweetSet.AddObject(t);
				db.SaveChanges();
				return true;
			} catch (Exception) {
				return false;
			}
		}

		public bool ChangePassword(string nickname, string oldPassword, string newPassword) {
			if (String.IsNullOrEmpty(nickname)) throw new ArgumentException("Value cannot be null or empty.", "nickname");
			if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
			if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

			try {
				db.UserSet.Where(u => u.Nickname == nickname).FirstOrDefault().Password = newPassword;
				db.SaveChanges();
				return true;
			} catch (Exception) {
				return false;
			}
		}
		public void SignIn(string userName, bool createPersistentCookie) {
			if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);

		}

		public void SignOut() {
			FormsAuthentication.SignOut();
		}

		public void UpdateTwitterAccount(OAuthTokenResponse atoken, TwUser usr) {
			usr.TokenSecret = atoken.TokenSecret;
			usr.Token = atoken.Token;
			db.SaveChanges();
		}

		public TwUser getUser(string s) {
			return db.UserSet.First(u => u.Nickname == s);
		}
	}
#endregion
}