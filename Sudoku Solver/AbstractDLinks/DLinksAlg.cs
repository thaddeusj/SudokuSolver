﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.AbstractDLinks
{
    public delegate RowHeader RowChooser(ColumnHeader c, List<RowHeader> rows);
    

    public abstract class DLinksAlg
    {
        public RowChooser chooseRow;
        

        protected List<ColumnHeader> columns;
        protected List<RowHeader> rows;

        public bool DLSolve()
        {
            if (columns.Count == 0) return true;

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
                RowHeader currentRow = chooseRow(currentColumn, rowsToChoose);
                if (rowsToChoose.Contains(currentRow))
                {
                    rowsToChoose.Remove(currentRow);


                    Node columnNode = currentRow.firstNode; //Indexes the column we're working on

                    do
                    {
                        Node rowNode = columnNode.column.firstNode; //Indexes the row within the column

                        do
                        {


                            Node rowRemNode = rowNode.right;

                            while (rowRemNode != rowNode)
                            {
                                rowRemNode.up.down = rowRemNode.down;
                                rowRemNode.down.up = rowRemNode.up;
                            }

                            if(rowNode.row != currentRow) rows.Remove(rowNode.row);
                            rowNode = rowNode.down;

                        } while (rowNode != columnNode.column.firstNode);



                        columns.Remove(columnNode.column);
                        columnNode = columnNode.right;


                    } while (columnNode != currentRow.firstNode);

                    if (DLSolve()) return true;
                    else
                    {
                        //Reverse the above process.

                        columnNode = currentRow.firstNode; //Indexes the column we're working on

                        do
                        {
                            Node rowNode = columnNode.column.firstNode; //Indexes the row within the column

                            do
                            {


                                Node rowRemNode = rowNode.right;

                                while (rowRemNode != rowNode)
                                {
                                    rowRemNode.up.down = rowRemNode;
                                    rowRemNode.down.up = rowRemNode;
                                }

                                if (rowNode.row != currentRow) rows.Add(rowNode.row);
                                rowNode = rowNode.down;

                            } while (rowNode != columnNode.column.firstNode);



                            columns.Add(columnNode.column);
                            columnNode = columnNode.right;


                        } while (columnNode != currentRow.firstNode);




                    }
                }
            }


            return false;
        }


    }
}