using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public float maxJumpHeight = 1.5f;
    public float groundHeight;
    public Vector3 groundPos;
    public float jumpSpeed = 7.0f;
    public float fallSpeed = 12.0f;
    public bool inputJump = false;
    public bool grounded = true;

    void Start()
    {
        groundPos = transform.position;
        groundHeight = transform.position.y;
        maxJumpHeight = transform.position.y + maxJumpHeight;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                groundPos = transform.position;
                inputJump = true;
                StartCoroutine("Jumping");
            }
        }
        if (transform.position == groundPos)
            grounded = true;
        else
            grounded = false;
    }

    IEnumerator Jumping()
    {
        while (true)
        {
            if (transform.position.y >= maxJumpHeight)
                inputJump = false;
            if (inputJump)
                transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
            else if (!inputJump)
            {
                transform.position = Vector3.Lerp(transform.position, groundPos, fallSpeed * Time.smoothDeltaTime);
                if (transform.position == groundPos)
                    StopAllCoroutines();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}