using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class PathFinding
    {
        Node[,] _nodes { get; set; }

        public PathFinding(Node[,] nodes)
        {
            _nodes = nodes;
        }

        public List<Node> FindPath(Vector3 start, Vector3 goal)
        {
            Node startNode = VectorToNode(start);
            Node goalNode = VectorToNode(goal);

            var cameFrom = new Dictionary<Node, Node>();

            var openNodes = new List<Node>();
            openNodes.Add(startNode);

            //gScore
            var costsFromStart = new Dictionary<Node, double>();
            costsFromStart.Add(startNode, 0);
            //fScore
            var costsFromStartPlusEstimatedToGoal = new Dictionary<Node, double>();
            costsFromStartPlusEstimatedToGoal.Add(startNode, EstimateCostToNode(startNode, goalNode));

            while (openNodes.Count != 0)
            {
                //This labmda should give us node from openNodes ordered by vost from costsFromStart
                Node current = openNodes.OrderBy(node => costsFromStart[node]).ToList()[0];
                if (current == goalNode)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openNodes.Remove(current);

                foreach (var neighborIndexes in current.NeighborIndexes)
                {
                    var neighbor = _nodes[neighborIndexes.Item1, neighborIndexes.Item2];
                    double startToNeighborCost = costsFromStart[current] + EstimateCostToNode(current, neighbor);
                    if (!costsFromStart.ContainsKey(neighbor))
                    {
                        costsFromStart.Add(neighbor, double.PositiveInfinity);
                        costsFromStartPlusEstimatedToGoal.Add(neighbor, double.PositiveInfinity);
                    }
                    if (startToNeighborCost < costsFromStart[neighbor])
                    {
                        cameFrom.Add(neighbor, current);
                        costsFromStart[neighbor] = startToNeighborCost;
                        costsFromStartPlusEstimatedToGoal[neighbor] = costsFromStart[neighbor] + EstimateCostToNode(neighbor, goalNode);
                        if(!openNodes.Contains(neighbor))
                        {
                            openNodes.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }

        private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
        {
            var path = new List<Node>();
            while(cameFrom.Keys.Contains(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            return path;
        }

        //In pseudo code it is called h()
        private double EstimateCostToNode(Node current, Node target)
        {
            return Vector2.Distance(new Vector2(current.X, current.Y), new Vector2(target.X, target.Y));
        }


        private Node VectorToNode(Vector3 vector)
        {
            /*
            double bestOffset = double.PositiveInfinity;
            Node bestNode = null;
            foreach (var node in _nodes)
            {
                if(!node.HasNeighbors)
                {
                    continue;
                }
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
            */
            int indexX = (int)Math.Round(vector.x) + (int)Math.Floor(_nodes.GetLength(0) / 2f);
            int indexY = (int)Math.Round(vector.y) + (int)Math.Floor(_nodes.GetLength(1) / 2f);
            return _nodes[indexX, indexY];
        }
    }
}
