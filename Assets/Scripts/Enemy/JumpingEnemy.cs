using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    //public float jumpForce;
    public float jumpRate;
    public float jumpAttackRate;
    public float nextJump;

    //Test
    public float maxJumpHeight = 0.75f;
    public float groundHeight;
    public Vector3 groundPos;
    public float fallSpeed = 12.0f;
    public bool canJump = false;
    public bool isGrounded = true;

    protected override void Start()
    {
        base.Start();
        this.groundPos = this.transform.position;
        this.groundHeight = this.transform.position.y;
        this.maxJumpHeight = this.transform.position.y + this.maxJumpHeight;

    }

    void Update()
    {
        base.time = Time.time;
        base.LineOfDetection();
        this.IsGrounded();
        this.CanJump();
        this.Movement();
    }

    void CanJump()
    {
        this.canJump = Time.time >= this.nextJump;
    }

    void IsGrounded()
    {
        this.isGrounded = transform.position.y <= groundPos.y;
    }

    protected override void Movement()
    {
        //this.rb2d.AddForce(new Vector2(base.GetDirection(), this.jumpForce), ForceMode2D.Impulse);
        float xDir = base.GetDirection();
        Vector2 jump = Vector2.zero;
        if (this.canJump && this.transform.position.y <= this.groundPos.y)
        {
            this.nextJump = Time.time + this.jumpRate;
            jump = new Vector2(xDir * .0625f, .125f);
            base.rb2d.velocity = jump.normalized;
            Debug.Log("Going up");
        }
        else if (transform.position.y > maxJumpHeight)
        {
            jump = new Vector2(xDir * .0625f, -.125f);
            base.rb2d.velocity = jump.normalized;
            this.isGrounded = true;
            Debug.Log("Going down");
        }
        else if (this.isGrounded)
        {
            base.rb2d.velocity = Vector2.zero;
            Debug.Log("Jump stopped");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isGrounded = true;
            Debug.Log("On Collision Enter");
        }
    }
}
