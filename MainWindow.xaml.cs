using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        Matrix matrix;
        public MainWindow()
        {
            InitializeComponent();
            calculateResultButton.IsEnabled = false;
        }

        private void createMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = int.Parse(textBoxRows.Text);
                int columns = int.Parse(textBoxColumns.Text);

                matrix = new Matrix(rows, columns);
                int max = int.Parse(textBoxMax.Text);
                int min = int.Parse(textBoxMin.Text);
                matrix.FillElementsRandom(min, max);
                Print(matrix);
                calculateResultButton.IsEnabled = true;
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Print(Matrix matrix)
        {
            DataTable dt = new DataTable();
            int columns = matrix.columnCount;
            int rows = matrix.rowCount;
            for(int i =0; i < rows; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(double));
            }
            for(int row = 0; row < rows; row++) {
            DataRow dr = dt.NewRow();
            for(int col = 0; col < columns; col++)
                {
                    dr[col] = matrix[row, col];
                }
            dt.Rows.Add(dr);
            }
            dataGrid.ItemsSource = dt.DefaultView;
            dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
            dataGrid.CanUserAddRows = false;
        }

        private void PrintResult(Matrix matrix)
        {
            result1.Content = "Суми елементів пизитивних рядків:";
            int[] sumPosRows = matrix.GetPositiveRowsSum();
            for(int i = 0; i < sumPosRows.Length; i++)
            {
                result1.Content += ' ' + sumPosRows[i].ToString();
            }
            int[] minSums = matrix.GetSumDiagonals();
            int minSum = minSums[0];
            for(int i = 0; i < minSums.Length; i++)
            {
                if (minSums[i] < minSum)
                {
                    minSum = minSums[i];
                }
            }
            result2.Content = "Мінімальна сума, серед всіх сум елементів діагоналей: " + minSum.ToString();
        }

        private void calculateResultButton_Click(object sender, RoutedEventArgs e)
        {
            PrintResult(matrix);
        }
    }
}
