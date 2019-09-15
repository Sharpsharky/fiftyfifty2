using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : FlyingEnemy
{

    public void FixedUpdate()
    {
        if (ShouldDisapear()) {
            ChopperFactory.ReturnChopper( this );
        }
    }

    public override void StartMoving()
    {
        rb.velocity = new Vector2( Direction, 0 ) * MoveSpeed;
    }

    public override void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }
}
