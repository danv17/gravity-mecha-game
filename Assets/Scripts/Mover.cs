using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody2D rb;
    private CharacterController characterController;
    [Range(1, 10)]
    public float speed;
    void Start()
    {
        this.characterController = FindObjectOfType<CharacterController>();
        this.rb = this.GetComponent<Rigidbody2D>();
        //this.rb.velocity = this.transform.right * this.speed;
        if (this.characterController.facingRight)
        {
            this.rb.velocity = this.transform.right * this.speed;
        }
        else
        {
            this.rb.velocity = -this.transform.right * this.speed;
        }
    }
}
