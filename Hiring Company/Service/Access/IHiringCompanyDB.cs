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
       bool AddCompany(Company company);
       bool AddProject(Project project);
       bool LogIn(string username, string password);
       bool LogOut(string username);

       bool UserRegister(User user);
       List<User> LoginUsersOverview();
       List<Company> GetAllCompanies();

       bool RequestPartnership(int id);

       

       bool UpdateUser(User user);

      // double PasswordAge();

        // jos treba funkcija za provjeru koliko je user storija zavrseno od nekog projekta.
       
      

    }
}
