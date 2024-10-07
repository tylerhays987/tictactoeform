using System;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class Form1 : Form
    {
        private char currentPlayer;
        private char[,] board;

        public Form1()
        {

            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            currentPlayer = 'X';
            board = new char[3, 3]; 

            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '\0'; 
                }
            }

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    button.UseVisualStyleBackColor = false;
                    button.Text = string.Empty;
                    button.Enabled = true;
                    button.Click += Button_Click; 
                }
            }
            labelStatus.Text = "Player X's Turn";
        }
        private void Button_Click(object sender, EventArgs e)
        {
            
            if (sender is Button button)
            {
                
                int row = tableLayoutPanel1.GetRow(button);
                int col = tableLayoutPanel1.GetColumn(button);

                
                if (row < 0 || row >= 3 || col < 0 || col >= 3)
                {
                    MessageBox.Show("Invalid move! Please try again.");
                    return;
                }

                
                if (board[row, col] != '\0') 
                { 
                    return;
                }

                
                board[row, col] = currentPlayer;
                button.Text = currentPlayer.ToString();
                button.Enabled = false; 
               

                
                if (CheckWinner())
                {
                    MessageBox.Show($"Player {currentPlayer} wins!");
                    DisableButtons(); 
                }
                else if (CheckDraw())
                {
                    MessageBox.Show("It's a draw!");
                }
                else
                {
                    
                    SwitchPlayer();
                }
            }
            else
            {
                MessageBox.Show("Error: The clicked control is not a button.");
            }
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            labelStatus.Text = $"Player {currentPlayer}'s Turn";
        }
        private bool CheckWinner()
        {
            
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                    (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
                {
                    return true;
                }
            }
            
            return (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                   (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer);
        }
        private bool CheckDraw()
        {
            foreach (var cell in board)
            {
                if (cell == '\0') 
                {
                    return false; 
                }
            }
            return true; 
        }
        private void DisableButtons()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = false;
                }
            }
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

    }



}
