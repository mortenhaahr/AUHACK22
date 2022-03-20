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
            loginInstance.LoginDelegate = () =>
            {
                loginInstance.Visibility = Visibility.Hidden;
                smashPassInstance.Visibility = Visibility.Visible;
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
            registerInstance.RegisterDelegate = () =>
            {
                registerInstance.Visibility = Visibility.Hidden;
                profileInstance.Visibility = Visibility.Visible;
            };
            profileInstance.CancelDelegate = () =>
            {
                profileInstance.Visibility = Visibility.Hidden;
                loginInstance.Visibility = Visibility.Visible;
            };
            profileInstance.SaveDelegate = () =>
            {
                profileInstance.Visibility = Visibility.Hidden;
                teamSelector.Visibility = Visibility.Visible;
            };
            teamSelector.AcceptDelegate = () =>
            {
                teamSelector.Visibility = Visibility.Hidden;
                smashPassInstance.Visibility = Visibility.Visible;
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
