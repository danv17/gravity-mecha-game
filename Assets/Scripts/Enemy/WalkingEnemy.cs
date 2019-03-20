using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        base.LineOfDetection();
        base.CanAttack();
        this.Movement();
    }

    protected override void Movement()
    {
        base.rb2d.velocity = base.transform.right * base.GetDirection();
        base.ResetPosition();
    }
}
