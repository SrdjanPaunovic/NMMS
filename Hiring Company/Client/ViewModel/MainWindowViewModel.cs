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
            COMPANIES,
            PROJECTS,
            NEW_PROJECT
        }



		#region Fields
		//TODO: INTGR change classes
		private string loggedUsername = "";
        private ObservableCollection<Object> companies = new ObservableCollection<Object>();
        private ObservableCollection<Object> projects = new ObservableCollection<Object>();
        
        private WindowState currentState = WindowState.LOGIN;
		private NetTcpBinding netTcpBinding = new NetTcpBinding();
		//commands
		private ICommand loginCommand;
		private ICommand logOutCommand;
		private ICommand displayProjectsCommand;
        private ICommand newProjectCommand;
        private ICommand showProfileCommand;

        
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
        public ObservableCollection<Object> Companies
        {
            get { return companies; }
            set { companies = value; }
        }

        public ObservableCollection<Object> Projects
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

		
		#endregion Properties

		#region Methods
		private void LoginClick(object param)
        {
            object[] parameters = param as object[];

            if (parameters == null)
            {
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
           
            
            ProfileDialog profileDialog = new ProfileDialog(LoggedUsername);
            profileDialog.ShowDialog();
        }

		private void LogOut(object param)
		{
            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
                bool success = proxy.LogOut(LoggedUsername);

				if (success)
				{
					LoggedUsername = "";
					CurrentState = WindowState.LOGIN;
				}else
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

        void FetchCompanies()
        {
            // TODO: INTGR call service and set companies
            // TODO: INTGR remove this
        }

        void FetchProjects()
        {
            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
                List<Project> result = proxy.GetAllProjects();
				Projects.Clear();
                if (result != null)
                {
					foreach(var proj in result)
					{
						Projects.Add(proj);
					}
                }
            }

            /*Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Approved" });
            Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Disapproved" });
            */
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
