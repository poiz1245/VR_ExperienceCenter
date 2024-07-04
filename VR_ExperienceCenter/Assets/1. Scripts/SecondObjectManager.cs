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
                /* if (i == 3) //virtualroom�� ��ġ�� �ű�� ���� �־����
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
