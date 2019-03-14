using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public int damage;
    public float horizontalOffset;
    public float radius;
    public float attackRadius;
    public float offset;
    public float moveSpeedAtk;
    public float atkRate;
    public float nextAtk;
    public HealthController healthController;
    public EnemyBehaviour behaviour;
    public GameObject smash;
    public Transform origin;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 startPoint;
    private GameObject player = null;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
        this.healthController = this.GetComponent<HealthController>();
        this.behaviour = this.GetComponent<EnemyBehaviour>();
    }

    void FixedUpdate()
    {
        this.PlayerDetection();
        if (!this.player)
        {
            this.EnemyPatrolling();
        }
        //this.PlayerDetection();
    }

    //IEnumerator Move()
    //{
    //    float rightLimit = this.origin.position.x + this.horizontalOffset;
    //    float leftLimit = this.origin.position.x - this.horizontalOffset;
    //    if (this.transform.position.x > rightLimit)
    //    {
    //        speed *= -1;
    //    }
    //    else if (this.transform.position.x < leftLimit)
    //    {
    //        speed *= -1;
    //    }
    //    this.rb.velocity = this.transform.right * speed;
    //    yield return null;
    //}

    //IEnumerator Attack()
    //{
    //    float rightAtkLimit = this.origin.position.x + this.attackRadius;
    //    float leftAtkLimit = this.origin.position.x - this.attackRadius;
    //    if (this.player)
    //    {
    //        if (Mathf.Abs(this.transform.position.x) - Mathf.Abs(this.player.gameObject.transform.position.x) < 0.5)
    //        {
    //            Debug.Log("Attack");
    //            this.Smash();
    //        }
    //    }
    //    yield return new WaitForSeconds(5);
    //}

    void EnemyPatrolling()
    {
        //float rightLimit = this.origin.position.x + this.horizontalOffset;
        //float leftLimit = this.origin.position.x - this.horizontalOffset;
        //if (this.transform.position.x > rightLimit)
        //{
        //    speed *= -1;
        //}
        //else if (this.transform.position.x < leftLimit)
        //{
        //    speed *= -1;
        //}
        //if (!this.isAttacking && !this.player)
        //{
        //    this.rb.velocity = this.transform.right * speed;
        //}
        this.behaviour.WalkPatrol();
    }

    /*Si, mientras patrulla, detecta al jugador*/
    void PlayerDetection()
    {
        float limit = this.origin.position.x + this.attackRadius;

        float direction = this.behaviour.speed > 0 ? 1 : -1;
        startPoint = new Vector2(this.transform.position.x + (offset * direction), this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.right * direction, radius);

        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Player")) {
                this.player = hit.transform.gameObject;
                //if (Mathf.Abs(this.transform.position.x) <= limit && this.player)
                //{
                //    this.MoveTowardsPlayer();
                //    this.Smash();
                //}
                Debug.Log(player.name);
            }
            Debug.DrawRay(startPoint, Vector3.right * direction, Color.yellow);
        }
        else
        {
            this.player = null;
        }

    }

    void ResetPosition()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.origin.position, 0.25f);
    }

    void Smash()
    {
        if (Mathf.Abs(this.transform.position.x - this.player.gameObject.transform.position.x) < 0.6 && Time.time > this.nextAtk)
        {
            Debug.Log("Attack");
            this.anim.SetTrigger("isAttacking");
            Instantiate(this.smash, this.player.gameObject.transform.position, Quaternion.identity);
        }
    }

    void MoveTowardsPlayer()
    {
        Debug.Log("Moving towards the player");
        float direction = Mathf.Abs(this.transform.position.x) - Mathf.Abs(this.player.gameObject.transform.position.x) < 0 ? 1 : -1;
        Debug.Log(direction);
        this.rb.velocity = this.transform.right * moveSpeedAtk * direction;
    }
}
