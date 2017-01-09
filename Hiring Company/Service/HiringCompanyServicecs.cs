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
			return HiringCompanyDB.Instance.LogIn(username, password);
		}

		public bool LogOut(string username)
		{
			return HiringCompanyDB.Instance.LogOut(username);

		}

		public bool UserRegister(User user)
		{
			return HiringCompanyDB.Instance.UserRegister(user);

		}

		public List<User> LoginUsersOverview()
		{
			return HiringCompanyDB.Instance.LoginUsersOverview();
		}

		public List<Company> GetAllCompanies()
		{
			return HiringCompanyDB.Instance.GetAllCompanies();
		}

		public bool UpdateUser(User user)
		{
			return HiringCompanyDB.Instance.UpdateUser(user);
		}


		public User GetUser(string username)
		{
			return HiringCompanyDB.Instance.GetUser(username);
		}

		public bool AddProject(Project project)
		{
			return HiringCompanyDB.Instance.AddProject(project);
		}

		public List<Project> GetAllProjects()
		{
			return HiringCompanyDB.Instance.GetAllProjects();
		}

		public bool SendRequest(Company company)
		{
			//TODO send real request
			Service.Hiring2OutSCompanyService.companies[company.Name].SendRequest(Program.baseAddress, Program.myHiringCompany);
			return HiringCompanyDB.Instance.ChangeCompanyState(company, State.CompanyState.Requested);
		}

		public List<UserStory> GetUserStoryFromProject(Project project)
		{
			return HiringCompanyDB.Instance.GetUserStoryFromProject(project);
		}


        public bool UpdateProject(Project project)
        {
            return HiringCompanyDB.Instance.UpdateProject(project);
        }

        public bool UpdateUserStory(UserStory userStory)
        {
            return HiringCompanyDB.Instance.UpdateUserStory(userStory);
        }
    }
}