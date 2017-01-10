using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using System.ServiceModel;
using Common.Entities;


namespace Client
{
    public class HiringClientProxy : ChannelFactory<IHiringContract>, IHiringContract
    {
        IHiringContract factory;
        public HiringClientProxy(NetTcpBinding binding, string address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
        }


        public bool LogIn(string username, string password)
        {
            bool result = false;

            try
            {
                result = factory.LogIn(username, password);
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }

        public bool LogOut(string username)
        {
            bool result = false;

            try
            {
                result = factory.LogOut(username);
            }
            catch (Exception e)
            {
                //TODO log
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
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }

        public bool UpdateUser(User user)
        {
            bool result = false;

            try
            {
                result = factory.UpdateUser(user);
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }

        public bool AddProject(Project project)
        {
            bool result = false;

            try
            {
                result = factory.AddProject(project);
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> result = null;

            try
            {
                result = factory.GetAllProjects();
            }
            catch (Exception e)
            {
                //TODO log
                var rre = e;
            }
            return result;
        }


        public bool SendRequest(Company company)
        {
            bool result = false;

            try
            {
                result = factory.SendRequest(company);
            }
            catch (Exception e)
            {
                var rre = e;
            }
            return result;
        }

        public List<UserStory> GetUserStoryFromProject(Project project)
        {
            List<UserStory> result = null;

            try
            {
                result = factory.GetUserStoryFromProject(project);
            }
            catch (Exception e)
            {
                //TODO log
                var rre = e;
            }
            return result;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> result = null;

            try
            {
                result = factory.GetAllCompanies();
            }
            catch (Exception e)
            {
                //TODO log
                var rre = e;
            }
            return result;
        }


        public bool UpdateProject(Project project)
        {
            bool result = false;

            try
            {
                result = factory.UpdateProject(project);
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }

        public bool UpdateUserStory(UserStory userStory)
        {
            bool result = false;

            try
            {
                result = factory.UpdateUserStory(userStory);
            }
            catch (Exception e)
            {
                //TODO log
                var r = e;
            }
            return result;
        }

        public List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory)
        {
            List<Common.Entities.Task> result = null;

            try
            {
                result = factory.GetTasksFromUserStory(userStory);
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }


        public Project GetProjectFromUserStory(UserStory userStory)
        {
            Project result = null;

            try
            {
                result = factory.GetProjectFromUserStory(userStory);
            }
            catch (Exception e)
            {
                //TODO log
            }
            return result;
        }
    }
}
