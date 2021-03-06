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
    [Header("Walls")]
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [Header("Colors")]
    [SerializeField] private Color visitedColor;
    [SerializeField] private Color currentPositionColor;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Redraw();
    }

    private void Redraw() { 

        UpdateWalls();
        UpdateColor();
    }

    private void UpdateWalls()
    {
        leftWall.SetActive(state.HasFlag(CellState.LEFT));
        rightWall.SetActive(state.HasFlag(CellState.RIGHT));
        topWall.SetActive(state.HasFlag(CellState.TOP));
        bottomWall.SetActive(state.HasFlag(CellState.BOTTOM));
    }

    private void UpdateColor()
    {
        if (state.HasFlag(CellState.CURRENT))
        {
            spriteRenderer.color = currentPositionColor;
        }
        else if (state.HasFlag(CellState.VISITED))
        {
            spriteRenderer.color = visitedColor;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        state |= CellState.VISITED;
        state &= ~CellState.RIGHT;
        Redraw();
    }
}
