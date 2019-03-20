using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float angle;
    public float rotationSpeed;
    public float movement;
    public Transform pivotRotation;
    public HiddenEnemy hiddenEnemy;
    public GameObject shot;
    public Transform shotSpawn;
    public Transform targetPosition;

    void Start()
    {
        this.hiddenEnemy = this.GetComponentInParent<HiddenEnemy>();
    }

    private void Update()
    {
        this.targetPosition = this.hiddenEnemy.player.transform;
        if (this.targetPosition)
        {
            Vector3 dir = this.targetPosition.position - this.pivotRotation.position;
            dir = this.targetPosition.InverseTransformDirection(dir);
            this.angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
    }

    private void FixedUpdate()
    {
        if (this.targetPosition)
        {
            while (this.transform.rotation.z != this.angle)
            {
                this.transform.RotateAround(this.pivotRotation.position, Vector3.forward, this.angle);
            }
        }
    }

    void Shot()
    {
        Instantiate(this.shot, this.shotSpawn.transform.position, this.shotSpawn.transform.rotation);
    }
}
