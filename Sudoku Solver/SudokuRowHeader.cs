using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class SudokuRowHeader : IEquatable<SudokuRowHeader>
    {
        public Triple<int> row;
        public DancingLinksNode firstNode;

        public SudokuRowHeader(Triple<int> r)
        {
            row = r;
        }

        public bool Equals(SudokuRowHeader other)
        {
            if (other == null) return false;

            return row.Equals(other.row);
        }
    }

    public class SudokuRowHeaderComparer : IEqualityComparer<SudokuRowHeader>
    {

        public bool Equals(SudokuRowHeader x, SudokuRowHeader y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(SudokuRowHeader obj)
        {
            return obj.GetHashCode();
        }
    }
}
