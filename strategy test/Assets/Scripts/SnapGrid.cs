﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapGrid : MonoBehaviour
{
    public bool drawGizmos = true;

    [SerializeField]
    private float size = 1f;
    
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.cyan;
        for (float x = transform.position.x - transform.lossyScale.x*5 ; x < transform.position.x + transform.lossyScale.x*5; x += size)
        //for (float x = 0; x < 100; x += size)
        {
            for (float z = transform.position.z - transform.lossyScale.z*5; z < transform.position.z + transform.lossyScale.z*5; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }                
        }
    }
}
