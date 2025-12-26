using TMPro;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    [SerializeField] MazeCell cell;
    [SerializeField] TMP_InputField xSize;
    [SerializeField] TMP_InputField zSize;

    //the multidimensional array to contain the createed cells
    MazeCell[,] mazeGrid;

    int x;
    int z;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if((x >= 10 && x <= 250) && (z >= 10 && z <= 250))
            {
                InputToInt(xSize, zSize);
                GenerateGrid(x, z);

            }
        }
    }


    void GenerateGrid(int  x, int z)
    {
        mazeGrid = new MazeCell[x, z];

        for (int i = 0; i < mazeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mazeGrid.GetLength(1); j++)
            {
                MazeCell currentCell = Instantiate(cell, new Vector3(i - (x / 2f), 0, j - (z /2f)), Quaternion.identity);

                mazeGrid[i, j] = currentCell;

            }
        }
    }

    //method to turn the input of the input fields into integers for the maze generation
    void InputToInt(TMP_InputField width, TMP_InputField height)
    {
        x = int.Parse(width.text);
        z = int.Parse(height.text);
    }

}
