using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isMovingPlatform;
    private bool moveRight;
    private int chanceForMovingPlatform;
    private int randDirection;
    private void Start()
    {
        randDirection = Random.Range(1, 3);
        if (randDirection == 1)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        chanceForMovingPlatform = Random.Range(1, 101);
        if (chanceForMovingPlatform < GameManager.chanceToBeat)
        {
            isMovingPlatform = true;
        }
        else
        {
            isMovingPlatform = false;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= -7.5f && isMovingPlatform && !moveRight)
        {
            moveRight = true;
        }
        else if (transform.position.x >= 7.5f && isMovingPlatform && moveRight)
        {
            moveRight = false;
        }
        
        if (moveRight && isMovingPlatform)
        {
            transform.Translate(Vector2.right * GameManager.platformMoveSpeed * Time.deltaTime);
        }
        else if (!moveRight && isMovingPlatform)
        {
            transform.Translate(Vector2.left * GameManager.platformMoveSpeed * Time.deltaTime);
        }
    }
    void Despawn()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.relativeVelocity.y <= 0)
        {
            Activation();
            Invoke(nameof(Despawn), 30f);
        }
    }

    public virtual void Activation()
    {

    }
}
