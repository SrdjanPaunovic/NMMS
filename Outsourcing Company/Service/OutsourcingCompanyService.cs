using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Service.Access;
using Common;
using Common.Entities;


namespace Service
{
	public class OutsourcingCompanyService : IOutsourcingCompanyService
	{



        public bool AddUser(OcUser user)
        {
        return  OutsourcingCompanyDB.Instance.AddUser(user);
        }

        public bool AddCompany(Company company)
        {
            return OutsourcingCompanyDB.Instance.AddCompany(company);
        }

        public bool AddProject(OcProject project)
        {
            return OutsourcingCompanyDB.Instance.AddProject(project);
        }

        public bool AddUserStory(UserStory userStory)
        {
            return OutsourcingCompanyDB.Instance.AddUserStory(userStory);
        }

        public bool AddTask(Common.Entities.Task task)
        {
            return OutsourcingCompanyDB.Instance.AddTask(task);
        }

        public bool AddTeam(Team team)
        {
            return OutsourcingCompanyDB.Instance.AddTeam(team);
        }

        public bool LogIn(string username, string password)
        {
            return OutsourcingCompanyDB.Instance.LogIn(username, password);
        }

        public bool LogOut(string username)
        {
            return OutsourcingCompanyDB.Instance.LogOut(username);
        }

        public bool UserRegister(OcUser user)
        {
            return OutsourcingCompanyDB.Instance.UserRegister(user);
        }

        public List<OcUser> LoginUsersOverview()
        {
            return OutsourcingCompanyDB.Instance.LoginUsersOverview();
        }

        public List<Company> GetAllCompanies()
        {
            return OutsourcingCompanyDB.Instance.GetAllCompanies();
        }
    }
}
