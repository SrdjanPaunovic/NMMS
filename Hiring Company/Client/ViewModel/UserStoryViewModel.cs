using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common;

namespace Client.ViewModel
{
    public class UserStoryViewModel
    {

        private UserStory userStory;

        

        public UserStoryViewModel(UserStory userStory)
        {
            this.UserStory = userStory;

        }

        #region Commands
        private ICommand addTaskCommand;
        private ICommand cancelCommand;
        private ICommand saveCommand;
        private ICommand editTaskCommand;
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

        public ICommand EditTaskCommand
        {
            get
            {
                return editTaskCommand ?? (editTaskCommand = new RelayCommand((param) => this.EditTaskClick(param)));
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
        #endregion Properties

        #region Methods

        private void CancelClick(object param)
        {
			LogHelper.GetLogger().Info("Cancel click occurred.");
            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);
			LogHelper.GetLogger().Info(parentWindow.Name+" closed");

            parentWindow.Close();
        }

        private void SaveClick(object param)
        {
			LogHelper.GetLogger().Info("Save click occurred.");

            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);

            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
               /*s bool success = false;
                if (isEditing)
                {
                    //TODO proxy.UpdateProject(Project);
                }
                else
                {
                    success = proxy.AddProject(Project);
                }

                if (success)
                {
                    //TODO Logger
                    parentWindow.Close();
                }*/
            }
        }

        private void AddTaskClick(object param)
        {
			LogHelper.GetLogger().Info("Add Task click occurred.");

            var name = param as string;
            if (name == String.Empty)
            {
                return;
            }
            Common.Entities.Task task = new Common.Entities.Task()            
            {
                Name = name
            };

            UserStory.Tasks.Add(task);
			LogHelper.GetLogger().Info("Task "+task.Name+" added");

        }

        private void EditTaskClick(object param)
        {
			LogHelper.GetLogger().Info("Edit Task click occurred.");
          
        }

        private void DeleteTaskClick(object param)
        {
			LogHelper.GetLogger().Info("Delete Task click occurred.");
           
        }
        #endregion Methods
    }
}
