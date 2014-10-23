using System;
using System.Collections.Generic;
using Game2048.Core;

namespace Game2048.Tests
{
    public class GameGrid
    {
        public const int SIZE = 4;
        private readonly int[,] grid;

        public GameGrid()
        {
            grid = new int[SIZE, SIZE];
        }

        public GameGrid(GameGrid s)
        {
            grid = new int[SIZE, SIZE];

            for (int r = 0; r < SIZE; r++)
                for (int c = 0; c < SIZE; c++)
                    grid[r, c] = s.grid[r, c];
        }


        internal bool SetTile(int row, int column, int value)
        {
            if (grid[row, column] > 0)
            {
                return false;
            }

            grid[row, column] = value;
            return true;
        }

        internal void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    PushDown();
                    break;
                case Direction.Left:
                    PushLeft();
                    break;
                case Direction.Right:
                    PushRight();
                    break;
                case Direction.Up:
                    PushUp();
                    break;
            }
        }

        public void PushLeft()
        {
            for (int r = 0; r < SIZE; r++)
                pushRowLeft(r);
        }

        public void PushRight()
        {
            for (int r = 0; r < SIZE; r++)
                pushRowRight(r);
        }

        private void pushRowLeft(int r)
        {
            //Collect the items
            var items = new List<int>(SIZE);
            for (int c = 0; c < SIZE; c++)
                if (grid[r, c] != 0)
                    items.Add(grid[r, c]);

            //Consolidate duplicates
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (items[i] == items[i + 1])
                {
                    items[i]++;
                    items.RemoveAt(i + 1);
                }
            }

            //Write the data back to the row
            for (int c = 0; c < SIZE; c++)
                grid[r, c] = (c < items.Count ? items[c] : 0);
        }

        private void pushRowRight(int r)
        {
            //Collect the items
            var items = new List<int>(SIZE);
            for (int c = 0; c < SIZE; c++)
                if (grid[r, c] != 0)
                    items.Add(grid[r, c]);

            //Consolidate duplicates
            for (int i = items.Count - 1; i > 0; i--)
            {
                if (items[i] == items[i - 1])
                {
                    items[i]++;
                    items.RemoveAt(i - 1);
                    i--;
                }
            }

            //Write the data back to the row
            for (int i = 0; i < SIZE; i++)
                grid[r, SIZE - 1 - i] = (items.Count - 1 - i >= 0 ? items[items.Count - 1 - i] : 0);
        }

        public void PushUp()
        {
            for (int c = 0; c < SIZE; c++)
                PushColUp(c);
        }

        private void PushColUp(int c)
        {
            //Collect the items
            var items = new List<int>(SIZE);
            for (int r = 0; r < SIZE; r++)
                if (grid[r, c] != 0)
                    items.Add(grid[r, c]);

            //Consolidate duplicates
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (items[i] == items[i + 1])
                {
                    items[i]++;
                    items.RemoveAt(i + 1);
                }
            }

            //Write the data back to the row
            for (int r = 0; r < SIZE; r++)
                grid[r, c] = (r < items.Count ? items[r] : 0);
        }

        public void PushDown()
        {
            for (int c = 0; c < SIZE; c++)
                pushColDown(c);
        }

        private void pushColDown(int c)
        {
            //Collect the items
            var items = new List<int>(SIZE);
            for (int r = 0; r < SIZE; r++)
                if (grid[r, c] != 0)
                    items.Add(grid[r, c]);

            //Consolidate duplicates
            for (int i = items.Count - 1; i > 0; i--)
            {
                if (items[i] == items[i - 1])
                {
                    items[i]++;
                    items.RemoveAt(i - 1);
                    i--;
                }
            }

            //Write the data back to the row
            for (int i = 0; i < SIZE; i++)
                grid[SIZE - 1 - i, c] = (items.Count - 1 - i >= 0 ? items[items.Count - 1 - i] : 0);
        }

        public List<MoveTransition> GetAllMoveStates()
        {
            var allMoves = new List<MoveTransition>();

            GameGrid next;

            next = new GameGrid(this);
            next.PushLeft();
            if (!equalTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Left));

            next = new GameGrid(this);
            next.PushRight();
            if (!equalTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Right));

            next = new GameGrid(this);
            next.PushUp();
            if (!equalTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Up));

            next = new GameGrid(this);
            next.PushDown();
            if (!equalTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Down));

            return allMoves;
        }

        public bool equalTo(GameGrid alt)
        {
            for (int r = 0; r < SIZE; r++)
                for (int c = 0; c < SIZE; c++)
                    if (grid[r, c] != alt.grid[r, c])
                        return false;

            return true;
        }

        public double rate()
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

        public static double Alphabetarate(GameGrid root, int depth, double alpha, double beta, bool player)
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
                    return double.MaxValue;

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
                List<GameGrid> moves = root.getAllRandom();

                foreach (GameGrid st in moves)
                {
                    beta = Math.Max(beta, Alphabetarate(st, depth - 1, alpha, beta, !player));
                    if (beta <= alpha)
                        break;
                }
                return beta;
            }
        }

        private List<GameGrid> getAllRandom()
        {
            var res = new List<GameGrid>();
            List<Tuple<int, int>> free = getFree();

            foreach (Tuple<int, int> x in free)
            {
                GameGrid next;

                next = new GameGrid(this);
                next.grid[x.Item1, x.Item2] = 1;
                res.Add(next);

                next = new GameGrid(this);
                next.grid[x.Item1, x.Item2] = 2;
                res.Add(next);
            }

            return res;
        }

        private List<Tuple<int, int>> getFree()
        {
            var free = new List<Tuple<int, int>>();
            for (int r = 0; r < SIZE; r++)
                for (int c = 0; c < SIZE; c++)
                    if (grid[r, c] == 0)
                        free.Add(new Tuple<int, int>(r, c));

            return free;
        }

        Random random = new Random();

        internal GridCell AddRandomTile()
        {
            var allFree = this.getFree();
            if (allFree.Count == 0)
            {
                return null;
            }

            var free = allFree[random.Next(0, allFree.Count)];
            var value = random.Next(2);
            this.grid[free.Item1, free.Item2] = value;
            return new GridCell(free.Item1, free.Item2, value);
        }
    }
}