using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SolveAction
    {
        public int CellNumber { get; set; }
        public int Value { get; set; }

        public SolveAction()
        {
            
        }

        public SolveAction(int cellNumber, int value)
        {
            CellNumber = cellNumber;
            Value = value;
        }
    }
}
