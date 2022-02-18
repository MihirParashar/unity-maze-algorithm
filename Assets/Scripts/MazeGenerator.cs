using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeGenerator : Grid2D
{
    private void Start()
    {
        UpdateCellState(Operation.ADD, 3, 3, CellState.VISITED);
    }
}
