using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage;
    public float horizontalOffset;
    public float rayToPlayerDetection;
    public float rayToAtkPlayer;
    public float attackRadius;
    public float offsetToRaycast;
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
    }

    void EnemyPatrolling()
    {
        this.behaviour.WalkPatrol();
    }

    void PlayerDetection()
    {
        float limit = this.origin.position.x + this.attackRadius;

        float direction = this.behaviour.speed > 0 ? 1 : -1;
        startPoint = new Vector2(this.transform.position.x + (offsetToRaycast * direction), this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.right * direction, rayToPlayerDetection);

        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Player")) {
                this.player = hit.transform.gameObject;
                Debug.Log(player.name);
                this.MoveTowardsPlayer();
            }
            Debug.DrawRay(startPoint, Vector3.right * direction, Color.yellow);
        }
        else
        {
            this.player = null;
            this.behaviour.isAttacking = false;
        }

    }

    void ResetPosition()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.origin.position, 0.25f);
    }

    void Smash()
    {
        float direction = this.behaviour.speed > 0 ? 1 : -1;
        startPoint = new Vector2(this.transform.position.x + (rayToAtkPlayer * direction), this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.right * direction, rayToAtkPlayer);
        if (hit && Time.time > this.nextAtk)
        {
            Debug.Log("Attack");
            this.anim.SetTrigger("isAttacking");
            Instantiate(this.smash, this.player.gameObject.transform.position, Quaternion.identity);
        }
    }

    void MoveTowardsPlayer()
    {
        Debug.Log("Moving towards the player");
        float direction = this.behaviour.speed > 0 ? 1 : -1;
        Debug.Log(direction);
        this.behaviour.isAttacking = true;
        this.rb.velocity = this.transform.right * this.moveSpeedAtk * direction;
    }
}
