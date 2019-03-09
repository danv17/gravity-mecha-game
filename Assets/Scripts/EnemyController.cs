using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int lifePoints;
    public float speed;
    public int damage;
    public float horizontalOffset;
    public float radius;
    public float attackRadius;
    public float offset;
    private Rigidbody2D rb;
    public Transform origin;
    private bool isAttacking;
    private Vector2 startPoint;
    private GameObject player;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        this.EnemyDetection();
        StartCoroutine("Move");
        //if (this.transform.position.x != this.origin.position.x)
        //{
        //    this.ResetPosition();
        //}
    }

    IEnumerator Move()
    {
        this.isAttacking = false;
        if (this.transform.position.x > this.origin.position.x + this.horizontalOffset)
        {
            speed *= -1;
        }
        else if (this.transform.position.x < this.origin.position.x - this.horizontalOffset)
        {
            speed *= -1;
        }
        this.rb.velocity = this.transform.right * speed;
        yield return null;
    }

    //IEnumerator Attack()
    //{
    //    this.isAttacking = true;
    //    if (this.transform.position.x < this.origin.position.x + this.attackRadius)
    //    {
    //        speed *= -1;
    //    }
    //    else if (this.transform.position.x > this.origin.position.x - this.attackRadius)
    //    {
    //        speed *= -1;
    //    }
    //    this.rb.velocity = this.transform.right * speed;
    //    if (Mathf.Abs(this.transform.position.x - this.player.transform.position.x) <= 1)
    //    {
    //        this.MeleeAttack();
    //    }
    //    yield return new WaitForSeconds(1.0f);
    //}

    //void MeleeAttack()
    //{
    //    this.player.GetComponent<CharacterController>().TakeDamage(this.damage);
    //}

    void EnemyDetection()
    {
        float direction = this.speed > 0 ? 1 : -1;
        startPoint = new Vector2(this.transform.position.x + (offset * direction), this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.right * direction, radius);

        if (hit)
        {
            Debug.DrawRay(startPoint, Vector3.right * direction, Color.yellow);
            Debug.Log(hit.transform.name);
        }
    }

    void ResetPosition()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.origin.position, 0.025f);
    }

    public void TakeDamage(int damage)
    {
        this.lifePoints -= damage;

        if(this.lifePoints <= 0)
        {
            this.Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
