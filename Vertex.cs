﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220525_DijkstraAlgoritm
{
    class Vertex
    {
        public string Name { get; set; }
        public Vertex(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
