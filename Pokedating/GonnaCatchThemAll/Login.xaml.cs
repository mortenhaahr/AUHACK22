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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        public Delegates.UserDelegate LoginDelegate = (WebAPI.User user) => { };
        public Delegates.TransitionDelegate RegistorDelegate = () => { };
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            var username = Username_TextBox.Text;
            var password = Password_TextBox.Password;
            WebAPI.WebClient.Get<WebAPI.User>("users/?email=" + Username_TextBox.Text).ContinueWith((a) =>
            {

                a.Wait();
                if(a.Result.count > 0)
                {
                    Profile.instance.user = a.Result.results[0];
                    LoginDelegate(Profile.instance.user);
                }
            });
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            RegistorDelegate();
        }

        private void Squirtr_btn_Click(object sender, RoutedEventArgs e)
        {
            NoteficationControl.Notify("You got lucky a hot girl near Herning wants to date you!", new BitmapImage(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\artwork\squirtle.jpg")));
        }

        private void Gastrogram_btn_Click(object sender, RoutedEventArgs e)
        {
            NoteficationControl.Notify("You need to click on Squirtr");
        }
    }
}
