using System;
using System.Collections.Generic;
using System.IO;
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

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for PokemonControl.xaml
    /// </summary>
    public partial class PokemonControl : UserControl
    {
        public delegate void OnClickHandler(PokemonControl context);
        public static OnClickHandler handlerFunction = (PokemonControl context) => { };
        private bool clickInitated = false;
        private bool stillInSope = false;
        public bool Stuck { get; set; }
        public PokemonControl()
        {
            InitializeComponent();
        }
        private string _uri;
        public string PokemonName { get => (string)labelName.Content; set
            {
                var bi = new BitmapImage(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\artwork\" + value.ToLower().Replace(" ", "-").Replace("'", "").Replace(".", "") + ".jpg"));
          
                pokeImg.Stretch = Stretch.Uniform;
                pokeImg.Source = bi;

                labelName.Content = value;
            }
        }

        private void BorderClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickInitated = true;
            stillInSope = true;
            if (!Stuck)
            {
                BorderHover.Visibility = Visibility.Hidden;
                BorderClick.Visibility = Visibility.Visible;
            }
        }

        private void BorderClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            handlerFunction(this);
            stillInSope = false;
            clickInitated = false;
            if (!Stuck)
            {
                BorderClick.Visibility = Visibility.Hidden;
                BorderHover.Visibility = Visibility.Visible;
            }

        }

        private void BorderClick_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Stuck)
            {
                if (clickInitated)
                {
                    stillInSope = true;
                    BorderClick.Visibility = Visibility.Visible;
                }
                else
                {
                BorderHover.Visibility = Visibility.Visible;
                }
            }
        }

        private void BorderClick_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!Stuck)
            {
                BorderHover.Visibility = Visibility.Hidden;
                BorderClick.Visibility = Visibility.Hidden;
            }

            stillInSope = false;

        }
    }
}
