using TMPro;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    [SerializeField] MazeCell cell;
    [SerializeField] TMP_InputField xSize;
    [SerializeField] TMP_InputField zSize;

    int x;
    int z;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            InputToInt(xSize, zSize);
            Debug.Log($"x = {x} and z = {z}");
        }
    }


    void InputToInt(TMP_InputField width, TMP_InputField height)
    {
        x = int.Parse(width.text);
        z = int.Parse(height.text);
    }

}
