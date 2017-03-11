using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuCell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Block { get; set; }
        public int Cell { get; set; }

        public int? Value { get; set; }
        public List<int> PossibleValues { get; set; }

        public bool Locked { get; set; }

        public SudokuCell()
        {
            Locked = false;
            PossibleValues = new List<int>();
        }

        public SudokuCell(int row, int column, int block, int cell)
        {
            Row = row;
            Column = column;
            Block = block;
            Cell = cell;
            Locked = true;
            PossibleValues = new List<int>();
        }
    }
}
