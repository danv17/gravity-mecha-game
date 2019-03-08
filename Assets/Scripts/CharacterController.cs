using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isJumping = false;
    private bool isGrounded = true;
    private bool isCeiling = false;
    //public bool facingRight = true;
    public float speed;
    [Range(1, 50)]
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float shotRate;
    private float nextShot;
    public GameObject shotSpawn;
    public GameObject shot;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > this.nextShot)
        {
            this.nextShot = Time.time + this.shotRate;
            this.Shot();
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(h, 0.0f);

        movement = movement.normalized * this.speed * Time.deltaTime;

        //if (h > 0 && !this.facingRight)
        //{
        //    this.FlipHorizontal();
        //}
        //else if (h < 0 && this.facingRight)
        //{
        //    this.FlipHorizontal();
        //}

        if (h != 0)
        {
            this.anim.SetBool("isWalking", true);
        }
        else
        {
            this.anim.SetBool("isWalking", false);
        }

        this.rb.position += movement;

        if (Input.GetButtonDown("Jump") && !this.isJumping)
        {
            this.Jump();
        }

        this.SmoothJump();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            this.SwitchGravity();
        }
    }

    void Jump()
    {
        if (this.isCeiling) {
            this.rb.velocity = Vector2.up * -jumpForce;
        }
        if (this.isGrounded)
        {
            this.rb.velocity = Vector2.up * jumpForce;
        }
        this.isJumping = true;
        Debug.Log("Is jumping");
    }

    void SmoothJump()
    {
        if (this.isCeiling)
        {
            this.fallMultiplier *= -1;
            this.lowJumpMultiplier *= -1;
        }
        if (this.rb.velocity.y < 0)
        {
            this.rb.velocity += Vector2.up * Physics2D.gravity.y * (this.fallMultiplier) * Time.deltaTime;
        }
        else if (this.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            this.rb.velocity += Vector2.up * Physics2D.gravity.y * (this.lowJumpMultiplier) * Time.deltaTime;
        }
    }

    void SwitchGravity()
    {
        float gravity = -35.0f;
        if (this.isGrounded) {
            Physics2D.gravity = new Vector3(0, -gravity);
        }
        if (!this.isGrounded) {
            Physics2D.gravity = new Vector3(0, gravity);
        }
        this.FlipVertical();
    }

    void Shot()
    {
        Instantiate(this.shot, this.shotSpawn.transform.position, this.shotSpawn.transform.rotation);
    }

    //void FlipHorizontal()
    //{
    //    this.facingRight = !this.facingRight;
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}

    void FlipVertical()
    {
        //Vector3 theScale = transform.localScale;
        //theScale.y *= -1;
        //transform.localScale = theScale;
        //this.shotSpawn.transform.rotation = Quaternion.Inverse(this.shotSpawn.transform.rotation);
        this.transform.Rotate(180f, 0f, 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isGrounded = true;
            this.isCeiling = false;
            this.isJumping = false;
            Debug.Log("Is grounded");
        }

        if (collision.gameObject.CompareTag("Ceiling"))
        {
            this.isCeiling = true;
            this.isGrounded = false;
            this.isJumping = false;
            Debug.Log("Is on the roof");
        }
    }
}
