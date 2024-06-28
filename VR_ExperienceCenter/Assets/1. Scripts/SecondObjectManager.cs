using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondObjectManager : MonoBehaviour
{
    [SerializeField] PortalToSecondStage portal;

    [SerializeField] GameObject[] tvObjects;
    [SerializeField] Transform[] tvObjectPoints;

    void Update()
    {
        if(portal.secondStageStart) 
        {
            for (int i = 0; i < tvObjects.Length; i++)
            {
                if (i == 3) 
                {
                    tvObjects[i].transform.position = tvObjectPoints[i].transform.position;
                }
                else
                {
                    tvObjects[i].gameObject.SetActive(true);
                    tvObjects[i].transform.position = tvObjectPoints[i].transform.position;
                }
            }
        }
    }
}
