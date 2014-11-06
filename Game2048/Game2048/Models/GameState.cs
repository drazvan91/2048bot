using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game2048.Models
{
    public class GameState
    {
        public class Cell
        {
            public int value { get; set; }
        }
        public class Row
        {
            public List<Cell> Cells { get; set; }
        }
        public class Grid
        {
            public int Size { get; set; }
            public List<List<Cell>> cells { get; set; }
        }

        public Grid grid { get; set; }
        public int score { get; set; }

        internal static GameState FromJson(string gameState)
        {
            return JsonConvert.DeserializeObject<GameState>(gameState);
        }
    }
}