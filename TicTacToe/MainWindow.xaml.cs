using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{

    public partial class MainWindow : Window
    {

        #region Private Members

        private MarkType[] mResults;

        private bool mPlayer1Turn;

        private bool mGameEnded;

        #endregion


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
               
        #endregion

        //Start een nieuwe game en doet het naar standaard waardes zetten
        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Dit worden de standaard waardes
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index]  != MarkType.Free)
                return;

            // Zet juiste icoon op basis van de speler die aan de beurt is

            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;


            mPlayer1Turn ^= true;


            // Kijken wie er heeft gewonnen
            CheckForWinner();
            

        }

        private void CheckForWinner()
        {
            #region Horizontale Wins

            // Check for horizontal wins
            //
            //  - Row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            //  - Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            //  - Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Verticale Wins

            // Kijkt voor vertrical wins

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            //  - Column 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            //  - Column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonale Wins

            // Kijk voor diagonale wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Spel stopt
                mGameEnded = true;

                // Kleur de winnende cellen groen
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region Geen Winnaars

            // Als er geen winnaar is of het bord vol is
            if (!mResults.Any(f => f == MarkType.Free))
            {
                // Spel stopt
                mGameEnded = true;

                // Maakt alle cellen oranje
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }

            #endregion
        }
    }
}
