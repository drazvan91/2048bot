using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Core
{
    /*
    public class Grid
    {
        int[,] M = new int[4, 4];

        internal IEnumerable<Position> EmptyCells()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (M[y, x] == 0)
                    {
                        yield return new Position(x, y);
                    }
                }
            }
        }

        internal double CalculateSmoothness()
        {
            var smoothness = 0d;
            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    if (this.M[y,x] != 0)
                    {
                        var value = Math.Log(this.M[y,x]) / Math.Log(2);
                        for (var direction = 1; direction <= 2; direction++)
                        {
                            Position vector = DirectionHelper.GetDirectionVector(direction);
                            Position targetCell = this.findFarthestPosition(x, y, vector).Next;

                            if (this.M[targetCell.Y, targetCell.X] > 0)
                            {
                                var target = this.M[targetCell.Y, targetCell.X];
                                var targetValue = Math.Log(target) / Math.Log(2);
                                smoothness -= Math.Abs(value - targetValue);
                            }
                        }
                    }
                }
            }
            return smoothness;
        }

        private PositionWithNext findFarthestPosition(int x, int y, Position vector)
        {
            int previousX;
            int previousY;

            // Progress towards the vector direction until an obstacle is found
            do
            {
                previousX = x;
                previousY = y;
                x = previousX + vector.X;
                y = previousY + vector.Y;
            } while (x >= 0 && x < 4 && y >= 0 && y < 4 && this.M[y, x] == 0);

            return new PositionWithNext()
            {
                X = previousX,
                Y = previousY,
                Next = new Position(x, y)
            };
        }

        internal int monotonicity2()
        {
            var marked = [];
  var queued = [];
  var highestValue = 0;
  var highestCell = {x:0, y:0};
  for (var x=0; x<4; x++) {
    marked.push([]);
    queued.push([]);
    for (var y=0; y<4; y++) {
      marked[x].push(false);
      queued[x].push(false);
      if (this.cells[x][y] &&
          this.cells[x][y].value > highestValue) {
        highestValue = this.cells[x][y].value;
        highestCell.x = x;
        highestCell.y = y;
      }
    }
  }

  increases = 0;
  cellQueue = [highestCell];
  queued[highestCell.x][highestCell.y] = true;
  markList = [highestCell];
  markAfter = 1; // only mark after all queued moves are done, as if searching in parallel

  var markAndScore = function(cell) {
    markList.push(cell);
    var value;
    if (self.cellOccupied(cell)) {
      value = Math.log(self.cellContent(cell).value) / Math.log(2);
    } else {
      value = 0;
    }
    for (direction in [0,1,2,3]) {
      var vector = self.getVector(direction);
      var target = { x: cell.x + vector.x, y: cell.y+vector.y }
      if (self.withinBounds(target) && !marked[target.x][target.y]) {
        if ( self.cellOccupied(target) ) {
          targetValue = Math.log(self.cellContent(target).value ) / Math.log(2);
          if ( targetValue > value ) {
            //console.log(cell, value, target, targetValue);
            increases += targetValue - value;
          }
        } 
        if (!queued[target.x][target.y]) {
          cellQueue.push(target);
          queued[target.x][target.y] = true;
        }
      }
    }
    if (markAfter == 0) {
      while (markList.length > 0) {
        var cel = markList.pop();
        marked[cel.x][cel.y] = true;
      }
      markAfter = cellQueue.length;
    }
  }

  while (cellQueue.length > 0) {
    markAfter--;
    markAndScore(cellQueue.shift())
  }

  return -increases;
        }

        internal int maxValue()
        {
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (M[i, j] > max)
                    {
                        max = M[i, j];
                    }
                }
            }
            return max;
        }

        public bool PlayerTurn { get; set; }
        internal Grid Clone()
        {
            Grid g = new Grid();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g.M[i, j] = this.M[i, j];
                }
            }

            g.PlayerTurn = this.PlayerTurn;

            return g;
        }
        internal Game2048.AI.MoveResult move(int direction)
        {
            throw new NotImplementedException();
        }
        internal bool isWin()
        {
            throw new NotImplementedException();
        }
        internal IList<GridCell> availableCells()
        {
            throw new NotImplementedException();
        }
        internal void insertTile(int p1, int p2, KeyValuePair<int, List<int>> score)
        {
            throw new NotImplementedException();
        }
        internal void removeTile(int p1, int p2)
        {
            throw new NotImplementedException();
        }
        internal int islands()
        {
            throw new NotImplementedException();
        }
        internal void insertTile(GridCell position)
        {
            throw new NotImplementedException();
        }
    }
}
