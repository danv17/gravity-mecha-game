using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody2D rb;
    [Range(1, 10)]
    public float speed;
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.rb.velocity = this.transform.up * this.speed;
    }
}
