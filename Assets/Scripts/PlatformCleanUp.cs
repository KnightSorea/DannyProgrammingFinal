using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCleanUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("LastPlatforms") || collision.gameObject.CompareTag("Death") || collision.gameObject.CompareTag("Spring"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.GameOver();
        }
    }
}
