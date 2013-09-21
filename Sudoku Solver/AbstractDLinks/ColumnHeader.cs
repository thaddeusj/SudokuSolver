using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.AbstractDLinks
{
    public abstract class ColumnHeader
    {

        public int memberCount;
        public Node firstNode;

        public void appendNode(Node n)
        {
            if (firstNode == null)
            {
                firstNode = n;
                n.up = n;
                n.down = n;
            }
            else
            {
                n.up = firstNode.up;
                n.down = firstNode;

                n.up.down = n;
                n.down.up = n;
            }

            memberCount++;
        }
    }
}
