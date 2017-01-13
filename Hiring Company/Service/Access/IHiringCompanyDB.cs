using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Access
{
    public interface IHiringCompanyDB
    {

        bool AddUser(User user);
        bool LogIn(string username, string password);
        bool LogOut(string username);
        bool UserRegister(User user);
        bool UpdateUser(User user);
        bool RemoveUser(User user);
		bool AddUserStory(UserStory userStory);
        User GetUser(string username);

        List<User> LoginUsersOverview();
        List<User> GetAllUsers();

        bool AddCompany(Company company);
        List<Company> GetAllCompanies();

        bool AddProject(Project project);
        List<Project> GetAllProjects();
        bool UpdateProject(Project project);

		bool ModifyCompanyToPartner(Company company);
		bool RemoveCompany(Company company);

        bool RequestPartnership(int id);

		List<UserStory> GetUserStoryFromProject(Project project);

        bool UpdateUserStory(UserStory userStory);

        bool UpdateTask(Common.Entities.Task task);

        List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory);
        Project GetProjectFromUserStory(UserStory userStory);

        bool ChangeCompanyState(Company company, State.CompanyState companyState);

        // double PasswordAge();

        // jos treba funkcija za provjeru koliko je user storija zavrseno od nekog projekta.




      
    }
}
