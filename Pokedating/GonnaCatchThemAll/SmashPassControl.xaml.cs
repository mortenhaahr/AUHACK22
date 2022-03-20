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

        public async void RetrieveCandidates()
        {
            WebAPI.WebClient.GetCandidates(1).ContinueWith((a) =>
            {
                a.Wait();
                WebAPI.User[] users = a.Result;
                candidates = users.ToList();
            });
        }

        private async void DecideFateOfCandidate(bool smash)
        {
            await WebAPI.WebClient.PostSmash(currentUser.id, candidates[0].id, smash);
            candidates.RemoveAt(0);
            GetNextCandidate();

            smashPassControl.Image_Profile.Dispatcher.Invoke(() =>
            {
                TranslateTransform translate = new TranslateTransform(); 
                smashPassControl.Image_Profile.RenderTransform = translate; 
            });
        }

        private void GetNextCandidate()
        {
            List<string> imageList = new List<string> { candidates[0].photo0, candidates[0].photo1, candidates[0].photo2, candidates[0].photo3, candidates[0].photo4, candidates[0].photo5, candidates[0].photo6, candidates[0].photo7, candidates[0].photo8, candidates[0].photo9 };

            Image_Profile.LoadNewImages(imageList);
            smashPassControl.Dispatcher.Invoke(() =>
            {
                smashPassControl.ProfileNameLabel.Content = candidates[0].first_name;
                smashPassControl.DiscriptionTextBlock.Text = candidates[0].description;
            });
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
                        smashPassControl.Image_Pokeball.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            animating = false;
                            smashPassControl.Image_Pokeball.Visibility = Visibility.Hidden;
                            DecideFateOfCandidate(true);
                            t.Stop();
                        }));
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
