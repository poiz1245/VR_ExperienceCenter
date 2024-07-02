using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public InputActionReference menu;
    [SerializeField] GameObject menuButton;

    bool menuActive = false;
    public enum SceneName
    {
        Main,
        Stage4,
        Stage5,
        Chase
    }

    public SceneName sceneName;

    void Start()
    {
        menu.action.performed += OnClickMenu;
    }

    void OnClickMenu(InputAction.CallbackContext context)
    {
        if (!menuActive)
        {
            menuActive = true;
            menuButton.gameObject.SetActive(true);
        }
        else
        {
            menuActive = false;
            menuButton.gameObject.SetActive(false);
        }
    }

    public void OnClickMenuButton()
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
