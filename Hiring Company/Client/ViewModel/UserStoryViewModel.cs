using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ServiceContract;

namespace Client.ViewModel
{
    public class UserStoryViewModel
    {

        private UserStory userStory;
        private static IHiringContract proxy;



        public UserStoryViewModel(UserStory userStory)
        {
            Proxy = ((App)App.Current).Proxy;

            this.UserStory = userStory;

            List<Common.Entities.Task> tasks = Proxy.GetTasksFromUserStory(UserStory);
            UserStory.Tasks = new ObservableCollection<Common.Entities.Task>(tasks);
            UserStory.Project = Proxy.GetProjectFromUserStory(UserStory);
        }

        #region Commands
        private ICommand addTaskCommand;
        private ICommand cancelCommand;
        private ICommand saveCommand;
        private ICommand deleteTaskCommand;

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand((param) => this.CancelClick(param)));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand((param) => this.SaveClick(param)));
            }
        }

        public ICommand AddTaskCommand
        {
            get
            {
                return addTaskCommand ?? (addTaskCommand = new RelayCommand((param) => this.AddTaskClick(param)));
            }
        }

        public ICommand DeleteTaskCommand
        {
            get
            {
                return deleteTaskCommand ?? (deleteTaskCommand = new RelayCommand((param) => this.DeleteTaskClick(param)));
            }
        }
        #endregion Commands

        #region Properties
        public UserStory UserStory
        {
            get { return userStory; }
            set { userStory = value; }
        }

        public static IHiringContract Proxy
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

        private void CancelClick(object param)
        {
            LogHelper.GetLogger().Info("Cancel click occurred.");
            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);
            LogHelper.GetLogger().Info(parentWindow.Name + " closed");

            parentWindow.Close();
        }

        private void SaveClick(object param)
        {
            LogHelper.GetLogger().Info("Save click occurred.");

            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);

            bool success = false;
            if (UserStory.Id != 0)
            {
                success = Proxy.UpdateUserStory(UserStory);
            }

            if (success)
            {
                parentWindow.Close();
                LogHelper.GetLogger().Info(parentWindow.Name + " closed");

            }
        }

        private void AddTaskClick(object param)
        {
            LogHelper.GetLogger().Info("Add Task click occurred.");

            var desc = param as string;
            if (desc == String.Empty)
            {
                return;
            }
            Common.Entities.Task task = new Common.Entities.Task()
            {
                Description = desc
            };

            UserStory.Tasks.Add(task);
            LogHelper.GetLogger().Info("Task " + task.Description + " added");

        }

        private void DeleteTaskClick(object param)
        {
            LogHelper.GetLogger().Info("Delete Task click occurred.");

            var task = param as Common.Entities.Task;
            UserStory.Tasks.Remove(task);
        }
        #endregion Methods
    }
}
