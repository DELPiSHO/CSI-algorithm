using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csi
{
    class GaussSeidelSparse
    {

        #region Properties
        public double OwnSparseMatrixTiming { get; set; }
        public SMatrix OwnSparseMatrix { get; set; }
        public double[] OwnSparseMatrixSolution { get; set; }
        public double[] OwnSparseVector { get; set; }
        public int MaxIterations { get; set; }
        public double Epsilon { get; set; }
        #endregion

        public GaussSeidelSparse(SMatrix sparseMatrix, double[] vector, int maxIterations, double epsilon)
        {
            OwnSparseMatrix = sparseMatrix;
            OwnSparseVector = vector;
            Epsilon = epsilon;
            MaxIterations = maxIterations;
            OwnSparseMatrixSolution = Calculate();
        }

        public double[] Calculate()
        {
            double[] result = new double[OwnSparseMatrix.RowCount];
            double[] previous = new double[OwnSparseMatrix.RowCount];
            double iterations = 0;
            bool run = true;

            while (run)
            {
                int rowCounter = 0;
                double sum = 0;
                int column = 0;
                double diagonalValue = 0;
                int start = OwnSparseMatrix.RowIndexes[0];
                int end = OwnSparseMatrix.RowIndexes[1];

                while (rowCounter < OwnSparseMatrix.RowIndexes.Length)
                {
                    sum = OwnSparseVector[rowCounter];

                    for (int i = start; i < end; i++)
                    {
                        column = OwnSparseMatrix.ColumnIndexes.ElementAt(i);
                        if (rowCounter != column)
                            sum -= OwnSparseMatrix.Values[i] * result[column];
                        else
                            diagonalValue = OwnSparseMatrix.Values[i];
                    }

                    result[rowCounter] = 1 / diagonalValue * sum;

                    rowCounter++;

                    if (rowCounter >= OwnSparseMatrix.RowIndexes.Length - 1)
                        break;

                    start = OwnSparseMatrix.RowIndexes[rowCounter];
                    end = OwnSparseMatrix.RowIndexes[rowCounter + 1];
                }

                sum = OwnSparseVector[rowCounter];
                column = OwnSparseMatrix.ColumnIndexes.Last();
                if (rowCounter != column)
                    sum -= OwnSparseMatrix.Values.LastOrDefault() * result[column];
                result[rowCounter] = 1 / OwnSparseMatrix.Values.LastOrDefault() * sum;

                iterations++;
                run = false;

                for (int i = 0; i < OwnSparseMatrix.RowCount; i++)
                {
                    if (Math.Abs(result[i] - previous[i]) > Epsilon || iterations == MaxIterations)
                    {
                        run = true;
                    }
                }

                previous = (double[])result.Clone();
            }


            return result;
        }
    }
}
