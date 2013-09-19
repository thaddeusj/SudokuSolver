using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.SudokuDLinks
{
    public class SudokuDLinksAlg : AbstractDLinks.DLinksAlg
    {
        public SudokuDLinksAlg(SudokuGrid s)
        {
            //Initialise the constraint matrix.

            chooseRow = new AbstractDLinks.RowChooser(rowChoose);

            List<SudokuRowHeader> tempRows = new List<SudokuRowHeader>();
            columns = new List<AbstractDLinks.ColumnHeader>();
            rows = new List<AbstractDLinks.RowHeader>();

            Dictionary<SudokuColumnHeader.ColumnType, Dictionary<Pair<int>, SudokuColumnHeader>> lookUpDict = 
                new Dictionary<SudokuColumnHeader.ColumnType, Dictionary<Pair<int>, SudokuColumnHeader>>();

            for(int i = 1; i <= 9; i++)
                for (int j = 1; j <= 9; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        SudokuColumnHeader curCol = new SudokuColumnHeader((SudokuColumnHeader.ColumnType)k, new Pair<int>(i, j));

                        if (lookUpDict.Keys.Contains((SudokuColumnHeader.ColumnType)k))
                        {
                            lookUpDict[(SudokuColumnHeader.ColumnType)k].Add(new Pair<int>(i, j), curCol);

                        }
                        else
                        {
                            lookUpDict.Add((SudokuColumnHeader.ColumnType)k, new Dictionary<Pair<int>, SudokuColumnHeader>());
                            lookUpDict[(SudokuColumnHeader.ColumnType)k].Add(new Pair<int>(i, j), curCol);
                        }
                        curCol.memberCount = 0;

                        #region commentedOut
                        //switch ((SudokuColumnHeader.ColumnType)k)
                        //{

                        //    #region CellConstraintInit
                        //    case SudokuColumnHeader.ColumnType.Cell:
                        //        {
                        //            int content;

                        //            if (int.TryParse(s.boxes[i - 1, j - 1].Text, out content))
                        //            {
                        //                SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, j, content));
                        //                AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //                if (tempRows.Contains(newRow))
                        //                {
                        //                    SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));


                        //                    firstNodeIndex.firstNode.left.right = newNode;
                        //                    firstNodeIndex.firstNode.left = newNode;
                        //                }
                        //                else
                        //                {
                        //                    tempRows.Add(newRow);

                        //                    newRow.firstNode = newNode;
                        //                    newNode.left = newNode;
                        //                    newNode.right = newNode;
                        //                }

                        //                curCol.firstNode = newNode;
                        //                curCol.memberCount = 1;

                        //                newNode.up = newNode;
                        //                newNode.down = newNode;
                        //            }
                        //            else
                        //            {
                        //                curCol.memberCount = 0;

                        //                for (int n = 1; n <= 9; n++)
                        //                {
                        //                    SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, j, n));
                        //                    AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //                    if (tempRows.Contains(newRow))
                        //                    {
                        //                        SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                        firstNodeIndex.firstNode.left.right = newNode;
                        //                        firstNodeIndex.firstNode.left = newNode;
                        //                    }
                        //                    else
                        //                    {
                        //                        tempRows.Add(newRow);

                        //                        newRow.firstNode = newNode;
                        //                        newNode.left = newNode;
                        //                        newNode.right = newNode;
                        //                    }

                        //                    if (curCol.firstNode == null)
                        //                    {
                        //                        curCol.firstNode = newNode;
                        //                        newNode.up = newNode;
                        //                        newNode.down = newNode;
                        //                    }
                        //                    else
                        //                    {
                        //                        curCol.firstNode.up.down = newNode;
                        //                        curCol.firstNode.up = newNode;
                        //                    }

                        //                    curCol.memberCount++;

                        //                }
                        //            }
                        //        }
                        //        break;
                        //    #endregion

                        //    #region ColumnConstraintInit
                        //    case SudokuColumnHeader.ColumnType.Column:
                        //        bool isInCol = false;
                        //        int row = 1;
                        //        for (int n = 1; n <= 9 && !isInCol ; n++)
                        //        {
                        //            int content;
                        //            int.TryParse(s.boxes[n - 1, i - 1].Text, out content);

                        //            if (content == j)
                        //            {
                        //                row = n;
                        //                isInCol = true;
                        //            }
                        //        }

                        //        if (isInCol)
                        //        {
                        //            SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(row, i, j));
                        //            AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //            if (tempRows.Contains(newRow))
                        //            {
                        //                SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                firstNodeIndex.firstNode.left.right = newNode;
                        //                firstNodeIndex.firstNode.left = newNode;
                        //            }
                        //            else
                        //            {
                        //                tempRows.Add(newRow);

                        //                newRow.firstNode = newNode;
                        //                newNode.left = newNode;
                        //                newNode.right = newNode;
                        //            }

                        //            curCol.firstNode = newNode;
                        //            curCol.memberCount = 1;

                        //            newNode.up = newNode;
                        //            newNode.down = newNode;
                        //        }
                        //        else
                        //        {
                        //            for (int n = 1; n <= 9; n++)
                        //            {
                        //                SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(n, i, j));
                        //                AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //                if (tempRows.Contains(newRow))
                        //                {
                        //                    SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                    firstNodeIndex.firstNode.left.right = newNode;
                        //                    firstNodeIndex.firstNode.left = newNode;
                        //                }
                        //                else
                        //                {
                        //                    tempRows.Add(newRow);

                        //                    newRow.firstNode = newNode;
                        //                    newNode.left = newNode;
                        //                    newNode.right = newNode;
                        //                }

                        //                if (curCol.firstNode == null)
                        //                {
                        //                    curCol.firstNode = newNode;
                        //                    newNode.up = newNode;
                        //                    newNode.down = newNode;
                        //                }
                        //                else
                        //                {
                        //                    curCol.firstNode.up.down = newNode;
                        //                    curCol.firstNode.up = newNode;
                        //                }

                        //                curCol.memberCount++;

                        //            }

                        //        }

                        //        break;
                        //    #endregion

                        //    #region RowConstraintInit
                        //    case SudokuColumnHeader.ColumnType.Row:

                        //        bool isInRow = false;
                        //        int col = 1;
                        //        for (int n = 1; n <= 9 && !isInRow ; n++)
                        //        {
                        //            int content;
                        //            int.TryParse(s.boxes[i-1, n-1].Text, out content);

                        //            if (content == j)
                        //            {
                        //                col = n;
                        //                isInRow = true;
                        //            }
                        //        }

                        //        if (isInRow)
                        //        {
                        //            SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, col, j));
                        //            AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //            if (tempRows.Contains(newRow))
                        //            {
                        //                SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                firstNodeIndex.firstNode.left.right = newNode;
                        //                firstNodeIndex.firstNode.left = newNode;
                        //            }
                        //            else
                        //            {
                        //                tempRows.Add(newRow);

                        //                newRow.firstNode = newNode;
                        //                newNode.left = newNode;
                        //                newNode.right = newNode;
                        //            }

                        //            curCol.firstNode = newNode;
                        //            curCol.memberCount = 1;

                        //            newNode.up = newNode;
                        //            newNode.down = newNode;
                        //        }
                        //        else
                        //        {
                        //            for (int n = 1; n <= 9; n++)
                        //            {
                        //                SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, n, j));
                        //                AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //                if (tempRows.Contains(newRow))
                        //                {
                        //                    SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                    firstNodeIndex.firstNode.left.right = newNode;
                        //                    firstNodeIndex.firstNode.left = newNode;
                        //                }
                        //                else
                        //                {
                        //                    tempRows.Add(newRow);

                        //                    newRow.firstNode = newNode;
                        //                    newNode.left = newNode;
                        //                    newNode.right = newNode;
                        //                }

                        //                if (curCol.firstNode == null)
                        //                {
                        //                    curCol.firstNode = newNode;
                        //                    newNode.up = newNode;
                        //                    newNode.down = newNode;
                        //                }
                        //                else
                        //                {
                        //                    curCol.firstNode.up.down = newNode;
                        //                    curCol.firstNode.up = newNode;
                        //                }

                        //                curCol.memberCount++;

                        //            }

                        //        }

                        //        break;

                        //    #endregion

                        //    #region SquareConstraintInit
                        //    case SudokuColumnHeader.ColumnType.Square:

                        //        int SquareX = (i + 2) % 3;
                        //        int SquareY = (i-1)/3;

                        //        Pair<int> cell = new Pair<int>(0,0);

                        //        bool isInSquare = false;

                        //        for (int n = 1; n <= 3 && !isInSquare; n++)
                        //        {
                        //            for (int m = 1; m <= 3 && !isInSquare; m++)
                        //            {
                        //                int content;

                        //                if (int.TryParse(s.boxes[SquareY * 3 + n - 1, SquareX * 3 + m - 1].Text, out content))
                        //                {
                        //                    if (content == j)
                        //                    {
                        //                        isInSquare = true;
                        //                        cell = new Pair<int>(SquareY * 3 + n, SquareX * 3 + m);
                        //                    }
                        //                }
                        //            }
                        //        }


                        //        if (isInSquare)
                        //        {
                        //            SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(cell.Item1, cell.Item2, j));
                        //            AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //            if (tempRows.Contains(newRow))
                        //            {
                        //                SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                firstNodeIndex.firstNode.left.right = newNode;
                        //                firstNodeIndex.firstNode.left = newNode;
                        //            }
                        //            else
                        //            {
                        //                tempRows.Add(newRow);

                        //                newRow.firstNode = newNode;
                        //                newNode.left = newNode;
                        //                newNode.right = newNode;
                        //            }

                        //            curCol.firstNode = newNode;
                        //            curCol.memberCount = 1;

                        //            newNode.up = newNode;
                        //            newNode.down = newNode;
                        //        }
                        //        else
                        //        {
                        //            curCol.memberCount = 0;

                        //            for (int n = 1; n <= 3; n++)
                        //            {
                        //                for (int m = 1; m <= 3; m++)
                        //                {
                        //                    SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(SquareY*3 + n, SquareX*3 + m, j));
                        //                    AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                        //                    if (tempRows.Contains(newRow))
                        //                    {
                        //                        SudokuRowHeader firstNodeIndex = tempRows.Find(x => x.Equals(newRow));

                        //                        firstNodeIndex.firstNode.left.right = newNode;
                        //                        firstNodeIndex.firstNode.left = newNode;
                        //                    }
                        //                    else
                        //                    {
                        //                        tempRows.Add(newRow);

                        //                        newRow.firstNode = newNode;
                        //                        newNode.left = newNode;
                        //                        newNode.right = newNode;
                        //                    }

                        //                    if (curCol.firstNode == null)
                        //                    {
                        //                        curCol.firstNode = newNode;
                        //                        newNode.up = newNode;
                        //                        newNode.down = newNode;
                        //                    }
                        //                    else
                        //                    {
                        //                        curCol.firstNode.up.down = newNode;
                        //                        curCol.firstNode.up = newNode;
                        //                    }

                        //                    curCol.memberCount++;
                        //                }
                        //            }
                        //        }

                        //        break;

                        //    #endregion
                        //}
                        #endregion




                        columns.Add(curCol);
                    }
                }

            for (int row = 1; row <= 9; row++)
            {
                for (int col = 1; col <= 9; col++)
                {
                    for (int content = 1; content <= 9; content++)
                    {

                        int gridContent;
                        if (int.TryParse(s.boxes[row - 1, col - 1].Text, out gridContent))
                        {
                            if (content == gridContent)
                            {
                                SudokuRowHeader thisRow = new SudokuRowHeader(new Triple<int>(row, col, content));

                                tempRows.Add(thisRow);

                                //4 constraints
                                AbstractDLinks.Node cellNode;
                                AbstractDLinks.Node rowNode;
                                AbstractDLinks.Node colNode;
                                AbstractDLinks.Node sqNode;

                                //Get column headers
                                SudokuColumnHeader cellHead = lookUpDict[SudokuColumnHeader.ColumnType.Cell][new Pair<int>(row, col)];
                                SudokuColumnHeader rowHead  = lookUpDict[SudokuColumnHeader.ColumnType.Row][new Pair<int>(row, content)];
                                SudokuColumnHeader colHead  = lookUpDict[SudokuColumnHeader.ColumnType.Column][new Pair<int>(col, content)];

                                int sqNum = ((row - 1) / 3) * 3 + (col + 2) / 3;

                                SudokuColumnHeader sqHead   = lookUpDict[SudokuColumnHeader.ColumnType.Square][new Pair<int>(sqNum, content)];


                                //Define nodes:
                                cellNode = new AbstractDLinks.Node(thisRow, cellHead);
                                rowNode  = new AbstractDLinks.Node(thisRow, rowHead);
                                colNode  = new AbstractDLinks.Node(thisRow, colHead);
                                sqNode   = new AbstractDLinks.Node(thisRow, sqHead);
                                
                                //Define relations between them.
                                cellNode.right = rowNode;
                                rowNode.right = colNode;
                                colNode.right = sqNode;
                                sqNode.right = cellNode;

                                cellNode.left = sqNode;
                                sqNode.left = colNode;
                                colNode.left = rowNode;
                                rowNode.left = cellNode;

                                //Insert nodes into their columns

                                AbstractDLinks.Node curNode = cellNode;
                                do
                                {
                                    if (curNode.column.firstNode == null)
                                    {
                                        curNode.column.firstNode = curNode;
                                        curNode.up = curNode;
                                        curNode.down = curNode;
                                    }
                                    else
                                    {
                                        curNode.up = curNode.column.firstNode.up;
                                        curNode.up.down = curNode;

                                        curNode.down = curNode.column.firstNode;
                                        curNode.down.up = curNode;
                                    }
                                    curNode.column.memberCount++;

                                    curNode = curNode.right;

                                } while (curNode != cellNode);

                                //Lastly, give the row a reference to the nodes.
                                thisRow.firstNode = cellNode;
                            }
                        }
                        else
                        {
                            //Check if the number is used in the column, row, or square.
                            bool numUsed = false;

                            for (int n = 1; n <= 9; n++)
                            {
                                int rowContent;
                                int colContent;

                                if (int.TryParse(s.boxes[n-1, col-1].Text, out rowContent))
                                {
                                    if (rowContent == content) numUsed = true;
                                }
                                if (int.TryParse(s.boxes[row-1, n-1].Text, out colContent))
                                {
                                    if (colContent == content) numUsed = true;
                                }
                            }

                            for (int n = ((row-1) / 3) * 3; n < ((row-1) / 3) * 3 + 3; n++)
                            {
                                for (int m = ((col-1) / 3) * 3; m < ((col-1) / 3) * 3 + 3; m++)
                                {
                                    int sqContent;
                                    if (int.TryParse(s.boxes[n, m].Text, out sqContent))
                                    {
                                        if (sqContent == content) numUsed = true;
                                    }
                                }
                            }

                            if (!numUsed)
                            {
                                SudokuRowHeader thisRow = new SudokuRowHeader(new Triple<int>(row, col, content));

                                tempRows.Add(thisRow);

                                //4 constraints
                                AbstractDLinks.Node cellNode;
                                AbstractDLinks.Node rowNode;
                                AbstractDLinks.Node colNode;
                                AbstractDLinks.Node sqNode;

                                //Get column headers
                                SudokuColumnHeader cellHead = lookUpDict[SudokuColumnHeader.ColumnType.Cell][new Pair<int>(row, col)];
                                SudokuColumnHeader rowHead = lookUpDict[SudokuColumnHeader.ColumnType.Row][new Pair<int>(row, content)];
                                SudokuColumnHeader colHead = lookUpDict[SudokuColumnHeader.ColumnType.Column][new Pair<int>(col, content)];

                                int sqNum = ((row - 1) / 3) * 3 + (col + 2) / 3;

                                SudokuColumnHeader sqHead = lookUpDict[SudokuColumnHeader.ColumnType.Square][new Pair<int>(sqNum, content)];


                                //Define nodes:
                                cellNode = new AbstractDLinks.Node(thisRow, cellHead);
                                rowNode = new AbstractDLinks.Node(thisRow, rowHead);
                                colNode = new AbstractDLinks.Node(thisRow, colHead);
                                sqNode = new AbstractDLinks.Node(thisRow, sqHead);

                                //Define relations between them.
                                cellNode.right = rowNode;
                                rowNode.right = colNode;
                                colNode.right = sqNode;
                                sqNode.right = cellNode;

                                cellNode.left = sqNode;
                                sqNode.left = colNode;
                                colNode.left = rowNode;
                                rowNode.left = cellNode;

                                //Insert nodes into their columns

                                AbstractDLinks.Node curNode = cellNode;
                                do
                                {
                                    if (curNode.column.firstNode == null)
                                    {
                                        curNode.column.firstNode = curNode;
                                        curNode.up = curNode;
                                        curNode.down = curNode;
                                    }
                                    else
                                    {                                        
                                        curNode.up = curNode.column.firstNode.up;
                                        curNode.up.down = curNode;

                                        curNode.down = curNode.column.firstNode;
                                        curNode.down.up = curNode;

                                    }
                                    curNode.column.memberCount++;

                                    curNode = curNode.right;

                                } while (curNode != cellNode);

                                //Lastly, give the row a reference to the nodes.
                                thisRow.firstNode = cellNode;
                            }

                        }

                    }
                }
            }


            foreach (AbstractDLinks.RowHeader row in tempRows)
            {
                if(row.firstNode != null) rows.Add(row);
            }
        }

        public AbstractDLinks.RowHeader rowChoose(List<AbstractDLinks.RowHeader> rows)
        {
            if (rows.Count == 0) throw new Exception("No rows to choose.");
            return rows[0];
        }

    }
}
