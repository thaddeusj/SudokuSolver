using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.AbstractDLinks
{
    public class Node
    {
        public Node up;
        public Node down;
        public Node right;
        public Node left;

        public RowHeader row;
        public ColumnHeader column;


        public Node(RowHeader r, ColumnHeader c)
        {
            row = r;
            column = c;
        } 


    }
}
