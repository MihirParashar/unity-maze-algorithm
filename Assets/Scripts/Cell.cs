using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum CellState
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,
    TOP = 4,
    BOTTOM = 8,
    VISITED = 16,
    CURRENT = 32
}
public class Cell : MonoBehaviour
{

    private CellState state = CellState.LEFT | CellState.RIGHT | CellState.TOP | CellState.BOTTOM;
    public CellState State
    {
        get { return state; }
        set { state = value; Redraw(); }
    }

    private int xPos, yPos;

    private bool positionInitialized;

    [Header("Walls")]
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [Header("Colors")]
    [SerializeField] private Color visitedColor;
    [SerializeField] private Color currentPositionColor;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Redraw();
    }

    public void SetPosition(int x, int y)
    {
        xPos = x;
        yPos = y;
        positionInitialized = true;
    }

    private void Redraw() { 

        if (!positionInitialized)
        {
            Debug.LogError("Cell's position not initalized!", this);
        }
        if (state.HasFlag(CellState.LEFT) && xPos == 0)
        {
            leftWall.SetActive(true);
        }
        if (state.HasFlag(CellState.RIGHT))
        {
            rightWall.SetActive(true);
        }
        if (state.HasFlag(CellState.TOP))
        {
            topWall.SetActive(true);
        }
        if (state.HasFlag(CellState.BOTTOM) && yPos == 0)
        {
            bottomWall.SetActive(true);
        }

        if (state.HasFlag(CellState.CURRENT))
        {
            spriteRenderer.color = currentPositionColor;
        } else if (state.HasFlag(CellState.VISITED))
        {
            spriteRenderer.color = visitedColor;
        } else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
