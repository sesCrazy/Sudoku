using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        SudokuClass sudoku = new SudokuClass();
        const int n = 3;
        const int sizeButton = 50;
        public int[,] map = new int[n * n, n * n];
        public Button[,] buttons = new Button[n * n, n * n];
        public Form1()
        {
            InitializeComponent();
            
            GenerateMap();
        }

        public void GenerateMap()
        {
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    map[i, j] = (i * n + i / n + j) % (n * n) + 1;
                    buttons[i, j] = new Button();
                }
            }
            Random r = new Random();
            for (int i = 0; i < 40; i++)
            {

                sudoku.ShuffleMap(r.Next(0, 5));
            }

            CreateMap();
            HideCells();
        }

        public void CreateMap()
        {
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Button button = new Button();
                    buttons[i, j] = button;
                    button.Size = new Size(sizeButton, sizeButton);
                    button.Text = map[i, j].ToString();
                    button.Click += OnCellPressed;
                    button.Location = new Point(j * sizeButton, i * sizeButton);
                    this.Controls.Add(button);
                }
            }
        }

        public void OnCellPressed(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            string buttonText = pressedButton.Text;
            if (string.IsNullOrEmpty(buttonText))
            {
                pressedButton.Text = "1";
            }
            else
            {
                int num = int.Parse(buttonText);
                num++;
                if (num == 10)
                    num = 1;
                pressedButton.Text = num.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < n * n; i++)
            {
                for(int j = 0; j < n * n; j++)
                {
                    var btnText = buttons[i, j].Text;
                    if(btnText != map[i, j].ToString())
                    {
                        MessageBox.Show("Неверно!");
                        return;
                    }
                }
            }
            MessageBox.Show("Верно!");
            for(int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    this.Controls.Remove(buttons[i, j]);
                }
            }
            GenerateMap();
        }

        public void HideCells()
        {
            int N = 40;
            Random r = new Random();
            while (N > 0)
            {
                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                        if (!string.IsNullOrEmpty(buttons[i, j].Text))
                        {
                            int a = r.Next(0, 3);
                            buttons[i, j].Text = a == 0 ? "" : buttons[i, j].Text;
                            buttons[i, j].Enabled = a == 0 ? true : false;

                            if (a == 0)
                                N--;
                            if (N <= 0)
                                break;
                        }
                    }
                    if (N <= 0)
                        break;
                }
            }
        }
    }
}
