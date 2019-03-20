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
    public float maxJumpHeight = 1.5f;
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
        //if (Time.time > this.nextJump)
        //{
        //    canJump = true;
        //}
        //else
        //{
        //    canJump = false;
        //}
    }

    void IsGrounded()
    {
        this.isGrounded = transform.position.y == groundPos.y;
        //if (transform.position.y == groundPos.y)
        //    isGrounded = true;
        //else
        //    isGrounded = false;
    }

    protected override void Movement()
    {
        //this.rb2d.AddForce(new Vector2(base.GetDirection(), this.jumpForce), ForceMode2D.Impulse);
        float xDir = base.GetDirection();
        if (this.canJump && this.isGrounded)
        {
            this.nextJump = Time.time + this.jumpRate;
            base.rb2d.velocity = new Vector2(xDir * .25f, .5f);
            Debug.Log("Going up");
        }
        else if (transform.position.y > maxJumpHeight)
        {
            base.rb2d.velocity = new Vector2(xDir * .25f, -.5f);
            this.isGrounded = true;
            //this.canJump = true;
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
