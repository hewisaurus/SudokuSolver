# SudokuSolver
Playing around with different methods to solve sudoku puzzles

Currently this uses a number of different methods to solve puzzles..

NB: A collection is a row, column or block of cells. A collection should always contain 9 cells.

##### #1: Solve cells for which there is only one possible value
Using the example below, we can see that Row 1, Column 7 can only possible contain the number 1, as all other numbers from 1-9 exist elsewhere in the same row, column and block

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/Step1.PNG">



##### #2: Solve cells where a given integer can only possible exist in one location within a collection

Using the example below, we can see that Row 7, Column 3 is the only cell within that block (Block 7) that can hold the value 3. Set the value to 3 
and then remove 3 from the list of possible solutions to all other cells in the same row or column.

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/Step2.PNG">
