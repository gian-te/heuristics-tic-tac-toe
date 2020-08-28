using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Documents;
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


        public Move MoveIntelligently(GamePlay game)
        {
            Move m;
            m = IntelligentMoveDecider.SelectSmartMove(game);
            History.Add(m);
            return m;
        }

        public Move MoveRandomly(List<Move> userHistory)
        {
            Move m = GenerateRandomMove(userHistory, out m);
            History.Add(m);

            return m;
        }

        private Move GenerateRandomMove(List<Move> userHistory, out Move m)
        {
            int usrCount, smrtCount, row, col = 0;
            m = new Move();
            do
            {
                col = Randomizer.RandomizeNumber(0, 2);
                row = Randomizer.RandomizeNumber(0, 2);
                usrCount = userHistory.Where(x => x.Row == row && x.Col == col).Count();
                smrtCount = _history.Where(x => x.Row == row && x.Col == col).Count();
            } while ((usrCount > 0) || (smrtCount > 0));
            // _history.Add(new Move() { Row = row, Col = col });
            m.Col = col;
            m.Row = row;

            return m;
        }

        /// <summary>
        /// This will take the center and/or prioritize blocking the opponent's moves using symmetrical checking
        /// </summary>
        /// <param name="gameData"></param>
        /// <param name="humanHistory"></param>
        /// <returns></returns>
        public Move MoveUsingHardCodedTable(Game gameData, List<Move> humanHistory)
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
                m.Col = 1;
                m.Row = 1;
            }
            else // if the center is already taken, either block the corners using symmetry or 
            {
                m = CheckSymmetricCombinations(humanHistory);
            }

            History.Add(m);

            return m;
        }
        /// <summary>
        /// [gian] This will check upper left (position [0,0] only) and then rotate the grid 90 degrees to the right, and check that position [0, 2], until it reaches the original position
        /// 
        /// </summary>
        private Move CheckSymmetricCombinations(List<Move> humanHistory)
        {
            Move m = null;

            if (History.Where(move => move.Row == 1 && move.Col == 1).FirstOrDefault() != null)
            {
                // this means that the agent has more chances of winning (even if the player moved first), we will go on the offensive and take the initiative to win
                var n = CheckForWinningMoves(humanHistory);
                if (n == null)
                {
                    // go on the defensive
                    n = CheckForBlockingMoves(humanHistory);
                    if (n != null)
                    {
                        if ((humanHistory.Where(move => move.Col == n.Col && move.Row == n.Row).FirstOrDefault() == null && History.Where(move => move.Col == n.Col && move.Row == n.Row).FirstOrDefault() == null))
                        {
                            return n;
                        }
                    }
                }
                else
                {
                    if ((humanHistory.Where(move => move.Col == n.Col && move.Row == n.Row).FirstOrDefault() == null && History.Where(move => move.Col == n.Col && move.Row == n.Row).FirstOrDefault() == null))
                    {
                        return n;
                    }
                }
                // intermediate moves
                //if (History.Where(move => move.Row == 0 && move.Col == 0).FirstOrDefault() == null && humanHistory.Where(move => (move.Row == 0 && move.Col == 0) || (move.Row == 2 && move.Col == 2)).FirstOrDefault() == null)
                //{
                //    m = new Move();
                //    m.Col = 0;
                //    m.Row = 0;
                //    return m;
                //}
                //if (History.Where(move => move.Row == 0 && move.Col == 2).FirstOrDefault() == null && humanHistory.Where(move => (move.Row == 0 && move.Col == 2) || (move.Row == 2 && move.Col == 0)).FirstOrDefault() == null)
                //{
                //    m = new Move();
                //    m.Col = 2;
                //    m.Row = 0;
                //    return m;
                //}
                //if (History.Where(move => move.Row == 2 && move.Col == 0).FirstOrDefault() == null && humanHistory.Where(move => move.Row == 2 && move.Col == 0).FirstOrDefault() == null)
                //{
                //    m = new Move();
                //    m.Col = 0;
                //    m.Row = 2;
                //    return m;
                //}
                //if (History.Where(move => move.Row == 2 && move.Col == 2).FirstOrDefault() == null && humanHistory.Where(move => move.Row == 2 && move.Col == 2).FirstOrDefault() == null)
                //{
                //    m = new Move();
                //    m.Col = 2;
                //    m.Row = 2;
                //    return m;
                //}
            }
            else if (humanHistory.Where(move => move.Row == 1 && move.Col == 1).FirstOrDefault() != null)
            {
                // we know that the human took the center, our job is to block his winning moves
                m = CheckForBlockingMoves(humanHistory);

            }

            if (m == null || (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null))
            {
                m = MoveRandomly(humanHistory);
            }
            return m;
        }

        private Move CheckForBlockingMoves(List<Move> humanHistory)
        {
            Move m = null;

            var lastMove = humanHistory.Last();

            m = BlockMovesOnSameRowOrCol(humanHistory);

            if (m == null)
            {

                if (lastMove.Row == 1)
                {
                    if (lastMove.Col == 0)
                    {
                        m = new Move()
                        {
                            Row = 1,
                            Col = 2
                        };
                    }
                    else if (lastMove.Col == 2)
                    {
                        m = new Move()
                        {
                            Row = 1,
                            Col = 0
                        };
                    }
                }
                else if (lastMove.Row == 0)
                {
                    if (lastMove.Col == 0)
                    {
                        // [0,0] was the player's move, counter with the opposite diagonal, [2,2]
                        m = new Move()
                        {
                            Row = 2,
                            Col = 2
                        };
                    }
                    else if (lastMove.Col == 2)
                    {
                        // [0,2] was the player's move, counter with the opposite diagonal, [2,0]
                        m = new Move()
                        {
                            Row = 2,
                            Col = 0
                        };
                    }
                }
                else if (lastMove.Row == 2)
                {
                    if (lastMove.Col == 0)
                    {
                        // [2,0] was the player's move, counter with the opposite diagonal, [0,2]
                        m = new Move()
                        {
                            Row = 0,
                            Col = 2
                        };
                    }
                    else if (lastMove.Col == 2)
                    {
                        // [2,2] was the player's move, counter with the opposite diagonal, [0,0]
                        m = new Move()
                        {
                            Row = 0,
                            Col = 0
                        };
                    }
                }
            }


            return m;
        }

        /// <summary>
        /// It just knows how to block diagonal moves, no heuristics involved here
        /// </summary>
        /// <param name="humanHistory"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private Move BlockMovesOnSameRowOrCol(List<Move> humanHistory)
        {
            Move m = new Move();
            var retVal = false;

            var numMoves = humanHistory.Count;
            if (numMoves >= 2)
            {
                foreach (var pastMove in humanHistory)
                {
                    foreach (var previousMove in humanHistory)
                    {
                        if (pastMove == previousMove)
                        {
                            continue;
                        }
                        if (pastMove.Col == previousMove.Col)
                        {
                            retVal = true;
                            for (int i = 0; i < 3; i++)
                            {
                                if (i != pastMove.Row && i != previousMove.Row)
                                {
                                    m = new Move();
                                    m.Row = i;
                                    m.Col = pastMove.Col;
                                    if (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null)
                                    {
                                        m = null;
                                        continue;
                                    }
                                    return m;
                                }
                            }

                        }
                        if (pastMove.Row == previousMove.Row)
                        {
                            retVal = true;
                            for (int i = 0; i < 3; i++)
                            {
                                if (i != pastMove.Col && i != previousMove.Col)
                                {
                                    m = new Move();
                                    m.Col = i;
                                    m.Row = pastMove.Row;
                                    if (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null)
                                    {
                                        m = null;
                                        continue;
                                    }
                                    return m;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                retVal = false;
            }

            return m;
        }

        private Move CheckForWinningMoves(List<Move> humanHistory)
        {
            Move m = null;
            var numMoves = History.Count;

            // check for vertical and horizontal winning moves
            if (numMoves >= 2)
            {
                foreach (var move in History)
                {
                    foreach (var previousMove in History)
                    {
                        if (move == previousMove)
                        {
                            continue;
                        }
                        if (move.Col == previousMove.Col)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (i != move.Row && i != previousMove.Row)
                                {
                                    m = new Move();
                                    m.Row = i;
                                    m.Col = move.Col;
                                    if (humanHistory.Where(t => t.Col == m.Col & t.Row == m.Row).FirstOrDefault() != null)
                                    {
                                        m = null;
                                        continue;
                                    }
                                }
                            }

                        }
                        if (move.Row == previousMove.Row)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (i != move.Col && i != previousMove.Col)
                                {
                                    m = new Move();
                                    m.Col = i;
                                    m.Row = move.Row;
                                    if (humanHistory.Where(t => t.Col == m.Col & t.Row == m.Row).FirstOrDefault() != null)
                                    {
                                        m = null;
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // check for diagonal winning moves
            if (m == null && History.Where(move => move.Row == 1 && move.Col == 1).FirstOrDefault() != null)
            {
                foreach (var item in History)
                {
                    if (item.Col == 0 && item.Row == 0)
                    {
                        m = new Move()
                        {
                            Col = 2,
                            Row = 2
                        };
                        if (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null)
                        {
                            m = null;
                            continue;
                        }
                    }
                    else if (item.Col == 2 && item.Row == 2)
                    {
                        m = new Move()
                        {
                            Col = 0,
                            Row = 0
                        };
                        if (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null)
                        {
                            m = null;
                            continue;
                        }
                    }
                    else if (item.Row == 0 && item.Col == 2)
                    {
                        m = new Move()
                        {
                            Col = 0,
                            Row = 2
                        };
                        if (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null)
                        {
                            m = null;
                            continue;
                        }
                    }
                    else if (item.Row == 2 && item.Col == 0)
                    {
                        m = new Move()
                        {
                            Col = 2,
                            Row = 0
                        };
                        if (humanHistory.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null || History.Where(move => move.Col == m.Col && move.Row == m.Row).FirstOrDefault() != null)
                        {
                            m = null;
                            continue;
                        }
                    }
                }
            }

            return m;
        }

    }
}
