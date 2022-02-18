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

    private Cell[,] cells;

    private void Start()
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

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 cellPos = new Vector2(i - width / 2, j - height / 2);
                Cell cell = Instantiate(cellPrefab, cellPos, Quaternion.identity, transform).GetComponent<Cell>();
                cell.SetPosition(i, j);
                Debug.Log(i + " " + j); 
                cells[i, j] = cell;
            }
        }
    }

    protected void UpdateCellState(Operation operation, int i, int j, CellState newState)
    {
        switch (operation)
        {
            case Operation.ADD:
                cells[i, j].State |= newState;
                break;
            case Operation.SUBTRACT:
                cells[i, j].State &= ~newState;
                break;
        }
    }

    protected Cell GetCell(int i, int j)
    {
        return cells[i, j];
    }
}
