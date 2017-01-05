
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;

namespace Service.Access
{
    public class HiringCompanyDB : IHiringCompanyDB
    {

        public static HiringCompanyDB hirinigCompanyDB;

        public static HiringCompanyDB Instance
        {
            get
            {
                if (hirinigCompanyDB == null)
                {
                    hirinigCompanyDB = new HiringCompanyDB();
                }
                return hirinigCompanyDB;
            }
            set
            {
                if (hirinigCompanyDB == null)
                {
                    hirinigCompanyDB = value;
                }
            }
        }

        public bool AddUser(User user)
        {
            using (var db = new AccessDB())
            {
                db.Users.Add(user);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool AddCompany(Company company)
        {
            using (var db = new AccessDB())
            {
                db.Companies.Add(company);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public bool AddProject(Project project)
        {
            using (var db = new AccessDB())
            {
                db.Projects.Add(project);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public bool LogIn(string username, string password)
        {
            using (AccessDB context = new AccessDB())
            {

                User user = context.Users.FirstOrDefault((x) => x.Username == username);
                if (user != null)
                {
                    if (user.Password.Equals(password))
                        user.IsAuthenticated = true;
                    context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool LogOut(string username)
        {
            using (AccessDB context = new AccessDB())
            {
                var result = from b in context.Users
                             where b.Username.Equals(username)
                             select b;
                User user = result.ToList().FirstOrDefault();
                if (user != null)
                {
                    if (user.IsAuthenticated)
                    {
                        user.IsAuthenticated = false;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UserRegister(User user)
        {
            using (AccessDB context = new AccessDB())
            {

                var result = from users in context.Users select users;
                List<User> userList = result.ToList();
                foreach (var registeredUser in userList)
                {
                    if (registeredUser.Username.Equals(user.Username))
                    {
                        return false;
                    }
                }
                if (HiringCompanyDB.Instance.AddUser(user))
                {
                    return true;
                }

            }

            return false;
        }

        public List<User> LoginUsersOverview()
        {

            using (AccessDB context = new AccessDB())
            {
                List<User> loginUsers = new List<User>();
                var result = from users in context.Users select users;
                List<User> userList = result.ToList();

                foreach (var user in userList)
                {
                    if (user.IsAuthenticated)
                    {
                        loginUsers.Add(user);
                    }

                }
                return loginUsers;
            }
        }

        public List<Company> GetAllCompanies()
        {
            using (AccessDB context = new AccessDB())
            {
                var result = from company in context.Companies select company;
                List<Company> companies = result.ToList();
                return companies;
            }
        }

        public bool RequestPartnership(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            using (AccessDB context = new AccessDB())
            {
                var result = from i in context.Users
                             where i.Id == user.Id
                             select i;
                User us = result.ToList<User>().FirstOrDefault();
                if (us != null)
                {
                    if (us.Username != user.Username)
                    {
                        var res = from j in context.Users
                                  where j.Username == user.Username
                                  select j;
                        List<User> users = res.ToList<User>();
                        if (users.Count == 0)
                        {
                            ModifieUserProperties(us, user);
                            context.Entry(us).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        ModifieUserProperties(us, user);
                        context.Entry(us).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                    return true;
                }
            }

            return false;
        }

        private void ModifieUserProperties(User original, User user)
        {
            original.Name = user.Name;
            original.Password = user.Password;
            original.StartTime = user.StartTime;
            original.EndTime = user.EndTime;
            original.IsAuthenticated = user.IsAuthenticated;
            original.Surname = user.Surname;
            original.Username = user.Username;
            original.Password_changed = user.Password_changed;
        }

        public User GetUser(string username)
        {
            using (AccessDB context = new AccessDB())
            {

                User user = context.Users.FirstOrDefault((x) => x.Username == username);
                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }

        public List<Project> GetAllProjects()
        {
            using (AccessDB context = new AccessDB())
            {
                List<Project> projects = context.Projects.ToList();
                return projects;
            }
        }

     
    }
}