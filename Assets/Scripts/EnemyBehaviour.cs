using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float offSet;

    public float rightLimit;
    public float leftLimit;

    public float jumpForce;
    public float jumpRate;
    public float nextJump;

    public bool canDetectPlayer = true;
    public bool isFacingRight = true;

    public Transform origin;
    private Rigidbody2D rb2d;

    void Awake()
    {
        this.rightLimit = this.origin.position.x + this.offSet;
        this.leftLimit = this.origin.position.x - this.offSet;
    }
    void Start()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    float IsMovingRight()
    {
        //float rightLimit = this.origin.position.x + this.offSet;
        //float leftLimit = this.origin.position.x - this.offSet;
        if (this.transform.position.x > rightLimit && this.isFacingRight)
        {
            this.speed *= -1;
            this.isFacingRight = false;
        }
        else if (this.transform.position.x < leftLimit && !this.isFacingRight)
        {
            this.speed *= -1;
            this.isFacingRight = true;
        }
        return this.speed;
    }

    public void WalkPatrol() 
    {
        this.rb2d.velocity = this.transform.right * this.IsMovingRight();
        this.ResetPosition();
    }

    public void JumpPatrol()
    {
        this.Jump();
    }

    void Jump()
    {
        if(Time.time > this.nextJump)
        {
            this.nextJump = Time.time + this.jumpRate;
            this.rb2d.AddForce(new Vector2(this.IsMovingRight(), this.jumpForce), ForceMode2D.Impulse);
        }
    }

    void ResetPosition()
    {
        //float rightLimit = this.origin.position.x + this.offSet;
        //float leftLimit = this.origin.position.x - this.offSet;
        if(this.transform.position.x > rightLimit || this.transform.position.x < leftLimit)
        {
            Vector3.MoveTowards(this.transform.position, this.origin.position, this.speed);
        }
    }

    public bool CanDetectPlayer()
    {
        //float rightLimit = this.origin.position.x + this.offSet;
        //float leftLimit = this.origin.position.x - this.offSet;
        if (this.transform.position.x == rightLimit || this.transform.position.x == leftLimit)
        {
            this.canDetectPlayer = false;
        }
        return this.canDetectPlayer;
    }
}