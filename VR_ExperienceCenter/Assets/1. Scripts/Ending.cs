using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene("Title");
    }
}
