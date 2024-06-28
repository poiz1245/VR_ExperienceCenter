using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdStageObjectManager : MonoBehaviour
{
    [SerializeField] PortalToStage3 portal;

    [SerializeField] GameObject tv;

    void Update()
    {
        if (portal.thirdStageStart)
        {
            tv.SetActive(true);
        }
    }
}
