﻿using Client.View;
using Common;
using Common.Entities;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public enum WindowState
        {
            LOGIN,
            EMPLOYEES,
            COMPANIES,
            PROJECTS,
            TEAMS
        }

        private IOutsourcingContract proxy = App.Proxy;

        public MainWindowViewModel()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("NMMS")) + "NMMS/Common";
            EditIcon = new BitmapImage(new Uri(path + "/Images/edit.png"));
            RemoveIcon = new BitmapImage(new Uri(path + "/Images/delete.png"));

            ViewIcon = new BitmapImage(new Uri(path + "/Images/viewIcon.png"));


        }

        public void UpdateData()
        {
            switch (CurrentState)
            {
                case WindowState.LOGIN:
                    break;
                case WindowState.EMPLOYEES:
                    //ShowEmployeesCommand.Execute(null);
                    break;
                case WindowState.COMPANIES:
                    DisplayCompaniesCommand.Execute(null);
                    break;
                case WindowState.PROJECTS:
                    DisplayProjectsCommand.Execute(null);
                    break;
                case WindowState.TEAMS:
                    DisplayTeamsCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }
        public MainWindowViewModel(string test)
        {

        }

        #region Fields
        private ObservableCollection<Company> partnerCompanies = new AsyncObservableCollection<Company>();
        private ObservableCollection<Company> nonPartnerCompanies = new AsyncObservableCollection<Company>();
        private ObservableCollection<Project> projects = new AsyncObservableCollection<Project>();
        private ObservableCollection<Team> teams = new AsyncObservableCollection<Team>();
        private ObservableCollection<OcProject> acceptedProjects = new AsyncObservableCollection<OcProject>();
        private ObservableCollection<OcUser> allEmployees = new AsyncObservableCollection<OcUser>();
        private ObservableCollection<UserStory> nonSentUS = new AsyncObservableCollection<UserStory>();





        private WindowState currentState = WindowState.LOGIN;
        private NetTcpBinding netTcpBinding = new NetTcpBinding();
        //commands
        private ICommand loginCommand;
        private ICommand logOutCommand;
        private ICommand displayProjectsCommand;
        private ICommand newProjectCommand;
        private ICommand editProjectCommand;
        private ICommand showProfileCommand;
        private ICommand showEmployeesCommand;
        private ICommand showCompaniesCommand;
        private ICommand showProjectsCommand;
        private ICommand showTeamsCommand;
        private ICommand displayCompaniesCommand;
        private ICommand acceptCompanyRequest;
        private ICommand rejectCompanyRequestCommand;
        private ICommand displayTeamsCommand;
        private ICommand newTeamCommand;
        private ICommand acceptProjectRequestCommand;
        private ICommand rejectProjectRequestCommand;
        private ICommand editUserProfileCommand;
        private ICommand deleteUserCommand;
        private ICommand addUserCommand;
        private ICommand userStoryRequestCommand;
        private ICommand editTeamCommand;

        #endregion Fields

        #region Properties
        public BitmapImage EditIcon { get; set; }
        public BitmapImage ViewIcon { get; set; }
        public BitmapImage RemoveIcon { get; set; }

        public OcUser LoggedUser
        {
            get
            {
                return App.LoggedUser;
            }

            set
            {
                App.LoggedUser = value;
                OnPropertyChanged("LoggedUser");

            }
        }


        public ObservableCollection<OcUser> AllEmployees
        {
            get { return allEmployees; }
            set { allEmployees = value; }
        }
        public WindowState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                OnPropertyChanged("CurrentState");
            }
        }

        public ObservableCollection<Company> PartnerCompanies
        {
            get { return partnerCompanies; }
            set { partnerCompanies = value; }
        }

        public ObservableCollection<Company> NonPartnerCompanies
        {
            get { return nonPartnerCompanies; }
            set { nonPartnerCompanies = value; }
        }

        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        public ObservableCollection<Team> Teams
        {
            get { return teams; }
            set { teams = value; }
        }

        public ObservableCollection<OcProject> AcceptedProjects
        {
            get { return acceptedProjects; }
            set { acceptedProjects = value; }
        }

        public ObservableCollection<UserStory> NonSentUS
        {
            get { return nonSentUS; }
            set { nonSentUS = value; }
        }

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(param => this.LoginClick(param)));
            }
        }

        public ICommand ShowProfileCommand
        {
            get
            {
                return showProfileCommand ?? (showProfileCommand = new RelayCommand(param => this.ShowProfile()));
            }
        }

        public ICommand ShowEmployeesCommand
        {
            get
            {
                return showEmployeesCommand ?? (showEmployeesCommand = new RelayCommand(param => this.ShowEmployees()));
            }
        }

        public ICommand ShowCompaniesCommand
        {
            get
            {
                return showCompaniesCommand ?? (showCompaniesCommand = new RelayCommand(param => this.ShowCompanies()));
            }
        }

        public ICommand ShowProjectsCommand
        {
            get
            {
                return showProjectsCommand ?? (showProjectsCommand = new RelayCommand(param => this.ShowProjects()));
            }
        }

        public ICommand ShowTeamsCommand
        {
            get
            {
                return showTeamsCommand ?? (showTeamsCommand = new RelayCommand(param => this.ShowTeams()));
            }
        }

        public ICommand DisplayProjectsCommand
        {
            get
            {
                return displayProjectsCommand ?? (displayProjectsCommand = new RelayCommand((param) => this.FetchProjects()));
            }
        }

        public ICommand NewProjectCommand
        {
            get
            {
                return newProjectCommand ?? (newProjectCommand = new RelayCommand((param) => this.NewProject()));
            }
        }

        public ICommand EditProjectCommand
        {
            get
            {
                return editProjectCommand ?? (editProjectCommand = new RelayCommand((param) => this.EditProject(param as OcProject)));
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return logOutCommand ?? (logOutCommand = new RelayCommand((param) => this.LogOut(param)));
            }

        }

        public ICommand DisplayCompaniesCommand
        {
            get
            {
                return displayCompaniesCommand ?? (displayCompaniesCommand = new RelayCommand((param) => this.FetchCompanies()));
            }
        }

        public ICommand DisplayTeamsCommand
        {
            get
            {
                return displayTeamsCommand ?? (displayTeamsCommand = new RelayCommand((param) => this.FetchTeams()));
            }
        }

        public ICommand AcceptCompanyRequestCommand
        {
            get
            {
                return acceptCompanyRequest ?? (acceptCompanyRequest = new RelayCommand((param) => this.AcceptRequest(param)));
            }

        }

        public ICommand RejectCompanyRequestCommand
        {
            get
            {
                return rejectCompanyRequestCommand ?? (rejectCompanyRequestCommand = new RelayCommand((param) => this.RejectRequest(param)));
            }

        }


        public ICommand NewTeamCommand
        {
            get
            {
                return newTeamCommand ?? (newTeamCommand = new RelayCommand((param) => this.NewTeam()));
            }
        }

        public ICommand EditTeamCommand
        {
            get
            {
                return editTeamCommand ?? (editTeamCommand = new RelayCommand((param) => this.EditTeam(param)));
            }
        }

        public ICommand AcceptProjectRequestCommand
        {
            get
            {
                return acceptProjectRequestCommand ?? (acceptProjectRequestCommand = new RelayCommand((param) => this.AcceptProjectRequest(param)));
            }

        }

        public ICommand RejectProjectRequestCommand
        {
            get
            {
                return rejectProjectRequestCommand ?? (rejectProjectRequestCommand = new RelayCommand((param) => this.RejectProjectRequest(param)));
            }

        }

        public ICommand EditUserProfileCommand
        {
            get
            {
                return editUserProfileCommand ?? (editUserProfileCommand = new RelayCommand(param => this.EditUserProfile(param)));
            }
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ?? (deleteUserCommand = new RelayCommand(param => this.DeleteUser(param)));
            }
        }

        public ICommand AddUserCommand
        {
            get
            {
                return addUserCommand ?? (addUserCommand = new RelayCommand(param => this.AddUser(param)));
            }
        }

        public ICommand UserStoryRequestCommand
        {
            get
            {
                return userStoryRequestCommand ?? (userStoryRequestCommand = new RelayCommand(param => this.SendUserStory(param)));
            }

        }

        public IOutsourcingContract Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }
        #endregion Properties

        #region Methods
        private void LoginClick(object param)
        {
            LogHelper.GetLogger().Info("LoginClick occurred.");

            object[] parameters = param as object[];

            if (parameters == null)
            {
                LogHelper.GetLogger().Error("Command parameters has NULL value.");

                throw new ArgumentNullException();

                //throw new Exception("[LoginCommnad] Command parameters has NULL value");
            }

            string username = parameters[0].ToString();
            string pass = parameters[1].ToString();

            bool success = Proxy.LogIn(username, pass);

            if (success)
            {
                LoggedUser = Proxy.GetUser(username);
                CurrentState = WindowState.PROJECTS;
            }

        }

        private void ShowProfile()
        {
            LogHelper.GetLogger().Info("ShowProfile called.");

            EditUserProfile(LoggedUser);
        }

        private void ShowEmployees()
        {
            LogHelper.GetLogger().Info("ShowEmployees called.");

            AllEmployees.Clear();

            List<OcUser> result = Proxy.GetAllUsers();
            if (result != null)
            {
                foreach (var user in result)
                {
                    AllEmployees.Add(user);
                }

                LogHelper.GetLogger().Info("Get employees is done successfuly");
            }
            else
            {
                LogHelper.GetLogger().Info("Get employees failed");
            }

            CurrentState = WindowState.EMPLOYEES;
        }

        private void ShowCompanies()
        {
            LogHelper.GetLogger().Info("ShowCompanies called.");

            CurrentState = WindowState.COMPANIES;
        }

        private void ShowProjects()
        {
            LogHelper.GetLogger().Info("ShowProjects called.");

            CurrentState = WindowState.PROJECTS;
        }

        private void ShowTeams()
        {
            LogHelper.GetLogger().Info("ShowTeams called.");
            CurrentState = WindowState.TEAMS;
        }

        private void LogOut(object param)
        {

            bool success = Proxy.LogOut(LoggedUser.Username);

            if (success)
            {
                LoggedUser.Username = "";
                LoggedUser = null;
                CurrentState = WindowState.LOGIN;
            }
            else
            {
                MessageBox.Show("Error while loggout");
            }

        }

        private void NewProject()
        {
            ProjectViewDialog projectDialog = new ProjectViewDialog();
            projectDialog.ShowDialog();
        }

        private void EditProject(OcProject project)
        {
            ProjectViewDialog projectDialog = new ProjectViewDialog(project);
            projectDialog.ShowDialog();
        }

        private void FetchCompanies()
        {

            List<Company> result = Proxy.GetAllCompanies();
            PartnerCompanies.Clear();
            NonPartnerCompanies.Clear();

            if (result != null)
            {
                foreach (Company company in result)
                {
                    if (company.State == State.CompanyState.NoPartner)
                    {
                        NonPartnerCompanies.Add(company);
                    }
                    else
                    {
                        PartnerCompanies.Add(company);
                    }
                }

                LogHelper.GetLogger().Info("There are some companies ready to cooperate");
            }
            else
            {
                LogHelper.GetLogger().Warn("GetAllCompanies, return NULL");
            }
        }

        private void FetchProjects()
        {
            FetchAcceptedProjects();
            List<OcProject> result = Proxy.GetAllProjects();
            Projects.Clear();
            acceptedProjects.Clear();

            if (result != null)
            {
                foreach (var proj in result)
                {
                    if (proj.IsAccepted)
                    {
                        acceptedProjects.Add(proj);
                    }
                    else
                    {
                        Projects.Add(proj);
                    }
                }
            }

        }
        private void FatchUserStories()
        {
            NonSentUS.Clear();
            List<UserStory> result = Proxy.GetAllUserStories();
            if (result != null)
            {
                foreach (UserStory us in result)
                {
                    if (us.IsUserStorySent)
                    {
                        NonSentUS.Add(us);
                    }
                }
            }

        }

        private void FetchAcceptedProjects()
        {
            List<OcProject> result = Proxy.GetAllProjects();
            AcceptedProjects.Clear();
            if (result != null)
            {
                foreach (OcProject proj in result)
                {

                    if (proj.IsAccepted)
                    {
                        AcceptedProjects.Add(proj);
                    }
                }
            }
            FetchUserStories();

        }

        private void FetchUserStories()
        {
            NonSentUS.Clear();
            List<UserStory> result = Proxy.GetAllUserStories();
            if (result != null)
            {
                foreach (UserStory us in result)
                {
                    if (!us.IsUserStorySent)
                    {
                        NonSentUS.Add(us);
                    }
                }
            }
        }


        private void FetchTeams()
        {
            Teams.Clear();
            List<Team> result = Proxy.GetAllTeams();
            if (result != null)
            {
                foreach (var proj in result)
                {
                    Teams.Add(proj);
                }
            }
        }

        private void AcceptRequest(object param)
        {
            LogHelper.GetLogger().Info("AcceptRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("[AcceptRequestCommnad] Command parameters has NULL value");
            }
            Company company = param as Company;

            company.State = State.CompanyState.Partner;
            bool success = Proxy.AnswerToRequest(company);
            Proxy.ModifyCompany(company);
            FetchCompanies();
        }

        private void RejectRequest(object param)
        {
            LogHelper.GetLogger().Info("RejectRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("[RejectRequestCommnad] Command parameters has NULL value");
            }
            Company company = param as Company;

            company.State = State.CompanyState.NoPartner;
            bool success = Proxy.AnswerToRequest(company);
            Proxy.RemoveCompany(company);
            FetchCompanies();

        }

        private void NewTeam()
        {
            LogHelper.GetLogger().Info("NewTeam called.");
            TeamViewDialog teamDialog = new TeamViewDialog();
            teamDialog.ShowDialog();
        }

        private void EditTeam(object param)
        {
            LogHelper.GetLogger().Info("EditTeam called.");

            var team = param as Team;

            if (team != null)
            {
                TeamViewDialog teamDialog = new TeamViewDialog(team);
                teamDialog.ShowDialog();
            }
            else
            {
                LogHelper.GetLogger().Warn("[EditTeam] param = NULL");

            }

        }


        private void AcceptProjectRequest(object param)
        {
            LogHelper.GetLogger().Info("AcceptProjectRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("[AcceptProjectRequestCommnad] Command parameters has NULL value");
            }
            OcProject ocProject = param as OcProject;
            Project project = new Project(ocProject);
            ocProject.IsAccepted = true;

            Company company = new Company(project.HiringCompany);
            project.IsAccepted = true;
            project.IsProjectRequestSent = true;
            bool success = Proxy.AnswerToProject(company, project);
            Proxy.UpdateProject(ocProject);
            FetchProjects();
        }


        private void RejectProjectRequest(object param)
        {
            LogHelper.GetLogger().Info("RejectProjectRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("[RejectProjectRequestCommnad] Command parameters has NULL value");
            }
            //Company company = param as Company;
            OcProject ocProject = param as OcProject;
            Project project = new Project(ocProject);
            ocProject.IsAccepted = false;

            Company company = new Company(project.HiringCompany);
            project.IsAccepted = false;
            project.IsProjectRequestSent = false;
            bool success = Proxy.AnswerToProject(company, project);
            Proxy.RemoveProject(ocProject);
            FetchProjects();

        }

        private void EditUserProfile(object param)
        {
            LogHelper.GetLogger().Info("ShowProfile called.");

            var user = param as OcUser;
            if (user == null)
            {
                LogHelper.GetLogger().Warn("ShowProfile params NULL.");
            }
            LogHelper.GetLogger().Info("ShowProfile params ok.");


            ProfileDialog profileDialog = new ProfileDialog(user);
            var res = profileDialog.ShowDialog();
            if (res == true)
            {
                if (profileDialog.Tag != null)
                {
                    LoggedUser = profileDialog.Tag as OcUser;
                }
            }
        }

        private void DeleteUser(object param)
        {

            LogHelper.GetLogger().Info("DeleteUser called.");

            var user = param as OcUser;
            if (user == null)
            {
                LogHelper.GetLogger().Warn("DeleteUser params NULL.");
            }
            LogHelper.GetLogger().Info("DeleteUser params ok.");


            bool success = false;

            success = Proxy.RemoveUser(user);
            if (success)
            {
                LogHelper.GetLogger().Info("DeleteUser is done successfuly");
                ShowEmployees();
            }
            else
            {
                LogHelper.GetLogger().Info("DeleteUser failed");

            }

        }

        private void AddUser(object param)
        {

            LogHelper.GetLogger().Info("AddUser called.");

            ProfileDialog profileDialog = new ProfileDialog();
            var res = profileDialog.ShowDialog();
            if (res == true)
            {
                LogHelper.GetLogger().Info("User successfully added.");
            }


        }

        private void SendUserStory(object param)
        {
            LogHelper.GetLogger().Info("SendUsRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("SendUsRequest Command parameters has NULL value.");
                return;
            }
            UserStory userStory = param as UserStory;
            userStory.IsUserStorySent = true;
            OcProject proj = Proxy.GetProjectFromUserStory(userStory);
            Project p = new Project(proj);
            Company comp = new Company(p.HiringCompany);
            if (userStory == null)
            {
                LogHelper.GetLogger().Warn("SendUsRequest Command parameter has Wrong type value.");
                return;
            }

            bool success = Proxy.SendUserStory(comp, userStory, p);
            Proxy.UpdateUserStory(userStory);
            FetchProjects();
        }
        #endregion Methods

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}

