using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeGenerator : Grid2D
{
    struct Neighbour {
        public Vector2Int pos;
        public CellState sharedWall;

        public Neighbour(Vector2Int pos, CellState sharedWall)
        {
            this.pos = pos;
            this.sharedWall = sharedWall;
        }
    }

    private Vector2Int leftOffset = new Vector2Int(-1, 0);
    private Vector2Int rightOffset = new Vector2Int(1, 0);
    private Vector2Int topOffset = new Vector2Int(0, 1);
    private Vector2Int bottomOffset = new Vector2Int(0, -1);

    private void Start()
    {
        GenerateMaze(width, height, Random.Range(int.MinValue, int.MaxValue));
    }

    void GenerateMaze(int width, int height, int seed)
    {
        int visited = 0;
        Stack<Vector2Int> positions = new Stack<Vector2Int>();
        Vector2Int currentPos = new Vector2Int(0, 0);
        positions.Push(currentPos);
        while (visited < width * height)
        {
            List<Neighbour> n = GetUnvisitedNeighbours(currentPos, width, height);
        }
    }

    List<Neighbour> GetUnvisitedNeighbours(Vector2Int currentPos, int width, int height)
    {
        List<Neighbour> neighbours = new List<Neighbour>();

        if (currentPos.x < width)
        {
            if (!GetCell(currentPos + leftOffset).State.HasFlag(CellState.VISITED))
            {
                neighbours.Add(new Neighbour(currentPos + leftOffset, CellState.LEFT));
            }
        }

        return neighbours;
    }
}
