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

            List<SudokuRowHeader> tempRows = new List<SudokuRowHeader>();

            for(int i = 1; i <= 9; i++)
                for (int j = 1; j <= 9; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        SudokuColumnHeader curCol = new SudokuColumnHeader((SudokuColumnHeader.ColumnType)k, new Pair<int>(i, j));

                        switch ((SudokuColumnHeader.ColumnType)k)
                        {

                            #region CellConstraintInit
                            case SudokuColumnHeader.ColumnType.Cell:
                                {
                                    int content;

                                    if (s.boxes[i, j].Text.Length > 0 && int.TryParse(s.boxes[i, j].Text, out content))
                                    {
                                        SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, j, content));
                                        AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                        if (tempRows.Contains(newRow))
                                        {
                                            newRow.firstNode.left.right = newNode;
                                            newRow.firstNode.left = newNode;
                                        }
                                        else
                                        {
                                            tempRows.Add(newRow);

                                            newRow.firstNode = newNode;
                                            newNode.left = newNode;
                                            newNode.right = newNode;
                                        }

                                        curCol.firstNode = newNode;
                                        curCol.memberCount = 1;

                                        newNode.up = newNode;
                                        newNode.down = newNode;
                                    }
                                    else
                                    {
                                        curCol.memberCount = 0;

                                        for (int n = 1; n <= 9; n++)
                                        {
                                            SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, j, n));
                                            AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                            if (tempRows.Contains(newRow))
                                            {
                                                newRow.firstNode.left.right = newNode;
                                                newRow.firstNode.left = newNode;
                                            }
                                            else
                                            {
                                                tempRows.Add(newRow);

                                                newRow.firstNode = newNode;
                                                newNode.left = newNode;
                                                newNode.right = newNode;
                                            }

                                            if (curCol.firstNode == null)
                                            {
                                                curCol.firstNode = newNode;
                                                newNode.up = newNode;
                                                newNode.down = newNode;
                                            }
                                            else
                                            {
                                                curCol.firstNode.up.down = newNode;
                                                curCol.firstNode.up = newNode;
                                            }

                                            curCol.memberCount++;

                                        }
                                    }
                                }
                                break;
                            #endregion

                            #region ColumnConstraintInit
                            case SudokuColumnHeader.ColumnType.Column:
                                bool isInCol = false;
                                int row = 1;
                                for (int n = 1; n <= 9 && !isInCol ; n++)
                                {
                                    int content;
                                    int.TryParse(s.boxes[n, i].Text, out content);

                                    if (content == j)
                                    {
                                        row = n;
                                        isInCol = true;
                                    }
                                }

                                if (isInCol)
                                {
                                    SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(row, i, j));
                                    AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                    if (tempRows.Contains(newRow))
                                    {
                                        newRow.firstNode.left.right = newNode;
                                        newRow.firstNode.left = newNode;
                                    }
                                    else
                                    {
                                        tempRows.Add(newRow);

                                        newRow.firstNode = newNode;
                                        newNode.left = newNode;
                                        newNode.right = newNode;
                                    }

                                    curCol.firstNode = newNode;
                                    curCol.memberCount = 1;

                                    newNode.up = newNode;
                                    newNode.down = newNode;
                                }
                                else
                                {
                                    for (int n = 1; n <= 9; n++)
                                    {
                                        SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(n, i, j));
                                        AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                        if (tempRows.Contains(newRow))
                                        {
                                            newRow.firstNode.left.right = newNode;
                                            newRow.firstNode.left = newNode;
                                        }
                                        else
                                        {
                                            tempRows.Add(newRow);

                                            newRow.firstNode = newNode;
                                            newNode.left = newNode;
                                            newNode.right = newNode;
                                        }

                                        if (curCol.firstNode == null)
                                        {
                                            curCol.firstNode = newNode;
                                            newNode.up = newNode;
                                            newNode.down = newNode;
                                        }
                                        else
                                        {
                                            curCol.firstNode.up.down = newNode;
                                            curCol.firstNode.up = newNode;
                                        }

                                        curCol.memberCount++;

                                    }

                                }

                                break;
                            #endregion

                            #region RowConstraintInit
                            case SudokuColumnHeader.ColumnType.Row:

                                bool isInRow = false;
                                int col = 1;
                                for (int n = 1; n <= 9 && !isInRow ; n++)
                                {
                                    int content;
                                    int.TryParse(s.boxes[i, n].Text, out content);

                                    if (content == j)
                                    {
                                        col = n;
                                        isInRow = true;
                                    }
                                }

                                if (isInRow)
                                {
                                    SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, col, j));
                                    AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                    if (tempRows.Contains(newRow))
                                    {
                                        newRow.firstNode.left.right = newNode;
                                        newRow.firstNode.left = newNode;
                                    }
                                    else
                                    {
                                        tempRows.Add(newRow);

                                        newRow.firstNode = newNode;
                                        newNode.left = newNode;
                                        newNode.right = newNode;
                                    }

                                    curCol.firstNode = newNode;
                                    curCol.memberCount = 1;

                                    newNode.up = newNode;
                                    newNode.down = newNode;
                                }
                                else
                                {
                                    for (int n = 1; n <= 9; n++)
                                    {
                                        SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(i, n, j));
                                        AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                        if (tempRows.Contains(newRow))
                                        {
                                            newRow.firstNode.left.right = newNode;
                                            newRow.firstNode.left = newNode;
                                        }
                                        else
                                        {
                                            tempRows.Add(newRow);

                                            newRow.firstNode = newNode;
                                            newNode.left = newNode;
                                            newNode.right = newNode;
                                        }

                                        if (curCol.firstNode == null)
                                        {
                                            curCol.firstNode = newNode;
                                            newNode.up = newNode;
                                            newNode.down = newNode;
                                        }
                                        else
                                        {
                                            curCol.firstNode.up.down = newNode;
                                            curCol.firstNode.up = newNode;
                                        }

                                        curCol.memberCount++;

                                    }

                                }

                                break;

                            #endregion

                            #region SquareConstraintInit
                            case SudokuColumnHeader.ColumnType.Square:

                                int SquareX = (i + 2) % 3;
                                int SquareY = i/3;

                                Pair<int> cell = new Pair<int>(0,0);

                                bool isInSquare = false;

                                for (int n = 1; n <= 3 && !isInSquare; n++)
                                {
                                    for (int m = 1; m <= 3 && !isInSquare; m++)
                                    {
                                        int content;

                                        if (int.TryParse(s.boxes[SquareY * 3 + n, SquareX * 3 + m].Text, out content))
                                        {
                                            if (content == j)
                                            {
                                                isInSquare = true;
                                                cell = new Pair<int>(SquareY * 3 + n, SquareX * 3 + m);
                                            }
                                        }
                                    }
                                }


                                if (isInSquare)
                                {
                                    SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(cell.Item1, cell.Item2, j));
                                    AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                    if (tempRows.Contains(newRow))
                                    {
                                        newRow.firstNode.left.right = newNode;
                                        newRow.firstNode.left = newNode;
                                    }
                                    else
                                    {
                                        tempRows.Add(newRow);

                                        newRow.firstNode = newNode;
                                        newNode.left = newNode;
                                        newNode.right = newNode;
                                    }

                                    curCol.firstNode = newNode;
                                    curCol.memberCount = 1;

                                    newNode.up = newNode;
                                    newNode.down = newNode;
                                }
                                else
                                {
                                    curCol.memberCount = 0;

                                    for (int n = 1; n <= 3; n++)
                                    {
                                        for (int m = 1; m <= 3; m++)
                                        {
                                            SudokuRowHeader newRow = new SudokuRowHeader(new Triple<int>(SquareY*3 + n, SquareX*3 + m, j));
                                            AbstractDLinks.Node newNode = new AbstractDLinks.Node(newRow, curCol);

                                            if (tempRows.Contains(newRow))
                                            {
                                                newRow.firstNode.left.right = newNode;
                                                newRow.firstNode.left = newNode;
                                            }
                                            else
                                            {
                                                tempRows.Add(newRow);

                                                newRow.firstNode = newNode;
                                                newNode.left = newNode;
                                                newNode.right = newNode;
                                            }

                                            if (curCol.firstNode == null)
                                            {
                                                curCol.firstNode = newNode;
                                                newNode.up = newNode;
                                                newNode.down = newNode;
                                            }
                                            else
                                            {
                                                curCol.firstNode.up.down = newNode;
                                                curCol.firstNode.up = newNode;
                                            }

                                            curCol.memberCount++;
                                        }
                                    }
                                }

                                break;

                            #endregion
                        }



                        columns.Add(curCol);
                    }
                }


        }
        
    }
}
