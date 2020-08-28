using System.Windows;
using System.Windows.Controls;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Utilities;
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
            ticTacToeGrid.Visibility = Visibility.Visible;
            GameManipulation.Data.GameData.GameState = InitGameBoard.gameBoard; // moved here, i think we still need to init game even when first move is the human
            if (GameManipulation.Data.FirstMove == Players.Agent && GameManipulation.Data.GameLevel == IntelligenceLevels.Smart)
            {
                GameManipulation.GenerateIntelligentMove();
            }
            else if (GameManipulation.Data.FirstMove == Players.Agent && GameManipulation.Data.GameLevel == IntelligenceLevels.Random)
            {
                GameManipulation.GenerateRandomMove();
            }
            else if (GameManipulation.Data.FirstMove == Players.Agent && GameManipulation.Data.GameLevel == IntelligenceLevels.Hardcoded)
            {
                GameManipulation.GenerateHardCodedMove();
            }
            // there is a bug where if the player spams start, the agent will spam moving without waiting for the player's turn
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            GameManipulation.ClearAllData();
            // adding this here for now for the user to not forget to click "start"
            ticTacToeGrid.Visibility = Visibility.Collapsed;
        }

        private void StartPlayerChecked(object sender, RoutedEventArgs e)
        {
            RadioButton chk = sender as RadioButton;
            switch (chk.Content.ToString())
            {
                case "Smart Agent":
                    GameManipulation.Data.FirstMove = Players.Agent;
                    GameManipulation.Data.AgentSymbol = Symbols.X.ToString();
                    GameManipulation.Data.HumanSymbol = Symbols.O.ToString();
                    break;
                case "Human Player":
                    GameManipulation.Data.FirstMove = Players.Human;
                    GameManipulation.Data.HumanSymbol = Symbols.X.ToString();
                    GameManipulation.Data.AgentSymbol = Symbols.O.ToString();
                    break;
                default:
                    break;
            }
       
        }

        private void LevelChecked(object sender, RoutedEventArgs e)
        {
            RadioButton chkTwo = sender as RadioButton;
            //GameManipulation.Data.GameLevel = chkTwo.Content.ToString();
            switch (chkTwo.Content.ToString())
            {
                case "Random":
                    GameManipulation.Data.GameLevel = IntelligenceLevels.Random;
                    break;
                case "Hardcoded":
                    GameManipulation.Data.GameLevel = IntelligenceLevels.Hardcoded;
                    break;
                case "Smart":
                    GameManipulation.Data.GameLevel = IntelligenceLevels.Smart;
                    break;
                default:
                    break;
            }
         

        }

        private void InitAgents()
        {
            IsSmartAgent.IsChecked = true;
            IsRandom.IsChecked = true;
            GameManipulation.Data.FirstMove = Players.Agent; // the one who is first to move  is always X, i think
            

            GameManipulation.Data.AgentSymbol = Symbols.X.ToString(); 
            GameManipulation.Data.HumanSymbol = Symbols.O.ToString(); 
        }
    }
}
