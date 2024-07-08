using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(24f);
        SceneManager.LoadScene("Title");
    }
}
