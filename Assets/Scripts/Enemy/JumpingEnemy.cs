using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    public float jumpForce;
    public float jumpRate;
    public float jumpAttackRate;
    public float nextJump;

    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        base.LineOfDetection();
        this.Movement();
    }

    protected override void Movement()
    {
        if (Time.time > this.nextJump)
        {
            if (base.player)
            {
                this.nextJump = Time.time + this.jumpAttackRate;
            }
            else
            {
                this.nextJump = Time.time + this.jumpRate;
            }
            this.rb2d.AddForce(new Vector2(base.GetDirection(), this.jumpForce), ForceMode2D.Impulse);
        }
    }
}
