using System;


namespace _220525_DijkstraAlgoritm
{
    class Program
    {
        static void Main()
        {

            var a = new Vertex("A");
            var b = new Vertex("B");
            var c = new Vertex("C");
            var d = new Vertex("D");
            var e = new Vertex("E");
            var f = new Vertex("F");
            var g = new Vertex("G");
            var i = new Vertex("I");

            Graph graph = new Graph();
           
            graph.AddDictionaryVertex(a);
            graph.AddDictionaryVertex(b);
            graph.AddDictionaryVertex(c);
            graph.AddDictionaryVertex(d);
            graph.AddDictionaryVertex(e);
            graph.AddDictionaryVertex(f);
            graph.AddDictionaryVertex(g);

            graph.AddEdge(a, b, 4);
            graph.AddEdge(a, c, 3);
            graph.AddEdge(a, e, 7);
            graph.AddEdge(b, d, 5);
            graph.AddEdge(b, c, 6);
            graph.AddEdge(c, d, 11);
            graph.AddEdge(c, e, 8);
            graph.AddEdge(d, e, 2);
            graph.AddEdge(d, g, 10);
            graph.AddEdge(d, f, 2);
            graph.AddEdge(e, g, 5);
            graph.AddEdge(g, f, 3);

            graph.DijkstraAlgoritm(a, b);
            graph.DijkstraAlgoritm(a, c);
            graph.DijkstraAlgoritm(a, d);
            graph.DijkstraAlgoritm(a, e);
            graph.DijkstraAlgoritm(a, f);
            graph.DijkstraAlgoritm(a, g);
            graph.DijkstraAlgoritm(d, f);
            graph.DijkstraAlgoritm(f, a);
            graph.DijkstraAlgoritm(f, i);
           
            Console.ReadLine();

        }
    }
}
