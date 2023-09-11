using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MeshGrid : MonoBehaviour
{
    public Material mat;
    private MeshFilter filter;
    private MeshRenderer render;

    public Camera cam;
    private Transform camT;
    
    private int vNumber = 22;
    private float vRangeC = 22f;
    public float vWidthC = 0.004f;

    private int hNumber = 12;
    private float hRangeC = 12f;
    public float hWidthC = 0.004f;

    private void Awake()
    {
        camT = cam.transform;
    }

    private void Start()
    {
        filter = GetComponent<MeshFilter>();

        mat.color = new Color(0, 0, 0, 0);
        render = GetComponent<MeshRenderer>();
        render.material = mat;
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
        float vWidth = vWidthC * cam.orthographicSize;
        float hWidth = hWidthC * cam.orthographicSize;

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4 * (vNumber + hNumber)];

        int[] triangles = new int[6 * (vNumber + hNumber)];

        int count = 0;
        float step = (2 * vRange) / vNumber;
        float A = (int)(camX / step) * step;
        float B = A - (int)(vRange / step) * step;
        for (float x = B; count < vNumber; x += step)
        {
            // depends on x:
            vertices[0 + 4 * count] = new Vector3(x - (vWidth / 2), camY - hRange); // down-left
            vertices[1 + 4 * count] = new Vector3(x - (vWidth / 2), camY + hRange); // up-left
            vertices[2 + 4 * count] = new Vector3(x + (vWidth / 2), camY - hRange); // down-right
            vertices[3 + 4 * count] = new Vector3(x + (vWidth / 2), camY + hRange); // up-right

            // doesn't depend on x:
            triangles[0 + 6 * count] = 0 + 4 * count;
            triangles[1 + 6 * count] = 1 + 4 * count;
            triangles[2 + 6 * count] = 2 + 4 * count;
            triangles[3 + 6 * count] = 2 + 4 * count;
            triangles[4 + 6 * count] = 1 + 4 * count;
            triangles[5 + 6 * count] = 3 + 4 * count;
            // 0, 1, 2; 2, 1, 3

            count++;
        }

        step = (2 * hRange) / hNumber;
        A = (int)(camY / step) * step;
        B = A - (int)(hRange / step) * step;
        for (float y = B; count < vNumber + hNumber; y += step)
        {
            // depends on x:
            vertices[0 + 4 * count] = new Vector3(camX - vRange, y - (vWidth / 2)); // down-left
            vertices[1 + 4 * count] = new Vector3(camX - vRange, y + (vWidth / 2)); // up-left
            vertices[2 + 4 * count] = new Vector3(camX + vRange, y - (vWidth / 2)); // down-right
            vertices[3 + 4 * count] = new Vector3(camX + vRange, y + (vWidth / 2)); // up-right

            // doesn't depend on x:
            triangles[0 + 6 * count] = 0 + 4 * count;
            triangles[1 + 6 * count] = 1 + 4 * count;
            triangles[2 + 6 * count] = 2 + 4 * count;
            triangles[3 + 6 * count] = 2 + 4 * count;
            triangles[4 + 6 * count] = 1 + 4 * count;
            triangles[5 + 6 * count] = 3 + 4 * count;
            // 0, 1, 2; 2, 1, 3

            count++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        filter.mesh = mesh;
    }
}
