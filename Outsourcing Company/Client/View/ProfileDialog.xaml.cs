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
using Common;
using Client.ViewModel;
using Common.UserControls;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for ProfileDialog.xaml
    /// </summary>
    public partial class ProfileDialog : Window
    {
        public ProfileDialog()
        {
            DataContext = new ProfileDialogViewModel();
            Initialized += ProfileDialog_Initialized;

            InitializeComponent();
        }

        public ProfileDialog(OcUser user)
        {
            DataContext = new ProfileDialogViewModel(user);
            Initialized += ProfileDialog_Initialized;

            InitializeComponent();

        }

        internal void ProfileDialog_Initialized(object sender, EventArgs e)
        {
            var viewModel = DataContext as ProfileDialogViewModel;

            Binding binding = new Binding
            {
                Source = viewModel.User,
            };
            userInputControl.SetBinding(UserInputView.UserProperty, binding);

            binding = new Binding
            {
                Source = viewModel.SaveCommand,
            };
            userInputControl.SetBinding(UserInputView.SaveCommandProperty, binding);

            binding = new Binding
            {
                Source = viewModel.CancelCommand,
            };
            userInputControl.SetBinding(UserInputView.CancelCommandProperty, binding);

            LogHelper.GetLogger().Info("Profile Dialog initialized.");

        }
    }
}
