using Assets.Scripts;
using UnityEngine;

public class GridNodes : MonoBehaviour
{
    public GameObject Node;
    private Node[,] _nodes { get; set; }

    void Start()
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
                //Lines below are for debug only
                GameObject node = Instantiate(Node);
                node.transform.position = new Vector3(x, y, 0);
            }
        }

        foreach (var node in _nodes)
        {
            //raycast to chec connection betwen nodes
        }
    }
}
