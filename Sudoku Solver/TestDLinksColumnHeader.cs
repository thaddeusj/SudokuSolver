using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class TestDLinksColumnHeader : AbstractDLinks.ColumnHeader
    {

        public int num;

        public TestDLinksColumnHeader(int n)
        {
            num = n;
        }

        public override string ToString()
        {
            return num.ToString();
        }

    }
}
