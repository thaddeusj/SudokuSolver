using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sudoku_Solver.AbstractDLinks
{
    public delegate RowHeader RowChooser(List<RowHeader> rows);
    

    public abstract class DLinksAlg
    {
        public RowChooser chooseRow;
        

        public List<ColumnHeader> columns;
        public List<RowHeader> rows;
        public List<RowHeader> partialSolution;

        public bool DLSolve()
        {
            if (columns.Count == 0) return true;

            //MessageBox.Show("Rows: " + rows.Count.ToString() + "\nConstraints: " + columns.Count);

            int minCount = columns.Min(x => x.memberCount);
            if (minCount == 0) return false;


            ColumnHeader currentColumn = columns.Where(x => x.memberCount == minCount).ToArray()[0];
            List<RowHeader> rowsToChoose = new List<RowHeader>();

            Node curNode = currentColumn.firstNode;

            do
            {
                rowsToChoose.Add(curNode.row);
                curNode = curNode.down;
            } while (curNode != currentColumn.firstNode);
           

            while (rowsToChoose.Count > 0)
            {
                int colBefore = columns.Count;
                int rowBefore = rows.Count;

                RowHeader currentRow = chooseRow(rowsToChoose);
                if (rowsToChoose.Contains(currentRow))
                {
                    rowsToChoose.Remove(currentRow);

                    Node columnNode = currentRow.firstNode; //Indexes the column we're working on

                    partialSolution.Add(currentRow);

                    do
                    {
                        constraintRemove(columnNode.column);
                        columnNode = columnNode.right;


                    } while (columnNode != currentRow.firstNode);

                    if (DLSolve()) return true;
                    else
                    {
                        //Reverse the above process.

                        columnNode = currentRow.firstNode; //Indexes the column we're working on
                        partialSolution.Remove(currentRow);

                        do
                        {
                            constraintAdd(columnNode.column);
                            columnNode = columnNode.right;

                        } while (columnNode != currentRow.firstNode);

                        if (columns.Count != colBefore) throw new Exception();
                        if (rows.Count != rowBefore) throw new Exception();
                    }
                }
            }


            return false;
        }


        public void constraintRemove(ColumnHeader c)
        {
            Node rowMarker = c.firstNode;

            do
            {

                Node rowRemovalNode = rowMarker.right;

                if (rows.Contains(rowMarker.row))
                {
                    while (rowRemovalNode != rowMarker)
                    {

                        rowRemovalNode.up.down = rowRemovalNode.down;
                        rowRemovalNode.down.up = rowRemovalNode.up;

                        rowRemovalNode.column.memberCount--;

                        if (rowRemovalNode == rowRemovalNode.column.firstNode) rowRemovalNode.column.firstNode = rowRemovalNode.up;

                        rowRemovalNode = rowRemovalNode.right;

                    }
                    rows.Remove(rowMarker.row);
                }

                //rowMarker.left.right = rowMarker.right;
                //rowMarker.right.left = rowMarker.left;

                rowMarker = rowMarker.down;

            } while (rowMarker != c.firstNode);

            columns.Remove(c);
        }

        public void constraintAdd(ColumnHeader c)
        {
            Node rowMarker = c.firstNode;

            do
            {

                Node rowAddNode = rowMarker.right;

                if (!rows.Contains(rowMarker.row))
                {
                    while (rowAddNode != rowMarker)
                    {

                        rowAddNode.up.down = rowAddNode;
                        rowAddNode.down.up = rowAddNode;

                        rowAddNode.column.memberCount++;

                        rowAddNode = rowAddNode.right;
                    }
                    if(!rows.Contains(rowMarker.row)) rows.Add(rowMarker.row);
                }

                //rowMarker.left.right = rowMarker;
                //rowMarker.right.left = rowMarker;
                rowMarker = rowMarker.down;

            } while (rowMarker != c.firstNode);

            if (!columns.Contains(c)) columns.Add(c);
        }
    }
}
