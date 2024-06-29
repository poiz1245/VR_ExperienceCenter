using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RightController : MonoBehaviour
{
    private float gravity = -9.81f; // 중력 가속도
    private float jumpForce = 5f; // 점프 힘
    private float verticalVelocity = 0f; // 수직 속도

    [SerializeField] InputActionReference grabButton;
    [SerializeField] InputActionReference primaryButton;

    [SerializeField] GameObject player;
    [SerializeField] CharacterController characterController;
    [SerializeField] Rigidbody rigid;

    public Vector3 rightControllerPosition;


    private void Start()
    {
        grabButton.action.performed += ObjectGrab;
        primaryButton.action.performed += OnJump;
    }
    private void Update()
    {
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 movement = new Vector3(0f, verticalVelocity, 0f);
        characterController.Move(movement * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // 점프 로직
        if (characterController.isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }
    public void ObjectGrab(InputAction.CallbackContext obj) // 큐브 잡고 돌릴 때 사용
    {
        rightControllerPosition = transform.position;
    }
}
