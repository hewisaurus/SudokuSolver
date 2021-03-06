﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class ExtensionMethods
    {
        public static void RemoveTwoValuePossibilitiesByBlock(this List<SudokuCell> cells)
        {
            foreach (var blockGroup in cells.GroupBy(c => c.Block))
            {
                // Determine if any two cells in this group contain only the same two possibilities
                var twoPossibilityCells = blockGroup.Where(c => c.PossibleValues.Count == 2).ToList();
                RemoveTwoValuePossibilities(blockGroup.ToList(), twoPossibilityCells);

                // On top of this, check if only two cells contain two values even if the list of possibilities is longer
                // i.e. if the possible values 7 & 8 only exist in two cells, then those cells must contain 7 or 8 and the related
                // cells cannot contain 7 nor 8

                var valueDict = new Dictionary<int, List<int>>();
                for (int i = 1; i <= 9; i++)
                {
                    // How many cells can this number exist in, if it hasn't been solved already?
                    if (blockGroup.All(c => c.Value != i))
                    {
                        // Value hasn't been solved, so check the number of cells it could exist in
                        var possibleValueCells = blockGroup.Where(c => c.Value == null && c.PossibleValues.Contains(i)).Select(c => c.CellId).ToList();
                        if (possibleValueCells.Count == 2)
                        {
                            // Record this value and check for others that only have two possible locations
                            valueDict.Add(i, new List<int>(possibleValueCells));
                        }
                    }
                }
                // Now that we have looped through 1-9, check whether any numbers from 1-9 can only exist in two places
                if (blockGroup.Key == 5)
                {
                    var valuePairs = new List<Tuple<int, int, List<int>>>();
                    if (valueDict.Keys.Count > 1)
                    {
                        foreach (var kvp in valueDict)
                        {
                            var matchingKvp = valueDict.FirstOrDefault(kv => kv.Value.SequenceEqual(kvp.Value) && kv.Key != kvp.Key);
                            if (matchingKvp.Key > 0)
                            {
                                // These two cells matched possible values, so set these two values as the only two options for these cells
                                foreach (var cellId in kvp.Value)
                                {
                                    blockGroup.First(c => c.CellId == cellId).PossibleValues = new List<int> { kvp.Key, matchingKvp.Key };
                                }
                                //blockGroup.First(c => c.CellId == kvp.Key).PossibleValues = kvp.Value;
                                //blockGroup.First(c => c.CellId == matchingKvp.Key).PossibleValues = kvp.Value;

                                //blockGroup.First(c => c.CellId == kvp.Key).PossibleValues.Remove()


                                //foreach (var cell in blockGroup.FirstOrDefault(c => c.CellId == kvp.Key))
                                //valuePairs.Add(new Tuple<int, int, List<int>>(kvp.Key, matchingKvp.Key, new List<int>(kvp.Value)));
                                break;
                            }
                        }
                    }

                    // Check if we found any matching pairs
                    //if (valuePairs.Any())
                    //{
                    //    // Item1 is the first value, Item2 is the second value and Item3 is the list of cells that they were found in.
                    //    // Loop through the 'found' cells and remove all possibilities other than the two that are in Item1 and Item2
                    //    var matchedPair = valuePairs.First();

                    //}
                }
            }
        }

        public static void RemoveTwoValuePossibilitiesByRow(this List<SudokuCell> cells)
        {
            foreach (var rowGroup in cells.GroupBy(c => c.Row))
            {
                // Determine if any two cells in this group contain only the same two possibilities
                var twoPossibilityCells = rowGroup.Where(c => c.PossibleValues.Count == 2).ToList();
                RemoveTwoValuePossibilities(rowGroup.ToList(), twoPossibilityCells);
            }
        }

        public static void RemoveTwoValuePossibilitiesByColumn(this List<SudokuCell> cells)
        {
            foreach (var columnGroup in cells.GroupBy(c => c.Column))
            {
                // Determine if any two cells in this group contain only the same two possibilities
                var twoPossibilityCells = columnGroup.Where(c => c.PossibleValues.Count == 2).ToList();
                RemoveTwoValuePossibilities(columnGroup.ToList(), twoPossibilityCells);
            }
        }

        private static void RemoveTwoValuePossibilities(List<SudokuCell> fullList, List<SudokuCell> matchList)
        {
            // Ignore if there aren't two cells or more
            if (matchList.Count >= 2)
            {
                // Loop through the cells and determine whether two are identical
                for (int i = 0; i < matchList.Count; i++)
                {
                    var firstCell = matchList[i];
                    foreach (var nextCell in matchList.Where(c => c.CellId != firstCell.CellId))
                    {
                        if (nextCell.PossibleValues.SequenceEqual(firstCell.PossibleValues))
                        {
                            // Found a matching cell, so remove these two possibilities from the other cells
                            foreach (var updateCell in fullList.Where(c => c.CellId != firstCell.CellId && c.CellId != nextCell.CellId))
                            {
                                updateCell.PossibleValues = updateCell.PossibleValues.Except(firstCell.PossibleValues).ToList();
                            }
                        }
                    }
                }
            }
        }

        public static List<SolveAction> SolveCellsForSingleValues(this List<SudokuCell> cells)
        {
            var returnValue = new List<SolveAction>();
            // Go block by block, then row by row, then column by column
            for (int i = 1; i <= 9; i++)
            {
                var i1 = i;
                var blockCells = cells.Where(c => c.Block == i1).ToList();
                var rowCells = cells.Where(c => c.Block == i1).ToList();
                var columnCells = cells.Where(c => c.Block == i1).ToList();

                for (int n = 1; n <= 9; n++)
                {
                    // Check each block, cell and row to see if this number exists as a solution for a cell
                    // Also check whether we've found it elsewhere and it's going to be a solution - in this case, ignore it
                    if (!blockCells.CollectionContainsSolvedValue(n))
                    {
                        // This value isn't a solution within this block yet, so see if we can determine where it should go
                        // unless we've already solved it and are waiting on it to be marked as solved on the UI
                        if (!blockCells.PendingSolutionExists(returnValue, n))
                        {
                            var destinationCell = blockCells.DetermineLocationForUnsolvedValue(n);
                            if (destinationCell > 0)
                            {
                                // Found a valid location! Add that to the list of returned solutions
                                returnValue.Add(new SolveAction(destinationCell, n));
                            }
                        }
                    }

                    if (!rowCells.CollectionContainsSolvedValue(n))
                    {
                        // This value isn't a solution within this row yet, so see if we can determine where it should go
                        // unless we've already solved it and are waiting on it to be marked as solved on the UI
                        if (!rowCells.PendingSolutionExists(returnValue, n))
                        {
                            var destinationCell = rowCells.DetermineLocationForUnsolvedValue(n);
                            if (destinationCell > 0)
                            {
                                // Found a valid location! Add that to the list of returned solutions
                                returnValue.Add(new SolveAction(destinationCell, n));
                            }
                        }
                    }
                    if (!columnCells.CollectionContainsSolvedValue(n))
                    {
                        // This value isn't a solution within this column yet, so see if we can determine where it should go
                        // unless we've already solved it and are waiting on it to be marked as solved on the UI
                        if (!columnCells.PendingSolutionExists(returnValue, n))
                        {
                            var destinationCell = columnCells.DetermineLocationForUnsolvedValue(n);
                            if (destinationCell > 0)
                            {
                                // Found a valid location! Add that to the list of returned solutions
                                returnValue.Add(new SolveAction(destinationCell, n));
                            }
                        }
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Determines whether a pending solution exists for a given cell and value
        /// </summary>
        /// <param name="collection">The cell collection (row, block or column)</param>
        /// <param name="solutions">The solution list</param>
        /// <param name="value">The value to check for</param>
        /// <returns>True if a pending solution is found, otherwise false</returns>
        private static bool PendingSolutionExists(this List<SudokuCell> collection, List<SolveAction> solutions, int value)
        {
            foreach (var cell in collection)
            {
                if (solutions.Any(s => s.CellNumber == cell.CellId && s.Value == value))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CollectionContainsSolvedValue(this List<SudokuCell> collection, int value)
        {
            return collection.Any(c => c.Value == value);
        }

        private static int DetermineLocationForUnsolvedValue(this List<SudokuCell> collection, int value)
        {
            if (collection.Count(c => c.PossibleValues.Contains(value)) == 1)
            {
                return collection.First(c => c.PossibleValues.Contains(value)).CellId;
            }

            return 0;
        }

        /// <summary>
        /// For example, if in block 4, the value 2 can only exist in column 2, then remove the possibility of the value 2 from the other two related blocks
        /// </summary>
        /// <param name="cells"></param>
        public static void RemoveInvalidPossibilitiesForRelatedCollections(this List<SudokuCell> cells)
        {
            // Within each block, check if the numbers 1-9 (unsolved) are only found in one row or column
            // If they are, then remove that number from the related rows and columns in the related blocks
            for (int i = 1; i <= 9; i++)
            {
                var i1 = i;

                // For blocks, check rows and columns
                var blockCells = cells.Where(c => c.Block == i1).ToList();
                // Loop through 1-9 and check which rows  / columns they can be found on
                for (int n = 1; n <= 9; n++)
                {
                    var n1 = n;
                    var validRows = blockCells.Where(c => c.Value == null && c.PossibleValues.Contains(n1)).Select(c => c.Row).Distinct().ToList();
                    if (validRows.Count == 1)
                    {
                        // The number n was only found on one row, therefore the same row in the related blocks should not
                        // have n as a possible value
                        foreach (var relatedCell in cells.Where(c => c.Block != i && c.Row == validRows[0]))
                        {
                            relatedCell.PossibleValues.Remove(n);
                        }

                        continue;
                    }

                    // If we didn't only have one valid row, check the number n across the columns
                    var validColumns = blockCells.Where(c => c.Value == null && c.PossibleValues.Contains(n1)).Select(c => c.Column).Distinct().ToList();
                    if (validColumns.Count == 1)
                    {
                        // The number n was only found on one row, therefore the same row in the related blocks should not
                        // have n as a possible value
                        foreach (var relatedCell in cells.Where(c => c.Block != i && c.Column == validColumns[0]))
                        {
                            relatedCell.PossibleValues.Remove(n);
                        }

                        continue;
                    }
                }

                // For columns, check blocks
                var columnCells = cells.Where(c => c.Column == i1).ToList();
                // Loop through 1-9 and check which blocks they are found in
                for (int n = 1; n <= 9; n++)
                {
                    var n1 = n;
                    var validBlocks = columnCells.Where(c => c.Value == null && c.PossibleValues.Contains(n1)).Select(c => c.Block).Distinct().ToList();
                    if (validBlocks.Count == 1)
                    {
                        // The number n was only found in one block, therefore other columns in this block should not
                        // have n as a possible value
                        foreach (var relatedCell in cells.Where(c => c.Column != i && c.Block == validBlocks[0]))
                        {
                            relatedCell.PossibleValues.Remove(n);
                        }

                        continue;
                    }
                }

                // For rows, check blocks
                var rowCells = cells.Where(c => c.Row == i1).ToList();
                // Loop through 1-9 and check which blocks they are found in
                for (int n = 1; n <= 9; n++)
                {
                    var n1 = n;
                    var validBlocks = rowCells.Where(c => c.Value == null && c.PossibleValues.Contains(n1)).Select(c => c.Block).Distinct().ToList();
                    if (validBlocks.Count == 1)
                    {
                        // The number n was only found in one block, therefore other rows in this block should not
                        // have n as a possible value
                        foreach (var relatedCell in cells.Where(c => c.Row != i && c.Block == validBlocks[0]))
                        {
                            relatedCell.PossibleValues.Remove(n);
                        }
                    }
                }
            }
        }

        public static List<SolveAction> SolveUsingAllLogic(this List<SudokuCell> cells)
        {
            var returnValue = new List<SolveAction>();

            // #3: Remove double-value possibilities from collections
            // e.g. If row 1, column 2 & 3 can only have the values 7 & 8 in them both, then remove the values 7 & 8 from all other cells in that row
            cells.RemoveTwoValuePossibilitiesByBlock();
            cells.RemoveTwoValuePossibilitiesByRow();
            cells.RemoveTwoValuePossibilitiesByColumn();

            // #4: Remove possibilities for single values in a collection
            // e.g. If in block 4, the value 7 can only exist in column 2, then remove the possibility of the value 7 from column 2 within the other two related blocks
            cells.RemoveInvalidPossibilitiesForRelatedCollections();

            // NB: A collection is a row, column or block of cells. A collection should always contain 9 cells.
            // Order of action:

            // #1: Solve cells where there is only one possible value
            // e.g. For row 1, column 3, the number must be 8 as all other numbers from 1-9 exist in other cells
            returnValue = cells.SolveSingleValuePossibilityCells();
            if (returnValue.Any())
            {
                return returnValue;
            }

            // #2: Solve cells where a given integer can only exist in one cell for a given collection
            // e.g. For row 1, the number 3 can only possible exist in column 6.
            returnValue = cells.SolveCellsForSingleValues();
            if (returnValue.Any())
            {
                return returnValue;
            }
            

            // Run #1 and #2 again as #3 and #4 may have removed invalid possibilities.
            returnValue = cells.SolveSingleValuePossibilityCells();
            if (returnValue.Any())
            {
                return returnValue;
            }
            returnValue = cells.SolveCellsForSingleValues();
            return returnValue;
        }

        public static List<SolveAction> SolveSingleValuePossibilityCells(this List<SudokuCell> cells)
        {
            var returnValue = new List<SolveAction>();
            foreach (var cell in cells.Where(c => c.Value == null && c.PossibleValues.Count == 1))
            {
                returnValue.Add(new SolveAction(cell.CellId, cell.PossibleValues[0]));
            }
            return returnValue;
        }
    }
}
