
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Common;

namespace Service.Access
{
	public class HiringCompanyDB : IHiringCompanyDB
	{

		public static IHiringCompanyDB hirinigCompanyDB;  

		public static IHiringCompanyDB Instance
		{
			get
			{
				if (hirinigCompanyDB == null)
				{
					hirinigCompanyDB = new HiringCompanyDB();
				}
				return hirinigCompanyDB;
			}
			set
			{
				if (hirinigCompanyDB == null)
				{
					hirinigCompanyDB = value;
				}
			}
		}

		public bool AddUser(User user)
		{
			using (var db = new AccessDB())
			{
				var uList = db.Users.ToList();
				if (uList.Exists(x => x.Username == user.Username))
				{
					LogHelper.GetLogger().Info("AddUser method returned false. User with username:" + user.Name + " already exists");

					return false;
				}
				db.Users.Add(user);
				int i = db.SaveChanges();
				if (i > 0)
				{
					LogHelper.GetLogger().Info(" AddUser method succeeded. Returned true.");
					return true;
				}
				LogHelper.GetLogger().Info("AddUser method returned false.");
				return false;
			}
		}

		public bool AddCompany(Company company)
		{

			using (var db = new AccessDB())
			{
				db.Companies.Add(company);
				int i = db.SaveChanges();
				if (i > 0)
				{
					LogHelper.GetLogger().Info("AddCompany method succeeded. Returned true.");

					return true;
				}
				LogHelper.GetLogger().Info("AddCompany method succeeded. Returned false.");
				return false;

			}
		}

		public bool AddUserStory(UserStory userStory)
		{
			using (var db = new AccessDB())
			{
				db.UserStories.Add(userStory);
				int i = db.SaveChanges();
				if (i > 0)
				{
                    LogHelper.GetLogger().Info(" AddUserStory method succeeded. Returned true.");

					return true;
				}

                LogHelper.GetLogger().Info("AddUserStory method returned false.");
				return false;

			}
		}

		public bool LogIn(string username, string password)
		{
			using (AccessDB context = new AccessDB())
			{

				User user = context.Users.FirstOrDefault((x) => x.Username == username);
				if (user != null)
				{
					if (user.Password.Equals(password))
					{
						user.IsAuthenticated = true;
						context.Entry(user).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
						LogHelper.GetLogger().Info("AddCompany method succeeded. Returned true.");

						return true;
					}
				}
			}
			LogHelper.GetLogger().Info("AddCompany method returned false.");
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
						LogHelper.GetLogger().Info("LogOut method succeeded. Returned true.");
						return true;
					}
				}
			}
			LogHelper.GetLogger().Info("LogOut method returned false.");
			return false;
		}

        //public List<User> LoginUsersOverview()
        //{

        //    using (AccessDB context = new AccessDB())
        //    {
        //        List<User> loginUsers = new List<User>();
        //        var result = from users in context.Users select users;
        //        List<User> userList = result.ToList();

        //        foreach (var user in userList)
        //        {
        //            if (user.IsAuthenticated)
        //            {
        //                loginUsers.Add(user);
        //            }

        //        }
        //        LogHelper.GetLogger().Info("LoginUsersOverview method succeeded. Returned list of users loged in.");

        //        return loginUsers;
        //    }
        //}

		public List<Company> GetAllCompanies()
		{
			using (AccessDB context = new AccessDB())
			{
				var result = from company in context.Companies select company;
				List<Company> companies = result.ToList();
				LogHelper.GetLogger().Info("GetAllCompanies method succeeded. Returned list of all companies.");

				return companies;
			}
		}

		public bool UpdateUser(User user)
		{
			using (AccessDB context = new AccessDB())
			{
				var result = from i in context.Users
							 where i.Id == user.Id
							 select i;
				User us = result.ToList<User>().FirstOrDefault();
				if (us != null)
				{
					if (us.Username != user.Username)
					{
						var res = from j in context.Users
								  where j.Username == user.Username
								  select j;
						List<User> users = res.ToList<User>();
						if (users.Count == 0)
						{
							us.UpdateProperties(user);
							context.Entry(us).State = System.Data.Entity.EntityState.Modified;
							context.SaveChanges();
							LogHelper.GetLogger().Info(" UpdateUser method succeeded.");

						}
						else
						{
							LogHelper.GetLogger().Info("UpdateUser method returned false.");

							return false;
						}

					}
					else
					{
						us.UpdateProperties(user);
						context.Entry(us).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
					}
					LogHelper.GetLogger().Info(" UpdateUser method succeeded. Returned true.");

					return true;
				}
			}
			LogHelper.GetLogger().Info(" UpdateUser method returned false.");

			return false;
		}

		public User GetUser(string username)
		{
			using (AccessDB context = new AccessDB())
			{

				User user = context.Users.FirstOrDefault((x) => x.Username == username);
				if (user != null)
				{
					LogHelper.GetLogger().Info("GetUser method succeeded.");

					return user;
				}
			}
			LogHelper.GetLogger().Info(" GetUser returned null.");

			return null;
		}

		public bool AddProject(Project project)
		{
			using (var db = new AccessDB())
			{
				User prodOw = null;
				if (project.ProductOwner != null)
				{
					prodOw = db.Users.FirstOrDefault<User>(x => x.Id == project.ProductOwner.Id);
				}
				project.ProductOwner = prodOw;
				db.Projects.Add(project);

				int i = db.SaveChanges();
				if (i > 0)
				{
					LogHelper.GetLogger().Info("AddProject method succeeded. Returned true.");

					return true;
				}
				LogHelper.GetLogger().Info(" AddProject method returned false.");

				return false;

			}
		}

		public bool UpdateProject(Project project)
		{
			using (AccessDB context = new AccessDB())
			{
				Project proj = context.Projects.FirstOrDefault<Project>((x) => x.Name == project.Name);

				if (proj != null)
				{

					proj.UpdateProperties(project);
					List<UserStory> userStories = context.UserStories.Where<UserStory>((x) => x.Project.Id == project.Id).ToList();
					foreach (var us in userStories)
					{
						if (project.UserStories.ToList().Exists((x) => x.Id == us.Id))
						{
							continue;
						}

						context.Entry(us).State = System.Data.Entity.EntityState.Deleted;
					}

					foreach (var us in project.UserStories)
					{
						if (us.Id == 0)
						{
							proj.UserStories.Add(us);
						}
					}
					proj.IsAccepted = project.IsAccepted;
					proj.IsProjectRequestSent = project.IsProjectRequestSent;
					if (project.DevelopCompany != null)
					{
						var v = context.Companies.FirstOrDefault<Company>(x => x.Name == project.DevelopCompany.Name);
						proj.DevelopCompany = v;
					}
					context.Entry(proj).State = System.Data.Entity.EntityState.Modified;
					context.SaveChanges();
					LogHelper.GetLogger().Info(" UpdateProject method succeeded. Returned true.");

					return true;
				}
				LogHelper.GetLogger().Info("UpdateProject method returned false.");

				return false;
			}
		}

		public List<Project> GetAllProjects()
		{
			using (AccessDB context = new AccessDB())
			{
				List<Project> projects = context.Projects.Include("DevelopCompany").Include("ProductOwner").ToList();

				foreach (var proj in projects)
				{
					if (proj.ProductOwner != null)
					{
					}
				}

				LogHelper.GetLogger().Info("GetAllProjects method succeeded. Returned list of projects.");

				return projects;
			}
		}

		public List<UserStory> GetUserStoryFromProject(Project project)
		{
			using (AccessDB context = new AccessDB())
			{
				List<UserStory> userStories = context.UserStories.Where<UserStory>((x) => x.Project.Id == project.Id).ToList();
				LogHelper.GetLogger().Info("GetUserStoryFromProject method succeeded. Returned list of user stories.");
				return userStories;
			}
		}

		public bool UpdateUserStory(UserStory userStory)
		{
			using (AccessDB context = new AccessDB())
			{
				UserStory us = context.UserStories.FirstOrDefault<UserStory>((x) => x.Id == userStory.Id);

				if (us != null)
				{
					us.UpdateProperties(userStory);
					List<Common.Entities.Task> tasks = context.Tasks.Where<Common.Entities.Task>((x) => x.UserStory.Id == userStory.Id).ToList();
					foreach (var t in tasks)
					{
						if (userStory.Tasks.ToList().Exists((x) => x.Id == t.Id))
						{
							continue;
						}

						context.Entry(t).State = System.Data.Entity.EntityState.Deleted;
					}

					foreach (var t in userStory.Tasks)
					{
						if (t.Id == 0)
						{
							us.Tasks.Add(t);
						}
					}
					context.Entry(us).State = System.Data.Entity.EntityState.Modified;
					context.SaveChanges();
					return true;
				}

				return false;
			}
		}

		public bool RemoveCompany(Company company)
		{
			using (AccessDB context = new AccessDB())
			{
				Company c = context.Companies.FirstOrDefault<Company>((x) => x.Name.Equals(company.Name));
				if (c == null)
				{
					return false;
				}
				context.Companies.Remove(c);
				context.Entry(c).State = System.Data.Entity.EntityState.Deleted;
				context.SaveChanges();
			}
			return true;
		}

		public List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory)
		{
			using (AccessDB context = new AccessDB())
			{
				List<Common.Entities.Task> tasks = context.Tasks.Where<Common.Entities.Task>((x) => x.UserStory.Id == userStory.Id).ToList();

				return tasks;
			}
		}

		public Project GetProjectFromUserStory(UserStory userStory)
		{
			using (AccessDB context = new AccessDB())
			{
				UserStory us = context.UserStories.Include("Project").FirstOrDefault<UserStory>((x) => x.Id == userStory.Id);
				Project proj = context.Entry(us).Reference("Project").CurrentValue as Project;
				proj.UserStories = null; //because of circular reference
				return proj;
			}
		}

		public bool ModifyCompanyToPartner(Company company)
		{
			using (AccessDB context = new AccessDB())
			{

				Company com = context.Companies.FirstOrDefault((x) => x.Name == company.Name);
				if (com != null)
				{
					com.State = State.CompanyState.Partner;
					context.Entry(com).State = System.Data.Entity.EntityState.Modified;
					context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public List<User> GetAllUsers()
		{
			using (AccessDB context = new AccessDB())
			{
				List<User> users = context.Users.ToList();

				LogHelper.GetLogger().Info("GetAllUsers method succeeded. Returned list of users.");

				return users;
			}
		}


		public bool RemoveUser(User user)
		{
			using (AccessDB context = new AccessDB())
			{
				User us = context.Users.FirstOrDefault<User>((x) => x.Id == user.Id);

				if (us != null)
				{

					context.Entry(us).State = System.Data.Entity.EntityState.Deleted;
					context.SaveChanges();
					LogHelper.GetLogger().Info(" RemoveUser method succeeded. Returned true.");

					return true;
				}
				LogHelper.GetLogger().Info("RemoveUser method returned false.");

				return false;
			}

		}


        public bool ChangeCompanyState(Company company, State.CompanyState companyState)
        {
            using (AccessDB context = new AccessDB())
            {
                Company c = context.Companies.FirstOrDefault<Company>((x) => x.Name == company.Name);
                if (c == null)
                {
                    return false;
                }
                c.State = companyState;
                context.Entry(c).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return true;
        }

    }
}