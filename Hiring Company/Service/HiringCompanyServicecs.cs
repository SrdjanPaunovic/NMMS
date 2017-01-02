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

        public bool RequestPartnership(int id)
        {
            throw new NotImplementedException();
        }


        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();

        }
        


        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}