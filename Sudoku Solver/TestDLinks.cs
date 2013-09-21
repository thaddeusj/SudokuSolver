using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
    class TestDLinks : AbstractDLinks.DLinksAlg
    {


        public TestDLinks()
        {
            init();

        }


        public void init()
        {
            columns = new List<AbstractDLinks.ColumnHeader>();
            partialSolution = new List<AbstractDLinks.RowHeader>();
            rows = new List<AbstractDLinks.RowHeader>();

            for (int i = 1; i <= 7; i++)
            {
                columns.Add(new TestDLinksColumnHeader(i));
            }


            TestDLinksRowHeader A = new TestDLinksRowHeader("A");
            TestDLinksRowHeader B = new TestDLinksRowHeader("B");
            TestDLinksRowHeader C = new TestDLinksRowHeader("C");
            TestDLinksRowHeader D = new TestDLinksRowHeader("D"); 
            TestDLinksRowHeader E = new TestDLinksRowHeader("E");
            TestDLinksRowHeader F = new TestDLinksRowHeader("F");

            //A

            AbstractDLinks.Node temp = new AbstractDLinks.Node(A, columns[0]);
            A.appendNode(temp);
            temp = new AbstractDLinks.Node(A, columns[3]);
            A.appendNode(temp);
            temp = new AbstractDLinks.Node(A, columns[6]);
            A.appendNode(temp);

            //B

            temp = new AbstractDLinks.Node(B, columns[0]);
            B.appendNode(temp);
            temp = new AbstractDLinks.Node(B, columns[3]);
            B.appendNode(temp);

            //C
            temp = new AbstractDLinks.Node(C, columns[3]);
            C.appendNode(temp);
            temp = new AbstractDLinks.Node(C, columns[4]);
            C.appendNode(temp);
            temp = new AbstractDLinks.Node(C, columns[6]);
            C.appendNode(temp);

            //D
            temp = new AbstractDLinks.Node(D, columns[2]);
            D.appendNode(temp);
            temp = new AbstractDLinks.Node(D, columns[4]);
            D.appendNode(temp);
            temp = new AbstractDLinks.Node(D, columns[5]);
            D.appendNode(temp);

            //E

            temp = new AbstractDLinks.Node(E, columns[1]);
            E.appendNode(temp);
            temp = new AbstractDLinks.Node(E, columns[2]);
            E.appendNode(temp);
            temp = new AbstractDLinks.Node(E, columns[5]);
            E.appendNode(temp);
            temp = new AbstractDLinks.Node(E, columns[6]);
            E.appendNode(temp);

            //F
            temp = new AbstractDLinks.Node(F, columns[1]);
            F.appendNode(temp);
            temp = new AbstractDLinks.Node(F, columns[6]);
            F.appendNode(temp);


            rows = new List<AbstractDLinks.RowHeader>();
            rows.Add(A);
            rows.Add(B);
            rows.Add(C);
            rows.Add(D);
            rows.Add(E);
            rows.Add(F);

            chooseRow = rowChoose;
        }
        public AbstractDLinks.RowHeader rowChoose(List<AbstractDLinks.RowHeader> rows)
        {
            if (rows.Count == 0) throw new Exception("No rows to choose.");
            return rows[0];
        }
    }
}
