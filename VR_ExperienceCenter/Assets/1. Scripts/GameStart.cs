using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] float minDistance;
    [SerializeField] Vector3 openRotation;
    [SerializeField] Vector3 closeRotation;

    XRSimpleInteractable simpleInteractable;

    private void Awake()
    {
        simpleInteractable = GetComponentInChildren<XRSimpleInteractable>();
    }
    private void Start()
    {
        triggerButton.action.performed += TriggerButtonClicked;
    }

    void TriggerButtonClicked(InputAction.CallbackContext context)
    {
        if (simpleInteractable.isHovered)
        {
            DoorInteraction();
        }
    }
    public void DoorInteraction()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        int duration = 2;

        if (dist < minDistance)
        {
            gameObject.transform.DORotate(openRotation, duration).SetEase(Ease.InQuad);
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Main");
    }
}
