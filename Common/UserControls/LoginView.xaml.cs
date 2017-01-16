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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            DataContext = this;
            InitializeComponent();
        }

        public static readonly DependencyProperty LoginCommandProperty =
        DependencyProperty.Register(
            "LoginCommand",
            typeof(ICommand),
            typeof(LoginView),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty UsernameProperty =
        DependencyProperty.Register(
            "Username",
            typeof(string),
            typeof(LoginView),
            new PropertyMetadata(string.Empty));

        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        private void Username_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Username.Trim() == "")
            {
                Password_TB.Password = "";
            }
        }

        private void Password_TB_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Password_TB.Tag = Password_TB.Password;
        }
    }
}
