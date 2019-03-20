using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public int damage;
    public float atkRate;
    public float nextAtk;
    public bool isPlayerDetected;
    public GameObject player;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerDetected = true;
            this.player = collision.gameObject;
            Debug.Log("Player Detected");
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerDetected = false;
            this.player = null;
            Debug.Log("Player Off Radius");
        }
    }
}
