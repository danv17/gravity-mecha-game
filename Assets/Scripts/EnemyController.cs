using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage;
    public float rayToPlayerDetection;
    public float rayToAtkPlayer;
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
        float direction = this.behaviour.speed > 0 ? 1 : -1;
        startPoint = new Vector2(this.transform.position.x + (offsetToRaycast * direction), this.transform.position.y);
        RaycastHit2D hitUpfront = Physics2D.Raycast(startPoint, Vector2.right * direction, rayToPlayerDetection);
        RaycastHit2D hitDiagonalUp = Physics2D.Raycast(startPoint, new Vector2(direction, 1), rayToPlayerDetection);
        RaycastHit2D hit;

        if (hitUpfront || hitDiagonalUp)
        {
            hit = hitUpfront ? hitUpfront : hitDiagonalUp;
            if (hit.transform.gameObject.CompareTag("Player")) {
                this.player = hit.transform.gameObject;
                Debug.Log("Enemy detected! ENGAGE");
                this.MoveTowardsPlayer();
                RaycastHit2D hitToAtk = Physics2D.Raycast(startPoint, Vector2.right * direction, rayToAtkPlayer);
                if (hitToAtk && Time.time > this.nextAtk)
                {
                    this.nextAtk = Time.time + this.atkRate;
                    this.Smash();
                }
            }
            Debug.DrawRay(startPoint, Vector2.right * direction, Color.cyan);
            Debug.DrawRay(startPoint, new Vector2(direction, 1), Color.magenta);
            if (this.player.gameObject.transform.position.x > this.behaviour.rightAtkLimit || this.player.gameObject.transform.position.x < this.behaviour.leftAtkLimit)
            {
                Debug.Log("There is no enemy nearby");
                this.player = null;
                this.behaviour.isAttacking = false;
            }
        }
        else
        {
            Debug.Log("There is no enemy nearby");
            this.player = null;
            this.behaviour.isAttacking = false;
        }
    }

    void Smash()
    {
        Debug.Log("Attack");
        //this.anim.SetTrigger("isAttacking");
        Instantiate(this.smash, this.player.gameObject.transform.position, Quaternion.identity);
    }

    void MoveTowardsPlayer()
    {
        Debug.Log("Moving towards the player");
        float direction = this.behaviour.speed > 0 ? 1 : -1;
        this.behaviour.isAttacking = true;
        this.rb.velocity = this.transform.right * this.moveSpeedAtk * direction;
    }
}
