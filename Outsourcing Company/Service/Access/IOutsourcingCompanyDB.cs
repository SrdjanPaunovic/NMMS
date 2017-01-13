using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Common;
using Common.Entities;

namespace Service.Access
{
    public interface IOutsourcingCompanyDB
    {
        bool AddUser(OcUser user);
        bool AddCompany(Company company);
        bool AddProject(OcProject project);
        bool AddUserStory(UserStory userStory);
        bool AddTask(Common.Entities.Task task);
        bool RemoveUser(OcUser user);

        bool AddTeam(Team team);
        bool LogIn(string username, string password);
        bool LogOut(string username);

        List<Company> GetAllCompanies();
        OcUser GetUser(string username);
        List<OcUser> GetAllUsers();
        List<OcUser> GetAllUsersWithoutTeam();
        bool UpdateUser(OcUser user);
        bool UpdateProject(OcProject project);
        List<OcProject> GetAllProjects();
        List<Team> GetAllTeams();
        bool ModyfyUserStory(UserStory userStory);
        bool ModifyCompanyToPartner(Company company);
        bool ChangeCompanyState(Company company, State.CompanyState state);
        bool RemoveCompany(Company company);
        bool RemoveProject(OcProject project);
        List<UserStory> GetUserStoryFromProject(OcProject project);
        List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory);
        OcProject GetProjectFromUserStory(UserStory userStory);
        bool UpdateUserStory(UserStory userStory);

    }
}
