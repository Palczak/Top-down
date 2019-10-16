using UnityEngine;

public class GridNodes : MonoBehaviour
{
    public GameObject Node;
    private GameObject[,] _nodes { get; set; }

    void Start()
    {
        Grid grid = gameObject.GetComponent<Grid>();
        var gridSize = GetComponent<RectTransform>().sizeDelta;
        _nodes = new GameObject[(int)gridSize.x + 1, (int)gridSize.y + 1];

        for (int i = 0; i <= gridSize.x; i++)
        {
            for (int j = 0; j <= gridSize.y; j++)
            {
                _nodes[i, j] = Instantiate(Node);
                _nodes[i, j].transform.position = new Vector3((i - 1 + grid.cellSize.x) - gridSize.x / 2, (j - 1 + grid.cellSize.y) - gridSize.y / 2, 0);
            }
        }
    }
}
