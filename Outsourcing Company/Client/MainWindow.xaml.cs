using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using Common;
using Client.ViewModel;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Thread updateThread;

		public MainWindow()
		{
			Initialized += MainWindow_Initialized;
			InitializeComponent();
			LogHelper.GetLogger().Debug("Main window for outsourcing company initialized.");

			CollectionView companiesView = (CollectionView)CollectionViewSource.GetDefaultView(partnerCompanies.ItemsSource);
			PropertyGroupDescription companyGroupDescription = new PropertyGroupDescription("State");
			companiesView.GroupDescriptions.Add(companyGroupDescription);

			// Grouping projects
			CollectionView projectsView = (CollectionView)CollectionViewSource.GetDefaultView(projects.ItemsSource);
			PropertyGroupDescription projectGroupDescription = new PropertyGroupDescription("Status");
			projectsView.GroupDescriptions.Add(projectGroupDescription);
            LogHelper.GetLogger().Debug("Main window initialized.");
		}

		private void MainWindow_Initialized(object sender, EventArgs e)
		{
			var viewModel = DataContext as MainWindowViewModel;

			if (viewModel != null)
			{
				updateThread = new Thread(() => UpdateData(viewModel));
				updateThread.Start();
			}

		}

		private void UpdateData(MainWindowViewModel viewModel)
		{
			while (true)
			{
				viewModel.UpdateData();
				Thread.Sleep(5000);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			var viewModel = DataContext as MainWindowViewModel;
			if (viewModel != null)
			{
				if (viewModel.LoggedUser != null)
				{
					viewModel.LogOutCommand.Execute(viewModel.LoggedUser.Username);
				}
			}
		}
	}
}
