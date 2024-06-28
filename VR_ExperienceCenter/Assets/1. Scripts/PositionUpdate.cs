using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdate : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] PortalToSecondStage portal;

    void Update()
    {
        if (portal.secondStageStart)
        {
            return;
        }
        //¸Ê ³Ñ¾î¿Â ´ÙÀ½¿¡´Â ¸ØÃç¾ßÇÔ
        transform.position = new Vector3(transform.position.x, targetObject.position.y, transform.position.z);
    }
}
