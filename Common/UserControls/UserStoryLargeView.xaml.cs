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
    /// Interaction logic for UserStoryLargeView.xaml
    /// </summary>
    public partial class UserStoryLargeView : UserControl
    {
        public UserStoryLargeView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty EditTaskCommandProperty =
        DependencyProperty.Register(
            "EditTaskCommand",
            typeof(ICommand),
            typeof(UserStoryLargeView),
            new UIPropertyMetadata(null));

        public ICommand EditTaskCommand
        {
            get { return (ICommand)GetValue(EditTaskCommandProperty); }
            set { SetValue(EditTaskCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteTaskCommandProperty =
        DependencyProperty.Register(
            "DeleteTaskCommand",
            typeof(ICommand),
            typeof(UserStoryLargeView),
            new UIPropertyMetadata(null));

        public ICommand DeleteTaskCommand
        {
            get { return (ICommand)GetValue(DeleteTaskCommandProperty); }
            set { SetValue(DeleteTaskCommandProperty, value); }
        }

        private void Task_EditClicked(object sender, EventArgs e)
        {
            if (EditTaskCommand != null && EditTaskCommand.CanExecute(sender))
            {
                EditTaskCommand.Execute(sender);
            }
        }

        private void Task_DeleteClicked(object sender, EventArgs e)
        {
            if (DeleteTaskCommand != null && DeleteTaskCommand.CanExecute(sender))
            {
                DeleteTaskCommand.Execute(sender);
            }
        }
    }
}
