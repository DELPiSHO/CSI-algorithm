using System;
using System.Collections.Generic;
using System.Text;

namespace csi
{
    class SMatrix
    {
         int[] rows;
         double[] values;
         int[] cols;

        public int RowCount => rows.Length;

        public double[] Values
        {
            get => values;
            set => values = value;
        }

        public int[] RowIndexes
        {
            get => rows;
            set => rows = value;
        }

        public int[] ColumnIndexes
        {
            get => cols;
            set => cols = value;
        }

        public SMatrix(double[] values, int[] rowIndexes, int[] columnIndexes)
        {
            this.values = values;
            rows = rowIndexes;
            cols = columnIndexes;
        }
        public SMatrix(int[] rowIndexes, int[] columnIndexes)
        {
            rows = rowIndexes;
            cols = columnIndexes;
            if (rowIndexes.Length > columnIndexes.Length)
            {
                values = new double[rows.Length];
            } else { values = new double[cols.Length]; }
            
        }

    }

}
