using UnityEngine;

public class Grid2D : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    private Cell[,] cells;

    private void Awake()
    {
        if (cellPrefab.GetComponent<Cell>() == null)
        {
            Debug.Log("Cell prefab specified does not contain the Cell script. Adding it manually...");
            cellPrefab.AddComponent<Cell>();
        }
    }

    protected void Initialize(int width, int height)
    {
        cells = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 cellPos = new Vector2(i - width / 2, j - height / 2);
                Cell cell = Instantiate(cellPrefab, cellPos, Quaternion.identity, transform).GetComponent<Cell>();
                cells[i, j] = cell;
            }
        }
    }

    protected void AddToCellState(Vector2Int pos, CellState stateToAdd)
    {
        cells[pos.x, pos.y].State |= stateToAdd;
    }

    protected void SubtractFromCellState(Vector2Int pos, CellState stateToSubtract)
    {
        cells[pos.x, pos.y].State &= ~stateToSubtract;
    }

    protected Cell GetCell(int x, int y)
    {
        return cells[x, y];
    }
    protected Cell GetCell(Vector2Int pos)
    {
        return cells[pos.x, pos.y];
    }
    protected void Reset()
    {
        if (cells == null) return;
        foreach (Cell cell in cells)
        {
            Destroy(cell.gameObject);
        }
        cells = null;
    }

}
