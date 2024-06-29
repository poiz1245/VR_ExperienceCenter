using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLayer : MonoBehaviour
{
    [SerializeField] GameObject[] keepLayerObjects;

    PortalToStage3 portal;

    bool portalPath = false;
    private void Start()
    {
        portal = GetComponent<PortalToStage3>();
    }
    void Update()
    {
        if (!portalPath && portal.thirdStageStart)
        {
            portalPath = true;

            for (int i = 0; i < keepLayerObjects.Length; i++)
            {
                keepLayerObjects[i].layer = LayerMask.NameToLayer("AlwaysVisible");
            }
        }


    }
}
