using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float moveSpeed = 600f;
    public float angle;
    public Transform other;
    public Transform pivot;

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        angle = GetAngle();
        transform.RotateAround(pivot.position, new Vector3(0f, 0f, 1f), angle * Time.fixedDeltaTime);
        //transform.RotateAround(pivot.position, Vector3.forward, angle);
    }

    private float GetAngle()
    {
        Vector3 dir = other.position - pivot.position;
        dir = other.InverseTransformDirection(dir);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }
}
