using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : Platform
{
    private PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public override void Activation()
    {
        player.isJumping = true;
        player.anim.SetBool("isJumping", player.isJumping);
        player.rb.velocity = Vector2.zero;
        player.rb.AddForce(Vector2.up * player.jumpForce * 2, ForceMode2D.Impulse);
        player.playJumpSound();
        player.StartCoroutine(player.resetJumpAnim());
    }
}
