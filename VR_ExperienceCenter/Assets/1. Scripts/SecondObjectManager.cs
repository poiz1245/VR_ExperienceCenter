using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondObjectManager : MonoBehaviour
{
    [SerializeField] Portal portal;
    [SerializeField] GameObject fog;
    [SerializeField] GameObject tv;
    [SerializeField] GameObject tvDisplay;
    [SerializeField] GameObject tvTable;
    [SerializeField] GameObject thirdStageObject;

    void Update()
    {
        if(portal.secondStageStart) 
        {
            //fog.SetActive(true);
            tv.SetActive(true);
            tvTable.SetActive(true);
            tvDisplay.SetActive(true);
            thirdStageObject.SetActive(true);
        }
    }
}
