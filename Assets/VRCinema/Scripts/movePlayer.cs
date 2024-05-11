using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float maxYAngle = 80.0f;
    public float zoomSpeed = 10.0f;
    public bool isCursorVisible;

    

    [SerializeField] private  Camera playerCamera;
    [SerializeField] private Transform pivot;
    [SerializeField] private bool isCameraEnabled = false;

    private Vector2 currentRotation;

    private void Start()
    {
        currentRotation = new Vector2(pivot.eulerAngles.y, pivot.eulerAngles.x);
    }

    private void Update()
    {
        
        if (!isCameraEnabled) 
            return;

        HandleCursorLock();
        HandleCameraRotation();
        HandleZoom();
    }

    private void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isCursorVisible = !isCursorVisible;
            Cursor.visible = isCursorVisible;
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        currentRotation.x += mouseX;
        currentRotation.y -= mouseY;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        pivot.rotation = Quaternion.Euler(0, currentRotation.x, 0);
        // Вращаем только pivot по оси Y
        playerCamera.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        // Вращаем камеру относительно pivot
    }

    private void HandleZoom()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        if (playerCamera.orthographic)
        {
            playerCamera.orthographicSize -= zoomInput * zoomSpeed;
            playerCamera.orthographicSize = Mathf.Clamp(playerCamera.orthographicSize, 1f, 10f); // Ограничение зума для ортографической камеры
        }
        else
        {
            playerCamera.fieldOfView -= zoomInput * zoomSpeed;
            playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, 10f, 60f); 
            // Ограничение зума для перспективной камеры
        }
    }

    public void EnableCameraControl() 
    {
        isCameraEnabled = true; 
    }
    public void DisableCameraControl()
    {
        isCameraEnabled = false;
    }
}

    //public float sensitivity = 2.0f;
    //public float maxYAngle = 80.0f;


    //private Vector2 currentRotation;

    //public float zoomSpeed = 10;
    //public bool isCursorVisible;

    //[SerializeField] new Camera camera;

    //private void Start()
    //{
    //    currentRotation = new Vector2(transform.eulerAngles.y, transform.eulerAngles.x);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        isCursorVisible = !isCursorVisible;
    //        Cursor.visible = isCursorVisible;
    //        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
    //    }



    //    float mouseX = Input.GetAxis("Mouse X") * sensitivity;
    //    float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

    //    currentRotation.x += mouseX;
    //    currentRotation.y -= mouseY;
    //    currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);     

    //    transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

    //    if (camera.orthographic)
    //    {
    //        camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    //    }
    //    else
    //    {
    //        camera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    //    }




