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
    /// Interaction logic for UserInputView.xaml
    /// </summary>
    public partial class UserInputView : UserControl
    {
        public UserInputView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event EventHandler SaveClicked;
        public event EventHandler CancelClicked;

        public static readonly DependencyProperty UserProperty =
        DependencyProperty.Register(
            "User",
            typeof(User),
            typeof(UserInputView),
            new UIPropertyMetadata(null));

        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public static readonly DependencyProperty SaveCommandProperty =
        DependencyProperty.Register(
            "SaveCommand",
            typeof(ICommand),
            typeof(UserInputView),
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
            typeof(UserInputView),
            new UIPropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (passBox.Password == confirmPassBox.Password)
            {
                //if something typed in Passwords fields
                if (passBox.Password.Trim() != String.Empty)
                {
                    User.Password = passBox.Password;
                }

                if (User.Password != String.Empty)
                {
                    if (SaveClicked != null)
                    {
                        SaveClicked(sender, e);
                    }

                    if (SaveCommand != null && SaveCommand.CanExecute(this))
                    {
                        SaveCommand.Execute(this);
                    }

                }
                else
                {
                    //if noting typed in Password fields and User have not set Password Property
                    passBox.Background = Brushes.Red;
                    confirmPassBox.Background = Brushes.Red;
                }
            }
            else
            {
                //Password does not match
                confirmPassBox.Background = Brushes.Red;
                passBox.Background = Brushes.White;
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked(sender, e);
            }

            if (CancelCommand != null && CancelCommand.CanExecute(this))
            {
                CancelCommand.Execute(this);
            }
        }
    }
}
