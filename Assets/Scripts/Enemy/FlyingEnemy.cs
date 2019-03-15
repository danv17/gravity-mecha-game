using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float frequency;
    public float magnitude;

    private Vector3 position;
    protected override void Start()
    {
        this.position = this.transform.position;
        base.Start();
    }

    private void FixedUpdate()
    {
        this.Movement();
    }

    protected override void Movement()
    {
        this.position += this.transform.right * Time.deltaTime * this.GetDirection();
        this.transform.position = this.position + this.transform.up * Mathf.Sin(Time.time * this.frequency) * this.magnitude;
        base.ResetPosition();
    }
}
