using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenterRayCast : MonoBehaviour
{
    public Camera mainCamera;
    int layerMask;
    LayerMask layerToIgnore;
    Vector3 startPoint;

    public Vector3 hitPoint { get; private set; }
    public Vector3 normal { get; private set; }

    void Start()
    {
        layerToIgnore = LayerMask.GetMask("Target");
        layerMask = ~(1 << layerToIgnore);
        
    }

    void Update()
    {
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Vector3 worldCenter = mainCamera.ViewportPointToRay(viewportCenter).origin;

        startPoint = worldCenter;

        bool collider = Physics.Raycast(startPoint, transform.forward, out RaycastHit hitInfo, float.PositiveInfinity, ~(layerMask));

        if (collider)
        {
            hitPoint = hitInfo.point;
            normal = hitInfo.normal;
        }
    }
}
