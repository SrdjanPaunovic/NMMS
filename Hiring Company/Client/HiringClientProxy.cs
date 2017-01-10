using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using System.ServiceModel;
using Common.Entities;
using Common;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "App.config", Watch = true)]

namespace Client
{
    public class HiringClientProxy : ChannelFactory<IHiringContract>, IHiringContract
    {
        IHiringContract factory;
        public HiringClientProxy(NetTcpBinding binding, string address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
			LogHelper.GetLogger().Info("Hiring company client started comunication with service.");
        }


        public bool LogIn(string username, string password)
        {
            bool result = false;

            try
            {
                result = factory.LogIn(username, password);
				LogHelper.GetLogger().Info("Login method succeeded.");
            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("Loging method failed. "+e.ToString());
            }
            return result;
        }

        public bool LogOut(string username)
        {
            bool result = false;

            try
            {
                result = factory.LogOut(username);
				LogHelper.GetLogger().Info("Logout method succeeded.");

            }
            catch (Exception e)
            {
                //TODO log
				LogHelper.GetLogger().Error(" Logout method failed.  "+e.ToString());

            }
            return result;
        }

        public bool UserRegister(Common.Entities.User user)
        {
            throw new NotImplementedException();
        }

        public List<Common.Entities.User> LoginUsersOverview()
        {
            throw new NotImplementedException();
        }

        public User GetUser(string username)
        {
            User result = null;

            try
            {
                result = factory.GetUser(username);
				LogHelper.GetLogger().Info("GetUser method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("GetUser method failed. " + e.ToString());

            }
            return result;
        }

        public bool UpdateUser(User user)
        {
            bool result = false;

            try
            {
                result = factory.UpdateUser(user);
				LogHelper.GetLogger().Info("UpdateUser method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("UpdateUser method failed. " +e.ToString());

            }
            return result;
        }

        public bool AddProject(Project project)
        {
            bool result = false;

            try
            {
                result = factory.AddProject(project);
				LogHelper.GetLogger().Info("AddProject method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("AddProject method failed. "+e.ToString());

            }
            return result;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> result = null;

            try
            {
                result = factory.GetAllProjects();
				LogHelper.GetLogger().Info("GetAllProjects method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("GetAllProjects method failed. "+e.ToString());

            }
            return result;
        }


        public bool SendRequest(Company company)
        {
            bool result = false;

            try
            {
                result = factory.SendRequest(company);
				LogHelper.GetLogger().Info("SendRequest method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("SendRequest method failed. "+e.ToString());

            }
            return result;
        }

        public List<UserStory> GetUserStoryFromProject(Project project)
        {
            List<UserStory> result = null;

            try
            {
                result = factory.GetUserStoryFromProject(project);

				LogHelper.GetLogger().Info("GetUserStoryFromProject method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("GetUserStoryFromProject method failed. " + e.ToString());

            }
            return result;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> result = null;

            try
            {
                result = factory.GetAllCompanies();
				LogHelper.GetLogger().Info("GetAllCompanies method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("GetAllCompanies method failed. "+e.ToString());

            }
            return result;
        }


        public bool UpdateProject(Project project)
        {
            bool result = false;

            try
            {
                result = factory.UpdateProject(project);
				LogHelper.GetLogger().Info("UpdateProject method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("UpdateProject method failed. "+e.ToString());
            }
            return result;
        }

        public bool UpdateUserStory(UserStory userStory)
        {
            bool result = false;

            try
            {
                result = factory.UpdateUserStory(userStory);

				LogHelper.GetLogger().Info("UpdateUserStory method succeeded.");

            }
            catch (Exception e)
            {
				LogHelper.GetLogger().Error("UpdateUserStory method failed. " +e.ToString());

            }
            return result;
        }

    }
}
