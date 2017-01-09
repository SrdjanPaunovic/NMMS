using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Common.Entities;


namespace Service.Access
{
	public class OutsourcingCompanyDB : IOutsourcingCompanyDB
	{
		private static IOutsourcingCompanyDB myDB;

		public static IOutsourcingCompanyDB Instance
		{
			get
			{
				if (myDB == null)
					myDB = new OutsourcingCompanyDB();

				return myDB;
			}
			set
			{
				if (myDB == null)
					myDB = value;
			}
		}




		public bool AddUser(OcUser user)
		{
			using (var context = new AccessDB())
			{
				context.Users.Add(user);
				int count = context.SaveChanges();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
		}

		public bool AddProject(OcProject project)
		{
			using (var context = new AccessDB())
			{
				context.Projects.Add(project);
				int count = context.SaveChanges();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public bool AddCompany(Company company)
		{
			using (var context = new AccessDB())
			{
				context.Companies.Add(company);
				int count = context.SaveChanges();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}


		public bool AddUserStory(UserStory userStory)
		{
			using (var context = new AccessDB())
			{
				context.UserStories.Add(userStory);
				int count = context.SaveChanges();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool AddTask(Common.Entities.Task task)
		{
			using (var context = new AccessDB())
			{
				context.Tasks.Add(task);
				int count = context.SaveChanges();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool AddTeam(Team team)
		{
			using (var context = new AccessDB())
			{
				context.Teams.Add(team);
				int count = context.SaveChanges();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool UserRegister(OcUser user)
		{
			using (AccessDB context = new AccessDB())
			{

				var result = from users in context.Users select users;
				List<OcUser> userList = result.ToList();
				foreach (var registeredUser in userList)
				{
					if (registeredUser.Username.Equals(user.Username))
					{
						return false;
					}
				}
				if (OutsourcingCompanyDB.Instance.AddUser(user))
				{
					return true;
				}

			}

			return false;
		}




		public bool LogIn(string username, string password)
		{
			using (AccessDB context = new AccessDB())
			{

				User user = context.Users.FirstOrDefault((x) => x.Username == username);
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





		public List<Company> GetAllCompanies()
		{
			using (AccessDB context = new AccessDB())
			{
				var result = from company in context.Companies select company;
				List<Company> companies = result.ToList();
				return companies;
			}
		}



		public List<OcUser> LoginUsersOverview()
		{
			using (AccessDB context = new AccessDB())
			{
				List<OcUser> loginUsers = new List<OcUser>();
				var result = from users in context.Users select users;
				List<OcUser> userList = result.ToList();

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


		public OcUser GetUser(string username)
		{
			using (AccessDB context = new AccessDB())
			{

				OcUser user = context.Users.FirstOrDefault((x) => x.Username == username);
				if (user != null)
				{
					return user;
				}
			}

			return null;
		}

		public bool UpdateUser(OcUser user)
		{
			using (AccessDB context = new AccessDB())
			{
				var result = from i in context.Users
							 where i.Id == user.Id
							 select i;
				OcUser us = result.ToList<OcUser>().FirstOrDefault();
				if (us != null)
				{
					if (us.Username != user.Username)
					{
						var res = from j in context.Users
								  where j.Username == user.Username
								  select j;
						List<OcUser> users = res.ToList<OcUser>();
						if (users.Count == 0)
						{
							ModifieUserProperties(us, user);
							context.Entry(us).State = System.Data.Entity.EntityState.Modified;
							context.SaveChanges();
						}
						else
						{
							return false;
						}

					}
					else
					{
						ModifieUserProperties(us, user);
						context.Entry(us).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
					}
					return true;
				}
			}

			return false;
		}

		private void ModifieUserProperties(OcUser original, OcUser user)
		{
			original.Name = user.Name;
			original.Password = user.Password;
			original.StartTime = user.StartTime;
			original.EndTime = user.EndTime;
			original.IsAuthenticated = user.IsAuthenticated;
			original.Surname = user.Surname;
			original.Username = user.Username;
			original.Password_changed = user.Password_changed;
		}

		public List<OcProject> GetAllProjects()
		{
			using (AccessDB context = new AccessDB())
			{
				List<OcProject> projects = context.Projects.ToList();

				return projects;
			}
		}





		public List<OcProject> GetAllProject()
		{
			throw new NotImplementedException();
		}
	}
}
