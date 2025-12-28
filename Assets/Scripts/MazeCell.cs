using UnityEngine;

public enum cellState
{
    Available,
    Passed,
    Completed
}

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject PosXWall; 
    [SerializeField] private GameObject NegXWall;
    [SerializeField] private GameObject PosZWall;
    [SerializeField] private GameObject NegZWall;
    [SerializeField] private MeshRenderer floor;

    //stores the location of the instance of the MazeCell in the mazeGrid array
    public int gridX;
    public int gridZ;

    public void RemoveRightWall()
    {
        PosXWall.gameObject.SetActive(false);
    }

    public void RemoveLeftWall()
    {
        NegXWall.gameObject.SetActive(false);
    }

    public void RemoveFrontWall()
    {
        PosZWall.gameObject.SetActive(false);
    }

    public void RemoveBackWall()
    {
        NegZWall.gameObject.SetActive(false);
    }

    public void SetState(cellState state)
    {
        //checks the state of the MazeCell and gives it a color to create a visual representation
        switch (state)
        {
            case cellState.Available:
                floor.material.color = Color.white;
                break;
            case cellState.Passed:
                floor.material.color = Color.yellow;
                break;
            case cellState.Completed:
                floor.material.color = Color.green;
                break;
        }
    }

}
