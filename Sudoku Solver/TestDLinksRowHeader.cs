using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class TestDLinksRowHeader : AbstractDLinks.RowHeader
    {

        public string set;


        public TestDLinksRowHeader(string s)
        {
            set = s;
        }

        public override string ToString()
        {
            return set;
        }
    }
}
