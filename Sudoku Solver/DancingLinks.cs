using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class DancingLinks
    {

        List<DancingLinksColumnHeader> columns;
        public List<SudokuRowHeader> rows;
        Dictionary<Triple<int>, SudokuRowHeader> rowLookUp;

        public DancingLinks(SudokuGrid s)
        {
            rowLookUp = new Dictionary<Triple<int>, SudokuRowHeader>();


            if (s.Validate())
            {
                columns = new List<DancingLinksColumnHeader>();
                rows = new List<SudokuRowHeader>();


                //Initialise blank Sudoku Grid.


                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        columns.Add(new DancingLinksColumnHeader(new Tuple<int, int>(i, j), DancingLinksColumnHeader.ColumnType.Cell));
                        columns.Add(new DancingLinksColumnHeader(new Tuple<int, int>(i, j), DancingLinksColumnHeader.ColumnType.Row));
                        columns.Add(new DancingLinksColumnHeader(new Tuple<int, int>(i, j), DancingLinksColumnHeader.ColumnType.Square));
                        columns.Add(new DancingLinksColumnHeader(new Tuple<int, int>(i, j), DancingLinksColumnHeader.ColumnType.Column));


                        for (int k = 1; k <= 9; k++)
                        {
                            SudokuRowHeader r = new SudokuRowHeader(new Triple<int>(i, j, k));
                            rows.Add(r);
                            rowLookUp.Add(new Triple<int>(i, j, k), r);
                        }
                    }
                }

                //Convention: for cell numbers, x is the row and y is the column. This is reversed for square numbering. This is an artefact of how I am visualising the objects.

                //Column Linking
                foreach (DancingLinksColumnHeader column in columns)
                {
                    column.columnMemberCount = 9;

                    if (column.type == DancingLinksColumnHeader.ColumnType.Cell)
                    {
                        int content;
                        if (int.TryParse(s.boxes[column.constraintNum.Item1 - 1, column.constraintNum.Item2- 1].Text, out content))
                        {
                            column.columnMemberCount = 1;
                            column.firstEntry = new DancingLinksNode(column, column.constraintNum.Item1, column.constraintNum.Item2, content);

                            column.firstEntry.up = column.firstEntry;
                            column.firstEntry.down = column.firstEntry;

                            if (rowLookUp.ContainsKey(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, content)))
                            {
                                rowLookUp[new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, content)].firstNode.left.right = column.firstEntry;
                                rowLookUp[new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, content)].firstNode.left = column.firstEntry;
                            }
                            else
                            {
                                SudokuRowHeader r = new SudokuRowHeader(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, content));
                                rows.Add(r);
                                rowLookUp.Add(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, content), r);

                                r.firstNode = column.firstEntry;
                            }

                        }
                        else
                        {

                            column.firstEntry = new DancingLinksNode(column, column.constraintNum.Item1, column.constraintNum.Item2, 1);

                            DancingLinksNode curNode = column.firstEntry;

                            if (rowLookUp.ContainsKey(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, 1)))
                            {
                                rowLookUp[new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, 1)].firstNode.left.right = curNode;
                                rowLookUp[new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, 1)].firstNode.left = curNode;
                            }
                            else
                            {
                                SudokuRowHeader r = new SudokuRowHeader(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, 1));
                                rows.Add(r);
                                rowLookUp.Add(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, 1), r);

                                r.firstNode = curNode;
                            }

                            
                            for (int i = 2; i <= 9; i++)
                            {
                                curNode.down = new DancingLinksNode(column, column.constraintNum.Item1, column.constraintNum.Item2, i);
                                curNode.down.up = curNode;
                                curNode = curNode.down;

                                if (i == 9)
                                {
                                    column.firstEntry.up = curNode;
                                    curNode.down = column.firstEntry;
                                }

                                if (rowLookUp.ContainsKey(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, i)))
                                {
                                    rowLookUp[new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, i)].firstNode.left.right = curNode;
                                    rowLookUp[new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, i)].firstNode.left = curNode;
                                }
                                else
                                {
                                    SudokuRowHeader r = new SudokuRowHeader(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, i));
                                    rows.Add(r);
                                    rowLookUp.Add(new Triple<int>(column.constraintNum.Item1, column.constraintNum.Item2, i), r);

                                    r.firstNode = curNode;
                                }
                            }
                        }
                    }

                    if (column.type == DancingLinksColumnHeader.ColumnType.Column)
                    {

                        int content;
                        int rowNum = 0;
                        bool isInColumn = false;

                        for (int i = 1; i <= 9 && !isInColumn; i++)
                        {
                            if (int.TryParse(s.boxes[i -1 , column.constraintNum.Item1 - 1].Text, out content))
                            {
                                if (content == column.constraintNum.Item2) isInColumn = true;
                                rowNum = i;
                            }
                        }

                        if (isInColumn)
                        {
                            column.columnMemberCount = 1;

                            column.firstEntry = new DancingLinksNode(column, rowNum, column.constraintNum.Item1, column.constraintNum.Item2);
                            column.firstEntry.up = column.firstEntry;
                            column.firstEntry.down = column.firstEntry;
                        }
                        else
                        {
                            column.firstEntry = new DancingLinksNode(column, 1, column.constraintNum.Item1, column.constraintNum.Item2);

                            DancingLinksNode curNode = column.firstEntry;


                            for (int i = 2; i <= 9; i++)
                            {
                                curNode.down = new DancingLinksNode(column, i, column.constraintNum.Item1, column.constraintNum.Item2);
                                curNode.down.up = curNode;
                                curNode = curNode.down;

                                if (i == 9)
                                {
                                    column.firstEntry.up = curNode;
                                    curNode.down = column.firstEntry;
                                }
                            }
                        }

                    }

                    if (column.type == DancingLinksColumnHeader.ColumnType.Row)
                    {

                        int content;
                        int colNum = 0;
                        bool isInColumn = false;

                        for (int i = 1; i <= 9 && !isInColumn; i++)
                        {
                            if (int.TryParse(s.boxes[column.constraintNum.Item1 - 1,i - 1].Text, out content))
                            {
                                if (content == column.constraintNum.Item2) isInColumn = true;
                                colNum = i;
                            }
                        }

                        if (isInColumn)
                        {
                            column.columnMemberCount = 1;

                            column.firstEntry = new DancingLinksNode(column, column.constraintNum.Item1, colNum, column.constraintNum.Item2);
                            column.firstEntry.up = column.firstEntry;
                            column.firstEntry.down = column.firstEntry;
                        }
                        else
                        {

                            column.firstEntry = new DancingLinksNode(column, column.constraintNum.Item1, 1, column.constraintNum.Item2);

                            DancingLinksNode curNode = column.firstEntry;


                            for (int i = 2; i <= 9; i++)
                            {
                                curNode.down = new DancingLinksNode(column, column.constraintNum.Item1, i, column.constraintNum.Item2);
                                curNode.down.up = curNode;
                                curNode = curNode.down;

                                if (i == 9)
                                {
                                    column.firstEntry.up = curNode;
                                    curNode.down = column.firstEntry;
                                }
                            }
                        }

                    }

                    if (column.type == DancingLinksColumnHeader.ColumnType.Square)
                    {
                        int squareX = column.constraintNum.Item1 % 3 - 1;
                        if (squareX < 0) squareX += 3;
                        int squareY = (column.constraintNum.Item1 - (squareX + 1)) / 3;

                        int rowNum = 0;
                        int colNum = 0;

                        bool isInSquare = false;

                        for (int i = 1; i <= 3; i++)
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                int content;

                                if (int.TryParse(s.boxes[squareY * 3 + i - 1, squareX * 3 + j - 1].Text, out content))
                                {
                                    if (content == column.constraintNum.Item2)
                                    {
                                        isInSquare = true;

                                        rowNum = squareY * 3 + i;
                                        colNum = squareX * 3 + j;
                                    }
                                }
                                   
                            }
                        }

                        if (isInSquare)
                        {
                            column.columnMemberCount = 1;

                            column.firstEntry = new DancingLinksNode(column, rowNum, colNum, column.constraintNum.Item2);


                            column.firstEntry.up = column.firstEntry;
                            column.firstEntry.down = column.firstEntry;


                        }
                        else
                        {
                            column.firstEntry = new DancingLinksNode(column, squareY * 3 + 1, squareX * 3 + 1, column.constraintNum.Item2);
                            DancingLinksNode curNode = column.firstEntry;


                            for (int i = 1; i <= 3; i++)
                            {
                                for (int j = 1; j <= 3; j++)
                                {
                                    if (i != 1 || j != 1)
                                    {

                                        curNode.down = new DancingLinksNode(column, squareY * 3 + i, squareX * 3 + j, column.constraintNum.Item2);
                                        curNode.down.up = curNode;
                                        curNode = curNode.down;
                                    }

                                    if (i == 3 && j == 3)
                                    {
                                        column.firstEntry.up = curNode;
                                        curNode.down = column.firstEntry;
                                    }

                                }
                            }
                        }
                    }

                }
                
            }
        }

        public bool dancingLinksAlg()
        {
            if (columns.Count == 0)
            {
                return true;
            }

            int minCount = columns.Min(x => x.columnMemberCount);

            if (minCount <= 0) return false;

            DancingLinksColumnHeader c = columns.Where(x => x.columnMemberCount == minCount).ToArray()[0];
            int count = c.columnMemberCount;
            DancingLinksNode curNode = c.firstEntry;


            //Outer loop iterates through rows intersecting the column

            //Inside this, we look at a given row. Iterate through the columns intersecting that row.


            while (count > 0)
            {

                #region Removal_and_Test
                DancingLinksNode curColPivot = curNode.right;

                while (curColPivot != curNode)
                {
                    DancingLinksNode curRowPivot = curColPivot.down;

                    while (curRowPivot != curColPivot)
                    {

                        #region Remove_rows_Except_Pivot_Row
                        DancingLinksNode curRemNode = curRowPivot.right;
                        while (curRemNode != curRowPivot)
                        {
                            curRemNode.up.down = curRemNode.down;
                            curRemNode.down.up = curRemNode.up;

                            curRemNode.header.columnMemberCount--;

                            curRemNode = curRemNode.right;
                        }

                        rows.Remove(rowLookUp[curRowPivot.row]);
                        #endregion

                        //curRowPivot.right.left = curRowPivot.left;
                        //curRowPivot.left.right = curRowPivot.right;

                        curRowPivot = curRowPivot.down;

                    }

                    columns.Remove(curColPivot.header);

                    //We must remove the Pivot Row as well.
                    //Notice that we do not remove the Pivot Row from the rows list. This is intentional.
                    curColPivot.up.down = curColPivot.down;
                    curColPivot.down.up = curColPivot.up;

                    curColPivot = curColPivot.right;
                }

                //Lastly, we remove the pivot column.
                columns.Remove(curNode.header);

                curNode.left.right = curNode.right;
                curNode.right.left = curNode.left;

                DancingLinksNode colRemNode = curNode.down;
                while (colRemNode != curNode)
                {
                    colRemNode.left.right = colRemNode.right;
                    colRemNode.right.left = colRemNode.left;

                    colRemNode = colRemNode.down;
                }



                if (dancingLinksAlg()) return true;

                #endregion

                #region Readd
                else
                {
                    //If removing this row didn't work, backtrack and undo all those row and column removals.

                    columns.Add(curNode.header);

                    curNode.right.left = curNode;
                    curNode.left.right = curNode;


                    DancingLinksNode colAddNode = curNode.down;
                    while (colAddNode != curNode)
                    {
                        colAddNode.left.right = colAddNode.right;
                        colAddNode.right.left = colAddNode.left;

                        colAddNode = colAddNode.down;
                    }

                    //Now, we follow the links into the inner rows and columns to restore them as well.
                    curColPivot = curNode.right;

                    while (curColPivot != curNode)
                    {
                        DancingLinksNode curRowPivot = curColPivot.down;

                        while (curRowPivot != curColPivot)
                        {

                            #region Add_rows_Except_Pivot_Row
                            DancingLinksNode curAddNode = curRowPivot.right;
                            while (curAddNode != curRowPivot)
                            {
                                curAddNode.up.down = curAddNode;
                                curAddNode.down.up = curAddNode;

                                curAddNode.header.columnMemberCount++;

                                curAddNode = curAddNode.right;
                            }

                            rows.Add(rowLookUp[curRowPivot.row]);
                            #endregion

                            //curRowPivot.right.left = curRowPivot.left;
                            //curRowPivot.left.right = curRowPivot.right;

                            curRowPivot = curRowPivot.down;

                        }

                        columns.Add(curColPivot.header);

                        //We must Add the Pivot Row as well.
                        curColPivot.up.down = curColPivot;
                        curColPivot.down.up = curColPivot;

                        curColPivot = curColPivot.right;
                    }

                }
                #endregion

                curNode = curNode.down;
                count--;
            }

            return false;

        }

    }
}
