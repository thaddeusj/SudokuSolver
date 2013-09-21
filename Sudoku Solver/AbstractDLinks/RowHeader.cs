using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.AbstractDLinks
{
    public abstract class RowHeader
    {
        public Node firstNode;

        public void appendNode(Node n)
        {
            if (firstNode == null)
            {
                firstNode = n;
                n.right = n;
                n.left = n;
            }
            else
            {
                n.right = firstNode;
                n.left = firstNode.left;

                n.right.left = n;
                n.left.right = n;
            }

            n.column.appendNode(n);
        }
    }
}
