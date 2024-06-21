using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilBoxRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 1f * rotationSpeed, 0f));
    }
}
