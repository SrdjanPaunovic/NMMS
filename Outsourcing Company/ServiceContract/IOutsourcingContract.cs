﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Entities;

namespace ServiceContract
{
    [ServiceContract]
    public interface IOutsourcingContract
    {
        [OperationContract]
        bool AddUser(OcUser user);

        [OperationContract]
        bool AddCompany(Company company);

        [OperationContract]
        bool AddProject(OcProject project);

        //[OperationContract]
        //bool AddUserStory(UserStory userStory);

        //[OperationContract]
        //bool AddTask(Common.Entities.Task task);

        [OperationContract]
        bool RemoveUser(OcUser user);

        [OperationContract]
        bool AddTeam(Team team);

        [OperationContract]
        bool UpdateTeam(Team team);

        [OperationContract]
        bool LogIn(string username, string password);

        [OperationContract]
        bool LogOut(string username);

        [OperationContract]
        bool AnswerToRequest(Company company);

        [OperationContract]
        List<Company> GetAllCompanies();

        [OperationContract]
        List<OcProject> GetAllProjects();

        [OperationContract]
        List<OcUser> GetAllUsers();

        [OperationContract]
        List<OcUser> GetAllUsersWithoutTeam();

        [OperationContract]
        OcUser GetUser(string username);

        [OperationContract]
        bool UpdateUser(OcUser user);

        [OperationContract]
        bool ModifyCompany(Company company);

        [OperationContract]
        bool ChangeCompanyState(Company company, State.CompanyState state);

        [OperationContract]
        bool RemoveCompany(Company company);

        [OperationContract]
        bool SendUserStory(Company company, UserStory userStrory, Project project);

        [OperationContract]
        bool AnswerToProject(Company company, Project project);

        [OperationContract]
        List<Team> GetAllTeams();

        [OperationContract]
        bool UpdateProject(OcProject project);

        [OperationContract]
        bool RemoveProject(OcProject project);

        [OperationContract]
        List<UserStory> GetUserStoryFromProject(OcProject project);

        [OperationContract]
        List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory);

        [OperationContract]
        OcProject GetProjectFromUserStory(UserStory userStory);

        [OperationContract]
        bool UpdateUserStory(UserStory userStory);

        [OperationContract]
        List<UserStory> GetAllUserStories();
    }
}
