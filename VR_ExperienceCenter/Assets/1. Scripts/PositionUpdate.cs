using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdate : MonoBehaviour
{
    [SerializeField] Transform targetObject;


    void Update()
    {
        transform.position = new Vector3(transform.position.x, targetObject.position.y, transform.position.z);
    }
}
