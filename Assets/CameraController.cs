using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 dragOrigin;
    private Vector3 origin;
    private float originalZoom;

    private Camera cam;
    public float zoomStep;

    private void Start()
    {
        cam = GetComponent<Camera>();
        origin = cam.transform.position;
        originalZoom = cam.orthographicSize;
    }

    private void LateUpdate()
    {
        Zoom();
        Drag();
        if (Input.GetMouseButtonUp(1)) 
        { 
            cam.transform.position = origin;
            cam.orthographicSize = originalZoom;
        }
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            cam.orthographicSize = cam.orthographicSize / zoomStep;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            cam.orthographicSize = cam.orthographicSize * zoomStep;
        }
    }

    private void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }
    }
}
