using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeGenerator : Grid2D
{
    private struct Neighbour
    {
        public Vector2Int pos;
        public CellState sharedWall;

        public Neighbour(Vector2Int p, CellState c)
        {
            pos = p;
            sharedWall = c;
        }
    }

    private Vector2Int leftOffset = new Vector2Int(-1, 0);
    private Vector2Int rightOffset = new Vector2Int(1, 0);
    private Vector2Int topOffset = new Vector2Int(0, 1);
    private Vector2Int bottomOffset = new Vector2Int(0, -1);

    private void Start()
    {
        StartCoroutine(GenerateMaze(width, height, Random.Range(int.MinValue, int.MaxValue)));
    }

    private IEnumerator GenerateMaze(int width, int height, int seed)
    {
        System.Random RNG = new System.Random(seed);
        Stack<Vector2Int> positions = new Stack<Vector2Int>();
        Vector2Int currentPos = new Vector2Int(0, 0);

        positions.Push(currentPos);

        while (positions.Count > 0)
        {
            currentPos = positions.Pop();
            UpdateCellState(Operation.ADD, currentPos.x, currentPos.y, CellState.VISITED | CellState.CURRENT);

            List<Neighbour> neighbours = GetUnvisitedNeighbours(currentPos, width, height);

            if (neighbours.Count > 0) {
                Neighbour neighbour = GetRandomItem(neighbours, RNG);

                UpdateCellState(Operation.SUBTRACT, currentPos.x, currentPos.y, GetOppositeWall(neighbour.sharedWall));
                UpdateCellState(Operation.SUBTRACT, neighbour.pos.x, neighbour.pos.y, neighbour.sharedWall);

                positions.Push(currentPos);
                positions.Push(neighbour.pos);
            }

            yield return new WaitForSeconds(0.05f);
            UpdateCellState(Operation.SUBTRACT, currentPos.x, currentPos.y, CellState.CURRENT);
        }
    }

    private List<Neighbour> GetUnvisitedNeighbours(Vector2Int currentPos, int width, int height)
    {
        List<Neighbour> neighbours = new List<Neighbour>();

        if (currentPos.x < width - 1)
        {
            if (!GetCell(currentPos + rightOffset).State.HasFlag(CellState.VISITED))
            {
                neighbours.Add(new Neighbour(currentPos + rightOffset, CellState.LEFT));
            }
        }
        if (currentPos.y < height - 1)
        {
            if (!GetCell(currentPos + topOffset).State.HasFlag(CellState.VISITED))
            {
                neighbours.Add(new Neighbour(currentPos + topOffset, CellState.BOTTOM));
            }
        }
        if (currentPos.x > 0)
        {
            if (!GetCell(currentPos + leftOffset).State.HasFlag(CellState.VISITED))
            {
                neighbours.Add(new Neighbour(currentPos + leftOffset, CellState.RIGHT));
            }
        }
        if (currentPos.y > 0)
        {
            if (!GetCell(currentPos + bottomOffset).State.HasFlag(CellState.VISITED))
            {
                neighbours.Add(new Neighbour(currentPos + bottomOffset, CellState.TOP));
            }
        }

        return neighbours;
    }

    private CellState GetOppositeWall(CellState wall)
    {
        switch (wall)
        {
            case CellState.LEFT:
                return CellState.RIGHT;
            case CellState.RIGHT:
                return CellState.LEFT;
            case CellState.TOP:
                return CellState.BOTTOM;
            case CellState.BOTTOM:
                return CellState.TOP;
            default:
                return CellState.NONE;
        }
    }

    private T GetRandomItem<T>(List<T> list, System.Random RNG)
    {
        return list[RNG.Next(0, list.Count)];
    }
}
