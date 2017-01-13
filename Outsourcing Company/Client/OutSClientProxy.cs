using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
using Common.Entities;
using Common;

namespace Client
{
    public class OutSClientProxy : ChannelFactory<IOutsourcingContract>, IOutsourcingContract
    {
        IOutsourcingContract factory;
        public OutSClientProxy(NetTcpBinding binding, string address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public OutSClientProxy(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
            LogHelper.GetLogger().Info("Outsourcing company client started comunication with service.");
        }

        public bool AddUser(OcUser user)
        {
            bool result = false;
            try
            {
                result = factory.AddUser(user);
                LogHelper.GetLogger().Info("AddUser method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("AddUser method failed. ", e);

            }
            return result;
        }

        public bool AddCompany(Company company)
        {
            bool result = false;

            try
            {
                LogHelper.GetLogger().Info("AddUser method succeeded.");

                result = factory.AddCompany(company);

            }
            catch (Exception e)
            {

                LogHelper.GetLogger().Error("AddCompany method failed. ", e);

            }
            return result;
        }

        public bool AddProject(OcProject project)
        {
            bool result = false;

            try
            {
                result = factory.AddProject(project);
                LogHelper.GetLogger().Info("AddProject method succeeded.");


            }
            catch (Exception e)
            {

                LogHelper.GetLogger().Error("AddCompany method failed. " + e.ToString());

            }
            return result;
        }
        /*
        public bool AddUserStory(UserStory userStory)
        {
            throw new NotImplementedException();
        }

        public bool AddTask(Common.Entities.Task task)
        {
            bool result = false;

            try
            {
                result = factory.AddTask(task);
                LogHelper.GetLogger().Info("AddTask method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("AddCompany method failed. " + e.ToString());
            }
            return result;

        }*/

        public bool AddTeam(Team team)
        {
            bool result = false;
            try
            {
                result = factory.AddTeam(team);
                LogHelper.GetLogger().Info("AddTeam method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("AddTeam method failed. " + e.ToString());

            }
            return result;

        }

        public bool LogIn(string username, string password)
        {
            bool result = false;

            try
            {
                result = factory.LogIn(username, password);
                LogHelper.GetLogger().Info(" Login method succeeded.");
            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("Loging method failed. " + e.ToString());
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
                LogHelper.GetLogger().Error("Logout method failed.  " + e.ToString());

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
                LogHelper.GetLogger().Error("GetAllCompanies method failed. " + e.ToString());

            }
            return result;
        }

        public List<Team> GetAllTeams()
        {
            List<Team> result = null;

            try
            {
                result = factory.GetAllTeams();
                LogHelper.GetLogger().Info("GetAllTeams method succeeded.");
            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("GetAllTeams method failed. " + e.ToString());

            }
            return result;
        }

        public OcUser GetUser(string username)
        {
            OcUser result = null;

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

        public bool UpdateUser(OcUser user)
        {
            bool result = false;

            try
            {
                result = factory.UpdateUser(user);
                LogHelper.GetLogger().Info("UpdateUser method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("UpdateUser method failed. " + e.ToString());
            }
            return result;
        }


        public bool AnswerToRequest(Company company)
        {
            bool result = false;
            try
            {
                result = factory.AnswerToRequest(company);
                LogHelper.GetLogger().Info("AnswerToRequest method succeeded.");


            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("AnswerToRequest method failed. " + e.ToString());

            }
            return result;
        }

        public List<OcProject> GetAllProjects()
        {
            List<OcProject> result = null;

            try
            {
                result = factory.GetAllProjects();
                LogHelper.GetLogger().Info("GetAllProjects method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("GetAllProjects method failed. " + e.ToString());

            }
            return result;
        }

        public bool SendUserStory(Company company, UserStory userStrory, Project project)
        {
            bool result = false;
            try
            {
                result = factory.SendUserStory(company, userStrory, project);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool AnswerToProject(Company company, Project project)
        {
            bool result = false;
            try
            {
                result = factory.AnswerToProject(company, project);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public bool ModifyCompany(Company company)
        {
            bool result = false;
            try
            {
                result = factory.ModifyCompany(company);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public bool ChangeCompanyState(Company company, State.CompanyState state)
        {
            bool result = false;
            try
            {
                result = factory.ChangeCompanyState(company, state);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public bool RemoveCompany(Company company)
        {
            bool result = false;
            try
            {
                result = factory.RemoveCompany(company);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public List<OcUser> GetAllUsers()
        {
            List<OcUser> result = null;

            try
            {
                result = factory.GetAllUsers();
                LogHelper.GetLogger().Info("GetAllUsers method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("GetAllUsers method failed. " + e.ToString());

            }
            return result;
        }


        public List<OcUser> GetAllUsersWithoutTeam()
        {
            List<OcUser> result = null;

            try
            {
                result = factory.GetAllUsersWithoutTeam();
                LogHelper.GetLogger().Info("GetAllUsersWithoutTeam method succeeded.");
            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("GetAllUsersWithoutTeam method failed. " + e.ToString());
            }
            return result;
        }


        public bool UpdateProject(OcProject project)
        {
            bool result = false;
            try
            {
                result = factory.UpdateProject(project);
                LogHelper.GetLogger().Info("UpdateProject method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("UpdateProject method failed. " + e.ToString());

            }
            return result;
        }


        public bool RemoveProject(OcProject project)
        {
            bool result = false;
            try
            {
                result = factory.RemoveProject(project);
                LogHelper.GetLogger().Info("RemoveProject method succeeded.");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("RemoveProject method failed. " + e.ToString());

            }
            return result;
        }


        public bool RemoveUser(OcUser user)
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


        public List<UserStory> GetUserStoryFromProject(OcProject project)
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

        public List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory)
        {
            List<Common.Entities.Task> result = null;

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

        public OcProject GetProjectFromUserStory(UserStory userStory)
        {
            OcProject result = null;

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
    }
}

