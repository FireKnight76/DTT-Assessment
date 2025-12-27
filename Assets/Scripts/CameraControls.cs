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
    [SerializeField] float cameraSensitivity;

    float xRotation = 0;

    bool cameraControl = false;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlPerspective();
        }

        if (_camera.orthographic)
        {
            //resets the camera back to the base rotation so that it looks down at the maze
            _camera.transform.eulerAngles = new Vector3(90f, 0, 0);

            //checks the way the scrollwheel is spinning to decide whether the camera should zoom in or out
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _camera.orthographicSize += zoomSpeed * Time.deltaTime;

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                 _camera.orthographicSize -= zoomSpeed * Time.deltaTime;
            }
            //prevents the zoom effect of the camera from going too far
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom2D, maxZoom2D);
            
            transform.Translate(new Vector3(moveX, 0, moveZ));
        }
        else
        {
            _camera.transform.eulerAngles = new Vector3(90f, 0, 0);

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _camera.fieldOfView += zoomSpeed * Time.deltaTime;

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _camera.fieldOfView -= zoomSpeed * Time.deltaTime;
            }
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minZoom3D, maxZoom3D);
            
            //checks if the user wants control of the camera rotation
            if (cameraControl)
            {
                float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.Rotate(Vector3.up * mouseX);

                _camera.transform.Translate(moveX, 0, moveZ);
            }
            else
            {
                transform.Translate(new Vector3(moveX, 0, moveZ));
            }
        }
    }

    void ControlPerspective()
    {
        if (!cameraControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraControl = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraControl = false;
        }
    }


}
