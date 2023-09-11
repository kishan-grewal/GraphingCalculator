using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AxesDrawer : MonoBehaviour
{
    public Material mat;
    private MeshFilter filter;
    private MeshRenderer render;

    public Camera cam;
    private Transform camT;

    private float xRangeC = 22f;
    public float xWidthC = 0.02f;

    private float yRangeC = 12f;
    public float yWidthC = 0.02f;

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


        float camX = camT.position.x;
        float camY = camT.position.y;

        float xRange = xRangeC * cam.orthographicSize;
        float yRange = yRangeC * cam.orthographicSize;
        float xWidth = xWidthC * cam.orthographicSize;
        float yWidth = yWidthC * cam.orthographicSize;

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[8];

        int[] triangles = new int[12];


        // XAXIS
        // depends on x:
        vertices[0] = new Vector3(camX - xRange, 0 - 0.5f * xWidth); // down-left
        vertices[1] = new Vector3(camX - xRange, 0 + 0.5f * xWidth); // up-left
        vertices[2] = new Vector3(camX + xRange, 0 - 0.5f * xWidth); // down-right
        vertices[3] = new Vector3(camX + xRange, 0 + 0.5f * xWidth); // up-right

        // doesn't depend on x:
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;
        // 0, 1, 2; 2, 1, 3


        // YAXIS
        // depends on x:
        vertices[0 + 4] = new Vector3(0 - 0.5f * yWidth, camY - yRange); // down-left
        vertices[1 + 4] = new Vector3(0 - 0.5f * yWidth, camY + yRange); // up-left
        vertices[2 + 4] = new Vector3(0 + 0.5f * yWidth, camY - yRange); // down-right
        vertices[3 + 4] = new Vector3(0 + 0.5f * yWidth, camY + yRange); // up-right

        // doesn't depend on x:
        triangles[0 + 6] = 0 + 4;
        triangles[1 + 6] = 1 + 4;
        triangles[2 + 6] = 2 + 4;
        triangles[3 + 6] = 2 + 4;
        triangles[4 + 6] = 1 + 4;
        triangles[5 + 6] = 3 + 4;
        // 0, 1, 2; 2, 1, 3

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        filter.mesh = mesh;
    }
}
