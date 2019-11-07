using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class AStar
    {
        Node[] _nodes { get; set; }

        public AStar(Node[] nodes)
        {
            _nodes = nodes;
        }

        public List<Node> FindPath(Vector3 start, Vector3 target)
        {
            Node startNode = VectorToNode(start);
            Node tartetNode = VectorToNode(target);
            List<Node> path = new List<Node>();



            return path;
        }

        private Node VectorToNode(Vector3 vector)
        {
            double bestOffset = double.PositiveInfinity;
            Node bestNode = null;
            foreach (var node in _nodes)
            {
                double xCurrentOffset = Math.Abs(node.X - vector.x);
                double yCurrentOffset = Math.Abs(node.Y - vector.y);
                double currentOffset = (xCurrentOffset + yCurrentOffset) / 2;
                if (currentOffset < bestOffset)
                {
                    bestOffset = currentOffset;
                    bestNode = node;
                    if (currentOffset == 0)
                    {
                        break;
                    }
                }
            }
            return bestNode;
        }
    }
}
