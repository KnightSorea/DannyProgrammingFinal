using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCleanUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("LastPlatforms") || collision.gameObject.CompareTag("Death"))
        {
            Destroy(collision.gameObject);
        }
    }

   /* public void saveScore()
    {
        PlayerPrefs.SetFloat("HighScore", 2);
        PlayerPrefs.GetFloat("HighScore");
    } */
}
