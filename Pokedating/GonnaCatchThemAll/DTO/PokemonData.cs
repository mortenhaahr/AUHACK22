using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Windows;

namespace GonnaCatchThemAll.DTO
{
    internal class PokemonData
    {
        public string Name = string.Empty;
        public string LevelingRate = string.Empty;
        public string Species = string.Empty;
        public int CatchRate = 0;
        public int Generation = 0;
        public float GenderRatio = 0;
        public Dictionary<string, int> Stats = new Dictionary<string, int>();
        public int Pokedex;
        public List<string> Type = new List<string>();
        public double Height = 0.0;
        public double Weight = 0.0;
        public double BMI = 0.0;
        public Dictionary<string, string> Evolution = new Dictionary<string, string>();
        public string Img = string.Empty;
    }
}
