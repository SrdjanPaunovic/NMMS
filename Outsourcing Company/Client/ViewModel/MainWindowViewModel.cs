using Client.View;
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

        public IOutsourcingContract proxy;

        public MainWindowViewModel()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("NMMS")) + "NMMS/Common";
            EditIcon = new BitmapImage(new Uri(path + "/Images/edit.png"));
            RemoveIcon = new BitmapImage(new Uri(path + "/Images/delete.png"));
            proxy = ((App)App.Current).Proxy;
        }
        public MainWindowViewModel(string test)
        {

        }

        #region Fields
        private string loggedUsername = "";
        private ObservableCollection<Company> partnerCompanies = new ObservableCollection<Company>();
        private ObservableCollection<Company> nonPartnerCompanies = new ObservableCollection<Company>();
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private ObservableCollection<Team> teams = new ObservableCollection<Team>();
        private ObservableCollection<OcProject> acceptedProjects = new ObservableCollection<OcProject>();
        private ObservableCollection<OcUser> allEmployees = new ObservableCollection<OcUser>();



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

        #endregion Fields

        #region Properties
        public BitmapImage EditIcon { get; set; }
        public BitmapImage RemoveIcon { get; set; }

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

        public string LoggedUsername
        {
            get
            {
                return loggedUsername;
            }

            set
            {
                loggedUsername = value;
                OnPropertyChanged("LoggedUsername");
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

        public ICommand AcceptProjectRequestCommand
        {
            get
            {
                return acceptProjectRequestCommand ?? (acceptCompanyRequest = new RelayCommand((param) => this.AcceptProjectRequest(param)));
            }

        }

        public ICommand RejectProjectRequestCommand
        {
            get
            {
                return rejectProjectRequestCommand ?? (rejectCompanyRequestCommand = new RelayCommand((param) => this.RejectProjectRequest(param)));
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
        #endregion Properties

        #region Methods
        private void LoginClick(object param)
        {
            LogHelper.GetLogger().Info("LoginClick occurred.");

            object[] parameters = param as object[];

            if (parameters == null)
            {
                LogHelper.GetLogger().Error("Command parameters has NULL value.");

                throw new Exception("[LoginCommnad] Command parameters has NULL value");
            }

            string username = parameters[0].ToString();
            string pass = parameters[1].ToString();

            bool success = proxy.LogIn(username, pass);

            if (success)
            {
                LoggedUsername = username;
                CurrentState = WindowState.PROJECTS;
            }

        }

        private void ShowProfile()
        {
            LogHelper.GetLogger().Info("ShowProfile called.");

            ProfileDialog profileDialog = new ProfileDialog(LoggedUsername);
            profileDialog.ShowDialog();
        }

        private void ShowEmployees()
        {
            LogHelper.GetLogger().Info("ShowEmployees called.");

            AllEmployees.Clear();

            List<OcUser> result = proxy.GetAllUsers();
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

            bool success = proxy.LogOut(LoggedUsername);

            if (success)
            {
                LoggedUsername = "";
                CurrentState = WindowState.LOGIN;
            }
            else
            {
                MessageBox.Show("Error while loggout");
            }

        }

        void NewProject()
        {
            ProjectViewDialog projectDialog = new ProjectViewDialog();
            projectDialog.ShowDialog();
        }

        void EditProject(OcProject project)
        {
            ProjectViewDialog projectDialog = new ProjectViewDialog(project);
            projectDialog.ShowDialog();
        }

        void FetchCompanies()
        {

            List<Company> result = proxy.GetAllCompanies();
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

        void FetchProjects()
        {
            List<OcProject> result = proxy.GetAllProjects();
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

        void FetchAcceptedProjects()
        {
            FetchProjects();

            List<OcProject> result = proxy.GetAllProjects();
            AcceptedProjects.Clear();
            if (result != null)
            {
                foreach (var proj in result)
                {
                    if (proj.IsAccepted)
                        AcceptedProjects.Add(proj);
                }
            }

        }


        void FetchTeams()
        {
            List<Team> result = proxy.GetAllTeams();
            Teams.Clear();
            foreach (var proj in result)
            {
                Teams.Add(proj);
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
            bool success = proxy.AnswerToRequest(company);
            proxy.ModifyCompany(company);
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
            bool success = proxy.AnswerToRequest(company);
            proxy.RemoveCompany(company);
            FetchCompanies();

        }

        private void NewTeam()
        {
            LogHelper.GetLogger().Info("NewTeam called.");
            TeamViewDialog teamDialog = new TeamViewDialog();
            teamDialog.ShowDialog();
        }

        private void AcceptProjectRequest(object param)
        {
            LogHelper.GetLogger().Info("AcceptProjectRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("[AcceptProjectRequestCommnad] Command parameters has NULL value");
            }
            OcProject OcProject = param as OcProject;
            Project project = new Project(OcProject);
            OcProject.IsAccepted = true;

            Company company = new Company(project.HiringCompany);
            project.IsAccepted = true;
            bool success = proxy.AnswerToProject(company, project);
            proxy.UpdateProject(OcProject);
            FetchAcceptedProjects();
        }


        private void RejectProjectRequest(object param)
        {
            LogHelper.GetLogger().Info("RejectProjectRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("[RejectProjectRequestCommnad] Command parameters has NULL value");
            }
            //Company company = param as Company;
            OcProject OcProject = param as OcProject;
            Project project = new Project(OcProject);
            OcProject.IsAccepted = false;

            Company company = new Company(project.HiringCompany);
            project.IsAccepted = false;
            bool success = proxy.AnswerToProject(company, project);
            proxy.RemoveProject(OcProject);
            FetchAcceptedProjects();

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

            ProfileDialog profileDialog = new ProfileDialog(user.Username);
            var res = profileDialog.ShowDialog();
            if (res == true)
            {
                if (profileDialog.Tag != null)
                {
                    LoggedUsername = profileDialog.Tag.ToString();
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

            success = proxy.RemoveUser(user);
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

