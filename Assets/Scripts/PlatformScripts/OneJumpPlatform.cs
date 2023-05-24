using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneJumpPlatform : Platform
{
    public override void Activation()
    {
        Destroy(gameObject);
    }
}
