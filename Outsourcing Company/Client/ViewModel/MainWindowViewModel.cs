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



        #region Fields
        private string loggedUsername = "";
        private ObservableCollection<Company> partnerCompanies = new ObservableCollection<Company>();
        private ObservableCollection<Company> nonPartnerCompanies = new ObservableCollection<Company>();
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private ObservableCollection<Team> teams = new ObservableCollection<Team>();

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

        #endregion Fields

        #region Properties
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
            string pass = parameters[0].ToString();

            //using(HiringClientProxy proxy = new HiringClientProxy(netTcpBinding, ((App)App.Current).HostAddress))
            using (OutSClientProxy proxy = ((App)App.Current).Proxy)
            {
                bool success = proxy.LogIn(username, pass);

                if (success)
                {
                    LoggedUsername = username;
                    CurrentState = WindowState.PROJECTS;
                }
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
            using (OutSClientProxy proxy = ((App)App.Current).Proxy)
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
        }

        void NewProject()
        {
            ProjectViewDialog projectDialog = new ProjectViewDialog();
            projectDialog.ShowDialog();
        }

        void EditProject(Project project)
        {
            ProjectViewDialog projectDialog = new ProjectViewDialog(project);
            projectDialog.ShowDialog();
        }

        void FetchCompanies()
        {
            try
            {
                using (OutSClientProxy proxy = ((App)Application.Current).Proxy)
                {
                    List<Company> result = proxy.GetAllCompanies();
                    PartnerCompanies.Clear();
                    NonPartnerCompanies.Clear();
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
                }
                LogHelper.GetLogger().Info("There are some companies ready to cooperate");

            }
            catch (Exception e)
            {
                LogHelper.GetLogger().Error("There are no companies", e);
            }

        }

        void FetchProjects()
        {
            using (OutSClientProxy proxy = ((App)Application.Current).Proxy)
            {
                List<OcProject> result = proxy.GetAllProjects();
                Projects.Clear();
                if (result != null)
                {
                    foreach (var proj in result)
                    {
                        Projects.Add(proj);
                    }
                }
            }

            /*Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Approved" });
            Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Disapproved" });
            */
        }

        void FetchTeams()
        {
            using (OutSClientProxy proxy = ((App)Application.Current).Proxy)
            {
                List<Team> result = proxy.GetAllTeams();
                Teams.Clear();
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
                throw new Exception("[AcceptRequestCommnad] Command parameters has NULL value");
            }
            Company company = param as Company;

            using (OutSClientProxy proxy = ((App)App.Current).Proxy)
            {
                company.State = State.CompanyState.Partner;
                bool success = proxy.AnswerToRequest(company);
                proxy.ModifyCompany(company);
                FetchCompanies();
            }

        }

        private void RejectRequest(object param)
        {
            LogHelper.GetLogger().Info("RejectRequest called.");

            if (param == null)
            {
                throw new Exception("[RejectRequestCommnad] Command parameters has NULL value");
            }
            Company company = param as Company;

            using (OutSClientProxy proxy = ((App)App.Current).Proxy)
            {
                company.State = State.CompanyState.NoPartner;
                bool success = proxy.AnswerToRequest(company);
                proxy.RemoveCompany(company);
                FetchCompanies();
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

