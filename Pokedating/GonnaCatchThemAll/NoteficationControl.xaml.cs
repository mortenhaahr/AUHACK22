using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for NoteficationControl.xaml
    /// </summary>
    public partial class NoteficationControl : UserControl
    {
        static NoteficationControl noteficationControl = null;
        public NoteficationControl()
        {
            InitializeComponent();
            noteficationControl = this;
        }

        public static void Notify(string message, BitmapImage image = null)
        {
            if(noteficationControl == null)
            {
                return;
            }
            if(image != null)
            {
                noteficationControl.ProfileImage.Source = image;

            } else
            {
                Grid.SetRow(noteficationControl.Notefication, 0);
                Grid.SetRowSpan(noteficationControl.Notefication, 2);
                noteficationControl.ImageGrid.Visibility = Visibility.Hidden;
            }
            noteficationControl.Notefication.Text = message;
            DoubleAnimation d = new DoubleAnimation(0,200,TimeSpan.FromMilliseconds(500));
            TranslateTransform translateTransform = new TranslateTransform();
            noteficationControl.NoteficationBox.RenderTransform=translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.YProperty, d);
            Timer t = new Timer();
            t.Interval = 3500;
            t.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
            {
                noteficationControl.NoteficationBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    t.Stop();
                    t = new Timer();
                    t.Interval = 500;
                    d.From = 200;
                    d.To = 0;
                    translateTransform.BeginAnimation(TranslateTransform.YProperty, d);
                    t.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
                    {
                        noteficationControl.NoteficationBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            noteficationControl.ImageGrid.Visibility = Visibility.Visible;
                            Grid.SetRow(noteficationControl.Notefication, 1);
                            Grid.SetRowSpan(noteficationControl.Notefication, 1);
                            t.Stop();
                        }));
                    });
                    t.Start();
                }));
            });
            t.Start();
        }
    }
}
