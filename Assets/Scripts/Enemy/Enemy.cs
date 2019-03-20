using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int damage;

    public float speed;
    public float offsetMovement;
    public float offsetAttack;

    public float rayToPlayerDetection;
    public float rayToAtkPlayer;
    public float offsetToRaycast;

    public float time;
    public float atkRate;
    public float nextAtk;

    public float rightMovementLimit;
    public float leftMovementLimit;
    public float rightAtkLimit;
    public float leftAtkLimit;
    protected float rightLimit;
    protected float leftLimit;

    public bool isPatrolEnemy;
    public bool isFacingRight = true;
    public bool isPlayerDetected = false;

    public Transform origin;
    public Rigidbody2D rb2d;
    protected Vector2 startPoint;
    public HealthController healthController;
    protected Animator anim;
    public GameObject player;

    protected virtual void Start()
    {
        this.rightMovementLimit = this.origin.position.x + this.offsetMovement;
        this.leftMovementLimit = this.origin.position.x - this.offsetMovement;
        this.rightAtkLimit = this.origin.position.x + this.offsetAttack;
        this.leftAtkLimit = this.origin.position.x - this.offsetAttack;
    }

    protected float GetDirection()
    {
        float rightLimit;
        float leftLimit;
        if (!this.isPatrolEnemy)
        {
            rightLimit = float.PositiveInfinity;
            leftLimit = float.NegativeInfinity;
        }
        else
        {
            rightLimit = this.rightMovementLimit;
            leftLimit = this.leftMovementLimit;
            if (this.isPlayerDetected)
            {
                rightLimit = this.rightAtkLimit;
                leftLimit = this.leftAtkLimit;
            }
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

    protected void ResetPosition()
    {
        if (this.transform.position.x > rightMovementLimit || this.transform.position.x < leftMovementLimit)
        {
            Debug.Log("Reset Position");
            Vector3.MoveTowards(this.transform.position, this.origin.position, this.speed);
        }
    }

    protected void LineOfDetection()
    {
        float direction = this.GetDirection();
        this.startPoint = new Vector2(this.transform.position.x + (offsetToRaycast * direction), this.transform.position.y);
        RaycastHit2D hitUpfront = Physics2D.Raycast(this.startPoint, Vector2.right * direction, rayToPlayerDetection);
        RaycastHit2D hitDiagonalUp = Physics2D.Raycast(this.startPoint, new Vector2(direction, 1), rayToPlayerDetection);
        Debug.DrawRay(startPoint, Vector2.right * direction, Color.cyan);
        Debug.DrawRay(startPoint, new Vector2(direction, 1), Color.magenta);
        RaycastHit2D hit = hitUpfront ? hitUpfront : hitDiagonalUp;
        
        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                this.player = hit.transform.gameObject;
                this.isPlayerDetected = true;
            }
        }
        else
        {
            this.player = null;
            this.isPlayerDetected = false;
        }
    }

    protected abstract void Movement();

    protected bool CanAttack()
    {
        return Time.time > this.nextAtk;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this.isPatrolEnemy)
            return;

        bool canAttack = this.CanAttack();
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            float xDir = this.transform.position.x > collision.gameObject.transform.position.x ? -1 : 1;
            collision.gameObject.GetComponent<HealthController>().TakeDamage(damage);
            collision.gameObject.GetComponent<CharacterController>().Recoil(xDir);
            this.nextAtk = Time.time + this.atkRate;
            Debug.Log("Attack by ENTER in contact");
        }
    }
}
