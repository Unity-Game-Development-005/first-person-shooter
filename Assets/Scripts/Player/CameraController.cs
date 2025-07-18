
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public static CameraController cameraController;

    public Transform target;

    private float startFOV;
        
    private float targetFOV;

    public float zoomSpeed = 1f;

    public Camera mainCamera;



    private void Awake()
    {
        cameraController = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        startFOV = mainCamera.fieldOfView;

        targetFOV = startFOV;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;

        transform.rotation = target.rotation;

        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }


    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }


    public void ZoomOut()
    {
        targetFOV = startFOV;
    }


} // end of class
