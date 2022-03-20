using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Text.Json;
using Newtonsoft.Json;
using GonnaCatchThemAll.DTO;
using GonnaCatchThemAll.Helpers;

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TeamSelector : UserControl
    {
        public Delegates.TransitionDelegate AcceptDelegate = () => { };
        public List<PokemonControl> pokemons = new List<PokemonControl>();

        public TeamSelector()
        {
            InitializeComponent();
            PokemonControl.handlerFunction = (PokemonControl context) =>
            {
                context.Stuck = TeamWindowInstance.AddOrRemove(context.PokemonName);
                if(TeamWindowInstance.TeamCount >= 6)
                {
                    AcceptBtn.IsEnabled = true;
                } else
                {
                    AcceptBtn.IsEnabled=false;
                }
            };
            StreamReader r = new StreamReader(@".\pokemons.json");
            string jsonString = r.ReadToEnd();
            var m = JsonConvert.DeserializeObject<List<PokemonData>>(jsonString);
            if (m != null)
            {
                foreach (PokemonData pokemon in m)
                {
                    var p = new PokemonControl();
                    p.PokemonName = pokemon.Name;
                    pokemons.Add(p);
                }
            }
            foreach (var pokemon in pokemons)
            {
                MainGrid.Children.Add(pokemon);
            }
        }

        public void RedrawRow(int width)
        {
            int mwWidth = width - 20;
            MainGrid.Width = mwWidth - (mwWidth % 120);
            int numOfCols = mwWidth / 120;
            MainGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < numOfCols; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            int numOfRows = pokemons.Count / numOfCols + (pokemons.Count % numOfCols == 0 ? 0 : 1);
            MainGrid.Height = numOfRows * 120;

            MainGrid.RowDefinitions.Clear();
            for (int i = 0; i < numOfRows; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int y = 0; y < numOfRows; y++)
            {
                for (int x = 0; x < numOfCols; x++)
                {
                    if ((y * numOfCols + x) >= pokemons.Count)
                    {
                        return;
                    }
                    Grid.SetColumn(pokemons[y * numOfCols + x], x);
                    Grid.SetRow(pokemons[y * numOfCols + x], y);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TeamWindowInstance.ToggleView();
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            AcceptDelegate();
        }
    }
}
