using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

//TODO
namespace csi
{
    class TestCsi
    {

        static int GetRandom()
        {
            Random random = new Random();
            return random.Next(1, 10);
        }

        public static void ExecuteRandom(int numberOfNodes)
        {
            Stopwatch stopwatch = new Stopwatch();
            string time;
            List<Node> randomNodes = new List<Node>();

            for (int i = 1; i <= numberOfNodes; i++)
            {
                randomNodes.Add(new Node(i, GetRandom()));
            }

            List<Node> nodeTest = new List<Node>();
            for (int i = 0; i < numberOfNodes; i += 2)
            {
                nodeTest.Add(randomNodes[i]);
            }
            csi csi = new csi(nodeTest);

            /*
            stopwatch.Start();
            csi.SetMVectorFromGaussElimination();
            stopwatch.Stop();
            time = stopwatch.Elapsed.ToString();
            File.AppendAllText(@$"C:\TEST4\T2_gauss.txt", (time + "\n"));
            csi.SaveToFile(numberOfNodes, "gaussR");
            */
            


            stopwatch.Restart();
            csi.SetMVectorFromGaussSeidel();
            stopwatch.Stop();
            time = stopwatch.Elapsed.ToString();
            File.AppendAllText(@$"C:\TEST4\T2_GSeidel.txt", (time + "\n"));
            csi.SaveToFile(numberOfNodes, "seidelR");

            
            stopwatch.Restart();
            csi.SetMVectorFromJacobi();
            stopwatch.Stop();
            time = stopwatch.Elapsed.ToString();
            File.AppendAllText(@$"C:\TEST4\T2_Jacobi.txt", (time + "\n"));
            csi.SaveToFile(numberOfNodes, "jacobiR");
            
        }

        public static void ExecuteGoogleElevation(int numberOfNodes)
        {
            Stopwatch stopwatch = new Stopwatch();
            string time;

            List<Node> nodeList = new List<Node>();
            nodeList = Reader.Read(numberOfNodes);
            List<Node> nodeTest = new List<Node>();
            for (int i = 0; i < numberOfNodes; i += 2)
            {
                nodeTest.Add(nodeList[i]);
            }
            
            csi csi = new csi(nodeTest);

            
            stopwatch.Start();
            csi.SetMVectorFromGaussElimination();
            stopwatch.Stop();
            time = stopwatch.Elapsed.ToString();
            File.AppendAllText(@$"C:\TEST4\T_gauss.txt", (time + "\n"));
            csi.SaveToFile(numberOfNodes, "gauss");
            

            stopwatch.Restart();
            csi.SetMVectorFromGaussSeidel();
            stopwatch.Stop();
            time = stopwatch.Elapsed.ToString();
            File.AppendAllText(@$"C:\TEST4\T_GSeidel.txt", (time + "\n"));
            csi.SaveToFile(numberOfNodes, "seidel");


            stopwatch.Restart();
            csi.SetMVectorFromJacobi();
            stopwatch.Stop();
            time = stopwatch.Elapsed.ToString();
            File.AppendAllText(@$"C:\TEST4\T_Jacobi.txt", (time + "\n"));
            csi.SaveToFile(numberOfNodes, "jacobi");
        }
    }
}
