using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//MG
namespace csi
{
    [Serializable]
    class Matrix
    {
        public double[,] values { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }

        public Matrix (int _rows, int _cols)
        {
            rows = _rows;
            cols = _cols;
            values = new double[rows, cols];
        }
        public void FillRandomValues()
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    values[i, j] = random.NextDouble();
                }
            }
        }


        public Matrix Multiply(Matrix _matrix)
        {
            var result = new Matrix(rows, _matrix.cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < _matrix.cols; j++)
                {
                    double multiply = 0.0;
                    for (int x = 0; x <_matrix.rows; x++)
                    {
                        multiply += values[i, x] * _matrix.values[x, j];
                    }
                    result.values[i, j] = multiply;
                }
            }
                    return result;
        }


        public Matrix Add(Matrix _matrix)
        {
            var result = this;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result.values[i, j] = values[i, j] + _matrix.values[i, j];
            return result;
        }

        public Matrix Subtract(Matrix _matrix)
        {
            var result = this;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result.values[i, j] = values[i, j] - _matrix.values[i, j];
            return result;
        }
        //DEBUG
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols)
            {
                throw new System.ArgumentException("Can't add matrices with different sizes");
            }

            Matrix result = new Matrix(a.rows,a.rows);

            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < b.cols; j++)
                {
                    result.values[i, j] = a.values[i, j] + b.values[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix matrix)
        {
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    matrix.values[i, j] = -matrix.values[i, j];
                }
            }
            return matrix;
        }

        public static Matrix operator -(Matrix a, Matrix b) => a + (-b);

        public Matrix LinkToVector(Matrix _vector)
        {
            var result = new Matrix(rows, cols + 1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols + 1; j++)
                {
                    if (j == cols)
                        result.values[i, j] = _vector.values[i, 0];
                    else
                        result.values[i, j] = values[i, j];
                }
            }

            return result;
        }

        public void SwitchRows(int _row1, int _row2)
        {
            for (int i = 0; i < cols; i++)
            {
                double temp;

                temp = values[_row1, i];

                values[_row1, i] = values[_row2, i];

                values[_row2, i] = temp;
            }
        }

        public void SwitchColumns(int _col1, int _col2)
        {
            for (int i = 0; i < rows; i++)
            {
                double temp;

                temp = values[i, _col1];

                values[i, _col1] = values[i, _col2];

                values[i, _col2] = temp;
            }
        }

        public Matrix Transpose()
        {
                var result = new Matrix(this.rows, this.cols);

                for (int i = 0; i < result.rows; i++)
                {
                    for (int j = 0; j < result.cols; j++)
                    {
                        result.values[j, i] = values[i, j];
                    }
                }
            return result;
        }

        public void AddLambda(double lambda)
        {
            for (int i = 0; i < Math.Min(rows, cols); i++)
            {
                values[i, i] += lambda;
            }
        }

        public double GetNorm()
        {
            double sum = 0;
            for (int i = 0; i < rows; i++)
            {
                sum += Math.Pow(values[i, 0], 2);
            }
            return Math.Sqrt(sum);
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result += $"{values[i, j]}  ";
                } 
                result += "\n";
            }

            return result;
        }

    }
}
