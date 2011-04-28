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

		public class LogOnForm {
			[Required]
			[Display(Name = "User name")]
			public string Nickname { get; set; }

			[Required]
			[DataType(DataType.Password)]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[Display(Name = "Remember me?")]
			public bool RememberMe { get; set; }
		}

		public class RegisterForm {
			[Required]
			[Display(Name = "User name")]
			public string Name { get; set; }

			[Required]
			[Display(Name = "Nickname")]
			public string Nickname { get; set; }

			[Required]
			[DataType(DataType.EmailAddress)]
			[Display(Name = "Email address")]
			public string Email { get; set; }

			[Required]
			[DataType(DataType.Password)]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }
		}
		#endregion

		#region Services
		public class UserService {
			private dbContainer db = new dbContainer();
			public bool ValidateUser(string nickname, string password) {
				if (String.IsNullOrEmpty(nickname)) throw new ArgumentException("Value cannot be null or empty.", "userName");
				if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
				return db.UserSet.Any(u => u.Nickname == nickname && u.Password == password);
			}
			public static int PassLength { get { return 4; } }

			public bool CreateUser(string userName, string nickname, string password, string email) {
				if (String.IsNullOrEmpty(userName))
					throw new ArgumentException("Value cannot be null or empty.", "userName");
				if (String.IsNullOrEmpty(nickname))
					throw new ArgumentException("Value cannot be null or empty.", "nickname");
				if (String.IsNullOrEmpty(password))
					throw new ArgumentException("Value cannot be null or empty.", "password");
				if (password.Length <= 4)
					throw new ArgumentException("Password too short, need more than 4 chars.", "password");
				if (String.IsNullOrEmpty(email))
					throw new ArgumentException("Value cannot be null or empty.", "email");
				if (db.UserSet.Any(u => u.Email == email))
					throw new ArgumentException("This email is already registred.", "email");
				if (db.UserSet.Any(u => u.Nickname == nickname))
					throw new ArgumentException("This nickname is already registred.", "nickname");
				try {
					var u = new TwUser();
					u.Name = userName;
					u.Nickname = nickname;
					u.Password = password;
					u.Email = email;
					u.TwitterNick = "";
					u.Token = "";
					u.TokenSecret = "";
					db.UserSet.AddObject(u);
					db.SaveChanges();
					return true;
				} catch (Exception e) {
					throw e;
				}
			}

			public bool ChangePassword(string nickname, string oldPassword, string newPassword) {
				if (String.IsNullOrEmpty(nickname)) throw new ArgumentException("Value cannot be null or empty.", "nickname");
				if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
				if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

				try {
					db.UserSet.Where(u => u.Nickname == nickname)
						.FirstOrDefault()
						.Password = newPassword;
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
		public class ReturnUrl {
			public ReturnUrl(string s) {
				returnUrl = s;
			}
			public string returnUrl { get; set; }
		}
		#endregion
}