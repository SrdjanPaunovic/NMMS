using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceContract;
using Service.Access;
using Common.Entities;
using Common;

namespace HiringCompanyService
{
	public class HiringCompanyService : IHiringContract
	{


		public bool LogIn(string username, string password)
		{

			LogHelper.GetLogger().Info("Call Login method.");
			return HiringCompanyDB.Instance.LogIn(username, password);
		}

		public bool LogOut(string username)
		{
			LogHelper.GetLogger().Info("Call LogOut method.");

			return HiringCompanyDB.Instance.LogOut(username);

		}

		public bool UserRegister(User user)
		{
			LogHelper.GetLogger().Info("Call UserRegister method.");
		
			return HiringCompanyDB.Instance.UserRegister(user);

		}

		public List<User> LoginUsersOverview()
		{
			LogHelper.GetLogger().Info("Call LoginUsersOverview method.");

			return HiringCompanyDB.Instance.LoginUsersOverview();
		}

		public List<Company> GetAllCompanies()
		{
			LogHelper.GetLogger().Info("Call GetAllCompanies method.");

			return HiringCompanyDB.Instance.GetAllCompanies();
		}

		public bool UpdateUser(User user)
		{
			LogHelper.GetLogger().Info("Call UpdateUser method.");

			return HiringCompanyDB.Instance.UpdateUser(user);
		}


		public User GetUser(string username)
		{
			LogHelper.GetLogger().Info("Call GetUser method.");

			return HiringCompanyDB.Instance.GetUser(username);
		}

		public bool AddProject(Project project)
		{
			LogHelper.GetLogger().Info("Call AddProject method.");

			return HiringCompanyDB.Instance.AddProject(project);
		}

		public List<Project> GetAllProjects()
		{
			LogHelper.GetLogger().Info("Call GetAllProjects method.");

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
			LogHelper.GetLogger().Info("Call GetUserStoryFromProject method.");

			return HiringCompanyDB.Instance.GetUserStoryFromProject(project);
		}


        public bool UpdateProject(Project project)
        {
			LogHelper.GetLogger().Info("Call UpdateProject method.");

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