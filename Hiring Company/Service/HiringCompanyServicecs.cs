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
            return HiringCompanyDB.Instance.ChangeCompanyState(company);
        }

        public List<UserStory> GetUserStoryFromProject(Project project)
        {
            return HiringCompanyDB.Instance.GetUserStoryFromProject(project);
        }
    }
}