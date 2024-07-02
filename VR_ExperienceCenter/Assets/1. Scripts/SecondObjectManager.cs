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
        if (portal.secondStageStart)
        {
            for (int i = 0; i < tvObjects.Length; i++)
            {
                tvObjects[i].gameObject.SetActive(true);
                /* if (i == 3) //virtualroom은 위치만 옮기고 꺼져 있어야함
                 {
                     //tvObjects[i].transform.position = tvObjectPoints[i].transform.position;
                 }
                 else
                 {
                     //tvObjects[i].transform.position = tvObjectPoints[i].transform.position;
                 }*/

            }
            portal.secondStageStart = false;
        }
    }
}
