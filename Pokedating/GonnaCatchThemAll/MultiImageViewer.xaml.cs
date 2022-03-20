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

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for MultiImageViewer.xaml
    /// </summary>
    public partial class MultiImageViewer : UserControl
    {
        int idx = 0;
        List<BitmapImage> images = new List<BitmapImage>();
        public MultiImageViewer()
        {
            var l = new List<string>() { "stolen", "stolen_deepfried", "stolen10", "stolen2", "stolen3", "stolen4", "stolen5", "stolen6", "stolen7", "stolen8" };
            foreach(var image in l)
            {
                images.Add(new BitmapImage(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\nudes\" + image + ".png")));
            }
            InitializeComponent();
            idx = 0;
            Image_viewer.Source = images[0];
            Left_btn.Content = "<";
            Right_btn.Content= ">";
        }

        public void LoadNewImages(List<string> imageList)
        {
            this.Image_viewer.Dispatcher.Invoke(() =>
            {
                Image_viewer.Source = null;
                images = new List<BitmapImage>();
                foreach (var image in imageList)
                {
                    if (image != null)
                    {
                        images.Add(new BitmapImage(new Uri(image)));
                    }
                }
                Image_viewer.Source = images[0];
            });
        }

        private void Show_MouseEnter(object sender, MouseEventArgs e)
        {
            Left_btn.Visibility = Visibility.Visible;
            Right_btn.Visibility = Visibility.Visible;
        }
        private void Show_MouseLeave(object sender, MouseEventArgs e)
        {
            Left_btn.Visibility = Visibility.Hidden;
            Right_btn.Visibility = Visibility.Hidden;
        }

        private void Left_btn_Click(object sender, RoutedEventArgs e)
        {
            if(idx == 0)
            {
                return;
            }
            Image_viewer.Source = images[--idx];
        }

        private void Right_btn_Click(object sender, RoutedEventArgs e)
        {
            if(idx < images.Count-1){
                Image_viewer.Source = images[++idx];
            }
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Show_MouseEnter(sender, e);
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
        }
    }
}
