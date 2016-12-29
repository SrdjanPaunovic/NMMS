using Client.ViewModel;
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

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();

            // Grouping companies
            CollectionView companiesView = (CollectionView)CollectionViewSource.GetDefaultView(companies.ItemsSource);
            PropertyGroupDescription companyGroupDescription = new PropertyGroupDescription("Partnership");
            companiesView.GroupDescriptions.Add(companyGroupDescription);

            // Grouping projects
            CollectionView projectsView = (CollectionView)CollectionViewSource.GetDefaultView(projects.ItemsSource);
            PropertyGroupDescription projectGroupDescription = new PropertyGroupDescription("Status");
            projectsView.GroupDescriptions.Add(projectGroupDescription);
		}

	}
}
