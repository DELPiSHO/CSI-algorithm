using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csi
{
    class Reader
    {
        public static List<Node> Read(int targetSize)
        {
            var points = new List<Node>();

            using (var reader = new StreamReader(@$"C:\TEST4\data.csv"))
            {
                int counter = 0;
                while (!reader.EndOfStream)
                {
                    double elevation = double.Parse(reader.ReadLine());

                    points.Add(new Node((double)counter+1, elevation));

                    counter++;
                    if (counter >= targetSize)
                    {
                        break;
                    }
                }
            }
            return points;
        }
    }
}
