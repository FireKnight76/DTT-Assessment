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



}
