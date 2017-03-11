using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Methods
    {
        /// <summary>
        /// Determine whether any cells have only one possible value
        /// </summary>
        /// <returns>The first cell found, in which only one value is possible, and the value itself, otherwise Null if no valid cells were found</returns>
        public async Task<SolveAction> DoSingleSolveWithLogic_OnlyPossibleValue_NoTpl(List<SudokuCell> cells)
        {
            // Find a concrete solution for a single cell, based on existing values in appropriate rows, columns and blocks
            foreach (var cell in cells.Where(c => c.Value == null))
            {
                var rowCells = cells.Where(c => c.Cell != cell.Cell && c.Row == cell.Row).ToList();
                var columnCells = cells.Where(c => c.Cell != cell.Cell && c.Column == cell.Column).ToList();
                var blockCells = cells.Where(c => c.Cell != cell.Cell && c.Block == cell.Block).ToList();

                var possibleValues = new List<int>();
                for (int i = 1; i <= 9; i++)
                {
                    // Can this cell hold this value?
                    if (rowCells.All(c => c.Value != i) &&
                        columnCells.All(c => c.Value != i) &&
                        blockCells.All(c => c.Value != i))
                    {
                        // Yes, it can
                        possibleValues.Add(i);
                    }
                }

                if (possibleValues.Count == 1)
                {
                    // There can only be one possible value for this cell, so consider it solved and return it here
                    return new SolveAction(cell.Cell, possibleValues[0]);
                }
            }

            return null;
        }

        /// <summary>
        /// Determine whether any cells have only one possible value
        /// </summary>
        /// <returns>All cells found, in which only one value is possible, and the values for each</returns>
        public async Task<List<SolveAction>> DoMultipleSolveWithLogic_OnlyPossibleValue_NoTpl(List<SudokuCell> cells)
        {
            var returnValue = new List<SolveAction>();
            // Find a concrete solution for a single cell, based on existing values in appropriate rows, columns and blocks
            foreach (var cell in cells.Where(c => c.Value == null))
            {
                var rowCells = cells.Where(c => c.Cell != cell.Cell && c.Row == cell.Row).ToList();
                var columnCells = cells.Where(c => c.Cell != cell.Cell && c.Column == cell.Column).ToList();
                var blockCells = cells.Where(c => c.Cell != cell.Cell && c.Block == cell.Block).ToList();

                var possibleValues = new List<int>();
                for (int i = 1; i <= 9; i++)
                {
                    // Can this cell hold this value?
                    if (rowCells.All(c => c.Value != i) &&
                        columnCells.All(c => c.Value != i) &&
                        blockCells.All(c => c.Value != i))
                    {
                        // Yes, it can
                        possibleValues.Add(i);
                    }
                }

                if (possibleValues.Count == 1)
                {
                    // There can only be one possible value for this cell, so consider it solved and add it to the list to return
                    returnValue.Add(new SolveAction(cell.Cell, possibleValues[0]));
                }
            }

            return returnValue;
        }
        
        
    }
}
