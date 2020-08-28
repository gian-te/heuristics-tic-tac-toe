using System.Linq;
using System.Windows;

namespace TicTacToe.WPF.ViewModel
{
    public static class CheckWinner
    {
        public static bool CheckWinningState(string symbol)
        {
            if (CheckWinState(symbol))
            {
                MessageBox.Show("Player "+ symbol + " has won!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            if (CheckDraw())
            {
                MessageBox.Show("The game resulted into a draw!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }

        //private static string DeclareWinner(string symbol)
        //{
        //    if (symbol == "X")
        //        return "Smart Agent ";
        //    else
        //        return "Human Player ";
        //}

        private static bool CheckWinState(string symbol)
        {
            if (CheckRow(symbol))
                return true;

            if (CheckColumn(symbol))
                return true;

            if (CheckDiagLeft(symbol))
                return true;

            if (CheckDiagRight(symbol))
                return true;

            return false;
        }

        private static bool CheckRow(string symbol)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                count = GameManipulation.Data.GameData.GameState[i].Where(x => x.Content != null &&
                        x.Content.ToString() == symbol).Count();
                if (count == 3)
                    return true;
            }
            return false;
        }

        private static bool CheckColumn(string symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                int count = 0;
                for (int a = 0; a < 3; a++)
                {
                    if ((GameManipulation.Data.GameData.GameState[a][i].Content != null)
                        && (GameManipulation.Data.GameData.GameState[a][i].Content.ToString() == symbol))
                    {
                        count++;
                    }
                }
                if (count == 3)
                    return true;
            }
            return false;
        }

        private static bool CheckDiagLeft(string symbol)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                if ((GameManipulation.Data.GameData.GameState[i][i].Content != null)
                    && (GameManipulation.Data.GameData.GameState[i][i].Content.ToString() == symbol))
                {
                    count++;
                }
            }

            if (count == 3)
                return true;
            return false;
        }

        private static bool CheckDiagRight(string symbol)
        {
            int count = 0;
            int a = 2;
            for (int i = 0; i < 3; i++)
            {
                if ((GameManipulation.Data.GameData.GameState[i][a].Content != null) 
                    && (GameManipulation.Data.GameData.GameState[i][a].Content.ToString() == symbol))
                {
                    count++;
                }
                a--;
            }

            if (count == 3)
                return true;
            return false;
        }

        private static bool CheckDraw()
        {
            int count = 0;
            for (int i = 0; i < 3; i++ )
            {
                count = GameManipulation.Data.GameData.GameState[i].Where(x => x.Content == null).Count();
                if (count > 0)
                    return false;
            }
            return true;
        }
    }
}
