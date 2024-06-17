using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRayCast : MonoBehaviour
{
    int layerMask;
    LayerMask layerToIgnore;
    public Vector3 hitPoint { get; private set; }
    public Vector3 normal { get; private set; }

    void Start()
    {
        layerToIgnore = LayerMask.GetMask("Target");
        layerMask = ~(1 << layerToIgnore);
    }

    void Update()
    {
        bool collider = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, float.PositiveInfinity, ~(layerMask));

        if (collider)
        {
            hitPoint = hitInfo.point;
            normal = hitInfo.normal;
        }
    }
}
