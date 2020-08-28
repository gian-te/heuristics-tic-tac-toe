using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TicTacToe.Common.Data;
using TicTacToe.Common.Utilities;

namespace TicTacToe.Common.Entities
{
    /// <summary>
    /// Agent
    /// </summary>
    public class Agent
    {
        /*
         https://en.wikipedia.org/wiki/Tic-tac-toe#:~:text=If%20played%20optimally%20by%20both,own%20color%20in%20a%20row
         If played optimally by both players, the game always ends in a draw, making tic-tac-toe a futile game
         */

        // List<Move> history

        // public Move GenerateMoveRandomly(Game game) <- Level 0, can lose if randomized movess are bad

        // public Move GenerateMoveUsingHardcodedTable(Game game) <- Level 1, never lose, can win if opponent plays bad movess, always forces a draw if opponent plays optimally

        // public Move GenerateMoveIntelligently(Game game) <- Level 2, most likely forced draw?? heuristic could be count of possible winning scenarios. best move would be one which generates the most number of possible win scenarios

        // public Move GenerateMoveMoreIntelligently() ? <- Level 3, heuristic could be count of possible winning scenarios AND blocks opponent's winning chances

        // public void Move(Game game, Move move)

        /*
         
        -|-|- 
        X|O|-  
        -|-|-

         */

        // Symbol <- X or O
        private readonly List<Move> _history = new List<Move>();
        public List<Move> History
        {
            get
            {
                return _history;
            }
        }


        public Move GenerateMoveIntelligently(GamePlay game)
        {
            Move m;
            m = IntelligentMoveDecider.SelectSmartMove(game);
            History.Add(new Move() { Row = m.Row, Col = m.Col });
            return m;
        }

        public Move GenerateMoveRandomly(Game game, List<Move> userHistory)
        {
            int usrCount, smrtCount, row = -1, col = -1;
            var m = new Move();
            do
            {
                col = Randomizer.RandomizeNumber(0, 2);
                row = Randomizer.RandomizeNumber(0, 2);
                usrCount = userHistory.Where(x => x.Row == row && x.Col == col).Count();
                smrtCount = _history.Where(x => x.Row == row && x.Col == col).Count();
            } while ((usrCount > 0) || (smrtCount > 0));
            _history.Add(new Move() { Row = row, Col = col });
            m.Col = col;
            m.Row = row;
            History.Add(m);

            return m;
        }

        /// <summary>
        /// This will take the center and/or prioritize blocking the opponent's moves using symmetrical checking
        /// </summary>
        /// <param name="gameData"></param>
        /// <param name="humanHistory"></param>
        /// <returns></returns>
        public Move GenerateMoveUsingHardCodedTable(Game gameData, List<Move> humanHistory)
        {
          
            /*
           -|-|- 
           X|O|-  
           -|-|-
            */
            var m = new Move();
            // prioritize taking the center
            if (humanHistory.Where(move => move.Col == 1 && move.Row == 1).FirstOrDefault() == null && History.Where(move => move.Col == 1 && move.Row == 1).FirstOrDefault() == null)
            {
                // literally hardcoded :D
                m.Col = 1;
                m.Row = 1;
                return m;
            }
            else // if the center is already taken, either block the corners using symmetry or 
            {
                // implement invert and rotate implementation for symmetry and inverse checking or just hard code?
                m = CheckSymmetricCornerCombinations(gameData, 0, 0); // rotates and checks all possible combinations for [0,0] and rotates again to check the next symmetric configurations, [0,2], [2,2], [2,0] 
                // m = CheckSymmetricCombinations(0, 1);

            }

            History.Add(m);

            return m;
        }


        /// <summary>
        /// [gian] This will check upper left (position [0,0] only) and then rotate the grid 90 degrees to the right, and check that position [0, 2], until it reaches the original position
        /// 
        /// </summary>
        private Move CheckSymmetricCornerCombinations(Game gameData, int initialRowIdx, int initialColIdx)
        {
            Dictionary<Tuple<int, int>, bool> moveDictionary = new Dictionary<Tuple<int, int>, bool>(); // memoization of moves that have been checked

            var m = new Move();
            // Rotate0(), initial
                // check [0,0] diagonal, vertical, and horizontal combinations, block if there are winning moves for the opponent
                // check if the agent should block the human's winning move in this current config, based on their 
                
            // Rotate1(), rotate 90 deg to the right
                // check 0,2 diagonal, vertical, and horizontal combinations, block if there are winning moves for the opponent

            // Rotate2()
                // check 2,2 diagonal, vertical, and horizontal combinations, block if there are winning moves for the opponent

            // Rotate3()
                // check 2,0 diagonal, vertical, and horizontal combinations, block if there are winning moves for the opponent




            return m;
        }
    }
}
