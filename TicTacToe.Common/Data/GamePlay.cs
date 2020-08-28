using System.Collections.Generic;
using TicTacToe.Common.Entities;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Utilities;

namespace TicTacToe.Common.Data
{
    public class GamePlay
    {
        /// <summary>
        /// X or O as string
        /// </summary>
        private string _agentSymbol;
        public string AgentSymbol
        {
            get
            {
                return _agentSymbol;
            }
            set
            {
                _agentSymbol = value;
            }
        }

        /// <summary>
        /// X or o as string
        /// </summary>
        private string _humanSymbol;
        public string HumanSymbol
        {
            get
            {
                return _humanSymbol;
            }
            set
            {
                _humanSymbol = value;
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

        private IntelligenceLevels _gameLevel;
        public IntelligenceLevels GameLevel
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
        public Agent Agent
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

        private Players _firstMove;
        public Players FirstMove
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
