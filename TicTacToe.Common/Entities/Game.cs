using System.Collections.Generic;
using System.Windows.Controls;

namespace TicTacToe.Common.Entities
{
    public class Game
    {
        // 3x3 2D array (?) to store the current game state
        private List<List<Label>> _gameState;
        public List<List<Label>> GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                _gameState = value;
            }
        }

        // DecideTurnOne()  <- randomly decide who goes first, alternating after first decision
    }
}
