using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Matrix
    {
        #region Поля 
        int[,] matrix;
        int[] positive_rows_sum;
        int[] sum_diagonal;
        #endregion

        #region Властивості та індексатори 
        public int rowCount {  get; set; }
        public int columnCount { get; set; }


        public int this[int i, int j]
        {
            get
            {
                if (i < rowCount && i >= 0 && j < columnCount && j >= 0)
                    return matrix[i, j];
                else throw new IndexOutOfRangeException("Індекс виходить за межі масиву!");
            }
        }
        #endregion

        #region Конструктори 
        public Matrix(int rows, int columns) {
        matrix = new int[rows, columns];
        rowCount = matrix.GetLength(0);
        columnCount = matrix.GetLength(1);
        }
        #endregion

        #region Методи 
        public void FillElementsRandom(int min, int max)
        {
            Random rand = new Random();
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < columnCount; j++)
                    matrix[i, j] = rand.Next(min, max + 1);
        }
        public int[] GetPositiveRowsSum()
        {
            positive_rows_sum = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                bool hasNegative = false;
                int sum = 0;
                for (int j = 0; j < columnCount; j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        hasNegative = true;
                        break;
                    }
                    sum += matrix[i, j];
                }
                if (!hasNegative)
                    positive_rows_sum[i] = sum;
            }
            return positive_rows_sum.Where(x => x != 0).ToArray();
            
        }
        public int[] GetSumDiagonals()
        {
            int maxDiagonals = Math.Min(rowCount, columnCount);
            sum_diagonal = new int[maxDiagonals];

            for (int k = 0; k < maxDiagonals; k++)
            {
                int sum = 0;
                for (int i = 0, j = k; i < rowCount && j < columnCount; i++, j++)
                {
                    sum += matrix[i, j];
                }
                sum_diagonal[k] = sum;
            }

            return sum_diagonal;
        }

        #endregion
    }
}
