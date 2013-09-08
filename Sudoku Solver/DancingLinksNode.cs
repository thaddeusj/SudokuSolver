using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class DancingLinksNode
    {

        //Same Column
        public DancingLinksNode up;
        public DancingLinksNode down;

        //Same Row
        public DancingLinksNode right;
        public DancingLinksNode left;

        public Tuple<int, int, int> row;
        public DancingLinksColumnHeader header;



        public DancingLinksNode(DancingLinksColumnHeader h, int cellx, int celly, int cellnum)
        {
            row = new Tuple<int, int, int>(cellx, celly, cellnum);
            header = h;

        }



    }
}
