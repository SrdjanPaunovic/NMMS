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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Common.UserControls
{
	/// <summary>
	/// Interaction logic for UserStoryShortView.xaml
	/// </summary>
	public partial class UserStoryShortView : UserControl
	{
		public UserStoryShortView()
		{
			DataContext = this;
			InitializeComponent();
		}

		public event EventHandler EditClicked;
		public event EventHandler DeleteClicked;

		public static readonly DependencyProperty UserStoryProperty =
		DependencyProperty.Register(
			"UserStory",
			typeof(UserStory),
			typeof(UserStoryShortView),
			new UIPropertyMetadata(null));

		public UserStory UserStory
		{
			get { return (UserStory)GetValue(UserStoryProperty); }
			set { SetValue(UserStoryProperty, value); }
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			EditClicked?.Invoke(sender, e);
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			DeleteClicked?.Invoke(sender, e);
		}
	}
}
