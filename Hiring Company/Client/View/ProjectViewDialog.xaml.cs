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
    /// Interaction logic for ProjectViewDialog.xaml
    /// </summary>
    public partial class ProjectViewDialog : Window
    {
        public Project Project { get; set; }

        public ProjectViewDialog()
        {
            Project = new Project();
            DataContext = this;
            InitializeComponent();
           
        }

        private void CreateProjectView_SaveClicked(object sender, EventArgs e)
        {
            using (HiringClientProxy proxy = ((App)App.Current).Proxy)
            {
                bool success = proxy.AddProject(Project);
                if (success)
                {
                    //TODO Logger
                    this.Close();
                }
            }
        }

        private void CreateProjectView_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
