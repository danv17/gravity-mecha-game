using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipHorizontal : MonoBehaviour
{
    public bool facingRight = true;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0 && !this.facingRight)
        {
            this.Flip();
        }
        else if (h < 0 && this.facingRight)
        {
            this.Flip();
        }
    }

    void Flip()
    {
        this.facingRight = !this.facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
