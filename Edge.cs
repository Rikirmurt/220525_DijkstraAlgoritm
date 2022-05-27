using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220525_DijkstraAlgoritm
{
    class Edge
    {
        public Vertex Point1 { get; set; }
        public Vertex Point2 { get; set; }
        public int Weight { get; set; }
        public Edge(Vertex point1, Vertex point2, int weight)
        {
            Point1 = point1;
            Point2 = point2;
            Weight = weight;
        }
        public override string ToString()
        {
            return $"({ Point1};{ Point2 };{ Weight })";
        }
    }
}
