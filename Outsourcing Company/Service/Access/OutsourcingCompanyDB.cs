using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Entities;

namespace Service.Access
{
    public class OutsourcingCompanyDB : IOutsourcingCompanyDB
    {
        private static IOutsourcingCompanyDB myDB;

        public static IOutsourcingCompanyDB Instance
        {
            get
            {
                if (myDB == null)
                    myDB = new OutsourcingCompanyDB();

                return myDB;
            }
            set
            {
                if (myDB == null)
                    myDB = value;
            }
        }


        public bool AddUser(OcUser user)
        {
            using (var context = new AccessDB())
            {
                context.Users.Add(user);
                var count = context.SaveChanges();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public bool AddProject(OcProject project)
        {
            using (var context = new AccessDB())
            {
                context.Projects.Add(project);
                var count = context.SaveChanges();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public bool AddCompany(Company company)
        {
            using (var context = new AccessDB())
            {
                context.Companies.Add(company);
                var count = context.SaveChanges();
                if (count > 0)
                    return true;
                return false;
            }
        }


        public bool AddUserStory(UserStory userStory)
        {
            using (var context = new AccessDB())
            {
                context.UserStories.Add(userStory);
                var count = context.SaveChanges();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public bool AddTask(Task task)
        {
            using (var context = new AccessDB())
            {
                context.Tasks.Add(task);
                var count = context.SaveChanges();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public bool AddTeam(Team team)
        {
            using (var context = new AccessDB())
            {
                context.Teams.Add(team);
                var count = context.SaveChanges();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public bool UserRegister(OcUser user)
        {
            using (var context = new AccessDB())
            {
                var result = from users in context.Users select users;
                var userList = result.ToList();
                foreach (var registeredUser in userList)
                    if (registeredUser.Username.Equals(user.Username))
                        return false;
                if (Instance.AddUser(user))
                    return true;
            }

            return false;
        }


        public bool LogIn(string username, string password)
        {
            using (var context = new AccessDB())
            {
                User user = context.Users.FirstOrDefault(x => x.Username == username);
                if (user != null)
                {
                    if (user.Password.Equals(password))
                        user.IsAuthenticated = true;
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool LogOut(string username)
        {
            using (var context = new AccessDB())
            {
                var result = from b in context.Users
                    where b.Username.Equals(username)
                    select b;
                User user = result.ToList().FirstOrDefault();
                if (user != null)
                    if (user.IsAuthenticated)
                    {
                        user.IsAuthenticated = false;
                        context.Entry(user).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
            }
            return false;
        }


        public List<Company> GetAllCompanies()
        {
            using (var context = new AccessDB())
            {
                var result = from company in context.Companies select company;
                var companies = result.ToList();
                return companies;
            }
        }


        public List<OcUser> LoginUsersOverview()
        {
            using (var context = new AccessDB())
            {
                var loginUsers = new List<OcUser>();
                var result = from users in context.Users select users;
                var userList = result.ToList();

                foreach (var user in userList)
                    if (user.IsAuthenticated)
                        loginUsers.Add(user);
                return loginUsers;
            }
        }
    }
}