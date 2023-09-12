using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class NumberPrinter : MonoBehaviour
{
    public GameObject textPrefab; // Reference to your Text prefab.

    private int vNumber = 21;
    private float vRangeC = 21f;
    private Text[] vNums;

    private int hNumber = 11;
    private float hRangeC = 11f;
    private Text[] hNums;

    private float offSetC = 0.5f;

    public Camera cam;
    private Transform camT;

    private void Awake()
    {
        camT = cam.transform;
    }

    private void Start()
    {
        vNums = new Text[vNumber];
        hNums = new Text[hNumber];

        // Create and position the text objects.
        for (int i = 0; i < vNumber; i++)
        {
            GameObject newText = Instantiate(textPrefab, transform);
            newText.transform.position = Vector3.zero;
            vNums[i] = newText.GetComponent<Text>();
        }
        for (int i = 0; i < hNumber; i++)
        {
            GameObject newText = Instantiate(textPrefab, transform);
            newText.transform.position = Vector3.zero;
            hNums[i] = newText.GetComponent<Text>();
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
        float offSet = offSetC * cam.orthographicSize;

        int count = 0;
        float step = (2 * vRange) / vNumber;
        float A = (int)(camX / step) * step;
        float B = A - (int)(vRange / step) * step;
        for (float x = B; count < vNumber; x += step)
        {
            vNums[count].text = (x).ToString();
            vNums[count].transform.position = new Vector3(x - step/5, -step/5);

            count++;
        }

        count = 0;
        step = (2 * hRange) / hNumber;
        A = (int)(camY / step) * step;
        B = A - (int)(hRange / step) * step;
        for (float y = B; count < hNumber; y += step)
        {
            hNums[count].text = (y).ToString();
            hNums[count].transform.position = new Vector3(-step / 5, y - step / 5);

            count++;
        }
    }
}
