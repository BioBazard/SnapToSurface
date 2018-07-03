using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SnapToSurface : MonoBehaviour {

    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] vertices;

    private bool isInit = false;

    [Header("Surface")]
    public LayerMask surfaceLayerMask;

    public float surfaceOffset = 0.1f;

    [Header("Ray")]
    public bool showRayDirection = true;

    [System.Serializable]
    public struct Direction
    {
        [Range(-1.0f,1.0f)]
        public float x;
        [Range(-1.0f, 1.0f)]
        public float y;
        [Range(-1.0f, 1.0f)]
        public float z;
    }
    public Direction rayDirection;

    public void Snap()
    {
        // Replace the vertices on the circuit
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = transform.TransformPoint(vertices[i]);

            RaycastHit hit;
            if (Physics.Raycast(v, GetRayDirection(), out hit, Mathf.Infinity, surfaceLayerMask))
            {
                Debug.DrawRay(v, GetRayDirection() * hit.distance, Color.yellow);

                vertices[i] = transform.InverseTransformPoint(hit.point + hit.normal * surfaceOffset);
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (showRayDirection)
            Debug.DrawRay(transform.position, GetRayDirection() * 100, Color.red);
    }

    private Vector3 GetRayDirection()
    {

        return new Vector3(rayDirection.x, rayDirection.y, rayDirection.z);
    }
}
