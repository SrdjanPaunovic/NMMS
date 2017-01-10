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

namespace Client.ViewModel
{

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public enum WindowState
        {
            LOGIN,
            EMPLOYEES,
            COMPANIES,
            PROJECTS
        }

        public MainWindowViewModel()
        {
          string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
          string path = System.IO.Path.GetDirectoryName(executable);
          path = path.Substring(0, path.LastIndexOf("NMMS")) + "NMMS/Common";
          EditIcon = new BitmapImage(new Uri(path + "/Images/edit.png"));
        }

        #region Fields
        private string loggedUsername = "";
        private ObservableCollection<Company> partnerCompanies = new ObservableCollection<Company>();
        private ObservableCollection<Company> nonPartnerCompanies = new ObservableCollection<Company>();
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();

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
        private ICommand displayCompaniesCommand;
        private ICommand sendCompanyRequestCommand;

        #endregion Fields

        #region Properties
        
        public BitmapImage EditIcon { get; set; }
        
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
            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
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
            //TODO
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

        private void LogOut(object param)
        {
            LogHelper.GetLogger().Info("LogOut called.");

            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
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
                    LogHelper.GetLogger().Error("Error while loggout");

                }
            }
        }

        void NewProject()
        {
            LogHelper.GetLogger().Info("NewProject called.");

            ProjectViewDialog projectDialog = new ProjectViewDialog();
            projectDialog.ShowDialog();
        }

        void EditProject(Project project)
        {
            LogHelper.GetLogger().Info("EditProject called.");

            ProjectViewDialog projectDialog = new ProjectViewDialog(project);
            projectDialog.ShowDialog();
        }

        void FetchCompanies()
        {

            LogHelper.GetLogger().Info("FetchCompanies called.");
            try
            {
                using (HiringClientProxy proxy = ((App)Application.Current).Proxy)
                {
                    PartnerCompanies.Clear();
                    NonPartnerCompanies.Clear();

                    List<Company> result = proxy.GetAllCompanies();

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
                LogHelper.GetLogger().Info("Get companies is done successfuly");
            }
            catch (Exception e)
            {

                LogHelper.GetLogger().Error("There is no companies", e);
            }

        }

        void FetchProjects()
        {
            LogHelper.GetLogger().Info("FetchProjects called.");

            using (HiringClientProxy proxy = ((App)Application.Current).Proxy)
            {
                List<Project> result = proxy.GetAllProjects();
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

        private void SendCompanyRequest(object param)
        {
            LogHelper.GetLogger().Info("SendCompanyRequest called.");

            if (param == null)
            {
                throw new Exception("[SendCompanyRequestCommnad] Command parameters has NULL value");
            }
            Company company = param as Company;

            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
                bool success = proxy.SendRequest(company);
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
