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


namespace Sudoku_Solver
{
    /// <summary>
    /// Interaction logic for SudokuGrid.xaml
    /// </summary>
    public partial class SudokuGrid : UserControl
    {

        public TextBox[,] boxes;

        public SudokuGrid()
        {
            InitializeComponent();

            
            boxes = new TextBox[9, 9];
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    
                    boxes[i, j] = new TextBox();
                    boxes[i, j].TextChanged += new TextChangedEventHandler(box_TextChanged);

                    NumberGrid.Children.Add(boxes[i, j]);
                    Grid.SetColumn(boxes[i, j], j);
                    Grid.SetRow(boxes[i, j], i);
                    
                }
            }

        }

        public bool Validate()
        {
            List<int>[] rowList = new List<int>[9];
            List<int>[] colList = new List<int>[9];

            bool result = true;
            
            for (int i = 0; i < 9; i++)
            {
                rowList[i] = new List<int>();

                for (int j = 0; j < 9; j++)
                {
                    if (colList[j] == null) { colList[j] = new List<int>(); }

                    if (boxes[i, j].Text != null && boxes[i,j].Text != "")
                    {
                        int content = int.Parse(boxes[i, j].Text);

                        if (rowList[i].Contains(content))
                        {
                            boxes[i, j].BorderBrush = new SolidColorBrush(Colors.Red);
                            boxes[i, j].BorderThickness = new Thickness(2);
                            result = false;
                        }
                        else { rowList[i].Add(content); }

                        if (colList[j].Contains(content))
                        {
                            boxes[i, j].BorderBrush = new SolidColorBrush(Colors.Red);
                            boxes[i, j].BorderThickness = new Thickness(2);
                            result = false;
                        }
                        else { colList[j].Add(content); }
                    }
                }
            }

            List<int>[,] boxList = new List<int>[3, 3];

            //two outer for loops run through the boxes
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    boxList[i, j] = new List<int>();

                    //inner loops run through the entries in each box
                    for (int a = 0; a < 3; a++)
                    {
                        for (int b = 0; b < 3; b++)
                        {
                            if (boxes[3 * i + a, 3 * j + b].Text != null && boxes[3*i + a, 3*j + b].Text != "")
                            {
                                int content = int.Parse(boxes[3 * i + a, 3 * j + b].Text);

                                if (boxList[i, j].Contains(content))
                                {
                                    boxes[3*i + a, 3*j + b].BorderBrush = new SolidColorBrush(Colors.Red);
                                    boxes[3 * i + a, 3 * j + b].BorderThickness = new Thickness(2);
                                    
                                    result = false;
                                }
                                else boxList[i, j].Add(content);

                            }
                        }
                    }

                }
            }



            return result;
        }

        private void box_TextChanged(object sender, RoutedEventArgs e)
        {
            int i;

            if (!(int.TryParse(((TextBox)sender).Text, out i)))
            {
                ((TextBox)sender).Text = null;

            }

            if (i > 9 || i == 0)
            {
                ((TextBox)sender).Text = null;
            }

            Brush defbrush = (Brush)Application.Current.TryFindResource("TextBoxBorder1");
            ((TextBox)sender).BorderThickness = new Thickness(1);
            ((TextBox)sender).ClearValue(TextBox.BorderBrushProperty);

            
            
        }
    }
}
