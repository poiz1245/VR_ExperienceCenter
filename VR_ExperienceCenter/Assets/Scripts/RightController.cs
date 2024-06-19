using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RightController : MonoBehaviour
{
    [SerializeField] float jumpPower;
    [SerializeField] InputActionReference grabButton;
    [SerializeField] InputActionReference primaryButton;
    [SerializeField] GameObject player;

    public Vector3 rightControllerPosition;


    private void Start()
    {
        grabButton.action.performed += ObjectGrab;
        primaryButton.action.performed += Jump;
    }
    public void ObjectGrab(InputAction.CallbackContext obj)
    {
        rightControllerPosition = transform.position;
    }
    public void Jump(InputAction.CallbackContext obj)
    {
        player.transform.Translate(0f, jumpPower, 0f);
    }
}
