using Client.ViewModel;
using Common.Entities;
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
using System.Windows.Shapes;

namespace Client.View
{
	/// <summary>
	/// Interaction logic for TeamViewDialog.xaml
	/// </summary>
	public partial class TeamViewDialog : Window
	{
		public TeamViewDialog()
		{
			DataContext = new TeamDialogViewModel();
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OcUser selectedDeveloper = (OcUser)developerChooser.SelectedItem;
			if (selectedDeveloper != null)
			{
				((TeamDialogViewModel)DataContext).TeamDevelopers.Add(selectedDeveloper);
				((TeamDialogViewModel)DataContext).Developers.Remove(selectedDeveloper);
			}
		}
	}
}
