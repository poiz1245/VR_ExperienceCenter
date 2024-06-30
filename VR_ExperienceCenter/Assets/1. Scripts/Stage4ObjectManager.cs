using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4ObjectManager : MonoBehaviour
{
    [SerializeField] DoorToKitchen doorToKitchen;
    [SerializeField] GameObject entranceWall;
    [SerializeField] GameObject dreamWay;

    void Start()
    {
        entranceWall.SetActive(false);
        dreamWay.SetActive(true);
    }

    void Update()
    {
        if (doorToKitchen.path)
        {
            entranceWall.SetActive (true);
            dreamWay.SetActive (false);
        }
        
    }
}
