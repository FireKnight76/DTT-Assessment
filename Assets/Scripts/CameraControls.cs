using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float cameraSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float minZoom2D;
    [SerializeField] float maxZoom2D;
    [SerializeField] float minZoom3D;
    [SerializeField] float maxZoom3D;

    // Update is called once per frame
    void Update()
    {
        if (_camera.orthographic)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _camera.orthographicSize += zoomSpeed * Time.deltaTime;

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                 _camera.orthographicSize -= zoomSpeed * Time.deltaTime;
            }
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom2D, maxZoom2D);
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _camera.fieldOfView += zoomSpeed * Time.deltaTime;

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _camera.fieldOfView -= zoomSpeed * Time.deltaTime;
            }
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minZoom3D, maxZoom3D);
        }




        float moveX = Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;

        transform.Translate(new Vector3(moveX, 0, moveZ));
    }
}
