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
        User GetUser(string username);
        List<User> LoginUsersOverview();

        bool AddCompany(Company company);
        List<Company> GetAllCompanies();

        bool AddProject(Project project);
        List<Project> GetAllProjects();

        bool RequestPartnership(int id);


        // double PasswordAge();

        // jos treba funkcija za provjeru koliko je user storija zavrseno od nekog projekta.



    }
}
