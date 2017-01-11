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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SaveClicked != null)
            {
				if(passBox.Password == confirmPassBox.Password)
				{
					//if something typed in Passwords fields
					if(passBox.Password.Trim() != String.Empty)
					{
						User.Password = passBox.Password;
					}

					if(User.Password!= String.Empty)
					{
						SaveClicked(sender, e);
					}else
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
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked(sender, e);

            }
        }
    }
}
