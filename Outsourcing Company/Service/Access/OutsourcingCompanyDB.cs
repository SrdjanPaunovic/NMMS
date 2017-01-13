using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract;
using Common.Entities;

using Common;

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
            using (var db = new AccessDB())
            {
                var uList = db.Users.ToList();
                if (uList.Exists(x => x.Username == user.Username))
                {
                    LogHelper.GetLogger().Info("AddUser method returned false. User with username:" + user.Name + " already exists");

                    return false;
                }
                db.Users.Add(user);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    LogHelper.GetLogger().Info(" AddUser method succeeded. Returned true.");
                    return true;
                }
                LogHelper.GetLogger().Info("AddUser method returned false.");
                return false;

            }
        }

        public bool AddProject(OcProject project)
        {
            using (var context = new AccessDB())
            {
                context.Projects.Add(project);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    LogHelper.GetLogger().Info(" AddProject method succeeded. Returned true.");
                    return true;
                }
                else
                {
                    LogHelper.GetLogger().Info("AddProject method returned false.");
                    return false;
                }
            }
        }

        public bool AddCompany(Company company)
        {
            using (var context = new AccessDB())
            {
                context.Companies.Add(company);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    LogHelper.GetLogger().Info(" AddCompany method succeeded. Returned true.");

                    return true;
                }
                else
                {
                    LogHelper.GetLogger().Info("AddCompany method returned false.");

                    return false;
                }
            }
        }

        public bool AddUserStory(UserStory userStory)
        {
            using (var context = new AccessDB())
            {
                context.UserStories.Add(userStory);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    LogHelper.GetLogger().Info(" AddUserStory method succeeded. Returned true.");

                    return true;
                }
                else
                {
                    LogHelper.GetLogger().Info("AddUserStory method returned false.");

                    return false;
                }
            }
        }

        public bool AddTask(Common.Entities.Task task)
        {
            using (var context = new AccessDB())
            {
                context.Tasks.Add(task);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    LogHelper.GetLogger().Info(" AddTask method succeeded. Returned true.");

                    return true;
                }
                else
                {
                    LogHelper.GetLogger().Info("AddTask method returned false.");

                    return false;
                }
            }
        }

        public bool AddTeam(Team team)
        {
            using (var context = new AccessDB())
            {
                if (team.Developers != null)
                {
                    for (int i = 0; i < team.Developers.Count; i++)
                    {
                        var developer = team.Developers[i];
                        var d = context.Users.FirstOrDefault((x) => x.Id == developer.Id);
                        team.Developers[i] = d;
                    }
                }
                context.Teams.Add(team);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    LogHelper.GetLogger().Info(" AddTeam method succeeded. Returned true.");
                    return true;
                }
                else
                {
                    LogHelper.GetLogger().Info("AddTeam method returned false.");

                    return false;
                }
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
                    {
                        user.IsAuthenticated = true;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        LogHelper.GetLogger().Info("AddCompany method succeeded. Returned true.");

                        return true;
                    }
                }
            }
            LogHelper.GetLogger().Info("AddCompany method returned false.");

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
                        LogHelper.GetLogger().Info("LogOut method succeeded. Returned true.");

                        return true;
                    }
                }
            }
            LogHelper.GetLogger().Info("LogOut method returned false.");

            return false;
        }

        public List<OcUser> GetAllUsersWithoutTeam()
        {
            using (AccessDB context = new AccessDB())
            {
                List<OcUser> users = context.Users.SqlQuery("SELECT * FROM dbo.Users WHERE Team_Id IS NULL").ToList();
                LogHelper.GetLogger().Info("GetAllUsers method succeeded. Returned list of all users.");

                return users;
            }
        }

        public List<OcUser> GetAllUsers()
        {
            using (AccessDB context = new AccessDB())
            {
                List<OcUser> users = context.Users.ToList();
                LogHelper.GetLogger().Info("GetAllUsers method succeeded. Returned list of all users.");

                return users;
            }
        }

        public List<Company> GetAllCompanies()
        {
            using (AccessDB context = new AccessDB())
            {
                var result = from company in context.Companies select company;
                List<Company> companies = result.ToList();
                LogHelper.GetLogger().Info("GetAllCompanies method succeeded. Returned list of all companies.");

                return companies;
            }
        }

        public List<Team> GetAllTeams()
        {
            using (AccessDB context = new AccessDB())
            {
                List<Team> teams = context.Teams.Include("TeamLead").ToList();
                foreach (var team in teams)
                {
                    if (team.TeamLead != null)
                    {
                        team.TeamLead.Team = null;
                    }
                    List<OcUser> users = context.Users.Include("Team").ToList();
                    foreach (var developer in users)
                    {
                        if (developer.Team != null && developer.Team.Id == team.Id)
                        {
                            team.Developers.Add(developer);
                        }
                    }
                }

                LogHelper.GetLogger().Info("GetAllTeams method succeeded. Returned list of all teams.");

                return teams;
            }
        }

        public OcUser GetUser(string username)
        {
            using (AccessDB context = new AccessDB())
            {

                OcUser user = context.Users.FirstOrDefault((x) => x.Username == username);
                if (user != null)
                {
                    LogHelper.GetLogger().Info("GetUser method succeeded.");

                    return user;
                }
            }
            LogHelper.GetLogger().Info(" GetUser returned null.");

            return null;
        }

        public bool UpdateUser(OcUser user)
        {
            using (AccessDB context = new AccessDB())
            {
                var result = from i in context.Users
                             where i.Id == user.Id
                             select i;
                OcUser us = result.ToList<OcUser>().FirstOrDefault();
                if (us != null)
                {
                    if (us.Username != user.Username)
                    {
                        var res = from j in context.Users
                                  where j.Username == user.Username
                                  select j;
                        List<OcUser> users = res.ToList<OcUser>();
                        if (users.Count == 0)
                        {
                            us.UpdateProperties(user);
                            context.Entry(us).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            LogHelper.GetLogger().Info(" UpdateUser method succeeded.");

                        }
                        else
                        {
                            LogHelper.GetLogger().Info("UpdateUser method returned false.");

                            return false;
                        }

                    }
                    else
                    {
                        us.UpdateProperties(user);
                        context.Entry(us).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                    LogHelper.GetLogger().Info(" UpdateUser method succeeded. Returned true.");

                    return true;
                }
            }
            LogHelper.GetLogger().Info(" UpdateUser method returned false.");

            return false;
        }

        public List<OcProject> GetAllProjects()
        {
            using (AccessDB context = new AccessDB())
            {
                List<OcProject> projects = context.Projects.ToList();
                LogHelper.GetLogger().Info("GetAllProjects method succeeded. Returned list of projects.");

                return projects;
            }
        }

        public bool ModyfyUserStory(UserStory userStory)
        {
            using (AccessDB context = new AccessDB())
            {

                UserStory us = context.UserStories.FirstOrDefault((x) => x.Id == userStory.Id);
                if (us != null)
                {
                    us.IsUserStoryAccepted = true;
                    context.Entry(us).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool ModifyCompanyToPartner(Company company)
        {
            using (AccessDB context = new AccessDB())
            {

                Company com = context.Companies.FirstOrDefault((x) => x.Name == company.Name);
                if (com != null)
                {
                    com.State = State.CompanyState.Partner;
                    context.Entry(com).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool ChangeCompanyState(Company company, State.CompanyState state)
        {

            using (AccessDB context = new AccessDB())
            {
                Company c = context.Companies.FirstOrDefault<Company>((x) => x.Name == company.Name);
                if (c == null)
                {
                    return false;
                }
                c.State = state;
                context.Entry(c).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return true;

        }

        public bool RemoveCompany(Company company)
        {
            using (AccessDB context = new AccessDB())
            {
                Company c = context.Companies.FirstOrDefault<Company>((x) => x.Name.Equals(company.Name));
                if (c == null)
                {
                    return false;
                }
                context.Companies.Remove(c);
                context.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return true;
        }

        public bool UpdateProject(OcProject project)
        {
            using (AccessDB context = new AccessDB())
            {
                OcProject proj = context.Projects.FirstOrDefault<OcProject>((x) => x.Name == project.Name);

                if (proj != null)
                {

                    proj.UpdateProperties(project);
                    List<UserStory> userStories = context.UserStories.Where<UserStory>((x) => x.Project.Id == project.Id).ToList();
                    foreach (var us in userStories)
                    {
                        if (project.UserStories.ToList().Exists((x) => x.Id == us.Id))
                        {
                            continue;
                        }

                        context.Entry(us).State = System.Data.Entity.EntityState.Deleted;
                    }

                    foreach (var us in project.UserStories)
                    {
                        if (us.Id == 0)
                        {
                            proj.UserStories.Add(us);
                        }
                    }
                    proj.IsAccepted = project.IsAccepted;
                    proj.DevelopCompany = project.DevelopCompany;
                    context.Entry(proj).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    LogHelper.GetLogger().Info(" UpdateProject method succeeded. Returned true.");

                    return true;
                }
                LogHelper.GetLogger().Info("UpdateProject method returned false.");

                return false;
            }
        }

        public bool RemoveProject(OcProject project)
        {
            using (AccessDB context = new AccessDB())
            {
                OcProject c = context.Projects.FirstOrDefault<OcProject>((x) => x.Name.Equals(project.Name));
                if (c == null)
                {
                    return false;
                }
                context.Projects.Remove(c);
                context.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return true;
        }

        public bool RemoveUser(OcUser user)
        {
            using (AccessDB context = new AccessDB())
            {
                OcUser us = context.Users.FirstOrDefault<OcUser>((x) => x.Id == user.Id);

                if (us != null)
                {

                    context.Entry(us).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    LogHelper.GetLogger().Info(" RemoveUser method succeeded. Returned true.");

                    return true;
                }
                LogHelper.GetLogger().Info("RemoveUser method returned false.");

                return false;
            }
        }


        public List<UserStory> GetUserStoryFromProject(OcProject project)
        {
            using (AccessDB context = new AccessDB())
            {
                List<UserStory> userStories = context.UserStories.Where<UserStory>((x) => x.Project.Id == project.Id).ToList();
                LogHelper.GetLogger().Info("GetUserStoryFromProject method succeeded. Returned list of user stories.");
                return userStories;
            }
        }

        public List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory)
        {
            using (AccessDB context = new AccessDB())
            {
                List<Common.Entities.Task> tasks = context.Tasks.Where<Common.Entities.Task>((x) => x.UserStory.Id == userStory.Id).ToList();

                return tasks;
            }
        }

        public OcProject GetProjectFromUserStory(UserStory userStory)
        {
            using (AccessDB context = new AccessDB())
            {
                UserStory us = context.UserStories.Include("Project").FirstOrDefault<UserStory>((x) => x.Id == userStory.Id);
                OcProject proj = context.Entry(us).Reference("Project").CurrentValue as OcProject;
                proj.UserStories = null; //because of circular reference
                return proj;
            }
        }

        public bool UpdateUserStory(UserStory userStory)
        {
            using (AccessDB context = new AccessDB())
            {
                UserStory us = context.UserStories.FirstOrDefault<UserStory>((x) => x.Id == userStory.Id);

                if (us != null)
                {
                    us.UpdateProperties(userStory);
                    List<Common.Entities.Task> tasks = context.Tasks.Where<Common.Entities.Task>((x) => x.UserStory.Id == userStory.Id).ToList();
                    foreach (var t in tasks)
                    {
                        if (userStory.Tasks.ToList().Exists((x) => x.Id == t.Id))
                        {
                            continue;
                        }

                        context.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                    }

                    foreach (var t in userStory.Tasks)
                    {
                        if (t.Id == 0)
                        {
                            us.Tasks.Add(t);
                        }
                    }
                    context.Entry(us).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}

