using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isJumping = false;
    private bool isGrounded = true;
    private bool facingRight = true;
    public float speed;
    [Range(1, 50)]
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(this.rb.velocity.y);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(h, 0.0f, 0.0f);

        movement = movement.normalized * this.speed * Time.deltaTime;

        if (h > 0 && !this.facingRight)
        {
            this.Flip();
        }
        else if (h < 0 && this.facingRight)
        {
            this.Flip();
        }

        if(h != 0)
        {
            this.anim.SetBool("isWalking", true);
        } else {
            this.anim.SetBool("isWalking", false);
        }

        this.rb.MovePosition(transform.position + movement);

        if (Input.GetKeyDown(KeyCode.Space) && !this.isJumping && this.isGrounded)
        {
            this.Jump();
        }

        if(this.rb.velocity.y < 0)
        {
            this.rb.velocity += Vector2.up * Physics2D.gravity.y * (this.fallMultiplier - 1) * Time.deltaTime;
        } else if (this.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            this.rb.velocity += Vector2.up * Physics2D.gravity.y * (this.lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Flip()
    {
        this.facingRight = !this.facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        this.isGrounded = false;
        Debug.Log("Is jumping");
        this.rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        //this.rb.velocity = new Vector2(0f, this.jumpForce * Time.deltaTime);
        this.isJumping = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isGrounded = true;
            this.isJumping = false;
            Debug.Log("Is grounded");
        }
    }
}
