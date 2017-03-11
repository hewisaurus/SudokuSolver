using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver
{
    public partial class MainWindow : Window
    {
        public List<SudokuCell> Cells = new List<SudokuCell>();

        private List<SolveAction> _actions;
        private List<List<SolveAction>> _invalidValues;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_Reset(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            _actions = new List<SolveAction>();
            _invalidValues = new List<List<SolveAction>>();

            SetupCells();
            foreach (var cell in Cells)
            {
                var tb = Helpers.FindChild<TextBox>(GrMain, $"C{cell.Cell}");
                if (tb != null)
                {
                    tb.Text = null;
                    tb.Background = Brushes.White;
                    tb.Foreground = Brushes.Black;
                    tb.FontSize = 36;
                }
            }
        }

        private void SetupCells()
        {
            Cells = new List<SudokuCell>();

            int row = 1;
            int column = 1;

            for (int i = 0; i <= 80; i++)
            {
                int cell = i + 1;
                if (i / 9 > 0 && i % 9 == 0)
                {
                    row++;
                    column = 1;
                }

                var block = 0;
                if (row <= 3 && column <= 3)
                {
                    block = 1;
                }
                else if (row <= 3 && column <= 6)
                {
                    block = 2;
                }
                else if (row <= 3)
                {
                    block = 3;
                }
                else if (row <= 6 && column <= 3)
                {
                    block = 4;
                }
                else if (row <= 6 && column <= 6)
                {
                    block = 5;
                }
                else if (row <= 6)
                {
                    block = 6;
                }
                else if (row <= 9 && column <= 3)
                {
                    block = 7;
                }
                else if (row <= 9 && column <= 6)
                {
                    block = 8;
                }
                else
                {
                    block = 9;
                }

                Cells.Add(new SudokuCell(row, column, block, cell));

                column++;
            }
        }

        private void BtnGenerateEasy_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            FillPuzzle(Puzzles.Get(Difficulty.Easy));
        }
        private void BtnGenerateMedium_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            FillPuzzle(Puzzles.Get(Difficulty.Medium));
        }
        private void BtnGenerateHard_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            FillPuzzle(Puzzles.Get(Difficulty.Hard));
        }
        private void BtnGenerateInsane_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            FillPuzzle(Puzzles.Get(Difficulty.Insane));
        }

        private void FillPuzzle(List<Tuple<int, int>> values)
        {
            foreach (var cell in Cells)
            {
                cell.Value = null;
                cell.Locked = false;
            }

            foreach (var t in values)
            {
                var cell = Cells.FirstOrDefault(c => c.Cell == t.Item1);
                if (cell != null)
                {
                    cell.Value = t.Item2;
                    cell.Locked = true;
                }
            }

            foreach (var cell in Cells)
            {
                var tb = Helpers.FindChild<TextBox>(GrMain, $"C{cell.Cell}");
                if (tb != null)
                {
                    tb.Text = cell.Value == null ? "" : cell.Value.ToString();
                    tb.Background = cell.Locked ? Brushes.LightGray : Brushes.White;
                    tb.Foreground = Brushes.Black;
                }
            }
        }
    }
}
