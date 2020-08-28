using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Common.Entities
{
    // store the human player's info, actions (?)
    public class Player
    {
        private List<Move> _history = new List<Move>();
        public List<Move> History
        {
            get
            {
                return _history;
            }
            set
            {
                _history = value;
            }
        }
        // public void Move(Game game, Move move)
    }
}
