using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.SudokuDLinks
{
    public class SudokuColumnHeader : AbstractDLinks.ColumnHeader
    {
        public enum ColumnType { Cell, Row, Column, Square };


        public ColumnType type;
        public Pair<int> constraintNum; //constraintNum is a catchall variable for the 4 different constraint types, since each can be labeled by 2 integers.
                                        //Cells are labelled by (row,column), rows by (row,content), columns by (column,content), and squares by (square, content).
                                        //Squares are labelled via: 1 2 3
                                        //                          4 5 6
                                        //                          7 8 9

        public SudokuColumnHeader(ColumnType t, Pair<int> constraints) { type = t; constraintNum = constraints; }


    }
}
