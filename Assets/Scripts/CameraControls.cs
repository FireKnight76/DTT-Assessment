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
        //reads if w, s, up or down are pressed and translates it into a float
        float moveX = Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime;
        //reads if a, d, left or right are pressed and translates it into a float
        float moveZ = Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;
        //reads if space or left shift are pressed and translates it into a float
        float moveY = Input.GetAxis("Jump") * cameraSpeed * Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlPerspective();
        }

        if (_camera.orthographic)
        {
            cameraControl = false;
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
                //translates mouse movement into a float
                float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

                //turns the float from mouseY into a value that can be applied to a Maathf.Clamp
                xRotation -= mouseY;
                //limits the rotation to a minimum of -90 and 90
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                //translates the value of xRotation into a rotation on the x axis
                _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                //rotates the parent for smooth rotating to the left and right
                transform.Rotate(Vector3.up * mouseX);
            }
        }

        transform.Translate(new Vector3(moveX, moveY, moveZ));
    }

    void ControlPerspective()
    {
        if (!cameraControl && !_camera.orthographic)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraControl = true;
        }
        else if (cameraControl && !_camera.orthographic) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraControl = false;

            //resets the rotation of the parent to keep movement stable
            transform.localRotation = Quaternion.identity;
            
        }
    }


}
