using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csi
{
    class csi
    {
        private List<Node> Nodes { get; set; }
        private Matrix Vector { get; set; }
        private Matrix MVector { get; set; }
        private Matrix Matrix { get; set; }

        public csi(List<Node> nodes)
        {
            Nodes = nodes;
            Matrix = new Matrix(nodes.Count, nodes.Count);
            Vector = new Matrix(nodes.Count, 1);

            Fill();
        }

        private double Delta(int i)
        {
            return 6 / (H(i) + H(i + 1)) *
                ((Nodes[i + 1].y - Nodes[i].y) / H(i + 1) -
                (((Nodes[i].y - Nodes[i - 1].y)) / H(i)));
        }

        private double L(int i)
        {
            return H(i + 1) / (H(i) + H(i + 1));
        }

        private double M(int i)
        {
            return H(i) / (H(i) + H(i + 1));
        }

        private double H(int i)
        {
            return Nodes[i].x - Nodes[i - 1].x;
        }

        private double A(int i)
        {
            return Nodes[i].y;
        }

        private double B(int i)
        {
            return (Nodes[i + 1].y - Nodes[i].y) / H(i + 1) - (2 * GetMVector(i) + GetMVector(i + 1)) / 6 * H(i + 1);
        }

        private double C(int i)
        {
            return GetMVector(i) / 2;
        }

        private double D(int i)
        {
            return (GetMVector(i + 1) - GetMVector(i)) / 6 * H(i + 1);
        }

        private double GetMVector(int i)
        {
            return MVector.values[i, 0];
        }

        private double S(double x)
        {
            int i = GetIndexX(x);

            double diff = x - Nodes[i].x;

            double value = A(i) + B(i) * diff + C(i) * Math.Pow(diff, 2) + D(i) * Math.Pow(diff, 3);

            return value;
        }

        public void Print(int i)
        {
            var start = Nodes[0].x;
            var stop = Nodes[Nodes.Count - 1].x;
            var jump = (stop - start) / i;

            for (double x = start; x < stop; x += jump)
            {
                Console.WriteLine(new Node(x, S(x)));
            }
        }

        public void SaveToFile(int i, string fileName)
        {
            var start = Nodes[0].x;
            var stop = Nodes[Nodes.Count - 1].x;
            var jump = (stop - start) / i;

            for (double x = start; x < stop; x += jump)
            {
                File.AppendAllText(@$"C:\TEST4\{fileName}.txt", (S(x) + "\n"));
            }
        }

        private int GetIndexX(double x)
        {
            for (int i = 1; i < Nodes.Count; i++)
            {
                if (x <= Nodes[i].x)
                {
                    return i - 1;
                }
            }
            throw new Exception("Value out of bounds");
        }

        private void Fill()
        {
            Matrix.values[0, 0] = 2;
            for (int i = 1; i < Matrix.rows - 1; i++)
            {
                Vector.values[i, 0] = Delta(i);
                Matrix.values[i, i - 1] = M(i);
                Matrix.values[i, i] = 2;
                Matrix.values[i, i + 1] = L(i);
            }

            Matrix.values[Matrix.rows - 1, Matrix.rows - 1] = 2;
        }



        public void SetMVectorFromGaussElimination()
        {
            MVector = new GaussianElimination().EliminationPG(Matrix, Vector);
        }

        public void SetMVectorFromGaussSeidel()
        {
            MVector = new GaussSeidel(Matrix, Vector).Calculate();
        }

        public void SetMVectorFromJacobi()
        {
            MVector = new Jacobi(Matrix, Vector).Calculate();
        }
        public void SetMVectorFromGaussSeidelSparse()
        {
            MVector = new GaussSeidelSparse(Matrix, Vector).Calculate();
        }
    }
}
