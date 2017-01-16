using Client.View;
using Common;
using Common.Entities;
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
using ServiceContract;
using System.Threading;
using System.Windows.Data;


namespace Client.ViewModel
{



    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static object _syncLock = new object();
        private IHiringContract proxy;
        public MainWindowViewModel()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("NMMS")) + "NMMS/Common";
            EditIcon = new BitmapImage(new Uri(path + "/Images/edit.png"));
            RemoveIcon = new BitmapImage(new Uri(path + "/Images/delete.png"));
            Proxy = App.Proxy;

        }

        public void UpdateData()
        {
            lock (_syncLock)
            {
                switch (CurrentState)
                {
                    case Common.Entities.WindowState.LOGIN:
                        break;
                    case Common.Entities.WindowState.EMPLOYEES:
                        //ShowEmployeesCommand.Execute(null);
                        break;
                    case Common.Entities.WindowState.COMPANIES:
                        DisplayCompaniesCommand.Execute(null);
                        break;
                    case Common.Entities.WindowState.PROJECTS:
                        DisplayProjectsCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            }
        }
        public MainWindowViewModel(string test)
        {

        }
        #region Fields



        private string loggedUsername = "";
        private ObservableCollection<Company> partnerCompanies = new AsyncObservableCollection<Company>();
        private ObservableCollection<Company> nonPartnerCompanies = new AsyncObservableCollection<Company>();
        private ObservableCollection<Project> projects = new AsyncObservableCollection<Project>();
        private ObservableCollection<User> allEmployees = new AsyncObservableCollection<User>();
        private ObservableCollection<Project> acceptedProjects = new AsyncObservableCollection<Project>();
        private ObservableCollection<Project> nonSentProjects = new AsyncObservableCollection<Project>();

        private ObservableCollection<UserStory> answerToUS = new AsyncObservableCollection<UserStory>();

        public ObservableCollection<UserStory> AnswerToUS
        {
            get { return answerToUS; }
            set { answerToUS = value; }
        }




        private Common.Entities.WindowState currentState = Common.Entities.WindowState.LOGIN;
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
        private ICommand displayCompaniesCommand;
        private ICommand sendCompanyRequestCommand;
        private ICommand editUserProfileCommand;
        private ICommand deleteUserCommand;
        private ICommand addUserCommand;
        private ICommand sendProjectRequestCommand;
        private ICommand acceptUSCommand;
        private ICommand rejectUSCommand;

        #endregion Fields

        #region Properties

        public BitmapImage EditIcon { get; set; }
        public BitmapImage RemoveIcon { get; set; }

        public ObservableCollection<User> AllEmployees
        {
            get { return allEmployees; }
            set { allEmployees = value; }
        }

        public Common.Entities.WindowState CurrentState
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

        public ObservableCollection<Project> AcceptedProjects
        {
            get { return acceptedProjects; }
            set { acceptedProjects = value; }
        }

        public ObservableCollection<Project> NonSentProjects
        {
            get { return nonSentProjects; }
            set { nonSentProjects = value; }
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
                return editProjectCommand ?? (editProjectCommand = new RelayCommand((param) => this.EditProject(param as Project)));
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

        public ICommand SendCompanyRequestCommand
        {
            get
            {
                return sendCompanyRequestCommand ?? (sendCompanyRequestCommand = new RelayCommand(param => this.SendCompanyRequest(param)));
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

        public ICommand SendProjectRequestCommand
        {
            get
            {
                return sendProjectRequestCommand ?? (sendProjectRequestCommand = new RelayCommand(param => this.SendProjectRequest(param)));
            }

        }

        public ICommand AcceptUSCommand
        {
            get
            {
                return acceptUSCommand ?? (acceptUSCommand = new RelayCommand(param => this.AcceptUS(param)));
            }

        }

        public ICommand RejectUSCommand
        {
            get
            {
                return rejectUSCommand ?? (rejectUSCommand = new RelayCommand(param => this.RejectUS(param)));
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

            }

            if (parameters[0] == null || parameters[1] == null)
            {
                LogHelper.GetLogger().Error("Some of command parameters has NULL value.");
                return;
            }

            string username = parameters[0].ToString();
            string pass = parameters[1].ToString();

            bool success = Proxy.LogIn(username, pass);

            if (success)
            {
                LoggedUser = Proxy.GetUser(username);
                CurrentState = Common.Entities.WindowState.COMPANIES;
            }

        }

        private void ShowProfile()
        {
            //TODO
            LogHelper.GetLogger().Info("ShowProfile called.");

            ProfileDialog profileDialog = new ProfileDialog(LoggedUser);
            var res = profileDialog.ShowDialog();
            if (res == true)
            {
                if (profileDialog.Tag != null)
                {
                    LoggedUsername = profileDialog.Tag.ToString();
                }
            }
        }

        private void ShowEmployees()
        {
            LogHelper.GetLogger().Info("ShowEmployees called.");
            try
            {

                AllEmployees.Clear();

                List<User> result = Proxy.GetAllUsers();

                foreach (var user in result)
                {
                    AllEmployees.Add(user);
                }

                LogHelper.GetLogger().Info("Get employees is done successfuly");
            }
            catch (Exception e)
            {

                LogHelper.GetLogger().Error("There is no employees", e);
            }


            CurrentState = Common.Entities.WindowState.EMPLOYEES;
        }

        private void ShowCompanies()
        {
            LogHelper.GetLogger().Info("ShowCompanies called.");

            CurrentState = Common.Entities.WindowState.COMPANIES;
        }

        private void ShowProjects()
        {
            LogHelper.GetLogger().Info("ShowProjects called.");

            CurrentState = Common.Entities.WindowState.PROJECTS;
        }

        private void LogOut(object param)
        {
            LogHelper.GetLogger().Info("LogOut called.");

            bool success = Proxy.LogOut(LoggedUser.Username);

            if (success)
            {
                LoggedUser.Username = "";
                LoggedUser = null;
                CurrentState = Common.Entities.WindowState.LOGIN;
            }
            else
            {
                MessageBox.Show("Error while loggout");
                LogHelper.GetLogger().Error("Error while loggout");

            }
        }

        private void NewProject()
        {
            LogHelper.GetLogger().Info("NewProject called.");

            ProjectViewDialog projectDialog = new ProjectViewDialog();
            projectDialog.ShowDialog();
        }

        private void EditProject(Project project)
        {
            LogHelper.GetLogger().Info("EditProject called.");

            ProjectViewDialog projectDialog = new ProjectViewDialog(project);
            projectDialog.ShowDialog();
        }

        private void FetchCompanies()
        {

            LogHelper.GetLogger().Info("FetchCompanies called.");
            FetchProjects();
            try
            {

                PartnerCompanies.Clear();
                NonPartnerCompanies.Clear();


                List<Company> result = Proxy.GetAllCompanies();

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

                LogHelper.GetLogger().Info("Get companies is done successfuly");
            }
            catch (Exception e)
            {

                LogHelper.GetLogger().Error("There is no companies", e);
            }

        }

        private void FetchProjects()
        {
            LogHelper.GetLogger().Info("FetchProjects called.");

            List<Project> result = Proxy.GetAllProjects();
            Projects.Clear();
            AcceptedProjects.Clear();
            NonSentProjects.Clear();
            if (result != null)
            {
                foreach (var proj in result)
                {
                    Projects.Add(proj);
                    if (proj.IsAccepted)
                    {
                        AcceptedProjects.Add(proj);
                    }
                    if (!proj.IsProjectRequestSent)
                    {
                        NonSentProjects.Add(proj);
                    }
                }
            }
            FetchUserStories();
            /*Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Approved" });

            Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Disapproved" });
            */
        }

        void FetchUserStories()
         {
             AnswerToUS.Clear();
             List<UserStory> result = proxy.GetAllUserStories();
             if (result != null)
             {
                 foreach (UserStory us in result)
                 {
                     if (us.IsUserStorySent)
                         AnswerToUS.Add(us);
                 }
             }
         }

        private void SendCompanyRequest(object param)
        {
            LogHelper.GetLogger().Info("SendCompanyRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("SendCompanyRequest Command parameters has NULL value.");
                return;
            }
            Company company = param as Company;

            if (company == null)
            {
                LogHelper.GetLogger().Warn("SendCompanyRequest Command parameter has Wrong type value.");
                return;
            }

            bool success = Proxy.SendRequest(company);
            FetchCompanies();

        }

        private void EditUserProfile(object param)
        {
            LogHelper.GetLogger().Info("ShowProfile called.");

            var user = param as User;
            if (user == null)
            {
                LogHelper.GetLogger().Warn("ShowProfile params NULL.");
            }
            LogHelper.GetLogger().Info("ShowProfile params ok.");

            ProfileDialog profileDialog = new ProfileDialog(LoggedUser);
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

            var user = param as User;
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
        private void SendProjectRequest(object param)
        {
            LogHelper.GetLogger().Info("SendProjectRequest called.");

            if (param == null)
            {
                LogHelper.GetLogger().Warn("SendProjectRequest Command parameters has NULL value.");
                return;
            }
            object[] parameters = param as object[];
            Company company = parameters[0] as Company;
            Project project = parameters[1] as Project;

            if (company == null || project == null)
            {
                LogHelper.GetLogger().Warn("SendCompanyRequest Command parameter has Wrong type value.");
                return;
            }

            bool success = Proxy.SendProject(company, project);
            FetchCompanies();


        }

        private void AcceptUS(object param)
         {
             LogHelper.GetLogger().Info("AcceptUS called.");
 
             if (param == null)
             {
                 LogHelper.GetLogger().Warn("[AcceptUS] Command parameters has NULL value");
             }
             UserStory us = param as UserStory;
             Project proj = proxy.GetProjectFromUserStory(us);
             Company company = new Company();
             // us.Project = new Project();
             us.IsUserStoryAccepted = true;
             us.IsUserStorySent = false;
             bool success = proxy.AnswerToUserStory(company, proj, us);
             proxy.UpdateUserStory(us);
             FetchProjects();
         }
 
         private void RejectUS(object param)
         {
             LogHelper.GetLogger().Info("RejectUS called.");
 
             if (param == null)
             {
                 LogHelper.GetLogger().Warn("[RejectUS] Command parameters has NULL value");
             }
             UserStory us = param as UserStory;
             //     us.Project = new Project();
             Project proj = proxy.GetProjectFromUserStory(us);
             Company company = new Company();
             us.IsUserStorySent = false;
             us.IsUserStoryAccepted = false;
             bool success = proxy.AnswerToUserStory(company, proj, us);
             proxy.UpdateUserStory(us);
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

        public User LoggedUser
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

        public IHiringContract Proxy
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
    }

}
