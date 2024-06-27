using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdate : MonoBehaviour
{
    [SerializeField] Transform targetObject;

    float previousTargetY;
    float deltaY;


    private void Awake()
    {
        previousTargetY = targetObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        deltaY = targetObject.position.y - previousTargetY;
        previousTargetY = targetObject.position.y;

        transform.Translate(Vector3.up *deltaY);
        
    }
}
