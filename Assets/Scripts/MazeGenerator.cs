using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _MazeCellPrefab;

    [SerializeField]
    private int _MazeWidth;

    [SerializeField]
    private int _MazeDepth;

    [SerializeField]
    private MazeCell[,] _MazeGrid;

    void Start()
    {
        _MazeGrid = new MazeCell[_MazeWidth, _MazeDepth];

        for (int x = 0; x < _MazeWidth; x++)
        {
            for (int z = 0; z < _MazeDepth; z++)
            {
                _MazeGrid[x, z] = Instantiate(_MazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        GenerateMaze(null, _MazeGrid[0, 0]);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);
        
        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _MazeWidth)
        {
            var cellToRight = _MazeGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
            { yield return cellToRight; }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _MazeGrid[x - 1, z];
            if (cellToLeft.IsVisited == false)
            { yield return cellToLeft; }
        }
        if (z + 1 < _MazeDepth)
        {
            var cellToFront = _MazeGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
            { yield return cellToFront; }
        }
        if (z - 1 >= 0)
        {
            var cellToBack = _MazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            { yield return cellToBack; }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
            return;

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRigthtWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRigthtWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFronttWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFronttWall();
            return;
        }
    }


    void Update()
    {

    }
}
