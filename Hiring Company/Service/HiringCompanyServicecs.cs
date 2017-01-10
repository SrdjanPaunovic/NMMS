﻿using System;
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
			Service.Hiring2OutSCompanyService.companies[company.Name].SendRequest(Program.baseAddress, Program.myHiringCompany);
			//return HiringCompanyDB.Instance.ChangeCompanyState(company, State.CompanyState.Requested);
            return true;
           
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
			LogHelper.GetLogger().Info("Call UpdateUserStory method.");

            return HiringCompanyDB.Instance.UpdateUserStory(userStory);
        }
    }
}