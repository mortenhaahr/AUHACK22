using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace smash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Smash_Click(object sender, RoutedEventArgs e)
        {

            Image_Pokeball.Visibility = Visibility.Visible;
            RotateTransform rotate = new RotateTransform();
            TranslateTransform position = new TranslateTransform();
            TransformGroup transform = new TransformGroup();
            DoubleAnimation upAnimation = new DoubleAnimation(0, -canvas.ActualHeight * 0.6, TimeSpan.FromSeconds(1.2));
            DoubleAnimation spinAnimation = new DoubleAnimation(0, 360 * 3, TimeSpan.FromSeconds(1.2));
            transform.Children.Add(position);
            transform.Children.Add(rotate);
            Image_Pokeball.RenderTransform = transform;
            position.BeginAnimation(TranslateTransform.YProperty, upAnimation);
            rotate.BeginAnimation(RotateTransform.CenterYProperty, upAnimation);
            rotate.BeginAnimation(RotateTransform.AngleProperty, spinAnimation);

        }

        private void Button_Pokeball_Select(object sender, MouseButtonEventArgs e)
        {
            Image srcImage = e.Source as Image;
            Image_Pokeball.Source = srcImage.Source;
            srcImage.Opacity = 1;
        }

        private void Button_Pokeball_MouseEnter(object sender, MouseEventArgs e)
        {
            Image srcImage = e.Source as Image;
            if (srcImage.Source != Image_Pokeball.Source)
            {
                srcImage.Opacity = 0.80;
            }
        }

        private void Button_Pokeball_MouseExit(object sender, MouseEventArgs e)
        {
            foreach (Image pokeselect in Pokeball_seletion.Children)
            {
                pokeselect.Opacity = (Image_Pokeball.Source != pokeselect.Source) ? 0.5 : 1;
            }
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(Image_Pokeball, (canvas.ActualWidth - Image_Pokeball.Width) / 2);
            Canvas.SetTop(Image_Pokeball, (canvas.ActualHeight - Image_Pokeball.Height) * 0.8);
        }
    }
}
