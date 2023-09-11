using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LineTester : MonoBehaviour
{
    [Range(-10, 10)]
    public float constant = 1;
    public float widthC = 0.01f;
    public float rangeC = 22f;
    public int numberOfPoints = 5000;

    public Camera cam;
    public LineRenderer lr;
    private Transform camT;

    private void Awake()
    {
        camT = cam.transform;
    }

    private void Start()
    {
        lr.positionCount = numberOfPoints;
    }

    private void Update()
    {
        float camX = camT.position.x;

        lr.startWidth = widthC * cam.orthographicSize;
        lr.endWidth = widthC * cam.orthographicSize;

        Vector3[] coords = new Vector3[numberOfPoints];
        int count = 0;
        float range = rangeC * cam.orthographicSize;
        float step = (2 * range) / numberOfPoints;
        for (float x = camX - range; count < numberOfPoints; x += step)
        {    
            coords[count] = new Vector3(x, f(x));
            count++;
        }

        lr.SetPositions(coords);
    }

    private float f(float x)
    {
        return (float)Math.Sin(constant * x);
    }

    private float fP(float x, float step)
    {
        return (f(x + step) - f(x)) / step;
    }
}
