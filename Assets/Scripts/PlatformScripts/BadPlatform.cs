using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPlatform : Platform
{
    private GameManager gm;
    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public override void Activation()
    {
        gm.GameOver();
    }
}
