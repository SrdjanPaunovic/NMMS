using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
using Service.Access;
using Common.Entities;

namespace HiringCompanyService
{
	public class HiringCompanyService : IHiringContract
	{


		public bool LogIn(string username, string password)
		{
			using (AccessDB context = new AccessDB())
			{
				
				User user = context.Users.FirstOrDefault((x)=> x.Username == username );
				if (user != null)
				{
					if (user.Password.Equals(password))
						user.IsAuthenticated = true;
					context.Entry(user).State = System.Data.Entity.EntityState.Modified;
					context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool LogOut(string username)
		{
			{
				using (AccessDB context = new AccessDB())
				{
					var result = from b in context.Users
								 where b.Username.Equals(username)
								 select b;
					User user = result.ToList().FirstOrDefault();
					if (user != null)
					{
						if (user.IsAuthenticated)
						{
							user.IsAuthenticated = false;
							context.Entry(user).State = System.Data.Entity.EntityState.Modified;
							context.SaveChanges();
							return true;
						}
					}
				}
				return false;
			}
		}

		public bool UserRegister(User user)
		{

			using (AccessDB context = new AccessDB())
			{

				var result = from users in context.Users select users;
				List<User> userList = result.ToList();
				foreach (var registeredUser in userList)
				{
					if (registeredUser.Username.Equals(user.Username))
					{
						return false;
					}
				}
				if (HiringCompanyDB.Instance.AddUser(user))
				{
					return true;
				}

			}

			return false;
		}


		public List<User> LoginUsersOverview()
		{
			using (AccessDB context = new AccessDB())
			{
				List<User> loginUsers = new List<User>();
				var result = from users in context.Users select users;
				List<User> userList = result.ToList();

				foreach (var user in userList)
				{
					if (user.IsAuthenticated)
					{
						loginUsers.Add(user);
					}

				}
				return loginUsers;
			}
		}



	}
}