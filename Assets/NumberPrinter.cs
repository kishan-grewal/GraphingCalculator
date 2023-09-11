using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class NumberPrinter : MonoBehaviour
{
    private int vNumber = 22;
    private float vRangeC = 22f;

    private int hNumber = 12;
    private float hRangeC = 12f;

    public GameObject[] vNumbers = new GameObject[22];
    public GameObject[] hNumbers = new GameObject[12];

    public Camera cam;
    private Transform camT;

    private void Awake()
    {
        camT = cam.transform;
    }

    private void Start()
    {
        for (int i = 0; i < vNumber; i++)
        {
            vNumbers[i].AddComponent<Text>();
        }
        for (int i = 0; i < hNumber; i++)
        {
            hNumbers[i].AddComponent<Text>();
        }
    }

    private void Update()
    {
        // problem in the ZOOMING PLEASE FIX
        int scale = Mathf.FloorToInt(Mathf.Log(cam.orthographicSize / 5, 2));
        float mult = Mathf.Pow(2, scale); // also will decide axis numbers
        float camX = camT.position.x;
        float camY = camT.position.y;
        float vRange = vRangeC * mult;
        float hRange = hRangeC * mult;

        int count = 0;
        float step = (2 * vRange) / vNumber;
        float A = (int)(camX / step) * step;
        float B = A - (int)(vRange / step) * step;
        for (float x = B; count < vNumber; x += step)
        {
            Text current = vNumbers[count].GetComponent<Text>();
            current.text = ((count-10) * mult).ToString();
            current.transform.position = new Vector3(x - 0.1f, -0.1f);
        }

        count = 0;
        step = (2 * hRange) / hNumber;
        A = (int)(camY / step) * step;
        B = A - (int)(hRange / step) * step;
        for (float y = B; count < hNumber; y += step)
        {
            Text current = hNumbers[count].GetComponent<Text>();
            current.text = ((count - 5) * mult).ToString();
            current.transform.position = new Vector3(-0.1f, y - 0.1f);
        }
    }
}
