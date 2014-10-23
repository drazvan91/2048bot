using System.Collections.Generic;
using Game2048.Core;

namespace Game2048.Tests
{
    internal class GameAi
    {
        private GameGrid grid;

        public GameAi()
        {
            this.grid = new GameGrid();
        }

        public GameAi(GameGrid grid)
        {
            this.grid = new GameGrid(grid);
        }

        internal Direction Move()
        {
            double bestScore = double.MaxValue;
            Direction result = Direction.None;

            List<MoveTransition> moves = grid.GetAllMoveStates();
            foreach (MoveTransition move in moves)
            {
                double rating = GameGrid.Alphabetarate(move.State, 4, double.MaxValue, double.MinValue, false);

                if (rating < bestScore)
                {
                    bestScore = rating;
                    result = move.Direction;
                }
            }

            this.grid.Move(result);

            return result;
        }

        public void AddTile(GridCell cell)
        {
            this.grid.SetTile(cell.Column, cell.Row, cell.Value);
        }
    }
}