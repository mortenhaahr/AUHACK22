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
using GonnaCatchThemAll.Helpers;

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Delegates.TransitionDelegate CancelDelegate = () => { };
        public Delegates.UserDelegate RegisterDelegate = (WebAPI.User user) => { };
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password_Textbox.Password != Confirm_Textbox.Password)
            {
                return;
            }
            var userEmail = Email_Textbox.Text;
            var password = Password_Textbox.Password;
            WebAPI.User user = new WebAPI.User { email = userEmail };

            RegisterDelegate(user);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Email_Textbox.Text = "";
            Password_Textbox.Password = "";
            CancelDelegate();
        }
    }
}
