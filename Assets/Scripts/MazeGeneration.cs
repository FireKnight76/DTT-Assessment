using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    [SerializeField] MazeCell cell;
    [SerializeField] TMP_InputField xSize;
    [SerializeField] TMP_InputField zSize;

    //the multidimensional array to contain the createed cells
    MazeCell[,] mazeGrid;
    GameObject[] clearCells;

    List<MazeCell> visitedCells = new List<MazeCell>();
    List<MazeCell> completedCells = new List<MazeCell>();

    int x;
    int z;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            InputToInt(xSize, zSize);

            if((x >= 10 && x <= 250) && (z >= 10 && z <= 250))
            {
                ClearCells();

                GenerateGrid(x, z);

                StartCoroutine(GenerateMaze(mazeGrid[Random.Range(0, x), Random.Range(0, z)]));
            }
            else
            {
                print("invalid values");
            }
        }


    }

    //Method to generate a grid of cells that will be used to create the maze later
    void GenerateGrid(int  x, int z)
    {
        //overrides the mazeGrid to the desired size for the creation of the maze in the inputted size
        mazeGrid = new MazeCell[x, z];

        for (int i = 0; i < mazeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mazeGrid.GetLength(1); j++)
            {
                //creates the grid in a way so that it is centered around the middle of the Unity editor
                MazeCell currentCell = Instantiate(cell, new Vector3(i - (x / 2f), 0, j - (z /2f)), Quaternion.identity);

                currentCell.gridX = i;
                currentCell.gridZ = j;

                mazeGrid[i, j] = currentCell;

            }
        }
    }

    private IEnumerator GenerateMaze(MazeCell currentcell)
    {

        List<int> possibleDirections = new List<int>();
        List<MazeCell> possibleCells = new List<MazeCell>();

        int x = (int) currentcell.gridX;
        int z = (int) currentcell.gridZ;

        //Checks if the cell is all the way on the right
        if (x < this.x - 1)
        {
            //checks if the cell has already been visited or is done, same for every other one
            if (!visitedCells.Contains(mazeGrid[x + 1, z]) && !completedCells.Contains(mazeGrid[x + 1, z]))
            {
                possibleDirections.Add(1);
                possibleCells.Add(mazeGrid[x + 1, z]);

            }
        }

        //checks if the cell is all the way on the left
        if (x > 0)
        {
            if(!visitedCells.Contains(mazeGrid[x - 1, z]) && !completedCells.Contains(mazeGrid[x - 1, z]))
            {
                possibleDirections.Add(2);
                possibleCells.Add(mazeGrid[x - 1, z]);
            }
        }

        //checks if the cell is all the way at the bottom
        if (z < this.z - 1)
        {
            if (!visitedCells.Contains(mazeGrid[x, z + 1]) && !completedCells.Contains(mazeGrid[x, z + 1]))
            {
                possibleDirections.Add(3);
                possibleCells.Add(mazeGrid[x, z + 1]);

            }
        }

        //checks if the cell is all the way at the top
        if (z > 0)
        {
            if (!visitedCells.Contains(mazeGrid[x, z - 1]) && !completedCells.Contains(mazeGrid[x, z - 1]))
            {
                possibleDirections.Add(4);
                possibleCells.Add(mazeGrid[x, z - 1]);
            }
        }

        yield return new WaitForSeconds(0.1f);

        if (possibleCells.Count > 0)
        {
            int chosenDirection = Random.Range(0, possibleDirections.Count);
            MazeCell chosenCell = possibleCells[chosenDirection];

            //print(chosenDirection);

            chosenCell.SetState(cellState.Passed);
            visitedCells.Add(chosenCell);

            yield return StartCoroutine(GenerateMaze(chosenCell));
        }
        else if (possibleCells.Count == 0)
        {
            completedCells.Add(currentcell);
            visitedCells.Remove(currentcell);
            currentcell.SetState(cellState.Completed);

            if (visitedCells.Count > 0)
            {
                yield return StartCoroutine(GenerateMaze(visitedCells[visitedCells.Count - 1]));
            }
        }


    }

    void ClearCells()
    {
        clearCells = GameObject.FindGameObjectsWithTag("MazeCell");

        foreach(GameObject g in clearCells)
        {
            Destroy(g);
        }
    }

    //method to turn the input of the input fields into integers for the maze generation
    void InputToInt(TMP_InputField width, TMP_InputField height)
    {
        x = int.Parse(width.text);
        z = int.Parse(height.text);
    }

}
