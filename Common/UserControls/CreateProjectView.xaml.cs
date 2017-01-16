using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static readonly DependencyProperty SaveCommandProperty =
        DependencyProperty.Register(
            "SaveCommand",
            typeof(ICommand),
            typeof(CreateProjectView),
            new UIPropertyMetadata(null));

        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
        DependencyProperty.Register(
            "CancelCommand",
            typeof(ICommand),
            typeof(CreateProjectView),
            new UIPropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty AddStoryCommandProperty =
        DependencyProperty.Register(
            "AddStoryCommand",
            typeof(ICommand),
            typeof(CreateProjectView),
            new UIPropertyMetadata(null));

        public ICommand AddStoryCommand
        {
            get { return (ICommand)GetValue(AddStoryCommandProperty); }
            set { SetValue(AddStoryCommandProperty, value); }
        }

        public static readonly DependencyProperty EditStoryCommandProperty =
        DependencyProperty.Register(
            "EditStoryCommand",
            typeof(ICommand),
            typeof(CreateProjectView),
            new UIPropertyMetadata(null));

        public ICommand EditStoryCommand
        {
            get { return (ICommand)GetValue(EditStoryCommandProperty); }
            set { SetValue(EditStoryCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteStoryCommandProperty =
        DependencyProperty.Register(
            "DeleteStoryCommand",
            typeof(ICommand),
            typeof(CreateProjectView),
            new UIPropertyMetadata(null));

        public ICommand DeleteStoryCommand
        {
            get { return (ICommand)GetValue(DeleteStoryCommandProperty); }
            set { SetValue(DeleteStoryCommandProperty, value); }
        }

        private void UserStoryShortView_EditClicked(object sender, EventArgs e)
        {
            if (EditStoryCommand != null && EditStoryCommand.CanExecute(sender))
            {
                EditStoryCommand.Execute(sender);
            }
        }

        private void UserStoryShortView_DeleteClicked(object sender, EventArgs e)
        {
            if (DeleteStoryCommand != null && DeleteStoryCommand.CanExecute(sender))
            {
                DeleteStoryCommand.Execute(sender);
            }
        }
    }
}
