using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region Fields
        //TODO: INTGR change classes
        private ObservableCollection<Object> companies = new ObservableCollection<Object>();
        private ObservableCollection<Object> projects = new ObservableCollection<Object>();
        private ICommand loginCommand;
        private ICommand displayProjectsCommand;
        private ICommand newProjectCommand;
        private WindowState currentState = WindowState.PROJECTS;


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
        #endregion Properties

        #region Methods
        private void LoginClick(object param)
        {
            object[] parameters = param as object[];

            if (parameters == null)
            {
                throw new Exception("[LoginCommnad] Command parameters has NULL value");
            }
            //TODO
        }

        void NewProject()
        {
            CurrentState = WindowState.NEW_PROJECT;
        }

        void FetchCompanies()
        {
            // TODO: INTGR call service and set companies
            // TODO: INTGR remove this
        }

        void FetchProjects()
        {
            // TODO: INTGR call service and set projects
            // TODO: INTGR remove this
            Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Approved" });
            Projects.Add(new { Name = "P1", Description = "This is description", StartTime = "Danas", Deadline = "Sutra", Status = "Disapproved" });

        }
        #endregion Methods
    }
}
