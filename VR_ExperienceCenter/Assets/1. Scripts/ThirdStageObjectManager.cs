using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdStageObjectManager : MonoBehaviour
{
    [SerializeField] PortalToStage3 portal;

    [SerializeField] GameObject tv;
    [SerializeField] GameObject[] hindObject;

    void Update()
    {
        if (portal.thirdStageStart)
        {
            tv.SetActive(true);
            for(int i = 0; i < hindObject.Length; i++)
            {
                hindObject[i].SetActive(true);
            }
        }
    }
}
