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
            InitializeComponent();
            //DataContext = this;

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("NMMS")) + "NMMS/Common";
            Edit_btn.NormalImage = new BitmapImage(new Uri(path + "/Images/edit.png"));
            Delete_btn.NormalImage = new BitmapImage(new Uri(path + "/Images/delete.png"));

        }

        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (EditClicked != null)
            {
                EditClicked.Invoke(DataContext, null);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EditClicked != null)
            {
                DeleteClicked.Invoke(DataContext, null);
            }
        }
    }
}
