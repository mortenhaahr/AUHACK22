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

namespace logn
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
        }

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

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button button = (e.Source as Button);
            if (button.Name == "Save_Button")
            {
                Save_ProfileData();
            } 
            else if (button.Name == "Cancel_Button")
            {

            }
        }

        private void Save_ProfileData()
        {

        }
    }
}
