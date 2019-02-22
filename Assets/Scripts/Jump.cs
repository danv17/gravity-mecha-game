using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Range(1, 10)]
    public float jumpVelocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            this.rb.velocity = Vector2.up * jumpVelocity;
        }
    }
}
