# SudokuSolver

Currently this uses a number of different methods to solve puzzles..

NB: A collection is a row, column or block of cells. A collection should always contain 9 cells.

The image below depicts how I refer to the cells. Cells are number 1 to 81, left to right and top to bottom.
Rows, columns and blocks as shown.

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/layout.jpg">

--- 
## Methods of solving

#### #1: Solve cells for which there is only one possible value
Using the example below, we can see that Row 1, Column 7 can only possible contain the number 1, as all other numbers from 1-9 exist elsewhere in the same row, column and block

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/Step1.PNG">



#### #2: Solve cells where a given integer can only possible exist in one location within a collection

Using the example below, we can see that Row 7, Column 3 is the only cell within that block (Block 7) that can hold the value 3. Set the value to 3 
and then remove 3 from the list of possible solutions to all other cells in the same row or column.

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/Step2.PNG">

#### #3: Remove possible values for related cells when two cells in a collection can have the same two values

Using the example below, we can see that columns 2 and 3 in row 3 can both contain only the values 7 and 8. 
This means that we can remove both 7 & 8 from the list of possible values for the rest of block 1, and we could 
do the same to the rest of row 3 if there were any other unsolved cells there. The same process counts for two cells in a given row or block.

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/step3.PNG">

#### #4: Remove possible values for related cells when only one related collection can hold a given value

Using the example below, we can see that within block 4, only column 2 can contain the value 2. 
As there must be a number 2 in column 2, we can safely remove 2 from the possible values for cells within column 2 in blocks 1 and 7.

<img src="https://raw.githubusercontent.com/hewisaurus/SudokuSolver/master/Images/step4.PNG">
