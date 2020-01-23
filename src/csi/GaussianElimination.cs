using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//MG
namespace csi
{
    class GaussianElimination
    {

        public Matrix CalculateResult(Matrix _matrix, Matrix _vector)
        {
            var result = new Matrix(_matrix.rows, 1);

            for (int i = _matrix.rows - 1; i >= 0; i--)
            {
                double sum = 0.0;

                for (int j = i + 1; j < _matrix.rows; j++)
                {
                    sum += (dynamic)_matrix.values[i, j] * result.values[j, 0];
                }
                    

                result.values[i, 0] = (_vector.values[i, 0] - sum) / _matrix.values[i, i];
            }

            return result;
        }

        public void ZeroColumn(Matrix _result1, Matrix _result2, int _colNumber)
        {
            for (int i = _colNumber + 1; i < _result1.rows; i++)
            {
                double zeroed = _result1.values[i, _colNumber] / _result1.values[_colNumber, _colNumber];

                for (int j = 0; j < _result1.cols; j++)
                {
                    _result1.values[i, j] -= _result1.values[_colNumber, j] * zeroed;
                }
                _result2.values[i, 0] -= _result2.values[i, 0] * zeroed;
                   
            }

        }


        public Matrix EliminationPG(Matrix _matrix, Matrix _vector)
        {
            var result1 = DeepCopy.Copy(_matrix);
            var result2 = DeepCopy.Copy(_vector);

            for (int i = 0; i < result1.rows - 1; i++)
            {
                PartialPivot(result1,result2, i);
                ZeroColumn(result1,result2, i);
            }
            return CalculateResult(result1,result2);
        }

        private Matrix FixColumns(Matrix _result, List<Tuple<int, int>> _swapList)
        {
            double[] order = new double[_result.rows];

            for (int i = 0; i < _result.rows; i++)
                order[i] = _result.values[i, _result.cols - 1];

            _swapList.Reverse();

            foreach (Tuple<int, int> swap in _swapList)
            {
                double temp = order[swap.Item1];
                order[swap.Item1] = order[swap.Item2];
                order[swap.Item2] = temp;
            }

            for (int i = 0; i < _result.rows; i++)
                _result.values[i, _result.cols - 1] = order[i];

            return _result;
        }

        public void PartialPivot(Matrix _matrix, Matrix _vector, int _p)
        {

            for (int i = _p; i < _matrix.rows; i++)
                if (Math.Abs(_matrix.values[_p, _p]) < Math.Abs(_matrix.values[i, _p]))
                {
                    _matrix.SwitchRows(_p, i);
                    _vector.SwitchRows(_p, i);
                }
        }
        /*
        public void PartialPivot(Matrix matrix, Matrix vector, int p)
        {
            for (int j = p; j < matrix.rows; j++)
            {
                if (Math.Abs(matrix.values[p, p]) < Math.Abs(matrix.values[j, p]))
                {
                    matrix.SwitchRows(p, j);
                    vector.SwitchRows(p, j);
                }
            }
        }
        */
    }
}

