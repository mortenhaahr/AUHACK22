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
using System.Threading;

namespace GonnaCatchThemAll {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TeamSelector : UserControl {
        int curWidth = 0;
        public Delegates.TransitionDelegate AcceptDelegate = () => { };
        public List<PokemonControl> pokemons = new List<PokemonControl>();
        public List<PokemonControl> pokemonsPresented = new List<PokemonControl>();

        public TeamSelector() {
            InitializeComponent();
            PokemonControl.handlerFunction = (PokemonControl context) => {
                context.Stuck = TeamWindowInstance.AddOrRemove(context.PokemonName);
                if (TeamWindowInstance.TeamCount >= 6) {
                    AcceptBtn.IsEnabled = true;
                } else {
                    AcceptBtn.IsEnabled = false;
                }
            };
            StreamReader r = new StreamReader(@".\pokemons.json");
            string jsonString = r.ReadToEnd();
            var m = JsonConvert.DeserializeObject<List<PokemonData>>(jsonString);
            if (m != null) {
                foreach (PokemonData pokemon in m) {
                    var p = new PokemonControl();
                    p.PokemonName = pokemon.Name;
                    pokemons.Add(p);
                }
            }
            pokemonsPresented = pokemons;
            foreach (var pokemon in pokemonsPresented) {
                MainGrid.Children.Add(pokemon);
            }
        }

        public void RedrawRow(int width) {
            curWidth = width;
            int mwWidth = width - 20;
            MainGrid.Width = mwWidth - (mwWidth % 120);
            int numOfCols = mwWidth / 120;
            MainGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < numOfCols; i++) {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            int numOfRows = pokemonsPresented.Count / numOfCols + (pokemonsPresented.Count % numOfCols == 0 ? 0 : 1);
            MainGrid.Height = numOfRows * 120;

            MainGrid.RowDefinitions.Clear();
            for (int i = 0; i < numOfRows; i++) {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int y = 0; y < numOfRows; y++) {
                for (int x = 0; x < numOfCols; x++) {
                    if ((y * numOfCols + x) >= pokemonsPresented.Count) {
                        return;
                    }
                    Grid.SetColumn(pokemonsPresented[y * numOfCols + x], x);
                    Grid.SetRow(pokemonsPresented[y * numOfCols + x], y);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            TeamWindowInstance.ToggleView();
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e) {
            WebAPI.WebClient.Post<WebAPI.PostPokeProfile>("poke_profiles/user/", new() { pokemons = TeamWindowInstance.GetTeam().ToArray() }, Profile.instance.user.id).ContinueWith(t => {
                t.Wait();
                Thread.Sleep(1000);
                AcceptDelegate();
            });
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            var test = pokemons.Where(poke => (SearchTextBox.Text.Length <= poke.PokemonName.Length && poke.PokemonName.Substring(0, SearchTextBox.Text.Length).ToLower() == SearchTextBox.Text.ToLower()));
            if (test.Count() != 0) {
                pokemonsPresented = test.ToList();
                MainGrid.Children.Clear();
                foreach (var pokemon in pokemonsPresented) {
                    MainGrid.Children.Add(pokemon);
                }
            } else {
                pokemonsPresented.Clear();
            }
            RedrawRow(curWidth);

        }
    }
}
