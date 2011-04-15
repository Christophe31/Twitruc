using System;
using Twitterizer;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Twitruc.Models {
		#region Models
		public class ChangePasswordForm {
			[Required]
			[DataType(DataType.Password)]
			[Display(Name = "Current password")]
			public string OldPassword { get; set; }

			[Required]
			[DataType(DataType.Password)]
			[Display(Name = "New password")]
			public string NewPassword { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm new password")]
			[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }
		}

		public class LogOnForm {
			[Required]
			[Display(Name = "User name")]
			public string Nickname { get; set; }

			[Required]
			[DataType(DataType.Password)]
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
			private EntityLib.Model1Container db = new EntityLib.Model1Container();
			public int MinPasswordLength {
				get {
					return 4;
				}
			}

			public bool ValidateUser(string nickname, string password) {
				if (String.IsNullOrEmpty(nickname)) throw new ArgumentException("Value cannot be null or empty.", "userName");
				if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
				return db.Users.Any(u => u.Nickname == nickname && u.Password == password);
			}

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
				if (db.Users.Any(u => u.Email == email))
					throw new ArgumentException("This email is already registred.", "email");
				if (db.Users.Any(u => u.Nickname == nickname))
					throw new ArgumentException("This nickname is already registred.", "nickname");
				try {
					var u = new EntityLib.User();
					u.Name = userName;
					u.Nickname = nickname;
					u.Password = password;
					u.Email = email;
					u.TwitterId = "";
					u.Token = "";
					u.Secret = "";
					db.Users.AddObject(u);
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
					db.Users.Where(u => u.Nickname == nickname).FirstOrDefault().Password = newPassword;
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

			public void UpdateTwitterAccount(OAuthTokenResponse atoken, EntityLib.User usr) {
				usr.Secret = atoken.TokenSecret;
				usr.Token = atoken.Token;
				db.SaveChanges();
			}

			public EntityLib.User getUser(string s) {
				return db.Users.First(u => u.Nickname == s);
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