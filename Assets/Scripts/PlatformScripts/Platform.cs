using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Despawn), 75f);
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
        }
    }

    public virtual void Activation()
    {

    }
}
