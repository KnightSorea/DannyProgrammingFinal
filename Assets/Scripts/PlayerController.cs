using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // floats
    private float horizontalInput;
    public float playerHighestYPos;
    public float moveSpeed;
    public float jumpForce;
    public float leftBoundery;
    public float rightBoundery;

    // bools
    public bool isJumping;

    // Access to other things (rigidbody, gameobject, etc)
    private GameManager gm;
    public Rigidbody2D rb;
    public Animator anim;
    public AudioSource AS;
    public AudioClip AC;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponentInChildren<Animator>();
        AS = GetComponent<AudioSource>();
    }

    public void playJumpSound()
    {
        AS.PlayOneShot(AC);
    }

    // Update is called once per frame

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;

    }
    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontalInput;
        rb.velocity = velocity;
        if (transform.position.y > playerHighestYPos)
        {
            playerHighestYPos = transform.position.y;
        }

        if (transform.position.x < leftBoundery)
        {
            transform.position = new Vector2(rightBoundery, transform.position.y);
        }

        if (transform.position.x > rightBoundery)
        {
            transform.position = new Vector2(leftBoundery, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Ground") && rb.velocity.y <=0 || collision.gameObject.CompareTag("LastPlatforms") && rb.velocity.y <= 0)
         {
            isJumping = true;
            anim.SetBool("isJumping", isJumping);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playJumpSound();
            StartCoroutine(resetJumpAnim());
         }

         if (collision.gameObject.CompareTag("LastPlatforms"))
         {
            foreach (GameObject Platform in GameObject.FindGameObjectsWithTag("LastPlatforms")) 
            {
                Platform.tag = "Ground";
            }
            gm.spawnNextPlatForms();
        }
    }
    public IEnumerator resetJumpAnim()
    {
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
        anim.SetBool("isJumping", isJumping);
    }
}


