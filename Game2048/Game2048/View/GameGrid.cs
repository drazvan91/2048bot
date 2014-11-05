using System;
using System.Collections.Generic;
using Game2048.Utils;

namespace Game2048.View
{
    public class GameGrid : BaseGameGrid, IGameGrid
    {
        public GameGrid()
        {
        }

        public GameGrid(ILogger logger) : base(logger)
        {
        }

        public GameGrid(GameGrid copyGrid)
            : this(copyGrid, new DebugOutputLogger())
        {
        }

        public GameGrid(GameGrid copyGrid, ILogger logger)
            : base(copyGrid, logger)
        {
        }


        public override double rate()
        {
            List<MoveTransition> moves = GetAllMoveStates();

            //If we can't move, the game is over; worst penalty
            if (moves.Count == 0)
                return double.MaxValue;


            int numZero = 0;
            double entropy = 0;

            var count = new Dictionary<int, int>();

            int cur;
            for (int r = 0; r < SIZE; r++)
            {
                for (int c = 0; c < SIZE; c++)
                {
                    if (grid[r, c] == 0)
                    {
                        numZero++;
                        continue;
                    }

                    if (!count.TryGetValue(grid[r, c], out cur))
                        count[grid[r, c]] = 1;
                    else
                        count[grid[r, c]] = cur + 1;
                }
            }

            int numNonZero = SIZE*SIZE - numZero;
            foreach (int k in count.Keys)
            {
                double freq = (double) count[k]/numNonZero;
                entropy -= freq*Math.Log(freq);
            }

            entropy /= Math.Log(SIZE*SIZE);

            return numNonZero + entropy;
        }

        public override BaseGameGrid GetCopy()
        {
            return new GameGrid(this);
        }

        internal static IGameGrid FromState(Models.GameState state)
        {
            var result = new GameGrid();
            for (int rowIndex = 0; rowIndex < state.grid.cells.Count; rowIndex++)
            {
                var row = state.grid.cells[rowIndex];
                for (int cellIndex = 0; cellIndex < row.Count; cellIndex++)
                {
                    var cell = row[cellIndex];
                    if (cell != null)
                    {
                        result.grid[cellIndex, rowIndex] = (int)Math.Log(cell.value, 2);
                    }
                }
            }
            return result;
        }
    }
}