using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float horizontalOffset;
    public float radius;
    public float offset;
    private Rigidbody2D rb;
    private float originX;
    private bool attacking;
    private Vector2 startPoint;

    void Awake()
    {
        this.originX = this.transform.position.x;
    }
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        this.enemyDetection();
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        if (this.transform.position.x > this.originX + this.horizontalOffset)
        {
            speed *= -1;
        }
        else if (this.transform.position.x < this.originX - this.horizontalOffset)
        {
            speed *= -1;
        }
        this.rb.velocity = this.transform.right * speed;
        yield return null;
    }

    void enemyDetection()
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
}
