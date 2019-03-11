using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float offSet;

    public float jumpForce;
    public float jumpRate;
    public float nextJump;

    public Transform origin;
    private Rigidbody2D rb2d;

    void Start()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    float IsMovingRight()
    {
        float rightLimit = this.origin.position.x + this.offSet;
        float leftLimit = this.origin.position.x - this.offSet;
        if (this.transform.position.x > rightLimit)
        {
            this.speed *= -1;
        }
        else if (this.transform.position.x < leftLimit)
        {
            this.speed *= -1;
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
        float rightLimit = this.origin.position.x + this.offSet;
        float leftLimit = this.origin.position.x - this.offSet;
        if(this.transform.position.x > rightLimit || this.transform.position.x < leftLimit)
        {
            Vector3.MoveTowards(this.transform.position, this.origin.position, 1f);
        }
    }
}