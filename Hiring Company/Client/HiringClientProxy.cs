// --------------------------------------------------------------------------------------------------------------------
// <copyright company="NMMS" file="HiringClientProxy.cs">
//   bfbhtgfgbnthg
// </copyright>
// <summary>
//   dgnbgngngn
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Client
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel;

	using Common;
	using Common.Entities;

	using ServiceContract;

	/// <summary>
	/// The hiring client proxy.
	/// </summary>
	public class HiringClientProxy : ChannelFactory<IHiringContract>, IHiringContract
	{
		/// <summary>
		/// The factory.
		/// </summary>
		public IHiringContract factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HiringClientProxy"/> class.
        /// </summary>
        /// <param name="binding">
        /// The binding.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        public HiringClientProxy(NetTcpBinding binding, string address)
            : base(binding, address)
        {
			try
			{
				this.factory = this.CreateChannel();
				LogHelper.GetLogger().Info("Hiring company client started comunication with service.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("Hiring company client failed. ", e);
			}
           
        }

		public HiringClientProxy() { }

		/// <summary>
		/// The add project.
		/// </summary>
		/// <param name="project">
		/// The project.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool AddProject(Project project)
		{
			bool result = false;

			try
			{
				result = this.factory.AddProject(project);
				LogHelper.GetLogger().Info("AddProject method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("AddProject method failed. ", e);
			}

			return result;
		}

        /// <summary>
        /// The answer to user story.
        /// </summary>
        /// <param name="company">
        /// The company.
        /// </param>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="userStory">
        /// The user story.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AnswerToUserStory(Company company, Project project, UserStory userStory)
        {
            bool result = false;
            try
            {
                result = this.factory.AnswerToUserStory(company, project, userStory);
            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("AnswerToUserStory method failed. " + e.ToString());

            }

			return result;
		}

		/// <summary>
		/// The get all companies.
		/// </summary>
		/// <returns>
		/// The <see cref="List"/>.
		/// </returns>
		public List<Company> GetAllCompanies()
		{
			List<Company> result = null;

			try
			{
				result = this.factory.GetAllCompanies();
				LogHelper.GetLogger().Info("GetAllCompanies method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetAllCompanies method failed. " + e.ToString());
			}

			return result;
		}

		/// <summary>
		/// The get all projects.
		/// </summary>
		/// <returns>
		/// The <see cref="List"/>.
		/// </returns>
		public List<Project> GetAllProjects()
		{
			List<Project> result = null;

			try
			{
				result = this.factory.GetAllProjects();
				LogHelper.GetLogger().Info("GetAllProjects method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetAllProjects method failed. " + e.ToString());
			}

			return result;
		}

		/// <summary>
		/// The get project from user story.
		/// </summary>
		/// <param name="userStory">
		/// The user story.
		/// </param>
		/// <returns>
		/// The <see cref="Project"/>.
		/// </returns>
		public Project GetProjectFromUserStory(UserStory userStory)
		{
			Project result = null;

			try
			{
				result = this.factory.GetProjectFromUserStory(userStory);
				LogHelper.GetLogger().Info("GetProjectFromUserStory method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetProjectFromUserStory method failed. ", e);
			}

			return result;
		}

		/// <summary>
		/// The get tasks from user story.
		/// </summary>
		/// <param name="userStory">
		/// The user story.
		/// </param>
		/// <returns>
		/// The <see cref="List"/>.
		/// </returns>
		public List<Task> GetTasksFromUserStory(UserStory userStory)
		{
			List<Task> result = null;

			try
			{
				result = this.factory.GetTasksFromUserStory(userStory);
				LogHelper.GetLogger().Info("GetTasksFromUserStory method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetTasksFromUserStory method failed. ", e);
			}

			return result;
		}

		/// <summary>
		/// The get user.
		/// </summary>
		/// <param name="username">
		/// The username.
		/// </param>
		/// <returns>
		/// The <see cref="User"/>.
		/// </returns>
		public User GetUser(string username)
		{
			User result = null;

			try
			{
				result = this.factory.GetUser(username);
				LogHelper.GetLogger().Info("GetUser method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetUser method failed. " + e.ToString());
			}

			return result;
		}

		/// <summary>
		/// The get user story from project.
		/// </summary>
		/// <param name="project">
		/// The project.
		/// </param>
		/// <returns>
		/// The <see cref="List"/>.
		/// </returns>
		public List<UserStory> GetUserStoryFromProject(Project project)
		{
			List<UserStory> result = null;

			try
			{
				result = this.factory.GetUserStoryFromProject(project);

				LogHelper.GetLogger().Info("GetUserStoryFromProject method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetUserStoryFromProject method failed. " + e.Message);
			}

			return result;
		}

		/// <summary>
		/// The log in.
		/// </summary>
		/// <param name="username">
		/// The username.
		/// </param>
		/// <param name="password">
		/// The password.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool LogIn(string username, string password)
		{
			bool result = false;

			try
			{
				result = this.factory.LogIn(username, password);
				LogHelper.GetLogger().Info("Login method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("Loging method failed. " + e.ToString());
			}

			return result;
		}

        /// <summary>
        /// The log out.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LogOut(string username)
        {
            bool result = false;

			try
			{
				result = this.factory.LogOut(username);
				LogHelper.GetLogger().Info("Logout method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error(" Logout method failed.  ", e);
			}

			return result;
		}

		/// <summary>
		/// The send project.
		/// </summary>
		/// <param name="company">
		/// The company.
		/// </param>
		/// <param name="project">
		/// The project.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool SendProject(Company company, Project project)
		{
			bool result = false;
			try
			{
				result = this.factory.SendProject(company, project);
				LogHelper.GetLogger().Info("SendProject method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetTasksFromUserStory method failed. ", e);
			}

			return result;
		}

		/// <summary>
		/// The send request.
		/// </summary>
		/// <param name="company">
		/// The company.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool SendRequest(Company company)
		{
			bool result = false;

			try
			{
				result = this.factory.SendRequest(company);
				LogHelper.GetLogger().Info("SendRequest method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("SendRequest method failed. ", e);
			}

			return result;
		}

		/// <summary>
		/// The update project.
		/// </summary>
		/// <param name="project">
		/// The project.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool UpdateProject(Project project)
		{
			bool result = false;

			try
			{
				result = this.factory.UpdateProject(project);
				LogHelper.GetLogger().Info("UpdateProject method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("UpdateProject method failed. " + e.ToString());
			}

			return result;
		}

		/// <summary>
		/// The update user.
		/// </summary>
		/// <param name="user">
		/// The user.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool UpdateUser(User user)
		{
			bool result = false;

			try
			{
				result = this.factory.UpdateUser(user);
				LogHelper.GetLogger().Info("UpdateUser method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("UpdateUser method failed. ", e);
			}

			return result;
		}

		/// <summary>
		/// The update user story.
		/// </summary>
		/// <param name="userStory">
		/// The user story.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool UpdateUserStory(UserStory userStory)
		{
			bool result = false;

			try
			{
				result = this.factory.UpdateUserStory(userStory);

				LogHelper.GetLogger().Info("UpdateUserStory method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("UpdateUserStory method failed. ", e);
			}

			return result;
        }
        
        public bool ModifyCompany(Company company)
        {
            throw new NotImplementedException();
        }
        
        public List<User> GetAllUsers()
        {
            List<User> result = null;

			try
			{
				result = this.factory.GetAllUsers();
				LogHelper.GetLogger().Info("GetAllUsers method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("GetAllUsers method failed.", e);
			}

            return result;
        }
        
        public bool RemoveUser(User user)
        {
            bool result = false;

			try
			{
				result = this.factory.RemoveUser(user);
				LogHelper.GetLogger().Info("UpdateUser method succeeded.");
			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("UpdateUser method failed. ", e);
			}

			return result;
		}

		public bool AddUser(User user)
		{
			bool result = false;

			try
			{
				result = this.factory.AddUser(user);
				LogHelper.GetLogger().Info("UpdateUser method succeeded.");

			}
			catch (Exception e)
			{
				LogHelper.GetLogger().Error("UpdateUser method failed. ", e);
			}
			return result;

		}


        public bool RemoveUS(UserStory userStory)
        {
            bool result = false;

            try
            {
                result = this.factory.RemoveUS(userStory);
                LogHelper.GetLogger().Info("RemoveUS method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("RemoveUS method failed. ", e);
            }
            return result;
        }


        public List<UserStory> GetAllUserStories()
        {
             List<UserStory> result = null;
             try
             {
                 result = this.factory.GetAllUserStories();
                 LogHelper.GetLogger().Info("GetAllUserStories method succeeded.");

             }
             catch (Exception e)
             {
                 LogHelper.GetLogger().Error("GetAllUserStories method failed. ", e);
             }
             return result;
        }
    }
		public bool RemoveAllCompanies()
		{
			bool result = false;

			try
			{
				result = factory.RemoveAllCompanies();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return result;
		}
	}
}