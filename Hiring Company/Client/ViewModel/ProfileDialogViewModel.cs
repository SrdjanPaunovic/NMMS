using Common;
using Common.Entities;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class ProfileDialogViewModel
    {

        public User User { get; set; }

        private IHiringContract proxy;
        public ProfileDialogViewModel()
        {
            User = new User();
            Proxy = App.Proxy;
            LogHelper.GetLogger().Info("Profile Dialog initialized.");
        }

        public ProfileDialogViewModel(User user)
        {
            Proxy = App.Proxy;

            User = user;

            if (User == null)
            {
                LogHelper.GetLogger().Error("Error while loading ProfileDialog, User = NULL");
            }

            LogHelper.GetLogger().Info("Profile Dialog initialized.");
        }

        #region Commands
        private ICommand cancelCommand;
        private ICommand saveCommand;

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
        #endregion Commands

        #region Methods
        private void CancelClick(object param)
        {
            LogHelper.GetLogger().Info("Cancel click occurred.");

            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);
            LogHelper.GetLogger().Info(parentWindow.Name + " closed.");

            parentWindow.Close();
        }

        private void SaveClick(object param)
        {
            LogHelper.GetLogger().Info("Save click occurred.");

            var userControl = param as UserControl;
            Window parentWindow = Window.GetWindow(userControl);

            LogHelper.GetLogger().Info("Save click occurred.");
            bool success = false;

            //Add if not exist(Create new User)
            if (User.Id == 0)   
            {
                success = Proxy.AddUser(User);
            }
            else
            {
                success = Proxy.UpdateUser(User);
            }

            if (success)
            {
                LogHelper.GetLogger().Info("Profile Dialog closed.");
                parentWindow.DialogResult = true;
                if (((App)App.Current).LoggedUser.Id == User.Id)
                {
                    parentWindow.Tag = User;
                }
                parentWindow.Close();
            }
            else
            {
                LogHelper.GetLogger().Warn("Profile Dialog UpdateUser failed.");
                LogHelper.GetLogger().Info("Profile Dialog closed.");
                parentWindow.Close();
            }
        }
        #endregion Methods
    }
}
