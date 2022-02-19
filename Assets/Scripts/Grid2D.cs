using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operation
{
    ADD,
    SUBTRACT
}

public class Grid2D : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] protected int width;
    [SerializeField] protected int height;
    private bool isInitialized;

    private Cell[,] cells;

    private void Awake()
    {
        if (cellPrefab.GetComponent<Cell>() == null)
        {
            Debug.Log("Cell prefab specified does not contain the Cell script. Adding it manually...");
            cellPrefab.AddComponent<Cell>();
        }

        Initialize();
    }

    void Initialize()
    {
        cells = new Cell[width, height];
        float halfWidth = cellPrefab.transform.localScale.x / 2f;
        float halfHeight = cellPrefab.transform.localScale.y / 2f;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 cellPos = new Vector2(i - width / 2, j - height / 2);
                Cell cell = Instantiate(cellPrefab, cellPos, Quaternion.identity, transform).GetComponent<Cell>();
                cells[i, j] = cell;
            }
        }

        isInitialized = true;
    }

    protected void UpdateCellState(Operation operation, int x, int y, CellState state)
    {
        if (!isInitialized)
        {
            Debug.LogError("Grid is not initialized yet!", this);
        }

        switch (operation)
        {
            case Operation.ADD:
                cells[x, y].State |= state;
                break;
            case Operation.SUBTRACT:
                cells[x, y].State &= ~state;
                break;
        }
    }

    protected Cell GetCell(int x, int y)
    {
        return cells[x, y];
    }
    protected Cell GetCell(Vector2Int pos)
    {
        return cells[pos.x, pos.y];
    }
}
