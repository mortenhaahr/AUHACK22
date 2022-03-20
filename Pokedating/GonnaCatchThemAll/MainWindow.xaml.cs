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

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            loginInstance.LoginDelegate = (WebAPI.User user) =>
            {
                loginInstance.Dispatcher.Invoke(() => loginInstance.Visibility = Visibility.Hidden);
                smashPassInstance.currentUser = user;
                smashPassInstance.RetrieveCandidates();
                smashPassInstance.Dispatcher.Invoke(() => smashPassInstance.Visibility = Visibility.Visible);
            };
            loginInstance.RegistorDelegate = () =>
            {
                loginInstance.Visibility = Visibility.Hidden;
                registerInstance.Visibility = Visibility.Visible;
            };

            registerInstance.CancelDelegate = () =>
            {
                loginInstance.Visibility = Visibility.Visible;
                registerInstance.Visibility = Visibility.Hidden;
            };
            registerInstance.RegisterDelegate = (WebAPI.User user) =>
            {
                registerInstance.Visibility = Visibility.Hidden;
                profileInstance.user = user;
                profileInstance.Visibility = Visibility.Visible;
            };
            profileInstance.CancelDelegate = () =>
            {
                profileInstance.Visibility = Visibility.Hidden;
                loginInstance.Visibility = Visibility.Visible;
            };
            profileInstance.SaveDelegate = (WebAPI.User user) =>
            {
                profileInstance.Dispatcher.Invoke(() => profileInstance.Visibility = Visibility.Hidden);
                smashPassInstance.currentUser = user;
                teamSelector.Dispatcher.Invoke(() => teamSelector.Visibility = Visibility.Visible);
            };
            teamSelector.AcceptDelegate = () =>
            {
                teamSelector.Dispatcher.Invoke(() => teamSelector.Visibility = Visibility.Hidden);
                smashPassInstance.Dispatcher.Invoke(() => smashPassInstance.Visibility = Visibility.Visible);
            };
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            teamSelector.RedrawRow((int)this.Width);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                case WindowState.Normal:
                    teamSelector.RedrawRow((int)this.Width);
                    break;
                default:
                    break;
            }
        }

    }
}
