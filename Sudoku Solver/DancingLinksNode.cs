using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    public class DancingLinksNode
    {

        //Same Column
        public DancingLinksNode up;
        public DancingLinksNode down;

        //Same Row
        public DancingLinksNode right;
        public DancingLinksNode left;

        public DancingLinksColumnHeader header;

        public Triple<int> row;


        public DancingLinksNode(DancingLinksColumnHeader h, int cellx, int celly, int cellnum)
        {
            row = new Triple<int>(cellx, celly, cellnum);
            header = h;

        }



    }
}
