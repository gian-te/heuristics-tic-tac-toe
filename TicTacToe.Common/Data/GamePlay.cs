using System.Collections.Generic;
using TicTacToe.Common.Entities;

namespace TicTacToe.Common.Data
{
    public class GamePlay
    {
        private string _smart;
        public string Smart
        {
            get
            {
                return _smart;
            }
            set
            {
                _smart = value;
            }
        }


        private string _human;
        public string Human
        {
            get
            {
                return _human;
            }
            set
            {
                _human = value;
            }
        }

        private List<Move> _smartHistory = new List<Move>();
        public List<Move> SmartHistory
        {
            get
            {
                return _smartHistory;
            }
            set
            {
                _smartHistory = value;
            }
        }

        private List<Move> _humanHistory = new List<Move>();
        public List<Move> HumanHistory
        {
            get
            {
                return _humanHistory;
            }
            set
            {
                _humanHistory = value;
            }
        }

        private Dictionary<Move, int> _smartMoves = new Dictionary<Move, int>();
        public Dictionary<Move, int> SmartMoves
        {
            get
            {
                return _smartMoves;
            }
            set
            {
                _smartMoves = value;
            }
        }

        private string _gameLevel;
        public string GameLevel
        {
            get
            {
                return _gameLevel;
            }
            set
            {
                _gameLevel = value;
            }
        }

        private readonly Agent _agent = new Agent();
        public Agent SmartAgent
        {
            get
            {
                return _agent;
            }
        }

        private readonly Game _game = new Game();
        public Game GameData
        {
            get
            {
                return _game;
            }
        }

        private string _firstMove;
        public string FirstMove
        {
            get
            {
                return _firstMove;
            }
            set
            {
                _firstMove = value;
            }
        }
    }
}
