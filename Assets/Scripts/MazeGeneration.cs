using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MazeGeneration : MonoBehaviour
{
    [SerializeField] MazeCell cell;
    [SerializeField] GameObject maze;
    [SerializeField] TMP_InputField xSize;
    [SerializeField] TMP_InputField zSize;
    [SerializeField] TMP_Text text;

    //the multidimensional array to contain the createed cells
    MazeCell[,] mazeGrid;
    GameObject[] clearCells;

    List<MazeCell> visitedCells = new List<MazeCell>();
    List<MazeCell> completedCells = new List<MazeCell>();

    int x;
    int z;

    bool createPath = false;
    bool paused = false;


    public void StartMazeCreation()
    {
        createPath = true;

        InputToInt(xSize, zSize);

        if((x >= 10 && x <= 250) && (z >= 10 && z <= 250))
        {
            ClearCells();

            GenerateGrid(x, z);

            if (createPath)
            {
                visitedCells.Clear();
                completedCells.Clear();

                MazeCell startCell = mazeGrid[Random.Range(0, x), Random.Range(0, z)];
                startCell.SetState(cellState.Passed);
                visitedCells.Add(startCell);

                StartCoroutine(GenerateMaze(startCell));

                createPath = false;
            }
        }
        else
        {
            print("invalid values");
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

                currentCell.transform.SetParent(maze.transform);

                mazeGrid[i, j] = currentCell;
            }
        }
    }

    private IEnumerator GenerateMaze(MazeCell currentcell)
    {
        while (paused)
        {
            yield return null;
        }

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

        //delays the method to show the generationa and prevent a StackOverflowException
        yield return new WaitForSeconds(0.01f);

        //checks if any of the spots next to the currentcell are available
        if (possibleCells.Count > 0)
        {
            int chosenDirection = Random.Range(0, possibleDirections.Count);
            MazeCell chosenCell = possibleCells[chosenDirection];

            //breaks down the walls between cells to create a visual path
            switch (possibleDirections[chosenDirection])
            {
                //Checks which direction the path is going in and tears down the required walls between the cells to create a visual path
                case 1:
                    chosenCell.RemoveLeftWall();
                    currentcell.RemoveRightWall();
                    break;
                case 2:
                    chosenCell.RemoveRightWall();
                    currentcell.RemoveLeftWall();
                    break;
                case 3:
                    chosenCell.RemoveBackWall();
                    currentcell.RemoveFrontWall();
                    break;
                case 4:
                    chosenCell.RemoveFrontWall();
                    currentcell.RemoveBackWall();
                    break;
            }


            //changes the color of the floor to indicate that the cells has been passed once and adds it to the list
            chosenCell.SetState(cellState.Passed);
            visitedCells.Add(chosenCell);

            //calls the same function from the position of the new location
            yield return StartCoroutine(GenerateMaze(chosenCell));
        }
        else if (possibleCells.Count == 0)
        {
            //Backtracks and sets the cell to complete so that it will not be passed again till it reaches a spot which has unpassed spots next to it to keep the maze going
            completedCells.Add(currentcell);
            visitedCells.Remove(currentcell);
            currentcell.SetState(cellState.Completed);

            if (visitedCells.Count > 0)
            {
                yield return StartCoroutine(GenerateMaze(visitedCells[visitedCells.Count - 1]));
            }
        }

        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        text.gameObject.SetActive(false);

        StopAllCoroutines();

    }

    public void Pause()
    {
        if (paused)
        {
            paused = false;
        }
        else if (!paused)
        {
            paused = true;
        }
    }

    public void ChangeCamera()
    {
        if (Camera.main.orthographic)
        {
            Camera.main.orthographic = false;
        }
        else
        {
            Camera.main.orthographic = true;
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
