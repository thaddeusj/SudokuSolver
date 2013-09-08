using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class DancingLinksColumnHeader
    {

        public enum ColumnType {Row,Column,Square,Cell};


        public int columnMemberCount;
        public ColumnType type;

        public DancingLinksNode firstEntry;

        
        //ConstaintNum is used to determine the row, column, or square, and the corresponding number in said entity
        // OR gives a cell position.
        public Tuple<int,int> constraintNum;




        public DancingLinksColumnHeader(Tuple<int, int> c, ColumnType t)
        {
            type = t;
            constraintNum = c;
        }
        

    }
}
