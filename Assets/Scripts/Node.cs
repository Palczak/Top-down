using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector3 Position { get { return new Vector3(X, Y, 0); } }
        public double Cost { get; set; }
        private List<Tuple<int, int>> _neighborIndexes;
        public IEnumerable<Tuple<int, int>> NeighborIndexes { get { return _neighborIndexes; } }
        public bool HasNeighbors { get { return _neighborIndexes.Count != 0; } }


        public Node(int x, int y)
        {
            Cost = 1;
            X = x;
            Y = y;
            _neighborIndexes = new List<Tuple<int, int>>();
        }

        public Node()
        {
            Cost = 1;
            X = 0;
            Y = 0;
            _neighborIndexes = new List<Tuple<int, int>>();
        }

        public void AddNeighborIndex(int x, int y)
        {
            _neighborIndexes.Add(new Tuple<int, int>(x, y));
        }
    }
}
