using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public InputActionReference menuButton;
    // Start is called before the first frame update
    void Start()
    {
        menuButton.action.performed += OnClickMenu;
    }

    void OnClickMenu(InputAction.CallbackContext context)
    {

    }
}
