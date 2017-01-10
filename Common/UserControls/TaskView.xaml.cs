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
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();
        }
        public event EventHandler DeleteClicked;

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteClicked != null)
            {
                DeleteClicked.Invoke(DataContext, null);
            }
        }
    }
}
