using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Core
{
  

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

            return this.grid.CalculateSmoothness() * smoothWeight
               + this.grid.monotonicity2() * mono2Weight
               + (float)Math.Log(emptyCells) * emptyWeight
               + this.grid.maxValue() * maxWeight;
        }

        public class MoveResult{

        public  bool moved { get; set; }}


        public class SearchResult
        {
            public int? move{get;set;}
        
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
            if (this.grid.PlayerTurn) 
            {
                bestScore = alpha;
                for (int direction = 0 ; direction<4;direction++)
                {
                    Grid newGrid = this.grid.Clone();
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
    var candidates = new List<Candidate>();
    var cells = this.grid.availableCells();
    var scores = new Dictionary<int, List<int>>();
    
                scores[2]=new List<int>();
                scores[4] = new List<int>();

    foreach (var score in scores) 
    {
          foreach (var cell in cells) 
          {
                
                this.grid.insertTile(cell.X, cell.Y, score);
                int value = -this.grid.CalculateSmoothness() + this.grid.islands();
              score.Value.Add(value);
                this.grid.removeTile(cell.X, cell.Y);
          }
    }



    

    // now just pick out the most annoying moves
    var maxScore = Math.Max(scores[2].Max(), scores[4].Max());
    foreach (var score in scores) { // 2 and 4
      for (var i=0; i<score.Value.Count; i++) {
        if (score.Value[i] == maxScore) {
          candidates.Add(new Candidate()
          { 
              position= cells[i], 
              value= score.Key 
          });
        }
      }
    }

                foreach(var candidate in candidates){
                    var position = candidate.position;
                    var value = candidate.value;
                    var newGrid = this.grid.Clone();
                    newGrid.insertTile(position);
                    newGrid.PlayerTurn=true;
                    positions ++;
                    var newAi = new AI(newGrid);
                    result = newAi.Search(depth, alpha, bestScore, positions, cutoffs);
                    positions = result.positions;
                    cutoffs=result.cutoffs;

                    if(result.score < bestScore){
                        bestScore = result.score;
                    }
                    if(bestScore < alpha){
                        cutoffs ++;
                        return new SearchResult(){
                            move = null,
                            score= alpha,
                            positions = positions,
                            cutoffs = cutoffs
                        };
                    }

                }

  return new SearchResult{ move= bestMove, score= bestScore, positions= positions, cutoffs= cutoffs };
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
