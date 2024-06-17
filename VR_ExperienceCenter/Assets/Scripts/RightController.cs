using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RightController : MonoBehaviour
{
    [SerializeField] InputActionReference grab;

    public GameObject rightController;
    public Vector3 rightControllerPosition;

    private void Start()
    {
        grab.action.performed += ObjectGrab;

    }
    public void ObjectGrab(InputAction.CallbackContext obj)
    {
        rightControllerPosition = rightController.transform.position;
    }
}
