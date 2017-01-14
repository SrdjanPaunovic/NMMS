using Client.ViewModel;
using Common;
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

//[assembly: log4net.Config.XmlConfigurator(Watch =true)]
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "App.config", Watch = true)]


namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public MainWindow()
		{
			InitializeComponent();

            // Grouping partnerCompanies
            CollectionView companiesView = (CollectionView)CollectionViewSource.GetDefaultView(partnerCompanies.ItemsSource);
            PropertyGroupDescription companyGroupDescription = new PropertyGroupDescription("State");
            companiesView.GroupDescriptions.Add(companyGroupDescription);

            // Grouping projects
            CollectionView projectsView = (CollectionView)CollectionViewSource.GetDefaultView(projects.ItemsSource);
            PropertyGroupDescription projectGroupDescription = new PropertyGroupDescription("Status");
            projectsView.GroupDescriptions.Add(projectGroupDescription);
			LogHelper.GetLogger().Debug("Main window initialized.");
		}

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                if(viewModel.LoggedUsername.Trim() != ""){
                    viewModel.LogOutCommand.Execute(viewModel.LoggedUsername);
                }
            }
        }

	}
}
