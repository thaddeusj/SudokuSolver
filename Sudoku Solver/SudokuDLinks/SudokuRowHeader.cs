using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.SudokuDLinks
{
    public class SudokuRowHeader : AbstractDLinks.RowHeader, IEquatable<SudokuRowHeader>
    {

        public Triple<int> cellLabel; //Cells are labelled via the scheme: (row,column, cell contents).


        public SudokuRowHeader(Triple<int> label) { cellLabel = label; }





        public bool Equals(SudokuRowHeader other)
        {
            if (other == null || this == null) return false;

            return cellLabel.Equals(other.cellLabel);
        }
    }

    public class SudokuRowHeaderComparer : IEqualityComparer<SudokuRowHeader>
    {

        bool IEqualityComparer<SudokuRowHeader>.Equals(SudokuRowHeader x, SudokuRowHeader y)
        {
            return x.Equals(y);
        }

        int IEqualityComparer<SudokuRowHeader>.GetHashCode(SudokuRowHeader obj)
        {
            return obj.GetHashCode();
        }
    }
}
