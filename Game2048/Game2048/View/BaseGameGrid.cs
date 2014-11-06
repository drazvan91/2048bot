using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Game2048.Bot;
using Game2048.Core;
using Game2048.Utils;

namespace Game2048.View
{
    public abstract class BaseGameGrid : IGameGrid
    {
        public const int SIZE = 4;

        private readonly ILogger _logger;
        protected int[,] grid;

        protected BaseGameGrid()
            : this(new DebugOutputLogger())
        {
        }

        protected BaseGameGrid(GameGrid copyGrid, ILogger logger)
            : this(logger)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    grid[i, j] = copyGrid.grid[i, j];
                }
            }
        }

        protected BaseGameGrid(ILogger logger)
        {
            _logger = logger;
            grid = new int[SIZE, SIZE];
        }

        public bool SetTile(int row, int column, int value)
        {
            if (grid[row, column] > 0)
            {
                return false;
            }

            grid[row, column] = value;
            return true;
        }

        public void Move(Direction direction)
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

        public List<MoveTransition> GetAllMoveStates()
        {
            var allMoves = new List<MoveTransition>();

            BaseGameGrid next;

            next = GetCopy();
            next.PushLeft();
            if (!IsEqualTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Left));

            next = GetCopy();
            next.PushRight();
            if (!IsEqualTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Right));

            next = GetCopy();
            next.PushUp();
            if (!IsEqualTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Up));

            next = GetCopy();
            next.PushDown();
            if (!IsEqualTo(next))
                allMoves.Add(new MoveTransition(next, Direction.Down));

            return allMoves;
        }

        public abstract double rate();

        public GridCell AddRandomTile()
        {
            List<Tuple<int, int>> allFree = getFree();
            if (allFree.Count == 0)
            {
                return null;
            }

            Tuple<int, int> free = RandomHelper.RandomElement(allFree);
            int value = RandomHelper.GetRandomTileValue();
            grid[free.Item1, free.Item2] = value;
            return new GridCell(free.Item1, free.Item2, value);
        }

        public GridCell UpdateFromState(Models.GameState state)
        {
            GridCell result = null;

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    var cell = state.grid.cells[i][j];
                    if (this.grid[j, i] == 0 && cell != null)
                    {
                        if (result != null) return null;
                        int value = (int) Math.Log(cell.value, 2);
                        this.grid[j, i] = value;
                        result = new GridCell(j, i, value);
                    }
                    else if (this.grid[j, i] > 0)
                    {
                        if (cell == null)
                        {
                            return null;
                        }
                        else if (cell.value != (int) Math.Pow(2, grid[j, i]))
                        {
                            return null;
                        }
                    }
                }
            }
            if (result == null)
            {
                result = new GridCell(0,0,-1);
            }
            return result;
        }

        public bool HasGreaterTile(int nextTileTarget)
        {
            for(int i=0;i<SIZE;i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (this.grid[i, j] >= nextTileTarget)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GridCell AddRightBottomTile()
        {
            List<Tuple<int, int>> allFree = getFree();
            if (allFree.Count == 0)
            {
                return null;
            }

            int value = RandomHelper.GetRandomTileValue();

            Tuple<int, int> free = allFree.OrderByDescending(d => d.Item1 + d.Item2).First();

            grid[free.Item1, free.Item2] = value;
            return new GridCell(free.Item1, free.Item2, value);
        }

        public void Print()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int column = 0; column < SIZE; column++)
                {
                    _logger.Write("{0} ", grid[row, column]);
                }
                _logger.WriteLine();
            }
        }

        public abstract BaseGameGrid GetCopy();

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

        public void PushUp()
        {
            for (int c = 0; c < SIZE; c++)
                PushColUp(c);
        }

        public void PushDown()
        {
            for (int c = 0; c < SIZE; c++)
                pushColDown(c);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    sb.AppendFormat("{0} ", grid[i, j]);
                }
                sb.AppendLine("| ");
            }
            return sb.ToString();
        }

        public virtual bool IsEqualTo(BaseGameGrid alt)
        {
            for (int r = 0; r < SIZE; r++)
                for (int c = 0; c < SIZE; c++)
                    if (grid[r, c] != alt.grid[r, c])
                        return false;

            return true;
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

        public List<BaseGameGrid> GetAllRandom()
        {
            var res = new List<BaseGameGrid>();
            List<Tuple<int, int>> free = getFree();

            foreach (var x in free)
            {
                BaseGameGrid next;

                next = GetCopy();
                next.grid[x.Item1, x.Item2] = 1;
                res.Add(next);

                next = GetCopy();
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


  
    }
}