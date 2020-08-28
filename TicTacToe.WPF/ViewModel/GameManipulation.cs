using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TicTacToe.Common.Data;
using TicTacToe.Common.Entities;
using TicTacToe.Common.Utilities;

namespace TicTacToe.WPF.ViewModel
{
    public static class GameManipulation
    {
        public static GamePlay Data = new GamePlay();
        public static bool IsWinnerExist = false;

        public static void ClearAllData()
        {
            try
            {
                Data.HumanHistory.Clear();
                Data.Agent.History.Clear();
                Data.AgentHistory.Clear();
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
            catch 
            {
                // swallow if the user resetted without any moves
            }
           
        }

        public static void EvaluateGame(List<List<Label>> gameState, int row, int col)
        {
            // assigns the player's move to the grid behind the scenes
            HumanPlayerMovement(gameState, row, col);
            int remSquares = 9 - (Data.Agent.History.Count() + Data.HumanHistory.Count());
            if ((remSquares > 0) && (IsWinnerExist == false))
            {
                // [gian] handle non-smart moves here ?
                if (Data.GameLevel == IntelligenceLevels.Smart)
                {
                    // use heuristics
                    GenerateIntelligentMove();
                }
                else if (Data.GameLevel == IntelligenceLevels.Random)
                {
                    // use randomizer
                    GenerateRandomMove();
                }
                else if (Data.GameLevel == IntelligenceLevels.Hardcoded)
                {
                    // generate move using hardcoded table
                    GenerateHardCodedMove();
                }
            }
        }


        public static void HumanPlayerMovement(List<List<Label>> gameState, int row, int col)
        {
            Data.GameData.GameState = gameState;
            // assign the move to the grid
            Data.GameData.GameState[row][col].Content = Data.HumanSymbol;
            Data.HumanHistory.Add(new Move() { Row = row, Col = col });
            IsWinnerExist = CheckWinner.CheckWinningState(Data.HumanSymbol);
        }

        public static void GenerateRandomMove()
        {
            Move m = new Move();
            m = Data.Agent.GenerateMoveRandomly(Data.GameData, Data.HumanHistory);
            Data.GameData.GameState[m.Row][m.Col].Content = Data.AgentSymbol;
            IsWinnerExist = CheckWinner.CheckWinningState(Data.AgentSymbol);
        }

        public static void GenerateHardCodedMove()
        {
            Move m = new Move();
            m = Data.Agent.GenerateMoveUsingHardCodedTable(Data.GameData, Data.HumanHistory);
            Data.GameData.GameState[m.Row][m.Col].Content = Data.AgentSymbol;
            IsWinnerExist = CheckWinner.CheckWinningState(Data.AgentSymbol);
        }

        public static void GenerateIntelligentMove()
        {
            Move m = new Move();
            m = Data.Agent.GenerateMoveIntelligently(Data);
            // assign the move to the grid
            Data.GameData.GameState[m.Row][m.Col].Content = Data.AgentSymbol;
            IsWinnerExist = CheckWinner.CheckWinningState(Data.AgentSymbol);
        }
    }
}
