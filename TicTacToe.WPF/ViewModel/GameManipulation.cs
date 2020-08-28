using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TicTacToe.Common.Data;
using TicTacToe.Common.Entities;

namespace TicTacToe.WPF.ViewModel
{
    public static class GameManipulation
    {
        public static GamePlay Data = new GamePlay();
        public static bool IsWinnerExist = false;

        public static void ClearAllData()
        {
            Data.HumanHistory.Clear();
            Data.SmartAgent.History.Clear();
            Data.SmartHistory.Clear();
            Data.SmartMoves.Clear();
            IsWinnerExist = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Data.GameData.GameState[i][j].Content = null;
                }
            }
        }

        public static void ChangeLabelContent(List<List<Label>> gameState, int row, int col)
        {
            HumanPlayerMovement(gameState, row, col);
            int remSquares = 9 - (Data.SmartAgent.History.Count() + Data.HumanHistory.Count());
            if ((remSquares > 0) && (IsWinnerExist == false))
                SmartAgentMovement();
        }


        public static void HumanPlayerMovement(List<List<Label>> gameState, int row, int col)
        {
            Data.GameData.GameState = gameState;
            Data.GameData.GameState[row][col].Content = Data.Human;
            Data.HumanHistory.Add(new Move() { Row = row, Col = col });
            IsWinnerExist = CheckWinner.CheckWinningState(Data.Smart);
        }

        public static void SmartAgentMovement()
        {
            Move m = new Move();
            switch (Data.GameLevel)
            {
                case "Random": m = Data.SmartAgent.GenerateMoveRandomly(Data.GameData, Data.HumanHistory); break;
                case "HardCoded": break;
                case "Smart": m = Data.SmartAgent.GenerateMoveIntelligently(Data);  break;
            }
            Data.GameData.GameState[m.Row][m.Col].Content = Data.Smart;
            IsWinnerExist = CheckWinner.CheckWinningState(Data.Smart);
        }
    }
}
