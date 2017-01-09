using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
using Common.Entities;

namespace Client
{
	public class OutSClientProxy : ChannelFactory<IOutsourcingCompanyService>, IOutsourcingCompanyService
	{
		IOutsourcingCompanyService factory;
		public OutSClientProxy(NetTcpBinding binding, string address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}

		public OutSClientProxy(NetTcpBinding binding, EndpointAddress address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}






        public bool AddUser(OcUser user)
        {
          return  factory.AddUser(user);
        }

        public bool AddCompany(Company company)
        {
            return factory.AddCompany(company);
        }

        public bool AddProject(OcProject project)
        {
            return factory.AddProject(project);
        }

        public bool AddUserStory(UserStory userStory)
        {
            throw new NotImplementedException();
        }

        public bool AddTask(Common.Entities.Task task)
        {
            return factory.AddTask(task);
        }

        public bool AddTeam(Team team)
        {
            return factory.AddTeam(team);
        }

        public bool LogIn(string username, string password)
        {
            return factory.LogIn(username, password);
        }

        public bool LogOut(string username)
        {
            return factory.LogOut(username);
        }

        public bool UserRegister(OcUser user)
        {
            return factory.UserRegister(user);
        }

        public List<OcUser> LoginUsersOverview()
        {
           return  factory.LoginUsersOverview();
        }

        public List<Company> GetAllCompanies()
        {
            return factory.GetAllCompanies();
        }

		public OcUser GetUser(string username)
		{
			OcUser result = null;

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

		public bool UpdateUser(OcUser user)
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


        public bool AnswerToRequest(Company company)
        {
           return factory.AnswerToRequest(company);
        }

		public List<OcProject> GetAllProjects()
		{
			List<OcProject> result = null;

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
    }
}

