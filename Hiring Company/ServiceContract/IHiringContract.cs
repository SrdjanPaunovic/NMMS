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
    public interface IHiringContract
    {
        [OperationContract]
        bool LogIn(string username, string password);

        [OperationContract]
        bool LogOut(string username);

        [OperationContract]
        User GetUser(string username);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool UpdateUser(User user);

        [OperationContract]
        bool RemoveUser(User user);

        [OperationContract]
        bool AddProject(Project project);

        [OperationContract]
        bool UpdateProject(Project project);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        List<Project> GetAllProjects();

        [OperationContract]
        bool SendRequest(Company company);

        [OperationContract]
        List<UserStory> GetUserStoryFromProject(Project project);

        [OperationContract]
        List<Common.Entities.Task> GetTasksFromUserStory(UserStory userStory);

        [OperationContract]
        Project GetProjectFromUserStory(UserStory userStory);

        [OperationContract]
        bool UpdateUserStory(UserStory userStory);

        [OperationContract]
        List<Company> GetAllCompanies();

        [OperationContract]
        bool SendProject(Company company, Project project);

        [OperationContract]
        bool AnswerToUserStory(Company company, Project project, UserStory userStory);

        [OperationContract]
        bool ModifyCompany(Company company);

        [OperationContract]
        bool RemoveUS(UserStory userStory);

        [OperationContract]
        List<UserStory> GetAllUserStories();


        [OperationContract]
        bool RemoveAllCompanies();
    }
}
