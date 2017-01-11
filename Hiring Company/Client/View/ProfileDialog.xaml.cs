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


		private HiringClientProxy proxy = ((App)App.Current).Proxy;

		public ProfileDialog()
		{

			User = new User();

			InitializeComponent();
			DataContext = this;
			LogHelper.GetLogger().Info("Profile Dialog initialized.");
		}

		public ProfileDialog(string LoggedUsername)
		{

			User = proxy.GetUser(LoggedUsername);

			if (User == null)
			{
				LogHelper.GetLogger().Error("Error while loading ProfileDialog, User = NULL");
			}

			InitializeComponent();
			DataContext = this;
			LogHelper.GetLogger().Info("Profile Dialog initialized.");
		}

		private void UserInputView_SaveClicked(object sender, EventArgs e)
		{
			LogHelper.GetLogger().Info("Save click occurred.");
			bool success = false;

			if (User.Id == 0)   //Add if not exist(Create new User)
			{
				success = proxy.AddUser(User);
			}
			else
			{
				success = proxy.UpdateUser(User);
			}

			if (success)
			{
				LogHelper.GetLogger().Info("Profile Dialog closed.");
				this.DialogResult = true;
				this.Close();
			}else
			{
				LogHelper.GetLogger().Warn("Profile Dialog UpdateUser failed.");
				LogHelper.GetLogger().Info("Profile Dialog closed.");
				this.Close();
			}
		}

		private void UserInputView_CancelClicked(object sender, EventArgs e)
		{
			LogHelper.GetLogger().Info("Cancel click occurred. Profile Dialog closed.");
			this.Close();
		}
	}
}
