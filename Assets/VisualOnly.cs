﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CodeMonkey.Utils;

public class VisualOnly : MonoBehaviour
{
    // Start is called before the first frame update
    public float viewAngle = 90f;
    public float distance = 10f;
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        float fov = viewAngle;
        Vector3 origin = Vector3.zero;
        int rayCount = 10;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = distance;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;


        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i<= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        //vertices[0] = Vector2.zero;
        //vertices[1] = new Vector3(50, 0);
        //vertices[2] = new Vector3(0, -50);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        //mesh.


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad - Mathf.PI/4),0, Mathf.Sin(angleRad - Mathf.PI / 4));
    }
}
