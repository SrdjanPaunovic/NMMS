using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for ProfileDialog.xaml
    /// </summary>
    public partial class ProfileDialog : Window
    {
        public User User { get; set; }
        public ProfileDialog(string LoggedUsername)
        {

            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
                User = proxy.GetUser(LoggedUsername);

                if (User == null)
                {
                    //TODO
					LogHelper.GetLogger().Error("Error while loading ProfileDialog, User = NULL");

                }
            }

            InitializeComponent();
            DataContext = this;
			LogHelper.GetLogger().Info("Profile Dialog initialized.");
        }

        private void UserInputView_SaveClicked(object sender, EventArgs e)
        {
			LogHelper.GetLogger().Info("Save click occurred.");
            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
                bool success = proxy.UpdateUser(User);

                if (success)
                {
					LogHelper.GetLogger().Info("Profile Dialog closed.");

                    this.Close();
                }
            }

        }

        private void UserInputView_CancelClicked(object sender, EventArgs e)
        {
			LogHelper.GetLogger().Info("Cancel click occurred. Profile Dialog closed.");
            this.Close();
        }
    }
}
