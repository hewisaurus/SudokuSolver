using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var tb = Helpers.FindChild<TextBox>(GrMain, $"C{cell.CellId}");
                if (tb != null)
                {
                    tb.Text = null;
                    tb.Background = Brushes.White;
                    tb.Foreground = Brushes.Black;
                    tb.FontSize = 36;
                    tb.IsReadOnly = false;
                    tb.TextWrapping = TextWrapping.WrapWithOverflow;
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
            foreach (var val in values)
            {
                SetCellValueSolved(val.Item1, val.Item2, true);
            }
            //foreach (var cell in Cells)
            //{
            //    cell.Value = null;
            //    cell.Locked = false;
            //}

            //foreach (var t in values)
            //{
            //    var cell = Cells.FirstOrDefault(c => c.CellId == t.Item1);
            //    if (cell != null)
            //    {
            //        cell.Value = t.Item2;
            //        cell.Locked = true;
            //    }
            //}

            //foreach (var cell in Cells)
            //{
            //    var tb = Helpers.FindChild<TextBox>(GrMain, $"C{cell.CellId}");
            //    if (tb != null)
            //    {
            //        tb.Text = cell.Value == null ? "" : cell.Value.ToString();
            //        tb.Background = cell.Locked ? Brushes.LightGray : Brushes.White;
            //        tb.Foreground = Brushes.Black;
            //        tb.IsReadOnly = cell.Locked;
            //    }
            //}
        }

        #region test methods

        #endregion

        private async void BtnTestSingleSolveOnce_Click(object sender, RoutedEventArgs e)
        {
            var methods = new Methods();
            bool stopSolve = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                try
                {
                    if (Cells.All(c => c.Value != null))
                    {
                        MessageBox.Show("No cells left to solve");
                        sw.Stop();
                        return;
                    }

                    var action = await methods.DoSingleSolveWithLogic_OnlyPossibleValue_NoTpl(Cells);
                    if (action == null)
                    {
                        MessageBox.Show("Couldn't find any more solutions");
                        sw.Stop();
                        return;
                    }

                    sw.Stop();
                    var thisCell = Cells.First(c => c.CellId == action.CellNumber);
                    thisCell.Value = action.Value;
                    var cellTb = Helpers.FindChild<TextBox>(GrMain, $"C{thisCell.CellId}");
                    if (cellTb != null)
                    {
                        cellTb.Text = thisCell.Value.ToString();
                        cellTb.Foreground = Brushes.DarkGreen;
                    }

                    MessageBox.Show($"Single run found one value in {sw.Elapsed}");
                    stopSolve = true;
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    MessageBox.Show($"Something went wrong! {ex.Message}");
                    stopSolve = true;
                }
            } while (!stopSolve);
        }

        private async void BtnTestSingleSolveMultiple_Click(object sender, RoutedEventArgs e)
        {
            var methods = new Methods();
            bool stopSolve = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                try
                {
                    if (Cells.All(c => c.Value != null))
                    {
                        MessageBox.Show("No cells left to solve");
                        sw.Stop();
                        return;
                    }

                    var actions = await methods.DoMultipleSolveWithLogic_OnlyPossibleValue_NoTpl(Cells);
                    if (!actions.Any())
                    {
                        MessageBox.Show("Couldn't find any more solutions");
                        sw.Stop();
                        return;
                    }

                    sw.Stop();
                    foreach (var action in actions)
                    {
                        var thisCell = Cells.First(c => c.CellId == action.CellNumber);
                        thisCell.Value = action.Value;
                        var cellTb = Helpers.FindChild<TextBox>(GrMain, $"C{thisCell.CellId}");
                        if (cellTb != null)
                        {
                            cellTb.Text = thisCell.Value.ToString();
                            cellTb.Foreground = Brushes.DarkGreen;
                        }
                    }
                    MessageBox.Show($"Single run found {actions.Count} value(s) in {sw.Elapsed}");

                    stopSolve = true;
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    MessageBox.Show($"Something went wrong! {ex.Message}");
                    stopSolve = true;
                }
            } while (!stopSolve);
        }

        private void BtnSaveState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRestoreState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClearState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdateShowPossibleValues_Click(object sender, RoutedEventArgs e)
        {
            UpdatePossibleValues();

            DrawPossibleValues();
        }

        private void BtnShowPossibleValues_Click(object sender, RoutedEventArgs e)
        {
            DrawPossibleValues();
        }

        private void DrawPossibleValues()
        {
            foreach (var cell in Cells.Where(c => c.Value == null))
            {
                SetCellValuePossibilities(cell.CellId, cell.PossibleValues);
            }
        }

        private void UpdatePossibleValues()
        {
            foreach (var cell in Cells)
            {
                if (cell.Value != null)
                {
                    cell.PossibleValues = new List<int>();
                }
                else
                {
                    var rowCells = Cells.Where(c => c.CellId != cell.CellId && c.Row == cell.Row).ToList();
                    var columnCells = Cells.Where(c => c.CellId != cell.CellId && c.Column == cell.Column).ToList();
                    var blockCells = Cells.Where(c => c.CellId != cell.CellId && c.Block == cell.Block).ToList();

                    cell.PossibleValues = new List<int>();
                    for (int i = 1; i <= 9; i++)
                    {
                        // Can this cell hold this value?
                        if (rowCells.All(c => c.Value != i) &&
                            columnCells.All(c => c.Value != i) &&
                            blockCells.All(c => c.Value != i))
                        {
                            // Yes, it can
                            cell.PossibleValues.Add(i);
                        }
                    }
                }
            }
        }

        private void BtnRemoveDoubleValuePossibilitiesBlocks_Click(object sender, RoutedEventArgs e)
        {
            Cells.RemoveTwoValuePossibilitiesByBlock();
            DrawPossibleValues();
        }

        private void BtnRemoveDoubleValuePossibilitiesRows_Click(object sender, RoutedEventArgs e)
        {
            Cells.RemoveTwoValuePossibilitiesByRow();
            DrawPossibleValues();
        }

        private void BtnRemoveDoubleValuePossibilitiesColumns_Click(object sender, RoutedEventArgs e)
        {
            Cells.RemoveTwoValuePossibilitiesByColumn();
            DrawPossibleValues();
        }

        private void BtnUpdateSinglePossibilityCells_Click(object sender, RoutedEventArgs e)
        {
            var actions = Cells.SolveSingleValuePossibilityCells();
            if (!actions.Any())
            {
                MessageBox.Show("Couldn't find any more solutions");
                return;
            }

            foreach (var action in actions)
            {
                SetCellValueSolved(action.CellNumber, action.Value);
            }
        }

        private void SetCellValueSolved(int cell, int value, bool fromTemplate = false)
        {
            // Update cell in memory
            var memCell = Cells.FirstOrDefault(c => c.CellId == cell);
            if (memCell != null)
            {
                memCell.PossibleValues = new List<int>();
                memCell.Value = value;
                if (fromTemplate)
                {
                    memCell.Locked = true;
                }
                // Remove this value from related cells
                foreach (var relatedCell in Cells.Where(c => c.Row == memCell.Row && c.CellId != memCell.CellId))
                {
                    if (relatedCell.PossibleValues.Contains(value))
                    {
                        relatedCell.PossibleValues.Remove(value);
                    }
                }
                foreach (var relatedCell in Cells.Where(c => c.Column == memCell.Column && c.CellId != memCell.CellId))
                {
                    if (relatedCell.PossibleValues.Contains(value))
                    {
                        relatedCell.PossibleValues.Remove(value);
                    }
                }
                foreach (var relatedCell in Cells.Where(c => c.Block == memCell.Block && c.CellId != memCell.CellId))
                {
                    if (relatedCell.PossibleValues.Contains(value))
                    {
                        relatedCell.PossibleValues.Remove(value);
                    }
                }
            }
            // Update cell on screen
            var cellTb = Helpers.FindChild<TextBox>(GrMain, $"C{cell}");
            if (cellTb != null)
            {
                cellTb.Text = value.ToString();
                cellTb.Foreground = fromTemplate ? Brushes.Black : Brushes.DarkGreen;
                cellTb.Background = fromTemplate ? Brushes.LightGray : Brushes.White;
                cellTb.FontSize = 36;
                cellTb.IsReadOnly = fromTemplate;
            }

            // TESTING ONLY - SHOW POSSIBILITIES AUTOMATICALLY
            //if (true)
            //{
            //    DrawPossibleValues();
            //}
        }

        private void SetCellValuePossibilities(int cell, List<int> values)
        {
            var cellTb = Helpers.FindChild<TextBox>(GrMain, $"C{cell}");
            if (cellTb != null)
            {
                cellTb.Text = string.Join(", ", values);
                cellTb.Foreground = Brushes.Red;
                cellTb.FontSize = 12;
                cellTb.IsReadOnly = false;
            }
        }

        private void BtnSolveSingleLocationValues_Click(object sender, RoutedEventArgs e)
        {
            var actions = Cells.SolveCellsForSingleValues();
            if (!actions.Any())
            {
                MessageBox.Show("Couldn't find any more solutions");
                return;
            }

            foreach (var action in actions)
            {
                SetCellValueSolved(action.CellNumber, action.Value);
            }
        }

        private void BtnManualCellSolve_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbManualCellId.Text) && !string.IsNullOrEmpty(TbManualCellValue.Text))
            {
                try
                {
                    var cellId = int.Parse(TbManualCellId.Text);
                    var cellValue = int.Parse(TbManualCellValue.Text);
                    if (cellId >= 1 && cellId <= 81 && cellValue >= 1 && cellValue <= 9)
                    {
                        SetCellValueSolved(cellId, cellValue);
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void Cell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                var tb = (TextBox)sender;
                LblMouseOverText.Content = $"Current cell: {tb.Name.Substring(1)}";
            }
        }

        private void Cell_MouseLeave(object sender, MouseEventArgs e)
        {
            LblMouseOverText.Content = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reset();
            for (int i = 1; i <= 81; i++)
            {
                var cellTb = Helpers.FindChild<TextBox>(GrMain, $"C{i}");
                if (cellTb != null)
                {
                    cellTb.MouseEnter += Cell_MouseEnter;
                    cellTb.MouseLeave += Cell_MouseLeave;
                }
            }
        }

        private void BtnRemovePossibilitiesForSingleValueInCollection_Click(object sender, RoutedEventArgs e)
        {
            Cells.RemoveInvalidPossibilitiesForRelatedCollections();
            // TESTING ONLY - SHOW POSSIBILITIES AUTOMATICALLY
            //if (true)
            //{
            //    DrawPossibleValues();
            //}
        }

        private async void BtnSolveUsingAllLogic_Click(object sender, RoutedEventArgs e)
        {
            // First, loop through the cells and set the values of any that the user may have entered
            for (int i = 1; i <= 81; i++)
            {
                var cellTb = Helpers.FindChild<TextBox>(GrMain, $"C{i}");
                if (!string.IsNullOrEmpty(cellTb?.Text))
                {
                    try
                    {
                        // If the cell has a comma in it (like we've clicked show possibilities), ignore it
                        if (!cellTb.Text.Contains(","))
                        {
                            var value = int.Parse(cellTb.Text);
                            SetCellValueSolved(i, value, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("One or more values are invalid and the puzzle can't be solved! Ensure you have only used the numbers 1-9.");
                        return;
                    }

                    //// Check if this was a templated cell
                    //var memCell = Cells.FirstOrDefault(c => c.CellId == i);
                    //if (memCell != null)
                    //{
                    //    if (!memCell.Locked)
                    //    {
                    //        // Not locked means not templated, so the user has entered this one
                    //        try
                    //        {
                    //            memCell.Value = int.Parse(cellTb.Text);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show("One or more values are invalid and the puzzle can't be solved! Ensure you have only used the numbers 1-9.");
                    //            return;
                    //        }
                    //    }
                    //}
                }
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool stopSolve = false;
            do
            {
                if (Cells.Count(c => c.Value == null) == 0)
                {
                    //MessageBox.Show("Puzzle solved!");
                    LblFinishTimer.Content = $"Puzzle solved in {sw.Elapsed}";
                    sw.Stop();
                    return;
                }
                var actions = Cells.SolveUsingAllLogic();
                if (!actions.Any())
                {
                    MessageBox.Show("Couldn't find any more solutions :(");
                    sw.Stop();
                    return;
                }
                foreach (var action in actions)
                {
                    SetCellValueSolved(action.CellNumber, action.Value);
                }
                await Task.Delay(1);
            } while (!stopSolve);
        }
    }
}
