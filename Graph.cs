using System;
using System.Collections.Generic;
using System.Linq;

namespace _220525_DijkstraAlgoritm
{ 
    class Graph 
    {
        readonly Dictionary<Vertex, int> dictionaryVertex = new Dictionary<Vertex, int>(); // словарь для вершин
        public List<Edge> edges = new List<Edge>(); // список всех рёбер графа
        private readonly List<Vertex> visitList = new List<Vertex>(); // список посещённых вершин 
        private readonly List<Vertex> wayList = new List<Vertex>();  //   путевой лист (маршрут)
        private string Name { get; set; }
        /// <summary>
        /// Добавление вершины в словарь вершин. Вершина ключ, значение бесконечность
        /// </summary>
        public void AddDictionaryVertex(Vertex vertex,int value=999999999) 
        {
            dictionaryVertex.Add(vertex, value);
        }
        /// <summary>
        /// Добавление вновь созданного ребра в  список всех рёбер (edges) графа . 
        /// </summary>
        public void AddEdge(Vertex point1, Vertex point2, int weight)
        {
            var edge = new Edge(point1, point2, weight);
            edges.Add(edge);
        }
        /// <summary>
        /// Алгоритм поиска кратчайшего пути.
        /// </summary>
        public void DijkstraAlgoritm(Vertex start,Vertex finsh)
        {
            Dictionary<Vertex, int> localDict = new Dictionary<Vertex, int>();
            foreach (var d in dictionaryVertex)
            {
                localDict.Add(d.Key,d.Value);
            }
            localDict[start] = 0;
            var vertex = start;
            while (true)
            {
                Dictionary<Vertex, int> compareList = new Dictionary<Vertex, int>(); // временный словарь для сравнения 
                var resultE = GetListEdges(vertex);
                var resultV = GetListVertexis(resultE, vertex);

                foreach (var e in resultE) // рёбра с vertex
                {
                    foreach (var v in resultV) //вершины рёбер
                    {
                        if (e.Point2 == v)
                        {
                            if (localDict[vertex] + e.Weight < localDict[v])
                            {
                                localDict[v] = localDict[vertex] + e.Weight;
                                compareList.Add(v, localDict[v]);
                            }
                            else
                            {
                                compareList.Add(v, localDict[v]);
                            }

                        }
                        if (e.Point1 == v)
                        {
                            if (localDict[vertex] + e.Weight < localDict[v])
                            {
                                localDict[v] = localDict[vertex] + e.Weight;
                                compareList.Add(v, localDict[v]);
                            }
                            else
                            {
                                compareList.Add(v, localDict[v]);
                            }
                        }
                    }
                }
                compareList = compareList.OrderBy(v => v.Value).ToDictionary(v => v.Key, v => v.Value);// сортировка наименьший элемент в верх 
                KeyValuePair<Vertex, int> bestVertex = new KeyValuePair<Vertex, int>();
                if (compareList.Count > 1)
                {
                    bestVertex = compareList.First(); // наилучший элемент 
                }
                else
                {
                    if (compareList.Count < 1) break;
                    bestVertex = compareList.Single();
                }
                visitList.Add(vertex); // занесли посещённую вершину с неизменным значением 
                //localDict.Remove(vertex);
                vertex = bestVertex.Key; // установили новую наилучшую вершину и цикл можно начинать сначала. 
                if (vertex == finsh) break;
            }
            if (vertex == finsh)  // путь домой
            {
                while (true)
                {
                    wayList.Add(vertex);
                    if (vertex == start) break; 
                    var listVertexWay = new List<Vertex>();
                    foreach (var edge in GetListEdgesWay(vertex))
                    {
                        if (localDict[edge.Point1] == localDict[vertex] - edge.Weight)
                        {
                            listVertexWay.Add(edge.Point1);
                        }
                        if (localDict[edge.Point2] == localDict[vertex] - edge.Weight) // добавил
                        {
                            listVertexWay.Add(edge.Point2);
                        }
                    }
                    if (listVertexWay.Count > 1) // есть два коротких пути их A->F. Ветвление в точек D .Можно через E можно чрез
                    {
                        
                        Console.WriteLine($"На пути из точки {start} точку {finsh} появилась развилка. Количество направлений : {listVertexWay.Count}");
                        Console.WriteLine("Расстояние пути не меняется при выборе направления.");
                        Console.Write($"Введите значение одной из точек, через которую будет пролегать маршрут:");
                        foreach (var v in listVertexWay)
                        {
                            Console.Write(v+" ");
                        }
                        Console.WriteLine();
                        List<string> tempListString = new List<string>();
                        foreach (var v in listVertexWay)
                        {
                            tempListString.Add(v.Name);
                        }
                        while (true)
                        {
                            Name = Console.ReadLine();
                            if (!tempListString.Contains(Name))
                            {
                                Console.WriteLine("Введено некорректное значение. Пробуй снова.");
                            }
                            if (tempListString.Contains(Name)) break;
                        }
                        foreach (var v in listVertexWay)
                        {
                            if (v.Name == Name)
                            {
                                vertex = v;
                            }
                        }
                    }
                    else
                    {
                        vertex = listVertexWay[0];
                    }
                }
                wayList.Reverse();
                Console.Write($"Расстояние пути из точки {start} в точку {finsh} равно {localDict[finsh]} км. Оптимальный маршрут: ");
                foreach (var v in wayList)
                {
                    Console.Write(v.Name + " ");
                }
                wayList.Clear();
                visitList.Clear();
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Из точки {start} в точку {finsh} пути нет.");
            }
        }
        /// <summary>
        /// Создание списка вершин смежных вершин с vertex используя список рёбер от этой вершины. 
        /// </summary>
        public List<Vertex> GetListVertexis(List<Edge> resultE,Vertex vertex)
        {
            var resultV = new List<Vertex>();           
            foreach (var edge in resultE)
            {
                if (edge.Point1 == vertex)
                {
                    resultV.Add(edge.Point2);
                }
                else
                {
                    if (edge.Point2 == vertex)
                    {
                        resultV.Add(edge.Point1);
                    }
                }
            }

            return resultV;
        }
        /// <summary>
        /// Создание списка рёбер от  bestPoint исключая посещённые вершины.
        /// </summary>
        private List<Edge> GetListEdges(Vertex vertex)
        {
            var resultE = new List<Edge>();// список ребер от bestPoint исключая посещённые вершины
            foreach (var edge in edges)
            {
                if (edge.Point1 == vertex || edge.Point2 == vertex)
                {
                    if (!visitList.Contains(edge.Point1)&&!visitList.Contains(edge.Point2))
                    {
                        resultE.Add(edge);
                    }
                }
            }
            return resultE;
        }
        /// <summary>
        /// Создание списка рёбер по пути домой. 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private List<Edge> GetListEdgesWay(Vertex vertex)
        {
            var resultEW = new List<Edge>();
            foreach (var edge in edges)
            {
                if (edge.Point2 == vertex|| edge.Point1 == vertex)
                {
                    resultEW.Add(edge);
                    
                }
            }
            return resultEW;
        }
        
    }
    
}
