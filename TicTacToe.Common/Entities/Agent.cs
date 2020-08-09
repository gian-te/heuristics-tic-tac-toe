using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // MoveRandomly() <- Level 0, can lose if randomized movess are bad

        // MoveUsingHardcodedTable() <- Level 1, never lose, can win if opponent plays bad movess, always forces a draw if opponent plays optimally

        // MoveIntelligently() <- Level 2, most likely forced draw?? heuristic could be count of possible winning scenarios. best move would be one which generates the most number of possible win scenarios
       
        /*
         
        -|-|- 
        X|O|-  
        -|-|-

         */

        // MoveMoreIntelligently() ? <- Level 3, heuristic could be count of possible winning scenarios AND blocks opponent's winning chances

        // Symbol <- X or O

    }
}
