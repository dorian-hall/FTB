using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera_Controller))]
[ExecuteInEditMode]
public class Camera_Visualisation : MonoBehaviour
{ 
    [SerializeField] Vector2 BoundingBoxSize = Vector2.one;
    [SerializeField] bool SeeBoundingBox;
    Vector3 WorldPosition;
    Camera_Controller controller;
    private void Start()
    {
        controller = GetComponent<Camera_Controller>();
        WorldPosition = transform.position;
    }
    private void OnDrawGizmos()
    {

        if (!SeeBoundingBox) return;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(transform.InverseTransformPoint(WorldPosition), new Vector3(BoundingBoxSize.x, BoundingBoxSize.y, 0));

    }
}
