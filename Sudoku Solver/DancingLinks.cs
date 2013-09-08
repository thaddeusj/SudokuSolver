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
        List<Tuple<int, int, int>> rows;


        public DancingLinks()
        {
            columns = new List<DancingLinksColumnHeader>();
            rows = new List<Tuple<int, int, int>>();


            //Initialise blank Sudoku Grid.


            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    columns.Add(new DancingLinksColumnHeader(new Tuple<int,int>(i,j),DancingLinksColumnHeader.ColumnType.Cell));
                    columns.Add(new DancingLinksColumnHeader(new Tuple<int,int>(i,j),DancingLinksColumnHeader.ColumnType.Row));
                    columns.Add(new DancingLinksColumnHeader(new Tuple<int,int>(i,j),DancingLinksColumnHeader.ColumnType.Square));
                    columns.Add(new DancingLinksColumnHeader(new Tuple<int,int>(i,j),DancingLinksColumnHeader.ColumnType.Column));


                    for (int k = 1; k <= 9; k++)
                    {
                        rows.Add(new Tuple<int, int, int>(i, j, k));
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

                    column.firstEntry = new DancingLinksNode(column, column.constraintNum.Item1, column.constraintNum.Item2, 1);

                    DancingLinksNode curNode = column.firstEntry;


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
                    }

                }

                if (column.type == DancingLinksColumnHeader.ColumnType.Column)
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

                if (column.type == DancingLinksColumnHeader.ColumnType.Row)
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

                if (column.type == DancingLinksColumnHeader.ColumnType.Square)
                {
                    int squareX = column.constraintNum.Item1 % 3 - 1;
                    int squareY = (column.constraintNum.Item1 - (squareX + 1)) / 3;


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

            //Row Linking. Eek.
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    for (int k = 1; k <= 9; k++)
                    {

                        DancingLinksNode curNode = null;
                        DancingLinksNode firstNode = null;

                        foreach (DancingLinksColumnHeader col in columns)
                        {
                            switch (col.type)
                            {
                                case DancingLinksColumnHeader.ColumnType.Cell:

                                    if (col.constraintNum.Item1 != i || col.constraintNum.Item2 != j) continue;

                                    if (curNode == null)
                                    {
                                        
                                        curNode = col.firstEntry;
                                        firstNode = curNode;
                                        for (int n = k - 1; n > 0; n--)
                                        {
                                            curNode = curNode.down;
                                        }
                                    }

                                    else
                                    {
                                        DancingLinksNode tempNode = col.firstEntry;

                                        for (int n = k - 1; n > 0; n--)
                                        {
                                            tempNode = tempNode.down;
                                        }

                                        curNode.right = tempNode;
                                        tempNode.left = curNode;
                                        curNode = tempNode;
                                    }
                                    
                                    break;


                            }

                        }

                        firstNode.left = curNode;
                        curNode.right = firstNode;
                    }
                }
            }


        }




    }
}
