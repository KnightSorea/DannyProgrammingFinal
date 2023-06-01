using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
  public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("StartButton"))
        {
            StartButton();
        }

        if (Input.GetButtonDown("QuitButton"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
