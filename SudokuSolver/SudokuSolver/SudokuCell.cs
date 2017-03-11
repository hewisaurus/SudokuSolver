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
        public int CellId { get; set; }

        public int? Value { get; set; }
        public List<int> PossibleValues { get; set; }

        public bool Locked { get; set; }

        public SudokuCell()
        {
            Locked = false;
            PossibleValues = new List<int>();
        }

        public SudokuCell(int row, int column, int block, int cellId)
        {
            Row = row;
            Column = column;
            Block = block;
            CellId = cellId;
            Locked = false;
            PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
    }
}
