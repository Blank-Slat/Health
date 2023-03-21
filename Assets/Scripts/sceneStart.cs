using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneStart : MonoBehaviour
{
    private bool canStart = false;

    private void Start()
    {
        Invoke("startDelay", 1);
    }
    private void Update()
    {
        if((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && canStart)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void startDelay()
    {
        canStart = true;
    }
}
