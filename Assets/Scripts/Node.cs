using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        private List<Node> _neighbors { get; }

        public Node (int x, int y)
        {
            X = x;
            Y = Y;
            _neighbors = new List<Node>();
        }

        public Node()
        {
            X = 0;
            Y = 0;
            _neighbors = new List<Node>();
        }
    }
}
