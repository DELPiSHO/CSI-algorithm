using System;
using System.Collections.Generic;
using System.Text;

namespace csi
{
    class Node
    {
        readonly public double x;
        readonly public double y;

        public Node(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
