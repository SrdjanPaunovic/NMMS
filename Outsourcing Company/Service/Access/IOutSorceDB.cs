using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using System.ServiceModel;
using Common.Entities;
namespace Service.Access
{
    
	public interface IOutsourcingCompanyDB
	{

        bool AddUser(User user);
        bool AddCompany(Company company);
        bool AddProject(Project project);
        bool LogIn(string username, string password);
        bool LogOut(string username);

        bool UserRegister(User user);
        List<User> LoginUsersOverview();
        List<Company> GetAllCompanies();

	}
}
