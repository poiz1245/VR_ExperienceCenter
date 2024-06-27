using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogActive : MonoBehaviour
{
    [SerializeField] Portal portal;

    void Update()
    {
        if(portal.secondStageStart) {gameObject.SetActive(true);}
    }
}
