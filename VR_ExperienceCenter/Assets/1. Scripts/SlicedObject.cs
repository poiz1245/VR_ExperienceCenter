using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObject : MonoBehaviour
{
    /*public Material[] baseMaterial;
    public Material[] firstRenderMaterial;
    public float minScale;
    public float maxScale;*/
    public bool isSliced = false;
    CuttingDoorOpen cuttingDoor;

    private void Start()
    {
        cuttingDoor = GameObject.Find("CuttingDoor").GetComponent<CuttingDoorOpen>();
    }

    private void Update()
    {
        if(isSliced)
        {
            cuttingDoor.barrierCutted = true;
        }
    }
}
