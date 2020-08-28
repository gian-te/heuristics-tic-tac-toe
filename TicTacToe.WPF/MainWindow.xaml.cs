using System.Windows;
using System.Windows.Controls;
using TicTacToe.Common.Enums;
using TicTacToe.WPF.View;
using TicTacToe.WPF.ViewModel;

namespace TicTacToe.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitGameBoard.InitGrid(ticTacToeGrid);
            InitAgents();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (GameManipulation.Data.FirstMove == "Smart Agent")
            {
                GameManipulation.Data.GameData.GameState = InitGameBoard.gameBoard;
                GameManipulation.SmartAgentMovement();
            }
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            GameManipulation.ClearAllData();
        }


        private void StartPlayerChecked(object sender, RoutedEventArgs e)
        {
            RadioButton chk = sender as RadioButton;
            GameManipulation.Data.FirstMove = chk.Content.ToString();
        }

        private void LevelChecked(object sender, RoutedEventArgs e)
        {
            RadioButton chkTwo = sender as RadioButton;
            GameManipulation.Data.GameLevel = chkTwo.Content.ToString();
        }

        private void InitAgents()
        {
            IsSmartAgent.IsChecked = true;
            IsRandom.IsChecked = true;
            GameManipulation.Data.FirstMove = "Smart Agent";
            GameManipulation.Data.GameLevel = "Random";
            GameManipulation.Data.Smart = Symbols.X.ToString();
            GameManipulation.Data.Human = Symbols.O.ToString();
        }
    }
}
