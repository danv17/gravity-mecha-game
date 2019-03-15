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

    public float jumpHeight;
    public float jumpForce;
    public float jumpRate;
    public float nextJump;

    public float magnitude;
    public float frequency;

    public bool canDetectPlayer = true;
    public bool isFacingRight = true;
    public bool isAttacking = false;

    public Vector3 position;
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
        this.position = this.transform.position;
    }

    float GetSpeed()
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
        this.rb2d.velocity = this.transform.right * this.GetSpeed();
        this.ResetPosition();
    }

    public void JumpPatrol()
    {
        if (Time.time > this.nextJump)
        {
            this.nextJump = Time.time + this.jumpRate;
            this.rb2d.AddForce(new Vector2(this.GetSpeed(), this.jumpHeight), ForceMode2D.Impulse);
        }
        this.ResetPosition();
    }

    public void SinusoidalMovement()
    {
        /* RigidBody2D MUST be Kinematic */
        this.position += this.transform.right * Time.deltaTime * this.GetSpeed();
        this.transform.position = this.position + this.transform.up * Mathf.Sin(Time.time * this.frequency) * this.magnitude;
    }

    void MeleeAttack()
    {
        /* Do melee attack */
        Debug.Log("Melee Attack");
    }

    public void JumpAttack()
    {
        /* Do jump attack */
        Debug.Log("Jump Attack");
    }

    public void FlyingAttack()
    {
        /* Do flying attack */
        Debug.Log("Flying Attack");
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