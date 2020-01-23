using System;
using System.Collections.Generic;

namespace csi
{
    class Program
    {

        static List<Node> sampleNodes = new List<Node>
            {
            new Node(1, 2),
            new Node(2, 3),
            new Node(3, 5),
            new Node(4, 1),
            new Node(5, 6),
            new Node(6, 4),
            new Node(7, 9)
            };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Node> nodeList = new List<Node>();
            nodeList = Reader.Read(10);
            //Console.WriteLine(nodeList.Count);
            csi csi = new csi(sampleNodes);
            Console.WriteLine("CSI END");
            csi.SetMVectorFromGaussSeidel();
            Console.WriteLine("GAUSS SEIDEL END");
            csi.Print(14);
            Console.WriteLine("PRINT END");
        }
    }
}
