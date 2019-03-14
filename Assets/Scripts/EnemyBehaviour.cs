using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float offsetMovement;
    public float offsetAttack;

    public float rightMovementLimit;
    public float leftMovementLimit;

    public float rightAtkLimit;
    public float leftAtkLimit;

    public float jumpForce;
    public float jumpRate;
    public float nextJump;

    public bool canDetectPlayer = true;
    public bool isFacingRight = true;
    public bool isAttacking = false;

    public Transform origin;
    private Rigidbody2D rb2d;

    void Awake()
    {
        this.rightMovementLimit = this.origin.position.x + this.offsetMovement;
        this.leftMovementLimit = this.origin.position.x - this.offsetMovement;
        this.rightAtkLimit = this.origin.position.x + this.offsetAttack;
        this.leftAtkLimit = this.origin.position.x - this.offsetAttack;
    }
    void Start()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    float IsMovingRight()
    {
        float rightLimit = this.rightMovementLimit;
        float leftLimit = this.leftMovementLimit;
        if (this.isAttacking)
        {
            rightLimit = this.rightAtkLimit;
            leftLimit = this.leftAtkLimit;
        }
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
        if (Time.time > this.nextJump)
        {
            this.nextJump = Time.time + this.jumpRate;
            this.rb2d.AddForce(new Vector2(this.IsMovingRight(), this.jumpForce), ForceMode2D.Impulse);
        }
        this.ResetPosition();
    }

    void MoveToAttack()
    {

    }

    void ResetPosition()
    {
        if(this.transform.position.x > rightMovementLimit || this.transform.position.x < leftMovementLimit)
        {
            Debug.Log("Reset Position");
            Vector3.MoveTowards(this.transform.position, this.origin.position, this.speed);
        }
    }

    public bool CanDetectPlayer()
    {
        /* En caso de que exista una condición que impida detectar al jugador */
        return this.canDetectPlayer;
    }
}