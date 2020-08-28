using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string smart = "X";

        private Agent _agent;
        public Agent GetInstance
        {
            get
            {
                if (_agent == null)
                {
                    _agent = new Agent();
                }
                return _agent;
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
            Move m = new Move();
            do
            {
                Random rnd = new Random();
                col = rnd.Next(3);
                row = rnd.Next(3);
                usrCount = userHistory.Where(x => x.Row == row && x.Col == col).Count();
                smrtCount = _history.Where(x => x.Row == row && x.Col == col).Count();
            } while ((usrCount > 0) || (smrtCount > 0));
            _history.Add(new Move() { Row = row, Col = col });
            m.Col = col;
            m.Row = row;
            return m;
        }
    }
}
