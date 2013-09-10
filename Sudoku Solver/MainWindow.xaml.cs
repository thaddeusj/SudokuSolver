using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Sudoku_Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SudokuGrid gr;

        Dictionary<Pair<int>, List<int>> invalidChoices;
        Dictionary<Pair<int>, int> filledSquares;

        public MainWindow()
        {
            InitializeComponent();

            PairComparer<int> pc = new PairComparer<int>();

            invalidChoices = new Dictionary<Pair<int>, List<int>>(pc);
            filledSquares = new Dictionary<Pair<int>, int>(pc);

            gr = new SudokuGrid();
            
            grid.Children.Add(gr);
            




        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            if (gr.Validate())
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        gr.boxes[i, j].Focusable = false;
                    }
                }
            }


        }

        private void solve_Click(object sender, RoutedEventArgs e)
        {
            //bool solved = false;


            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        int content;

            //        if(int.TryParse(gr.boxes[i,j].Text,out content))
            //        {
            //            filledSquares.Add(new Pair<int>(i, j), content);
            //        }

            //    }
            //}

            //while (!solved)
            //{
            //    #region invalidate_column_values

            //    foreach (Pair<int> key in filledSquares.Keys)
            //    {
            //        for (int i = 0; i < 9; i++)
            //        {
            //            if (i != key.Item1 && !filledSquares.ContainsKey(new Pair<int>(i, key.Item2)))
            //            {
            //                if (invalidChoices.ContainsKey(new Pair<int>(i, key.Item2)) && !invalidChoices[new Pair<int>(i, key.Item2)].Contains(int.Parse(gr.boxes[key.Item1, key.Item2].Text)))
            //                {
            //                    invalidChoices[new Pair<int>(i, key.Item2)].Add(int.Parse(gr.boxes[key.Item1, key.Item2].Text));
            //                }
            //                else
            //                {
            //                    if (!invalidChoices.ContainsKey(new Pair<int>(i, key.Item2)))
            //                    {
            //                        invalidChoices.Add(new Pair<int>(i, key.Item2), new List<int>());
            //                        invalidChoices[new Pair<int>(i, key.Item2)].Add(int.Parse(gr.boxes[key.Item1, key.Item2].Text));
            //                    }
            //                }
            //            }
            //        }

            //    }
            //    #endregion

            //    #region invalidate_row_values

            //    foreach (Pair<int> key in filledSquares.Keys)
            //    {
            //        for (int i = 0; i < 9; i++)
            //        {
            //            if (i != key.Item2 && !filledSquares.ContainsKey(new Pair<int>(key.Item1, i)))
            //            {
            //                if (invalidChoices.ContainsKey(new Pair<int>(key.Item1, i)) && !invalidChoices[new Pair<int>(key.Item1, i)].Contains(int.Parse(gr.boxes[key.Item1, key.Item2].Text)))
            //                {
            //                    invalidChoices[new Pair<int>(key.Item1, i)].Add(int.Parse(gr.boxes[key.Item1, key.Item2].Text));
            //                }
            //                else
            //                {
            //                    if (!invalidChoices.ContainsKey(new Pair<int>(key.Item1, i)))
            //                    {
            //                        invalidChoices.Add(new Pair<int>(key.Item1, i), new List<int>());
            //                        invalidChoices[new Pair<int>(key.Item1, i)].Add(int.Parse(gr.boxes[key.Item1, key.Item2].Text));
            //                    }
            //                }
            //            }
            //        }

            //    }
            //    #endregion


            //    #region invalid_square_values
            //    foreach (Pair<int> key in filledSquares.Keys)
            //    {

            //        int squareI = (key.Item1 / 3) * 3;
            //        int squareJ = (key.Item2 / 3) * 3;

            //        for (int i = 0; i < 3; i++)
            //        {
            //            for (int j = 0; j < 3; j++)
            //            {

            //                if (squareI + i != key.Item1 && squareJ + j != key.Item2) //both && and || work here, since we've already invalidated columns and rows
            //                {

            //                    if (invalidChoices.ContainsKey(new Pair<int>(squareI + i, squareJ + j))
            //                        && !invalidChoices[new Pair<int>(squareI + i, squareJ + j)].Contains(int.Parse(gr.boxes[key.Item1, key.Item2].Text)))
            //                    {
            //                        invalidChoices[new Pair<int>(squareI + i, squareJ + j)].Add(int.Parse(gr.boxes[key.Item1, key.Item2].Text));
            //                    }
            //                    if (!invalidChoices.ContainsKey(new Pair<int>(squareI + i, squareJ + j)))
            //                    {
            //                        invalidChoices.Add(new Pair<int>(squareI + i, squareJ + j), new List<int>());
            //                        invalidChoices[new Pair<int>(squareI + i, squareJ + j)].Add(int.Parse(gr.boxes[key.Item1, key.Item2].Text));
            //                    }


            //                }
            //            }
            //        }


            //    }

            //    #endregion


            //    //MessageBox.Show("Before fill loop.\n" + invalidChoices.Keys.Count.ToString());
            //    List<Pair<int>> keysToRemove = new List<Pair<int>>();

            //    foreach (Pair<int> key in invalidChoices.Keys)
            //    {
                    

            //        //MessageBox.Show("In fill loop.\n" + invalidChoices.Keys.Count.ToString());

                    

            //        if (invalidChoices[key].Count == 8)
            //        {
            //            //MessageBox.Show("Count is 8 in square:" + key.Item1 + "," + key.Item2);


            //            for (int i = 1; i < 10; i++)
            //            {
            //                if (!invalidChoices[key].Contains(i))
            //                {
            //                    if (!filledSquares.Keys.Contains(key))
            //                    {
            //                        filledSquares.Add(key, i);
            //                    }
            //                    else
            //                    {
            //                        filledSquares[key] = i;
            //                    }


            //                    keysToRemove.Add(key);
            //                    gr.boxes[key.Item1, key.Item2].Text = i.ToString();

            //                }
            //            }
            //        }

            //    }

            //    foreach (Pair<int> k in keysToRemove)
            //    {
            //        invalidChoices.Remove(k);
            //    }


            //    if (filledSquares.Keys.Count == 81 && gr.Validate()) 
            //    {
            //        solved = true;

            //        MessageBox.Show("Puzzle solved.");
            //    }
            //}


            DancingLinks d = new DancingLinks(gr);



            //d.dancingLinksAlg();

            //if (d.rows.Count != 81) MessageBox.Show("Uhoh!");
            //else
            //{
            //    foreach (Triple<int> row in d.rows)
            //    {
            //        gr.boxes[row.Item1, row.Item2].Text = row.Item3.ToString();
            //    }

            //}
        }


        public bool exact_cover_solve()
        {

            //Attempting to implement dancing links.


            


            





            return true;
        }



    }
}
