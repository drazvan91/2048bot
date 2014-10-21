using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    class Grid
    {

        internal int[] AvailableCells()
        {
 	        return null;
        }
    
        internal float smoothness()
        {
 	        throw new NotImplementedException();
        }
        internal float monotonicity2()
        {
 	        throw new NotImplementedException();
        }
        internal float maxValue()
        {
 	        throw new NotImplementedException();
        }
    
public  bool playerTurn { get; set; }
internal Grid clone()
{
 	throw new NotImplementedException();
}
internal Game2048.AI.MoveResult move(int direction)
{
 	throw new NotImplementedException();
}
internal bool isWin()
{
 	throw new NotImplementedException();
}
internal IEnumerable<GridCell> availableCells()
{
 	throw new NotImplementedException();
}}

    public class GridCell
    {
        public int X{get;set;}
        public int Y{get;set;}
        public int Value{get;set;}
    }

    class AI
    {
        Grid grid;
        public AI (Grid grid)
	    {
            this.grid = grid;
        }

        public float Eval()
        {
            int emptyCells = this.grid.AvailableCells().Length;
              float smoothWeight = 0.1f,
              mono2Weight  = 1.0f,
              emptyWeight  = 2.7f,
              maxWeight    = 1.0f;

            return this.grid.smoothness() * smoothWeight
               + this.grid.monotonicity2() * mono2Weight
               + (float)Math.Log(emptyCells) * emptyWeight
               + this.grid.maxValue() * maxWeight;
        }

        public class MoveResult{

        public  bool moved { get; set; }}


        public class SearchResult
        {
            public int move{get;set;}
        
public  int score { get; set; }
public  int positions { get; set; }
public  int cutoffs { get; set; }}

        // http://ov3y.github.io/2048-AI/
        public SearchResult Search(int depth, int alpha, int beta, int positions, int cutoffs) 
        {
              int bestScore;
              int bestMove = -1;
              SearchResult result;

                // the maxing player
            if (this.grid.playerTurn) 
            {
                bestScore = alpha;
                for (int direction = 0 ; direction<4;direction++)
                {
                    Grid newGrid = this.grid.clone();
                    if (newGrid.move(direction).moved)
                    {
                        positions++;
                        if (newGrid.isWin())
                        {
                            return new AI.SearchResult()
                            { 
                                move= direction, 
                                score= 10000, 
                                positions= positions, 
                                cutoffs= cutoffs 
                            };
                         }
                        var newAI = new AI(newGrid);

                        if (depth == 0)
                        {
                            result = new SearchResult(){ move= direction, score= newAI.eval() };
                         } 
                        else 
                        {
                            result = newAI.Search(depth-1, bestScore, beta, positions, cutoffs);
                            if (result.score > 9900) 
                            { // win
                                result.score--; // to slightly penalize higher depth from win
                            }
                            positions = result.positions;
                            cutoffs = result.cutoffs;
                        }

                        if (result.score > bestScore) {
                             bestScore = result.score;
                             bestMove = direction;
                        }
                        if (bestScore > beta) {
                            cutoffs++;
                            return new SearchResult
                            { 
                                move= bestMove, score= beta, positions= positions, cutoffs= cutoffs 
                            };
                        }
                    }
                }
            }

        else 
            { // computer's turn, we'll do heavy pruning to keep the branching factor low
    bestScore = beta;

    // try a 2 and 4 in each cell and measure how annoying it is
    // with metrics from eval
    var candidates = new List<int>();
    var cells = this.grid.availableCells();
    var scores = new Dictionary<int, List<int>>();
    
                scores[2]=new List<int>();
                scores[4] = new List<int>();

    foreach (var score in scores) 
    {
          foreach (var cell in cells) 
          {
                scores[score].push(null);
                
                var tile = new Tile(cell, parseInt(score, 10));
                this.grid.insertTile(tile);
                scores[score][i] = -this.grid.smoothness() + this.grid.islands();
                this.grid.removeTile(cell);
          }
    }



    

    // now just pick out the most annoying moves
    var maxScore = Math.max(Math.max.apply(null, scores[2]), Math.max.apply(null, scores[4]));
    for (var value in scores) { // 2 and 4
      for (var i=0; i<scores[value].length; i++) {
        if (scores[value][i] == maxScore) {
          candidates.push( { position: cells[i], value: parseInt(value, 10) } );
        }
      }
    }

    // search on each candidate
    for (var i=0; i<candidates.length; i++) {
      var position = candidates[i].position;
      var value = candidates[i].value;
      var newGrid = this.grid.clone();
      var tile = new Tile(position, value);
      newGrid.insertTile(tile);
      newGrid.playerTurn = true;
      positions++;
      newAI = new AI(newGrid);
      result = newAI.search(depth, alpha, bestScore, positions, cutoffs);
      positions = result.positions;
      cutoffs = result.cutoffs;

      if (result.score < bestScore) {
        bestScore = result.score;
      }
      if (bestScore < alpha) {
        cutoffs++;
        return { move: null, score: alpha, positions: positions, cutoffs: cutoffs };
      }
    }
  }

  return { move: bestMove, score: bestScore, positions: positions, cutoffs: cutoffs };
}

private int eval()
{
 	throw new NotImplementedException();
}

// performs a search and returns the best move
AI.prototype.getBest = function() {
  return this.iterativeDeep();
}

// performs iterative deepening over the alpha-beta search
AI.prototype.iterativeDeep = function() {
  var start = (new Date()).getTime();
  var depth = 0;
  var best;
  do {
    var newBest = this.search(depth, -10000, 10000, 0 ,0);
    if (newBest.move == -1) {
      //console.log('BREAKING EARLY');
      break;
    } else {
      best = newBest;
    }
    depth++;
  } while ( (new Date()).getTime() - start < minSearchTime);
  //console.log('depth', --depth);
  //console.log(this.translate(best.move));
  //console.log(best);
  return best
}

AI.prototype.translate = function(move) {
 return {
    0: 'up',
    1: 'right',
    2: 'down',
    3: 'left'
  }[move];
}


    }
}
