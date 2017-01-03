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
	/// Interaction logic for CreateProjectView.xaml
	/// </summary>
	public partial class CreateProjectView : UserControl
	{
		public CreateProjectView()
		{
			InitializeComponent();
		}
        public event EventHandler SaveClicked;
        public event EventHandler CancelClicked;

        public static readonly DependencyProperty ProjectProperty =
        DependencyProperty.Register(
            "Project",
            typeof(Project),
            typeof(CreateProjectView),
            new UIPropertyMetadata(null));

        public Project Project
        {
            get { return (Project)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var prj = Project;
            SaveClicked?.Invoke(sender, e);
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(sender, e);
        }
    }
}
