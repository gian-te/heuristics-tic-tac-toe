using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe.WPF.ViewModel;

namespace TicTacToe.WPF.View
{
    class InitGameBoard
    {
        private static readonly int cellSize = 150;
        public static List<List<Label>> gameBoard = new List<List<Label>>();
        public static void InitGrid(Grid ticTacToeGrid)
        {
            int locTop = 0, locLeft = 0;
            for (int i = 0; i < 3; i++)
            {
                if (i > 0)
                    locTop += 10;
                gameBoard.Add(CreateRow(locLeft, locTop, ticTacToeGrid, i));
                locLeft = 0;
                locTop += cellSize;
            }
        }

        private static List<Label> CreateRow(int locLeft, int locTop, Grid ticTacToeGrid, int row)
        {
            List<Label> rw = new List<Label>();
            for (int j = 0; j < 3; j++)
            {
                if (j > 0)
                    locLeft += 10;
                Label lbl = CreateLblControl(cellSize, locLeft, locTop);
                SubscribeEvent(lbl, row, j);
                ticTacToeGrid.Children.Add(lbl);
                locLeft += cellSize;
                rw.Add(lbl);
            }
            return rw;
        }

        private static Label CreateLblControl(int cellSize, int locLeft, int locTop)
        {
            Label lbl = new Label
            {
                Height = cellSize,
                Width = cellSize,
                Margin = new Thickness(locLeft, locTop, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FontSize = 100,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFEB547")),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#383C3B"))
            };

            return lbl;
        }

        public static void SubscribeEvent(Label lbl, int row, int col)
        {
            lbl.MouseDoubleClick += (sender, eventArgs) => {
                if (lbl.Content == null)
                    GameManipulation.ChangeLabelContent(gameBoard, row, col);
                else
                    MessageBox.Show("Please select another square!", "Invalid Move", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            };

        }
    }
}
