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

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : UserControl
    {
        private List<Image> images;
        private List<Label> labels;
        public int TeamCount {  get; private set; } = 0;
        public TeamWindow()
        {
            InitializeComponent();
            images = new List<Image>() { Team1Img, Team2Img, Team3Img, Team4Img, Team5Img, Team6Img };
            labels = new List<Label>() { Team1Label, Team2Label, Team3Label, Team4Label, Team5Label, Team6Label };
        }
        public void ToggleView()
        {
            if(this.Visibility == Visibility.Visible)
            {
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Visibility = Visibility.Visible;
            }
        }

        public List<string> GetTeam()
        {
            return labels.Select(x => (string)x.Content).ToList();
        } 

        public bool AddOrRemove(string pokeName)
        {
            for(int i = 0; i < 6; i++)
            {
                if((string)labels[i].Content == pokeName)
                {
                    for(int k = i; k < 5; k++)
                    {
                        labels[k].Content = labels[k+1].Content;
                        images[k].Source = images[k + 1].Source;
                    }
                    TeamCount--;
                    labels[5].Content = "";
                    images[5].Source = null;
                    return false;
                }
            }
            if(TeamCount == 6)
            {
                return false;
            }
            var bi = new BitmapImage(new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\artwork\" + pokeName.ToLower().Replace(" ", "-").Replace("'", "").Replace(".", "") + ".jpg"));

            images[TeamCount].Stretch = Stretch.Uniform;
            images[TeamCount].Source = bi;

            labels[TeamCount].Content = pokeName;
            TeamCount++;
            return true;
        }
    }

}
