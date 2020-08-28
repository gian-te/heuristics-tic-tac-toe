using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TicTacToe.Common.Data;
using TicTacToe.Common.Entities;

namespace TicTacToe.Common.Utilities
{
    public static class IntelligentMoveDecider
    {
        private static GamePlay gameData;

        public static Move SelectSmartMove(GamePlay game)
        {
            Move m = null;
            int col, row;
            gameData = game;
            if (FilterSmartMove(out col, out row))
            {
                m = new Move() { Row = row, Col = col};
            }
            else
                MessageBox.Show("No possible moves can be generated.");
            return m;
        }

        private static bool FilterSmartMove(out int col, out int row)
        {
            if (GetNearWinMove(out col, out row))
                return true;

            if (BlockOpponentsWinMove(out col, out row))
                return true;

            if (SelectMoveBasedOnMinMax(out col, out row))
                return true;

            if (SelectRandomMove(out col, out row))
                return true;

            return false;
        }


        public static void FilterPosActionRow()
        {
            Dictionary<Move, int> tmp = new Dictionary<Move, int>();
            for (int i = 0; i < 3; i++)
            {
                int opCount = 0;
                for (int a = 0; a < 3; a++)
                {
                    if (gameData.GameData.GameState[i][a].Content != null)
                    {
                        if (gameData.GameData.GameState[i][a].Content.ToString() == gameData.HumanSymbol)
                        {
                            opCount++;
                        }
                    }
                    if (gameData.GameData.GameState[i][a].Content == null)
                        tmp.Add(new Move() { Row = i, Col = a }, 0);
                }

                if ((opCount == 0) && (tmp.Count() > 0))
                {
                    foreach (var t in tmp)
                    {
                        gameData.SmartMoves.Add(new Move() { Row = t.Key.Row, Col = t.Key.Col }, t.Value);
                    }
                }
                tmp.Clear();
            }
        }

        public static void FilterPosActionCol()
        {
            Dictionary<Move, int> temp = new Dictionary<Move, int>();
            bool hasUserVal = false;
            for (int i = 0; i < 3; i++)
            {
                for (int a = 0; a < 3; a++)
                {
                    if (gameData.GameData.GameState[a][i].Content != null)
                    {
                        if (gameData.GameData.GameState[a][i].Content.ToString() == gameData.HumanSymbol)
                            hasUserVal = true;
                    }
                    if (gameData.GameData.GameState[a][i].Content == null)
                        temp.Add(new Move() { Row = a, Col = i }, 0);
                }

                if ((hasUserVal == true) && (temp.Count() > 0))
                {
                    foreach (var t in temp)
                    {
                        foreach (var postMove in gameData.SmartMoves)
                        {
                            if ((postMove.Key.Col == t.Key.Col)
                                && (postMove.Key.Row == t.Key.Row))
                            {
                                gameData.SmartMoves.Remove(postMove.Key);
                                break;
                            }
                        }
                    }
                }

                if ((hasUserVal == false) && (temp.Count() > 0))
                {
                    foreach (var t in temp)
                    {
                        bool IsExists = false;
                        foreach (var postMove in gameData.SmartMoves)
                        {
                            if ((postMove.Key.Col == t.Key.Col)
                                && (postMove.Key.Row == t.Key.Row))
                            {
                                IsExists = true;
                            }
                        }

                        if (IsExists == false)
                        {
                            gameData.SmartMoves.Add(new Move() { Row = t.Key.Row, Col = t.Key.Col }, t.Value);
                        }
                    }
                }
                hasUserVal = false;
                temp.Clear();
            }

            if (gameData.SmartMoves.Count() == 0)
            {
                FilterPosActionRow();
            }
        }

        public static void FilterPosActionDiag()
        {
            int opCount = 0;
            Dictionary<Move, int> tmp = new Dictionary<Move, int>();
            for (int i = 0; i < 3; i++)
            {
                if (gameData.GameData.GameState[i][i].Content != null)
                {
                    if (gameData.GameData.GameState[i][i].Content.ToString() != gameData.AgentSymbol)
                        opCount++;
                }
                else
                {
                    tmp.Add(new Move() { Row = i, Col = i }, 0);
                }
            }

            if ((opCount == 0) && (tmp.Count() > 0))
            {
                foreach (var t in tmp)
                    gameData.SmartMoves.Add(new Move() { Col = t.Key.Col, Row = t.Key.Row }, t.Value);
            }

            tmp.Clear();
            opCount = 0;
            int x = 2;
            for (int a = 0; a < 3; a++)
            {
                if (gameData.GameData.GameState[a][x].Content != null)
                {
                    if (gameData.GameData.GameState[a][x].Content.ToString() != gameData.AgentSymbol)
                        opCount++;
                }
                else
                {
                    tmp.Add(new Move() { Row = a, Col = x }, 0);
                }
            }
            if ((opCount == 0) && (tmp.Count() > 0))
            {
                foreach (var t in tmp)
                    gameData.SmartMoves.Add(new Move() { Col = t.Key.Col, Row = t.Key.Row }, t.Value);
            }
        }

        public static void CheckOpponentsDiagMove(out int col, out int row, string symbol)
        {
            col = -1;
            row = -1;
            int count = 0, empCount = 0;
            int tmpRow = -1, tmpCol = -1;
            for (int i = 0; i < 3; i++)
            {
                if (gameData.GameData.GameState[i][i].Content != null)
                {
                    if (gameData.GameData.GameState[i][i].Content.ToString() == symbol)
                    {
                        count++;
                    }
                }
                else
                {
                    empCount++;
                    tmpRow = i;
                    tmpCol = i;
                }
            }
            if ((count == 2) && (empCount > 0))
            {
                row = tmpRow;
                col = tmpCol;
                return;
            }

            count = 0;
            empCount = 0;
            int x = 2;
            for (int a = 0; a < 3; a++)
            {

                if (gameData.GameData.GameState[a][x].Content != null)
                {
                    if (gameData.GameData.GameState[a][x].Content.ToString() == symbol)
                    {
                        count++;
                    }
                }
                else
                {
                    empCount++;
                    tmpRow = a;
                    tmpCol = x;
                }
                x--;
            }
            if ((count == 2) && (empCount > 0))
            {
                row = tmpRow;
                col = tmpCol;
                return;
            }
        }

        public static void CheckOpponentsRowMove(out int col, out int row, string symbol)
        {
            col = -1;
            row = -1;
            for (int i = 0; i < 3; i++)
            {
                int count = gameData.GameData.GameState[i].Where(x => x.Content != null && x.Content.ToString() == symbol).Count();
                int empCount = gameData.GameData.GameState[i].Where(x => x.Content == null).Count();
                if ((count == 2) && (empCount > 0))
                {
                    row = i;
                    col = gameData.GameData.GameState[i].FindIndex(x => x.Content == null);
                    break;
                }
            }
        }

        public static void CheckOpponentsColMove(out int col, out int row, string symbol)
        {
            col = -1;
            row = -1;
            bool IsBlockNeeded = false;
            int opCount = 0;
            int noNullCount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int a = 0; a < 3; a++)
                {
                    if (gameData.GameData.GameState[a][i].Content != null)
                    {
                        noNullCount++;
                        if (gameData.GameData.GameState[a][i].Content.ToString() == symbol)
                            opCount++;
                    }

                    if (gameData.GameData.GameState[a][i].Content == null)
                    {
                        row = a; col = i;
                    }
                }

                if ((opCount == 2) && (noNullCount != 3))
                {
                    IsBlockNeeded = true;
                    break;
                }
                opCount = 0;
                noNullCount = 0;
            }

            if (IsBlockNeeded == false)
            {
                row = -1; col = -1;
            }
        }

        public static bool BlockOpponentsWinMove(out int col, out int row)
        {
            CheckOpponentsDiagMove(out col, out row, gameData.HumanSymbol);
            if ((col != -1) && (row != -1))
                return true;

            CheckOpponentsRowMove(out col, out row, gameData.HumanSymbol);
            if ((col != -1) && (row != -1))
                return true;

            CheckOpponentsColMove(out col, out row, gameData.HumanSymbol);
            if ((col != -1) && (row != -1))
                return true;

            return false;
        }

        public static bool SelectRandomMove(out int col, out int row)
        {
            col = -1;
            row = -1;
            for (int i = 0; i < 3; i++)
            {
                var IsNull = gameData.GameData.GameState[i].Where(x => x.Content == null).Count();
                if (IsNull != 0)
                {
                    col = gameData.GameData.GameState[i].FindIndex(x => x.Content == null);
                    row = i;
                    return true;
                }
            }
            return false;
        }

        public static bool GetNearWinMove(out int col, out int row)
        {
            CheckOpponentsDiagMove(out col, out row, gameData.AgentSymbol);
            if ((col != -1) && (row != -1))
                return true;

            CheckOpponentsRowMove(out col, out row, gameData.AgentSymbol);
            if ((col != -1) && (row != -1))
                return true;

            CheckOpponentsColMove(out col, out row, gameData.AgentSymbol);
            if ((col != -1) && (row != -1))
                return true;

            return false;
        }

        public static bool SelectMoveBasedOnMinMax(out int col, out int row)
        {
            col = -1;
            row = -1;
            gameData.SmartMoves.Clear();
            FilterPosActionRow();
            FilterPosActionCol();
            RemoveInvalidMoves();

            if (gameData.SmartMoves.Count() > 0)
            {
                ComputeWinningMove();
                CheckNextMove(out row, out col);
                if ((col == -1) && (row == -1))
                    return false;
            }
            else
                return false;
            return true;
        }

        public static void RemoveInvalidMoves()
        {
            foreach (Move c in gameData.HumanHistory)
            {
                if (gameData.SmartMoves.ContainsKey(c))
                    gameData.SmartMoves.Remove(c);
            }

            foreach (Move c in gameData.AgentHistory)
            {
                if (gameData.SmartMoves.ContainsKey(c))
                    gameData.SmartMoves.Remove(c);
            }
        }

        public static void CheckNextMove(out int row, out int col)
        {
            row = -1;
            col = -1;
            var maxValue = gameData.SmartMoves.Values.Max();
            var myKey = gameData.SmartMoves.FirstOrDefault(x => x.Value == maxValue).Key;
            row = myKey.Row;
            col = myKey.Col;
        }

        public static void ComputeWinningMove()
        {
            Dictionary<Move, int> tmp = new Dictionary<Move, int>();
            foreach (KeyValuePair<Move, int> pair in gameData.SmartMoves)
            {
                gameData.GameData.GameState[pair.Key.Row][pair.Key.Col].Content = gameData.AgentSymbol;
                int smCountWin = CountWinMoves(gameData.AgentSymbol);
                int opCountWin = CountWinMoves(gameData.HumanSymbol);
                int diff = smCountWin - opCountWin;
                gameData.GameData.GameState[pair.Key.Row][pair.Key.Col].Content = null;
                tmp.Add(new Move() { Row = pair.Key.Row, Col = pair.Key.Col }, diff);
            }
            gameData.SmartMoves.Clear();
            gameData.SmartMoves = tmp;
        }

        public static int CountWinMoves(string symbol)
        {
            int winMoves;
            winMoves = CountRowWinMoves(symbol);
            winMoves += CountColWinMoves(symbol);
            winMoves += CountDiagWinMoves(symbol);
            return winMoves;
        }

        public static int CountRowWinMoves(string symbol)
        {
            int winCount = 0;
            for (int i = 0; i < 3; i++)
            {
                int symCount = gameData.GameData.GameState[i].Where(x => x.Content != null && x.Content.ToString() == symbol).Count();
                int opCount = gameData.GameData.GameState[i].Where(x => x.Content != null
                            && x.Content.ToString() != symbol).Count();
                if ((symCount > 0) && (opCount == 0))
                    winCount++;
            }
            return winCount;
        }

        public static int CountColWinMoves(string symbol)
        {
            int winCount = 0;
            for (int i = 0; i < 3; i++)
            {
                int symCount = 0, opCount = 0;
                for (int a = 0; a < 3; a++)
                {
                    if (gameData.GameData.GameState[a][i].Content != null)
                    {
                        if (gameData.GameData.GameState[a][i].Content.ToString() == symbol)
                        {
                            symCount++;
                        }
                    }
                    else if ((gameData.GameData.GameState[a][i].Content != null)
                        && (gameData.GameData.GameState[a][i].Content.ToString() != symbol))
                    {
                        opCount++;
                    }
                }

                if ((symCount > 0) && (opCount == 0))
                    winCount++;
            }
            return winCount;
        }

        public static int CountDiagWinMoves(string symbol)
        {
            int winCount = 0;
            int symCount = 0, opCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (gameData.GameData.GameState[i][i].Content != null)
                {
                    if (gameData.GameData.GameState[i][i].Content.ToString() == symbol)
                    {
                        symCount++;
                    }
                }
                else if ((gameData.GameData.GameState[i][i].Content != null)
                    && (gameData.GameData.GameState[i][i].Content.ToString() != symbol))
                {
                    opCount++;
                }
            }

            if ((symCount > 0) && (opCount == 0))
                winCount++;

            symCount = 0;
            opCount = 0;
            int x = 2;
            for (int a = 0; a < 3; a++)
            {
                if ((gameData.GameData.GameState[a][x].Content != null) &&
                   (gameData.GameData.GameState[a][x].Content.ToString() == symbol))
                {
                    symCount++;
                }
                else if ((gameData.GameData.GameState[a][x].Content != null)
                    && (gameData.GameData.GameState[a][x].Content.ToString() != symbol))
                {
                    opCount++;
                }
                x--;
            }

            if ((symCount > 0) && (opCount == 0))
                winCount++;

            return winCount;
        }
    }
}
