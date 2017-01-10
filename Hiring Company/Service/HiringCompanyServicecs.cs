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
			bool success = HiringCompanyDB.Instance.ChangeCompanyState(company, State.CompanyState.Requested);
			if (success)
			{
				try
				{
					Service.Hiring2OutSCompanyService.companies[company.Name].SendRequest(Program.baseAddress, Program.myHiringCompany);
					return true;
				}
				catch (Exception)
				{
					
					throw;
				}
			}
			else
			{
				return false;
			}
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


		public bool SendProject(Company company, Project project)
		{
			try
			{
				// salje napravljen i odobren projekat
				Service.Hiring2OutSCompanyService.companies[company.Name].SendProject(Program.myHiringCompany, project);  
				return true;
			}
			catch (Exception)
			{
				
				throw;
			}
		}

		public bool AnswerToUserStory(Company company, Project project, UserStory userStory)
		{
			try
			{
				// odgovara na zahtev za US
				Service.Hiring2OutSCompanyService.companies[company.Name].AnswerToUserStory(Program.myHiringCompany, userStory, project); 
				return true;
			}
			catch (Exception)
			{
				
				throw;
			}
		}

        public List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory)
        {
            return HiringCompanyDB.Instance.GetTasksFromUserStory(userStory);
        }


        public Project GetProjectFromUserStory(UserStory userStory)
        {
            return HiringCompanyDB.Instance.GetProjectFromUserStory(userStory);
        }
    }
}