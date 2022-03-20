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
using Microsoft.Win32;
using GonnaCatchThemAll.Helpers;
using System.Text.Json;

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Delegates.TransitionDelegate CancelDelegate = () => { };
        public Delegates.UserDelegate SaveDelegate = (WebAPI.User user) => { };
        public Profile()
        {
            InitializeComponent();
            instance = this;
        }
        public static Profile instance = null;
        public WebAPI.User user { get; set; }

        private void ageSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if ((sender as Slider).SelectionEnd != (sender as Slider).SelectionStart)
            {
                if (e.NewValue > (sender as Slider).SelectionEnd || e.NewValue < (sender as Slider).SelectionStart)
                {
                    (sender as Slider).Value = e.OldValue;
                }
            }
        }

        private void thumbLeft_DragDelta_ageSlider(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double left = Canvas.GetLeft(thumbLeft_ageSlider);
            double right = Canvas.GetLeft(thumbRight_ageSlider);
            if (left + e.HorizontalChange < right && left + e.HorizontalChange > 0)
            {
                Canvas.SetLeft(thumbLeft_ageSlider, left + e.HorizontalChange);
                Canvas.SetLeft(ageSlider_lowerLabel, Canvas.GetLeft(thumbLeft_ageSlider) + (thumbLeft_ageSlider.Width/2) - (ageSlider_lowerLabel.Width/2));
                ageSlider.SelectionStart = Math.Round((left + e.HorizontalChange) / ageSlider.Width * (ageSlider.Maximum - ageSlider.Minimum) + ageSlider.Minimum);
                if (ageSlider.Value < ageSlider.SelectionStart)
                {
                    ageSlider.Value = ageSlider.SelectionStart;
                }
            }
        }

        private void thumbRight_DragDelta_ageSlider(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double left = Canvas.GetLeft(thumbLeft_ageSlider);
            double right = Canvas.GetLeft(thumbRight_ageSlider);
            if (right + e.HorizontalChange > left && right + e.HorizontalChange < distSlider.Width)
            {
                Canvas.SetLeft(thumbRight_ageSlider, right + e.HorizontalChange);
                Canvas.SetLeft(ageSlider_higherLabel, Canvas.GetLeft(thumbRight_ageSlider) + (thumbRight_ageSlider.Width/2) - (ageSlider_higherLabel.Width/2));
                ageSlider.SelectionEnd = Math.Round((right + e.HorizontalChange) / ageSlider.Width * (ageSlider.Maximum - ageSlider.Minimum) + ageSlider.Minimum);
                if (ageSlider.Value > ageSlider.SelectionEnd)
                {
                    ageSlider.Value = ageSlider.SelectionEnd;
                }
            }
        }

        private void distSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if ((sender as Slider).SelectionEnd != (sender as Slider).SelectionStart)
            {
                if (e.NewValue > (sender as Slider).SelectionEnd || e.NewValue < (sender as Slider).SelectionStart)
                {
                    (sender as Slider).Value = e.OldValue;
                }
            }
        }

        private void thumbLeft_DragDelta_distSlider(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double left = Canvas.GetLeft(thumbLeft_distSlider);
            double right = Canvas.GetLeft(thumbRight_distSlider);
            if (left + e.HorizontalChange < right && left + e.HorizontalChange > 0)
            {
                Canvas.SetLeft(thumbLeft_distSlider, left + e.HorizontalChange);
                Canvas.SetLeft(distSlider_lowerLabel, Canvas.GetLeft(thumbLeft_distSlider) + (thumbLeft_distSlider.Width/2) - (distSlider_lowerLabel.Width/2));
                distSlider.SelectionStart = (left + e.HorizontalChange) / distSlider.Width * (distSlider.Maximum - distSlider.Minimum) + distSlider.Minimum;
                if (distSlider.Value < distSlider.SelectionStart)
                {
                    distSlider.Value = distSlider.SelectionStart;
                }
            }
        }

        private void thumbRight_DragDelta_distSlider(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double left = Canvas.GetLeft(thumbLeft_distSlider);
            double right = Canvas.GetLeft(thumbRight_distSlider);
            if (right + e.HorizontalChange > left && right + e.HorizontalChange < distSlider.Width)
            {
                Canvas.SetLeft(thumbRight_distSlider, right + e.HorizontalChange);
                Canvas.SetLeft(distSlider_higherLabel, Canvas.GetLeft(thumbRight_distSlider) + (thumbRight_distSlider.Width/2) - (distSlider_higherLabel.Width/2));
                distSlider.SelectionEnd = (right + e.HorizontalChange) / distSlider.Width * (distSlider.Maximum - distSlider.Minimum) + distSlider.Minimum;
                if (distSlider.Value > ageSlider.SelectionEnd)
                {
                    distSlider.Value = distSlider.SelectionEnd;
                }
            }
        }

        private void Bio_Textbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if ((e.Source as TextBox).Foreground == Brushes.Black)
            {
                return;
            }
            (e.Source as TextBox).Foreground = Brushes.Black;
            (e.Source as TextBox).Text = "";
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            (e.Source as Image).Opacity = 0.7;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (e.Source as Image).Opacity = 1;
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                (e.Source as Image).Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void Save_ProfileData()
        {
            user.first_name = FirstName_TextBox.Text;
            user.last_name = LastName_TextBox.Text;
            user.age = int.Parse(Age_TextBox.Text);
            switch (Gender_ComboBox.Text)
            {
                case "Other":
                    {
                        user.gender = 0;
                    }
                    break;
                case "Male":
                    {
                        user.gender = 1;
                    }
                    break;
                case "Female":
                    {
                        user.gender = 2;
                    }
                    break;
            }
            user.description = Bio_Textbox.Text;
            user.age_from = (int)Math.Round(ageSlider.SelectionStart);
            user.age_to = (int)Math.Round(ageSlider.SelectionEnd);
            user.search_radius = (int)Math.Round(distSlider.SelectionEnd);
            user.photo0 = null; //Image0.Source.ToString();
            user.photo1 = null; //Image1.Source.ToString();
            user.photo2 = null; //Image2.Source.ToString();
            user.photo3 = null; //Image3.Source.ToString();
            user.photo4 = null; //Image4.Source.ToString();
            user.photo5 = null; //Image5.Source.ToString();
            user.photo6 = null; //Image6.Source.ToString();
            user.photo7 = null; //Image7.Source.ToString();
            user.photo8 = null; //Image8.Source.ToString();
            user.last_seen_lat = 56.171089;
            user.last_seen_long = 10.189372;
            List<int> lookFor = new List<int>();
            if (CheckBoxOther.IsChecked == true)
            {
                lookFor.Add(0);
            }
            if (CheckBoxMale.IsChecked == true)
            {
                lookFor.Add(1);
            }
            if (CheckBoxFemale.IsChecked == true)
            {
                lookFor.Add(2);
            }
            user.looking_for = lookFor.ToArray();
            var result = WebAPI.WebClient.Post<WebAPI.User>("users/", user).ContinueWith((task) =>
            {
                task.Wait();
                user = JsonSerializer.Deserialize<WebAPI.User>(task.Result);
                SaveDelegate(user);
            });
            
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            CancelDelegate();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save_ProfileData();
        }
    }
}
