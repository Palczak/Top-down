using Assets.Scripts;
using System;
using System.Linq;
using UnityEngine;

public class GridNodes : MonoBehaviour
{
    private Node[,] _nodes { get; set; }
    public Node[,] Nodes { get { return _nodes; } }

    private GameObject[] _enemies { get; set; }
    private GameObject _player { get; set; }

    private enum Direction
    {
        Left,
        LeftTop,
        Top,
        RightTop,
        Right,
        RightBottom,
        Bottom,
        LeftBottom
    }

    private void Update()
    {
        EstimateCosts();
    }

    private void EstimateCosts()
    {
        foreach (var node in _nodes)
        {
            node.Cost = 1;
            if (_player != null)
            {
                float playerToNodeArc = Vector2.Dot((node.Position - _player.transform.position).normalized, _player.transform.up);
                if (playerToNodeArc >= 0.75f)
                {
                    node.Cost += 20;
                }
            }
        }
    }

    private Vector3 DirectionToRelativePosition(Direction direction)
    {
        var outputVector = new Vector3(0, 0, 0);
        switch (direction)
        {
            case Direction.Left:
                {
                    outputVector.x = -1;
                    return outputVector;
                }
            case Direction.LeftTop:
                {
                    outputVector.x = -1;
                    outputVector.y = 1;
                    return outputVector;
                }
            case Direction.Top:
                {
                    outputVector.y = 1;
                    return outputVector;
                }
            case Direction.RightTop:
                {
                    outputVector.x = 1;
                    outputVector.y = 1;
                    return outputVector;
                }
            case Direction.Right:
                {
                    outputVector.x = 1;
                    return outputVector;
                }
            case Direction.RightBottom:
                {
                    outputVector.x = 1;
                    outputVector.y = -1;
                    return outputVector;
                }
            case Direction.Bottom:
                {
                    outputVector.y = -1;
                    return outputVector;
                }
            case Direction.LeftBottom:
                {
                    outputVector.x = -1;
                    outputVector.y = -1;
                    return outputVector;
                }
            default:
                return outputVector;
        }
    }

    private bool AddNieghtborByDirection(Direction direction, int x, int y)
    {
        int neighborX = 0;
        int neighborY = 0;
        switch (direction)
        {
            case Direction.Left:
                {
                    neighborX = x - 1;
                    neighborY = y;
                    break;
                }
            case Direction.LeftTop:
                {
                    neighborX = x - 1;
                    neighborY = y + 1;
                    break;
                }
            case Direction.Top:
                {
                    neighborX = x;
                    neighborY = y + 1;
                    break;
                }
            case Direction.RightTop:
                {
                    neighborX = x + 1;
                    neighborY = y + 1;
                    break;
                }
            case Direction.Right:
                {
                    neighborX = x + 1;
                    neighborY = y;
                    break;
                }
            case Direction.RightBottom:
                {
                    neighborX = x + 1;
                    neighborY = y - 1;
                    break;
                }
            case Direction.Bottom:
                {
                    neighborX = x;
                    neighborY = y - 1;
                    break;
                }
            case Direction.LeftBottom:
                {
                    neighborX = x - 1;
                    neighborY = y - 1;
                    break;
                }
        }
        if (neighborX < 0 || neighborX >= _nodes.GetLength(0) || neighborY < 0 || neighborY >= _nodes.GetLength(1))
        {
            return false;
        }
        else
        {
            _nodes[x, y].AddNeighborIndex(neighborX, neighborY);
            return true;
        }
    }

    void Awake()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Character");
        _player = GameObject.FindGameObjectWithTag("Player");

        InitializeLogicGrid();

        //This loop will slightly size up all walls to ensure all raycasts won't go thru tips.
        var walls = GameObject.FindGameObjectsWithTag("Terrain");
        foreach (var wall in walls)
        {
            Vector3 transformScale = wall.transform.localScale;
            wall.GetComponent<BoxCollider2D>().size = new Vector2((transformScale.x + 0.2f) / transformScale.x, (transformScale.y + 0.2f) / transformScale.y);
        }

        RaycastNodes();

        //Returning to original size of walls.
        foreach (var wall in walls)
        {
            wall.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        }
    }

    private void InitializeLogicGrid()
    {
        Grid grid = gameObject.GetComponent<Grid>();
        var gridSize = GetComponent<RectTransform>().sizeDelta;
        _nodes = new Node[(int)gridSize.x + 1, (int)gridSize.y + 1];

        for (int i = 0; i <= gridSize.x; i++)
        {
            for (int j = 0; j <= gridSize.y; j++)
            {
                int x = (int)((i - 1 + grid.cellSize.x) - gridSize.x / 2);
                int y = (int)((j - 1 + grid.cellSize.y) - gridSize.y / 2);
                _nodes[i, j] = new Node(x, y);
                //node posision in array equals node.x + floor(gridSize.x / 2), node.y + floor(gridSize.y / 2)
            }
        }
    }

    private void RaycastNodes()
    {
        int layerMask = LayerMask.GetMask("Walls");
        for (int i = 0; i < _nodes.GetLength(0); i++)
        {
            for (int j = 0; j < _nodes.GetLength(1); j++)
            {
                var curentNode = _nodes[i, j];
                var currentNodePosition = new Vector3(curentNode.X, curentNode.Y, 0);
                var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();
                foreach (var direction in directions)
                {
                    var target = DirectionToRelativePosition(direction);
                    target += currentNodePosition;
                    var hit = Physics2D.Raycast(currentNodePosition, target - currentNodePosition, 1.42f, layerMask);
                    if (hit.collider == null)
                    {
                        AddNieghtborByDirection(direction, i, j);
                    }
                }
            }
        }
    }
}
