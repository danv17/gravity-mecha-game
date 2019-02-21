using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public bool facingRight = true;
    public float speed;


    void Awake() {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(h, 0.0f, 0.0f);

        movement = movement.normalized * speed * Time.deltaTime;

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        if(h != 0) {
            this.anim.SetBool("isWalking", true);
        } else {
            this.anim.SetBool("isWalking", false);
        }

        this.rb.MovePosition(transform.position + movement);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
