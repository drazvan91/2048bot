using System;
using System.Collections.Generic;
using Game2048.Core;
using Game2048.View;

namespace Game2048.Bot
{
    public class GameAi : IGameAi
    {
        private IGameGrid grid;

        public GameAi()
        {
            this.grid = new GameGrid();
        }

        public GameAi(IGameGrid grid)
        {
            this.grid = grid.GetCopy();
        }

        public Direction Move()
        {
            double bestScore = Double.MaxValue;
            Direction result = Direction.None;

            List<MoveTransition> moves = grid.GetAllMoveStates();
            foreach (MoveTransition move in moves)
            {
                double rating = Alphabetarate(move.State, 12, Double.MaxValue, Double.MinValue, false);

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
            this.grid.SetTile(cell.Row, cell.Column, cell.Value);
        }

        private double Alphabetarate(IGameGrid root, int depth, double alpha, double beta, bool player)
        {
            if (depth == 0)
            {
                return root.rate();
            }

            if (player)
            {
                List<MoveTransition> moves = root.GetAllMoveStates();

                //If we can't move, the game is over; worst penalty
                if (moves.Count == 0)
                    return Double.MaxValue;

                foreach (MoveTransition st in moves)
                {
                    alpha = Math.Min(alpha, Alphabetarate(st.State, depth - 1, alpha, beta, !player));
                    if (beta >= alpha)
                        break;
                }
                return alpha;
            }
            else
            {
                List<BaseGameGrid> moves = root.GetAllRandom();

                foreach (GameGrid st in moves)
                {
                    beta = Math.Max(beta, Alphabetarate(st, depth - 1, alpha, beta, !player));
                    if (beta <= alpha)
                        break;
                }
                return beta;
            }
        }

        public override string ToString()
        {
            return this.grid.ToString();
        }
    }
}