using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.SudokuDLinks
{

    public class SudokuDLinksAlg : AbstractDLinks.DLinksAlg
    {

        Dictionary<Pair<int>, int> squareDict;
        Dictionary<int, List<Pair<int>>> reverseSquareDict;


        public SudokuDLinksAlg(SudokuGrid s)
        {
            //Initialise the constraint matrix.

            initSquareLookup();

            chooseRow = new AbstractDLinks.RowChooser(rowChoose);

            List<SudokuRowHeader> tempRows = new List<SudokuRowHeader>();
            columns = new List<AbstractDLinks.ColumnHeader>();
            rows = new List<AbstractDLinks.RowHeader>();
            partialSolution = new List<AbstractDLinks.RowHeader>();

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

                                int sqNum = squareDict[new Pair<int>(row, col)];

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
                                    //if (curNode.column.firstNode == null)
                                    //{
                                        curNode.column.firstNode = curNode;
                                        curNode.up = curNode;
                                        curNode.down = curNode;
                                    //}
                                    //else
                                    //{
                                    //    curNode.up = curNode.column.firstNode.up;
                                    //    curNode.up.down = curNode;

                                    //    curNode.down = curNode.column.firstNode;
                                    //    curNode.down.up = curNode;
                                    //}
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

                                

                                if (n != row && int.TryParse(s.boxes[n-1, col-1].Text, out rowContent))
                                {
                                    if (rowContent == content) numUsed = true;
                                }
                                if (n != col && int.TryParse(s.boxes[row-1, n-1].Text, out colContent))
                                {
                                    if (colContent == content) numUsed = true;
                                }
                            }
                            foreach (Pair<int> p in reverseSquareDict[squareDict[new Pair<int>(row, col)]])
                            {
                                int sqContent;
                                if (int.TryParse(s.boxes[p.Item1 - 1, p.Item2-1].Text, out sqContent))
                                {
                                    if (sqContent == content) numUsed = true;
                                }
                            }

                            //for (int n = ((row-1) / 3) * 3; n < ((row-1) / 3) * 3 + 3; n++)
                            //{
                            //    for (int m = ((col-1) / 3) * 3; m < ((col-1) / 3) * 3 + 3; m++)
                            //    {
                            //        int sqContent;
                            //        if (int.TryParse(s.boxes[n, m].Text, out sqContent))
                            //        {
                            //            if (sqContent == content) numUsed = true;
                            //        }
                            //    }
                            //}

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

                                int sqNum = squareDict[new Pair<int>(row, col)];

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



            //foreach (AbstractDLinks.ColumnHeader col in columns)
            //{
            //    if (col.firstNode == null) System.Windows.MessageBox.Show(((SudokuDLinks.SudokuColumnHeader)col).type.ToString() + " " + ((SudokuDLinks.SudokuColumnHeader)col).constraintNum.ToString());
            //    if (col.memberCount != 9 && col.memberCount != 1) System.Windows.MessageBox.Show(((SudokuDLinks.SudokuColumnHeader)col).type.ToString() + " " + ((SudokuDLinks.SudokuColumnHeader)col).constraintNum.ToString());
            //}

            foreach (AbstractDLinks.RowHeader row in tempRows)
            {
                if(row.firstNode != null) rows.Add(row);
            }
        }

        public void initSquareLookup()
        {
            PairComparer<int> p = new PairComparer<int>();
            squareDict = new Dictionary<Pair<int>, int>(p);
            reverseSquareDict = new Dictionary<int, List<Pair<int>>>();

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {


                    if (i <= 3)
                    {
                        if (j <= 3)
                        {
                            squareDict.Add(new Pair<int>(i, j), 1);
                            if (!reverseSquareDict.ContainsKey(1))
                            {
                                reverseSquareDict.Add(1, new List<Pair<int>>());
                                
                            }
                            reverseSquareDict[1].Add(new Pair<int>(i, j));
                        }
                        else if (j > 3 && j <= 6)
                        {
                            squareDict.Add(new Pair<int>(i, j), 2);
                            if (!reverseSquareDict.ContainsKey(2))
                            {
                                reverseSquareDict.Add(2, new List<Pair<int>>());

                            }
                            reverseSquareDict[2].Add(new Pair<int>(i, j));
                        }
                        else
                        {
                            squareDict.Add(new Pair<int>(i, j), 3);
                            if (!reverseSquareDict.ContainsKey(3))
                            {
                                reverseSquareDict.Add(3, new List<Pair<int>>());

                            }
                            reverseSquareDict[3].Add(new Pair<int>(i, j));
                        }
                    }
                    else if (i > 3 && i <= 6)
                    {
                        if (j <= 3)
                        {
                            squareDict.Add(new Pair<int>(i, j), 4);
                            if (!reverseSquareDict.ContainsKey(4))
                            {
                                reverseSquareDict.Add(4, new List<Pair<int>>());

                            }
                            reverseSquareDict[4].Add(new Pair<int>(i, j));
                        }
                        else if (j > 3 && j <= 6)
                        {
                            squareDict.Add(new Pair<int>(i, j), 5);
                            if (!reverseSquareDict.ContainsKey(5))
                            {
                                reverseSquareDict.Add(5, new List<Pair<int>>());

                            }
                            reverseSquareDict[5].Add(new Pair<int>(i, j));
                        }
                        else
                        {
                            squareDict.Add(new Pair<int>(i, j), 6);
                            if (!reverseSquareDict.ContainsKey(6))
                            {
                                reverseSquareDict.Add(6, new List<Pair<int>>());

                            }
                            reverseSquareDict[6].Add(new Pair<int>(i, j));
                        }
                    }
                    else
                    {
                        if (j <= 3)
                        {
                            squareDict.Add(new Pair<int>(i, j), 7);
                            if (!reverseSquareDict.ContainsKey(7))
                            {
                                reverseSquareDict.Add(7, new List<Pair<int>>());

                            }
                            reverseSquareDict[7].Add(new Pair<int>(i, j));
                        }
                        else if (j > 3 && j <= 6)
                        {
                            squareDict.Add(new Pair<int>(i, j), 8);
                            if (!reverseSquareDict.ContainsKey(8))
                            {
                                reverseSquareDict.Add(8, new List<Pair<int>>());

                            }
                            reverseSquareDict[8].Add(new Pair<int>(i, j));
                        }
                        else
                        {
                            squareDict.Add(new Pair<int>(i, j), 9);
                            if (!reverseSquareDict.ContainsKey(9))
                            {
                                reverseSquareDict.Add(9, new List<Pair<int>>());

                            }
                            reverseSquareDict[9].Add(new Pair<int>(i, j));
                        }
                    }

                }
            }
        }

        public AbstractDLinks.RowHeader rowChoose(List<AbstractDLinks.RowHeader> rows)
        {
            if (rows.Count == 0) throw new Exception("No rows to choose.");
            return rows[0];
        }

    }
}
