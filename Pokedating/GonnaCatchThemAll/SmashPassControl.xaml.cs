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
using System.Timers;
using WebAPI;

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class SmashPassControl : UserControl
    {

        static SmashPassControl smashPassControl = null;
        private bool animating = false;
        public SmashPassControl()
        {
            InitializeComponent();
            smashPassControl = this;
        }

        public WebAPI.User currentUser;
        private List<WebAPI.User> candidates = new List<WebAPI.User>();

        public void RetrieveCandidates()
        {
            Task<WebAPI.User[]?> task = WebAPI.WebClient.GetCandidates(currentUser.id);
            task.Start();
            task.Wait();
            if (task.Result == null)
            {
                return; // Her skal vi snakke shit omkring useren
            }
            candidates = task.Result.ToList();
        }

        private void DecideFateOfCandidate(bool smash)
        {
            WebAPI.WebClient.PostSmash(currentUser.id, candidates[0].id, smash);
            candidates.RemoveAt(0);
        }

        private void Button_Smash_Click(object sender, RoutedEventArgs e)
        {
            animating = true;
            Image_Pokeball.Visibility = Visibility.Visible;
            RotateTransform rotate = new RotateTransform();
            TranslateTransform position = new TranslateTransform();
            ScaleTransform scale = new ScaleTransform();
            TransformGroup transform = new TransformGroup();
            DoubleAnimation upAnimation = new DoubleAnimation(0, -canvas.ActualHeight * 0.4, TimeSpan.FromSeconds(1.2));
            DoubleAnimation spinAnimation = new DoubleAnimation(0, 360 * 3, TimeSpan.FromSeconds(1.2));
            transform.Children.Add(position);
            transform.Children.Add(rotate);
            transform.Children.Add(scale);
            Image_Pokeball.RenderTransform = transform;
            position.BeginAnimation(TranslateTransform.YProperty, upAnimation);
            rotate.BeginAnimation(RotateTransform.CenterYProperty, upAnimation);
            rotate.BeginAnimation(RotateTransform.AngleProperty, spinAnimation);
            Timer t = new Timer();
            t.Interval = 1200;
            t.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
            {
                smashPassControl.Image_Profile.Dispatcher.BeginInvoke(new Action(() =>
                {
                    t.Stop();
                    t = new Timer();
                    t.Interval = 1200;
                    Point relativePoint = Image_Pokeball.TransformToAncestor(canvas).Transform(new Point(0, 0));
                    ScaleTransform scale = new ScaleTransform();
                    RotateTransform rotate = new RotateTransform();
                    TransformGroup transform = new TransformGroup();
                    transform.Children.Add(scale);
                    transform.Children.Add(rotate);
                    smashPassControl.Image_Profile.RenderTransform = transform;
                    scale.CenterX = relativePoint.X + Image_Pokeball.ActualWidth / 2;
                    scale.CenterY = relativePoint.Y - Image_Pokeball.ActualHeight / 2;
                    rotate.CenterX = relativePoint.X + Image_Pokeball.ActualWidth / 2;
                    rotate.CenterY = relativePoint.Y - Image_Pokeball.ActualHeight / 2;
                    DoubleAnimation scaleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                    DoubleAnimation rotateAnimation = new DoubleAnimation(0, 360 * 7, TimeSpan.FromSeconds(1));
                    scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                    scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                    rotate.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
                    t.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
                    {
                animating = false;
                DecideFateOfCandidate(true);
                        t.Stop();
                    });
                    t.Start();
                }));
            });
            t.Start();

        }

        private void Button_Pass_Click(object sender, RoutedEventArgs e)
        {
            animating = true;
            ScaleTransform scale = new ScaleTransform();
            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.2));
            Image_Profile.RenderTransform = scale;
            Point relativePoint = Image_Profile.TransformToAncestor(canvas).Transform(new Point(0, 0));
            scale.CenterX = relativePoint.X + Image_Profile.ActualWidth / 2;
            scale.CenterY = relativePoint.Y + Image_Profile.ActualHeight / 2;
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            Timer t = new Timer();
            t.Interval = 1200;
            t.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
            {
                animating = false;
                t.Stop();
            });
            t.Start();
            DecideFateOfCandidate(false);
        }

        private void Button_Pokeball_Select(object sender, MouseButtonEventArgs e)
        {
            if (animating) { return; }
            Image_Pokeball.Visibility=Visibility.Hidden;
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

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(Image_Pokeball, (canvas.ActualWidth - Image_Pokeball.Width) / 2);
            Canvas.SetTop(Image_Pokeball, (canvas.ActualHeight - Image_Pokeball.Height) * 0.8);
        }
    }
}
